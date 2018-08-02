namespace PCBSInjector
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.modloaderInstallBox = new System.Windows.Forms.GroupBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.removeBtn = new System.Windows.Forms.Button();
            this.installBtn = new System.Windows.Forms.Button();
            this.gamePathBtn = new System.Windows.Forms.Button();
            this.gamePathLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.informationBox = new System.Windows.Forms.GroupBox();
            this.modloaderInstalledVersionLabel = new System.Windows.Forms.Label();
            this.modloaderVersionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.modloaderInstallBox.SuspendLayout();
            this.informationBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // modloaderInstallBox
            // 
            this.modloaderInstallBox.Controls.Add(this.pathLabel);
            this.modloaderInstallBox.Controls.Add(this.removeBtn);
            this.modloaderInstallBox.Controls.Add(this.installBtn);
            this.modloaderInstallBox.Controls.Add(this.gamePathBtn);
            this.modloaderInstallBox.Controls.Add(this.gamePathLabel);
            this.modloaderInstallBox.Location = new System.Drawing.Point(13, 13);
            this.modloaderInstallBox.Name = "modloaderInstallBox";
            this.modloaderInstallBox.Size = new System.Drawing.Size(423, 81);
            this.modloaderInstallBox.TabIndex = 0;
            this.modloaderInstallBox.TabStop = false;
            this.modloaderInstallBox.Text = "Modloader Installation";
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(74, 20);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(0, 13);
            this.pathLabel.TabIndex = 5;
            // 
            // removeBtn
            // 
            this.removeBtn.Enabled = false;
            this.removeBtn.Location = new System.Drawing.Point(287, 48);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(130, 23);
            this.removeBtn.TabIndex = 4;
            this.removeBtn.Text = "Remove Modloader";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // installBtn
            // 
            this.installBtn.Enabled = false;
            this.installBtn.Location = new System.Drawing.Point(148, 48);
            this.installBtn.Name = "installBtn";
            this.installBtn.Size = new System.Drawing.Size(130, 23);
            this.installBtn.TabIndex = 3;
            this.installBtn.Text = "Install Modloader";
            this.installBtn.UseVisualStyleBackColor = true;
            this.installBtn.Click += new System.EventHandler(this.installBtn_Click);
            // 
            // gamePathBtn
            // 
            this.gamePathBtn.Location = new System.Drawing.Point(10, 48);
            this.gamePathBtn.Name = "gamePathBtn";
            this.gamePathBtn.Size = new System.Drawing.Size(130, 23);
            this.gamePathBtn.TabIndex = 2;
            this.gamePathBtn.Text = "Game Path";
            this.gamePathBtn.UseVisualStyleBackColor = true;
            this.gamePathBtn.Click += new System.EventHandler(this.gamePathBtn_Click);
            // 
            // gamePathLabel
            // 
            this.gamePathLabel.AutoSize = true;
            this.gamePathLabel.Location = new System.Drawing.Point(7, 20);
            this.gamePathLabel.Name = "gamePathLabel";
            this.gamePathLabel.Size = new System.Drawing.Size(66, 13);
            this.gamePathLabel.TabIndex = 0;
            this.gamePathLabel.Text = "Game Path: ";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 100);
            this.progressBar.Maximum = 1;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(423, 23);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(12, 129);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(90, 13);
            this.progressLabel.TabIndex = 6;
            this.progressLabel.Text = "Select game path";
            // 
            // informationBox
            // 
            this.informationBox.Controls.Add(this.modloaderInstalledVersionLabel);
            this.informationBox.Controls.Add(this.modloaderVersionLabel);
            this.informationBox.Controls.Add(this.label2);
            this.informationBox.Controls.Add(this.label1);
            this.informationBox.Location = new System.Drawing.Point(13, 157);
            this.informationBox.Name = "informationBox";
            this.informationBox.Size = new System.Drawing.Size(423, 75);
            this.informationBox.TabIndex = 7;
            this.informationBox.TabStop = false;
            this.informationBox.Text = "Information";
            // 
            // modloaderInstalledVersionLabel
            // 
            this.modloaderInstalledVersionLabel.AutoSize = true;
            this.modloaderInstalledVersionLabel.Location = new System.Drawing.Point(181, 46);
            this.modloaderInstalledVersionLabel.Name = "modloaderInstalledVersionLabel";
            this.modloaderInstalledVersionLabel.Size = new System.Drawing.Size(10, 13);
            this.modloaderInstalledVersionLabel.TabIndex = 4;
            this.modloaderInstalledVersionLabel.Text = "-";
            // 
            // modloaderVersionLabel
            // 
            this.modloaderVersionLabel.AutoSize = true;
            this.modloaderVersionLabel.Location = new System.Drawing.Point(181, 28);
            this.modloaderVersionLabel.Name = "modloaderVersionLabel";
            this.modloaderVersionLabel.Size = new System.Drawing.Size(10, 13);
            this.modloaderVersionLabel.TabIndex = 3;
            this.modloaderVersionLabel.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Installed Modloader version:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modloader version:";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 244);
            this.Controls.Add(this.informationBox);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.modloaderInstallBox);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GUI";
            this.Text = "Modloader Installer for PC Building Simulator v0.5";
            this.modloaderInstallBox.ResumeLayout(false);
            this.modloaderInstallBox.PerformLayout();
            this.informationBox.ResumeLayout(false);
            this.informationBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox modloaderInstallBox;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.Button installBtn;
        private System.Windows.Forms.Button gamePathBtn;
        private System.Windows.Forms.Label gamePathLabel;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.GroupBox informationBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label modloaderInstalledVersionLabel;
        private System.Windows.Forms.Label modloaderVersionLabel;
    }
}

