namespace COSC3506_Project
{
    partial class MemberForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberForm));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.jobList = new System.Windows.Forms.ListView();
            this.btnViewTags = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 201);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(91, 35);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add Job";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_OnClick);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(109, 201);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(91, 35);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove Job";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Visible = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_OnClick);
            // 
            // btnView
            // 
            this.btnView.Enabled = false;
            this.btnView.Location = new System.Drawing.Point(206, 201);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(85, 35);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "View Apps";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_OnClick);
            // 
            // jobList
            // 
            this.jobList.Location = new System.Drawing.Point(12, 12);
            this.jobList.Name = "jobList";
            this.jobList.Size = new System.Drawing.Size(435, 183);
            this.jobList.TabIndex = 3;
            this.jobList.UseCompatibleStateImageBehavior = false;
            this.jobList.SelectedIndexChanged += new System.EventHandler(this.jobList_SelectedIndexChanged);
            // 
            // btnViewTags
            // 
            this.btnViewTags.Location = new System.Drawing.Point(12, 201);
            this.btnViewTags.Name = "btnViewTags";
            this.btnViewTags.Size = new System.Drawing.Size(91, 35);
            this.btnViewTags.TabIndex = 4;
            this.btnViewTags.Text = "View Tags";
            this.btnViewTags.UseVisualStyleBackColor = true;
            this.btnViewTags.Visible = false;
            this.btnViewTags.Click += new System.EventHandler(this.btnViewTags_onClick);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(374, 201);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(73, 35);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Log Out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // MemberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 248);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnViewTags);
            this.Controls.Add(this.jobList);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MemberForm";
            this.Text = "EARS - Main";
            this.Activated += new System.EventHandler(this.MemberForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MemberForm_FormClosed);
            this.Load += new System.EventHandler(this.MemberForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ListView jobList;
        private System.Windows.Forms.Button btnViewTags;
        private System.Windows.Forms.Button btnLogout;
    }
}