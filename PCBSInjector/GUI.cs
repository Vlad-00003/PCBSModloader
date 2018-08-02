using Mono.Cecil;
using System;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using Mono.Cecil.Cil;
using System.Reflection;

namespace PCBSInjector
{
    public partial class GUI : Form
    {
        private string assemblySubPath = "/PCBS_Data/Managed";

        private string lastPathFile = "lastpath.txt";

        public GUI()
        {
            InitializeComponent();
            UpdateVersionInformation();
            if (File.Exists(lastPathFile))
            {
                this.pathLabel.Text = File.ReadAllLines(lastPathFile)[0];
                CheckAssemblyStatus();
            }
        }

        private void gamePathBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            if (this.pathLabel.Text != null)
            {
                fileDialog.SelectedPath = this.pathLabel.Text;
            }
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.pathLabel.Text = fileDialog.SelectedPath;
                this.installBtn.Enabled = true;

                // save chosen path for next time
                if (File.Exists(lastPathFile))
                {
                    File.Delete(lastPathFile);
                }
                File.AppendAllText(lastPathFile, this.pathLabel.Text);

                CheckAssemblyStatus();
            }
        }

        private void CheckAssemblyStatus()
        {
            try
            {
                if (!File.Exists(pathLabel.Text + assemblySubPath + "/Assembly-CSharp-firstpass.dll"))
                {
                    StatusCannotInstall();
                    MessageBox.Show("Could not find " + pathLabel.Text + assemblySubPath + "/Assembly-CSharp-firstpass.dll \n" +
                        "Make sure you have selected the right game directory!");
                    return;
                }
                bool isInjected = IsInjected(pathLabel.Text + assemblySubPath + "/Assembly-CSharp-firstpass.dll", "LogoSplash", "Awake", Directory.GetCurrentDirectory() + "/PCBSModloader.dll", "PCBSModloader.ModLoader", "Init");
                if (isInjected)
                {
                    StatusAlreadyInstalled();
                }
                else
                {
                    StatusReadyToInstall();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private bool isUpdateable = false;

        private void UpdateVersionInformation()
        {

            Version modloaderVersion = File.Exists(Directory.GetCurrentDirectory() + "/PCBSModloader.dll") ? Assembly.Load(File.ReadAllBytes(Directory.GetCurrentDirectory() + "/PCBSModloader.dll")).GetName().Version : null;
            Version modloaderInstalledVersion = File.Exists(pathLabel.Text + assemblySubPath + "/PCBSModloader.dll") ? Assembly.Load(File.ReadAllBytes(pathLabel.Text + assemblySubPath + "/PCBSModloader.dll")).GetName().Version : null;
            this.modloaderVersionLabel.Text = modloaderVersion != null ? modloaderVersion.ToString() : "-";
            this.modloaderInstalledVersionLabel.Text = modloaderInstalledVersion != null ? modloaderInstalledVersion.ToString() : "-";

            if (modloaderVersion != null && modloaderInstalledVersion != null && modloaderVersion.CompareTo(modloaderInstalledVersion) > 0)
            {
                this.installBtn.Text = "Update Modloader";
                isUpdateable = true;
            }
            else
            {
                this.installBtn.Text = "Install Modloader";
                isUpdateable = false;
            }
        }

        private void StatusCannotInstall()
        {
            UpdateVersionInformation();
            this.installBtn.Enabled = false;
            this.removeBtn.Enabled = false;
            progressBar.Value = 0;
            progressLabel.Text = "Can not install or update modloader in the chosen path.";
        }

        private void StatusAlreadyInstalled()
        {
            UpdateVersionInformation();
            this.installBtn.Enabled = isUpdateable;
            this.removeBtn.Enabled = true;
            progressBar.Value = 1;
            progressLabel.Text = isUpdateable ? "Old Modloader version detected, ready to update!" : "Modloader already installed! ";
        }

        private void StatusReadyToInstall()
        {
            UpdateVersionInformation();
            this.installBtn.Enabled = true;
            this.removeBtn.Enabled = false;
            progressBar.Value = 0;
            progressLabel.Text = "Modloader ready to install!";
        }

        private void StatusInstalledSuccessfully()
        {
            UpdateVersionInformation();
            this.installBtn.Enabled = false;
            this.removeBtn.Enabled = true;
            progressBar.Value = 1;
            progressLabel.Text = "Modloader successfully installed!";
        }

        private void StatusUpdatedSuccessfully()
        {
            UpdateVersionInformation();
            this.installBtn.Enabled = false;
            this.removeBtn.Enabled = true;
            progressBar.Value = 1;
            progressLabel.Text = "Modloader successfully updated!";
        }

        private void StatusUninstalledSuccessfully()
        {
            UpdateVersionInformation();
            this.installBtn.Enabled = true;
            this.removeBtn.Enabled = false;
            progressBar.Value = 0;
            progressLabel.Text = "Modloader successfully uninstalled!";
        }

        private void installBtn_Click(object sender, EventArgs e)
        {
            string mainPath = pathLabel.Text + assemblySubPath;
            File.Copy(Directory.GetCurrentDirectory() + "/PCBSModloader.dll", mainPath + "/PCBSModloader.dll", true);
            File.Copy(Directory.GetCurrentDirectory() + "/0Harmony.dll", mainPath + "/0Harmony.dll", true);
            if (!Directory.Exists(pathLabel.Text + "/Mods"))
            {
                Directory.CreateDirectory(pathLabel.Text + "/Mods");
            }
            if (isUpdateable)
            {
                StatusUpdatedSuccessfully();
                return;
            }
            try
            {
                Inject(mainPath, "Assembly-CSharp-firstpass.dll", "LogoSplash", "Awake", "PCBSModloader.dll", "PCBSModloader.ModLoader", "Init");
                StatusInstalledSuccessfully();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void Inject(string mainPath, string assemblyToPatch, string assemblyType, string assemblyMethod, string loaderAssembly, string loaderType, string loaderMethod)
        {
            DefaultAssemblyResolver resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(mainPath);

            using (ModuleDefinition
                assembly = ModuleDefinition.ReadModule(mainPath + "/" + assemblyToPatch, new ReaderParameters { ReadWrite = true, AssemblyResolver = resolver }),
                loader = ModuleDefinition.ReadModule(mainPath + "/" + loaderAssembly)
            )
            {
                MethodDefinition methodToInject = loader.GetType(loaderType).Methods.Single(x => x.Name == loaderMethod);
                MethodDefinition methodToHook = assembly.GetType(assemblyType).Methods.First(x => x.Name == assemblyMethod);

                Instruction loaderInit = Instruction.Create(OpCodes.Call, assembly.ImportReference(methodToInject));
                ILProcessor processor = methodToHook.Body.GetILProcessor();
                processor.InsertBefore(methodToHook.Body.Instructions[0], loaderInit);

                assembly.Write();
            }
        }

        private bool IsInjected(string assemblyToPatch, string assemblyType, string assemblyMethod, string loaderAssembly, string loaderType, string loaderMethod)
        {
            using (ModuleDefinition
                assembly = ModuleDefinition.ReadModule(assemblyToPatch),
                loader = ModuleDefinition.ReadModule(loaderAssembly)
            )
            {
                MethodDefinition methodToInject = loader.GetType(loaderType).Methods.Single(x => x.Name == loaderMethod);
                MethodDefinition methodToHook = assembly.GetType(assemblyType).Methods.First(x => x.Name == assemblyMethod);

                foreach (Instruction instruction in methodToHook.Body.Instructions)
                {
                    if (instruction.OpCode.Equals(OpCodes.Call) && instruction.Operand.ToString().Equals($"System.Void {loaderType}::{loaderMethod}()"))
                    {
                        return true;
                    }
                }
                return false;
            }
           
        }

        private void Remove(string mainPath, string assemblyToPatch, string assemblyType, string assemblyMethod, string loaderAssembly, string loaderType, string loaderMethod)
        {
            DefaultAssemblyResolver resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(mainPath);

            using (ModuleDefinition
                assembly = ModuleDefinition.ReadModule(mainPath + "/" + assemblyToPatch, new ReaderParameters { ReadWrite = true, AssemblyResolver = resolver }),
                loader = ModuleDefinition.ReadModule(mainPath + "/" + loaderAssembly)
            )
            {
                MethodDefinition methodToInject = loader.GetType(loaderType).Methods.Single(x => x.Name == loaderMethod);
                MethodDefinition methodToHook = assembly.GetType(assemblyType).Methods.First(x => x.Name == assemblyMethod);

                Instruction toRemove = null;
                foreach (Instruction instruction in methodToHook.Body.Instructions)
                {
                    if (instruction.OpCode.Equals(OpCodes.Call) && instruction.Operand.ToString().Equals($"System.Void {loaderType}::{loaderMethod}()"))
                    {
                        toRemove = instruction;
                        break;
                    }
                }
                if (toRemove != null)
                {
                    ILProcessor processor = methodToHook.Body.GetILProcessor();
                    processor.Remove(toRemove);
                }

                assembly.Write();
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            string mainPath = pathLabel.Text + assemblySubPath;
            try
            {
                Remove(mainPath, "Assembly-CSharp-firstpass.dll", "LogoSplash", "Awake", "PCBSModloader.dll", "PCBSModloader.ModLoader", "Init");
                StatusUninstalledSuccessfully();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            File.Delete(mainPath + "/PCBSModloader.dll");
            File.Delete(mainPath + "/0Harmony.dll");
        }
    }
}
