﻿using Mono.Cecil;
using System;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using Mono.Cecil.Cil;

namespace PCBSInjector
{
    public partial class GUI : Form
    {
        private string assemblySubPath = "/PCBS_Data/Managed";

        public GUI()
        {
            InitializeComponent();
        }

        private void gamePathBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.pathLabel.Text = fileDialog.SelectedPath;
                this.installBtn.Enabled = true;

                try
                {
                    bool isInjected = IsInjected(pathLabel.Text + assemblySubPath + "/Assembly-CSharp-firstpass.dll", "LogoSplash", "Awake", Directory.GetCurrentDirectory()+ "/PCBSModloader.dll", "PCBSModloader.ModLoader", "Init");
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
        }

        private void StatusAlreadyInstalled()
        {
            this.installBtn.Enabled = false;
            this.removeBtn.Enabled = true;
            progressBar.Value = 1;
            progressLabel.Text = "Modloader already installed!";
        }

        private void StatusReadyToInstall()
        {
            this.installBtn.Enabled = true;
            this.removeBtn.Enabled = false;
            progressBar.Value = 0;
            progressLabel.Text = "Modloader ready to install!";
        }

        private void StatusInstalledSuccessfully()
        {
            this.installBtn.Enabled = false;
            this.removeBtn.Enabled = true;
            progressBar.Value = 1;
            progressLabel.Text = "Modloader successfully installed!";
        }

        private void StatusUninstalledSuccessfully()
        {
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
