namespace COSC3506_Project
{
    partial class CommentsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommentsForm));
            this.btnBack = new System.Windows.Forms.Button();
            this.commentList = new System.Windows.Forms.ListView();
            this.btnCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(446, 217);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(72, 36);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // commentList
            // 
            this.commentList.Location = new System.Drawing.Point(12, 12);
            this.commentList.Name = "commentList";
            this.commentList.Size = new System.Drawing.Size(506, 199);
            this.commentList.TabIndex = 2;
            this.commentList.UseCompatibleStateImageBehavior = false;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(12, 217);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(72, 36);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // CommentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 265);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.commentList);
            this.Controls.Add(this.btnBack);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CommentsForm";
            this.Text = "EARS - Comments for this Application";
            this.Activated += new System.EventHandler(this.CommentsForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CommentsForm_FormClosed);
            this.Load += new System.EventHandler(this.CommentsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ListView commentList;
        private System.Windows.Forms.Button btnCreate;
    }
}