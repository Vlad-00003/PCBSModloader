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
            this.InformationBox = new System.Windows.Forms.GroupBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.removeBtn = new System.Windows.Forms.Button();
            this.installBtn = new System.Windows.Forms.Button();
            this.gamePathBtn = new System.Windows.Forms.Button();
            this.gamePathLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.InformationBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // InformationBox
            // 
            this.InformationBox.Controls.Add(this.pathLabel);
            this.InformationBox.Controls.Add(this.removeBtn);
            this.InformationBox.Controls.Add(this.installBtn);
            this.InformationBox.Controls.Add(this.gamePathBtn);
            this.InformationBox.Controls.Add(this.gamePathLabel);
            this.InformationBox.Location = new System.Drawing.Point(13, 13);
            this.InformationBox.Name = "InformationBox";
            this.InformationBox.Size = new System.Drawing.Size(423, 81);
            this.InformationBox.TabIndex = 0;
            this.InformationBox.TabStop = false;
            this.InformationBox.Text = "Information";
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
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(423, 23);
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
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 151);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.InformationBox);
            this.Controls.Add(this.progressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.Text = "Modloader Injector for PC Building Simulator v0.1";
            this.InformationBox.ResumeLayout(false);
            this.InformationBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox InformationBox;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.Button installBtn;
        private System.Windows.Forms.Button gamePathBtn;
        private System.Windows.Forms.Label gamePathLabel;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progressLabel;
    }
}

