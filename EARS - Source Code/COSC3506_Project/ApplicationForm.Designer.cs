namespace COSC3506_Project
{
    partial class ApplicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationForm));
            this.btnAdd = new System.Windows.Forms.Button();
            this.applicationList = new System.Windows.Forms.ListView();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnComment = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnTag = new System.Windows.Forms.Button();
            this.btnFinalize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(13, 248);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(82, 44);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add App";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_OnClick);
            // 
            // applicationList
            // 
            this.applicationList.Location = new System.Drawing.Point(13, 13);
            this.applicationList.Name = "applicationList";
            this.applicationList.Size = new System.Drawing.Size(892, 217);
            this.applicationList.TabIndex = 1;
            this.applicationList.UseCompatibleStateImageBehavior = false;
            this.applicationList.SelectedIndexChanged += new System.EventHandler(this.applicationList_SelectedIndexChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(101, 248);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(82, 44);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove App";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_OnClick);
            // 
            // btnDownload
            // 
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(823, 248);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(82, 44);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_OnClick);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(735, 248);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(82, 44);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_OnClick);
            // 
            // btnComment
            // 
            this.btnComment.Enabled = false;
            this.btnComment.Location = new System.Drawing.Point(12, 248);
            this.btnComment.Name = "btnComment";
            this.btnComment.Size = new System.Drawing.Size(82, 44);
            this.btnComment.TabIndex = 5;
            this.btnComment.Text = "Comment";
            this.btnComment.UseVisualStyleBackColor = true;
            this.btnComment.Visible = false;
            this.btnComment.Click += new System.EventHandler(this.btnComment_OnClick);
            // 
            // btnApprove
            // 
            this.btnApprove.Enabled = false;
            this.btnApprove.Location = new System.Drawing.Point(101, 248);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(82, 44);
            this.btnApprove.TabIndex = 6;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Visible = false;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_OnClick);
            // 
            // btnTag
            // 
            this.btnTag.Enabled = false;
            this.btnTag.Location = new System.Drawing.Point(189, 248);
            this.btnTag.Name = "btnTag";
            this.btnTag.Size = new System.Drawing.Size(82, 44);
            this.btnTag.TabIndex = 7;
            this.btnTag.Text = "Tag";
            this.btnTag.UseVisualStyleBackColor = true;
            this.btnTag.Visible = false;
            this.btnTag.Click += new System.EventHandler(this.btnTag_OnClick);
            // 
            // btnFinalize
            // 
            this.btnFinalize.Enabled = false;
            this.btnFinalize.Location = new System.Drawing.Point(101, 248);
            this.btnFinalize.Name = "btnFinalize";
            this.btnFinalize.Size = new System.Drawing.Size(82, 44);
            this.btnFinalize.TabIndex = 8;
            this.btnFinalize.Text = "Finalize";
            this.btnFinalize.UseVisualStyleBackColor = true;
            this.btnFinalize.Visible = false;
            this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);
            // 
            // ApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 304);
            this.Controls.Add(this.btnFinalize);
            this.Controls.Add(this.btnTag);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.btnComment);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.applicationList);
            this.Controls.Add(this.btnAdd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ApplicationForm";
            this.Text = "EARS - Application List";
            this.Activated += new System.EventHandler(this.ApplicationForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ApplicationForm_Closed);
            this.Load += new System.EventHandler(this.Application_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListView applicationList;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnComment;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnTag;
        private System.Windows.Forms.Button btnFinalize;
    }
}