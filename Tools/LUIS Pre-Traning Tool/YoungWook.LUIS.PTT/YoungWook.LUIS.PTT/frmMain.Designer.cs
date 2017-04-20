namespace YoungWook.LUIS.PTT
{
    partial class frmMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabProject = new System.Windows.Forms.TabPage();
            this.btnFromExportJson = new System.Windows.Forms.Button();
            this.btnNewProject = new System.Windows.Forms.Button();
            this.radioFromExportJson = new System.Windows.Forms.RadioButton();
            this.radioNewProject = new System.Windows.Forms.RadioButton();
            this.tabLoadData = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblIntents = new System.Windows.Forms.Label();
            this.grdSentence = new System.Windows.Forms.DataGridView();
            this.grdEntityData = new System.Windows.Forms.DataGridView();
            this.grdEntities = new System.Windows.Forms.DataGridView();
            this.grdIntents = new System.Windows.Forms.DataGridView();
            this.btnLoadExcel = new System.Windows.Forms.Button();
            this.tabPublish = new System.Windows.Forms.TabPage();
            this.btnSaveJSON = new System.Windows.Forms.Button();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.txtJson = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabProject.SuspendLayout();
            this.tabLoadData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSentence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEntityData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEntities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdIntents)).BeginInit();
            this.tabPublish.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabProject);
            this.tabControl1.Controls.Add(this.tabLoadData);
            this.tabControl1.Controls.Add(this.tabPublish);
            this.tabControl1.Controls.Add(this.tabAbout);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(916, 393);
            this.tabControl1.TabIndex = 0;
            // 
            // tabProject
            // 
            this.tabProject.Controls.Add(this.btnFromExportJson);
            this.tabProject.Controls.Add(this.btnNewProject);
            this.tabProject.Controls.Add(this.radioFromExportJson);
            this.tabProject.Controls.Add(this.radioNewProject);
            this.tabProject.Location = new System.Drawing.Point(4, 22);
            this.tabProject.Name = "tabProject";
            this.tabProject.Padding = new System.Windows.Forms.Padding(3);
            this.tabProject.Size = new System.Drawing.Size(908, 367);
            this.tabProject.TabIndex = 0;
            this.tabProject.Text = "Project";
            this.tabProject.UseVisualStyleBackColor = true;
            // 
            // btnFromExportJson
            // 
            this.btnFromExportJson.Location = new System.Drawing.Point(168, 67);
            this.btnFromExportJson.Name = "btnFromExportJson";
            this.btnFromExportJson.Size = new System.Drawing.Size(75, 23);
            this.btnFromExportJson.TabIndex = 3;
            this.btnFromExportJson.Text = "Load";
            this.btnFromExportJson.UseVisualStyleBackColor = true;
            // 
            // btnNewProject
            // 
            this.btnNewProject.Location = new System.Drawing.Point(168, 38);
            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Size = new System.Drawing.Size(75, 23);
            this.btnNewProject.TabIndex = 2;
            this.btnNewProject.Text = "Create";
            this.btnNewProject.UseVisualStyleBackColor = true;
            this.btnNewProject.Click += new System.EventHandler(this.btnNewProject_Click);
            // 
            // radioFromExportJson
            // 
            this.radioFromExportJson.AutoSize = true;
            this.radioFromExportJson.Location = new System.Drawing.Point(27, 70);
            this.radioFromExportJson.Name = "radioFromExportJson";
            this.radioFromExportJson.Size = new System.Drawing.Size(112, 17);
            this.radioFromExportJson.TabIndex = 1;
            this.radioFromExportJson.Text = "From Export JSON";
            this.radioFromExportJson.UseVisualStyleBackColor = true;
            // 
            // radioNewProject
            // 
            this.radioNewProject.AutoSize = true;
            this.radioNewProject.Checked = true;
            this.radioNewProject.Location = new System.Drawing.Point(27, 47);
            this.radioNewProject.Name = "radioNewProject";
            this.radioNewProject.Size = new System.Drawing.Size(83, 17);
            this.radioNewProject.TabIndex = 0;
            this.radioNewProject.TabStop = true;
            this.radioNewProject.Text = "New Project";
            this.radioNewProject.UseVisualStyleBackColor = true;
            // 
            // tabLoadData
            // 
            this.tabLoadData.Controls.Add(this.label3);
            this.tabLoadData.Controls.Add(this.label2);
            this.tabLoadData.Controls.Add(this.label1);
            this.tabLoadData.Controls.Add(this.lblIntents);
            this.tabLoadData.Controls.Add(this.grdSentence);
            this.tabLoadData.Controls.Add(this.grdEntityData);
            this.tabLoadData.Controls.Add(this.grdEntities);
            this.tabLoadData.Controls.Add(this.grdIntents);
            this.tabLoadData.Controls.Add(this.btnLoadExcel);
            this.tabLoadData.Location = new System.Drawing.Point(4, 22);
            this.tabLoadData.Name = "tabLoadData";
            this.tabLoadData.Padding = new System.Windows.Forms.Padding(3);
            this.tabLoadData.Size = new System.Drawing.Size(908, 367);
            this.tabLoadData.TabIndex = 1;
            this.tabLoadData.Text = "Load Data";
            this.tabLoadData.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(469, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Sentence";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Entity Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Entities";
            // 
            // lblIntents
            // 
            this.lblIntents.AutoSize = true;
            this.lblIntents.Location = new System.Drawing.Point(16, 57);
            this.lblIntents.Name = "lblIntents";
            this.lblIntents.Size = new System.Drawing.Size(39, 13);
            this.lblIntents.TabIndex = 8;
            this.lblIntents.Text = "Intents";
            // 
            // grdSentence
            // 
            this.grdSentence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSentence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSentence.ColumnHeadersVisible = false;
            this.grdSentence.Location = new System.Drawing.Point(494, 73);
            this.grdSentence.Name = "grdSentence";
            this.grdSentence.RowHeadersVisible = false;
            this.grdSentence.Size = new System.Drawing.Size(406, 277);
            this.grdSentence.TabIndex = 7;
            // 
            // grdEntityData
            // 
            this.grdEntityData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdEntityData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdEntityData.ColumnHeadersVisible = false;
            this.grdEntityData.Location = new System.Drawing.Point(263, 73);
            this.grdEntityData.Name = "grdEntityData";
            this.grdEntityData.RowHeadersVisible = false;
            this.grdEntityData.Size = new System.Drawing.Size(225, 277);
            this.grdEntityData.TabIndex = 6;
            // 
            // grdEntities
            // 
            this.grdEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdEntities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdEntities.ColumnHeadersVisible = false;
            this.grdEntities.Location = new System.Drawing.Point(141, 73);
            this.grdEntities.Name = "grdEntities";
            this.grdEntities.RowHeadersVisible = false;
            this.grdEntities.Size = new System.Drawing.Size(116, 277);
            this.grdEntities.TabIndex = 5;
            // 
            // grdIntents
            // 
            this.grdIntents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdIntents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdIntents.ColumnHeadersVisible = false;
            this.grdIntents.Location = new System.Drawing.Point(19, 73);
            this.grdIntents.Name = "grdIntents";
            this.grdIntents.RowHeadersVisible = false;
            this.grdIntents.Size = new System.Drawing.Size(116, 277);
            this.grdIntents.TabIndex = 4;
            // 
            // btnLoadExcel
            // 
            this.btnLoadExcel.Location = new System.Drawing.Point(19, 17);
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.Size = new System.Drawing.Size(75, 23);
            this.btnLoadExcel.TabIndex = 3;
            this.btnLoadExcel.Text = "Load Excel";
            this.btnLoadExcel.UseVisualStyleBackColor = true;
            this.btnLoadExcel.Click += new System.EventHandler(this.btnLoadExcel_Click);
            // 
            // tabPublish
            // 
            this.tabPublish.Controls.Add(this.txtJson);
            this.tabPublish.Controls.Add(this.btnSaveJSON);
            this.tabPublish.Location = new System.Drawing.Point(4, 22);
            this.tabPublish.Name = "tabPublish";
            this.tabPublish.Size = new System.Drawing.Size(908, 367);
            this.tabPublish.TabIndex = 2;
            this.tabPublish.Text = "Publish";
            this.tabPublish.UseVisualStyleBackColor = true;
            // 
            // btnSaveJSON
            // 
            this.btnSaveJSON.Location = new System.Drawing.Point(8, 13);
            this.btnSaveJSON.Name = "btnSaveJSON";
            this.btnSaveJSON.Size = new System.Drawing.Size(75, 23);
            this.btnSaveJSON.TabIndex = 4;
            this.btnSaveJSON.Text = "Save JSON";
            this.btnSaveJSON.UseVisualStyleBackColor = true;
            this.btnSaveJSON.Click += new System.EventHandler(this.btnSaveJSON_Click);
            // 
            // tabAbout
            // 
            this.tabAbout.Location = new System.Drawing.Point(4, 22);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(908, 367);
            this.tabAbout.TabIndex = 3;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // txtJson
            // 
            this.txtJson.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJson.Location = new System.Drawing.Point(8, 42);
            this.txtJson.Multiline = true;
            this.txtJson.Name = "txtJson";
            this.txtJson.Size = new System.Drawing.Size(885, 499);
            this.txtJson.TabIndex = 1;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 393);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "Pre-Traning Tool";
            this.tabControl1.ResumeLayout(false);
            this.tabProject.ResumeLayout(false);
            this.tabProject.PerformLayout();
            this.tabLoadData.ResumeLayout(false);
            this.tabLoadData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSentence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEntityData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEntities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdIntents)).EndInit();
            this.tabPublish.ResumeLayout(false);
            this.tabPublish.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabProject;
        private System.Windows.Forms.RadioButton radioFromExportJson;
        private System.Windows.Forms.RadioButton radioNewProject;
        private System.Windows.Forms.TabPage tabLoadData;
        private System.Windows.Forms.TabPage tabPublish;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.Button btnFromExportJson;
        private System.Windows.Forms.Button btnNewProject;
        private System.Windows.Forms.Button btnLoadExcel;
        private System.Windows.Forms.DataGridView grdIntents;
        private System.Windows.Forms.DataGridView grdEntities;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIntents;
        private System.Windows.Forms.DataGridView grdSentence;
        private System.Windows.Forms.DataGridView grdEntityData;
        private System.Windows.Forms.Button btnSaveJSON;
        private System.Windows.Forms.TextBox txtJson;
    }
}

