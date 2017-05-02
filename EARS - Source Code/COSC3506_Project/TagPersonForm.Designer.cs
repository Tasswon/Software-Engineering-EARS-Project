namespace COSC3506_Project
{
    partial class TagPersonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagPersonForm));
            this.positionLevel = new System.Windows.Forms.ComboBox();
            this.lblSecurityLevel = new System.Windows.Forms.Label();
            this.usersListView = new System.Windows.Forms.ListView();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.tagInfo = new System.Windows.Forms.Label();
            this.TagHelp = new System.Windows.Forms.Label();
            this.TagInfoContinued = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // positionLevel
            // 
            this.positionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.positionLevel.FormattingEnabled = true;
            this.positionLevel.Location = new System.Drawing.Point(24, 25);
            this.positionLevel.Name = "positionLevel";
            this.positionLevel.Size = new System.Drawing.Size(138, 21);
            this.positionLevel.TabIndex = 17;
            // 
            // lblSecurityLevel
            // 
            this.lblSecurityLevel.AutoSize = true;
            this.lblSecurityLevel.Location = new System.Drawing.Point(21, 9);
            this.lblSecurityLevel.Name = "lblSecurityLevel";
            this.lblSecurityLevel.Size = new System.Drawing.Size(44, 13);
            this.lblSecurityLevel.TabIndex = 18;
            this.lblSecurityLevel.Text = "Position";
            // 
            // usersListView
            // 
            this.usersListView.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usersListView.Location = new System.Drawing.Point(24, 52);
            this.usersListView.Name = "usersListView";
            this.usersListView.Size = new System.Drawing.Size(568, 183);
            this.usersListView.TabIndex = 19;
            this.usersListView.UseCompatibleStateImageBehavior = false;
            this.usersListView.SelectedIndexChanged += new System.EventHandler(this.usersListView_SelectedIndexChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(24, 241);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(85, 38);
            this.btnSelect.TabIndex = 20;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(511, 241);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 38);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(168, 25);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(138, 20);
            this.txtFirstName.TabIndex = 22;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(312, 25);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(138, 20);
            this.txtLastName.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "First Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Last Name";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(456, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(53, 20);
            this.btnSearch.TabIndex = 26;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(515, 25);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(53, 20);
            this.btnClear.TabIndex = 27;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tagInfo
            // 
            this.tagInfo.AutoSize = true;
            this.tagInfo.Location = new System.Drawing.Point(115, 246);
            this.tagInfo.Name = "tagInfo";
            this.tagInfo.Size = new System.Drawing.Size(380, 13);
            this.tagInfo.TabIndex = 28;
            this.tagInfo.Text = "Note: Only other committee members will be notified if tagged on an application.";
            // 
            // TagHelp
            // 
            this.TagHelp.AutoSize = true;
            this.TagHelp.Location = new System.Drawing.Point(115, 259);
            this.TagHelp.Name = "TagHelp";
            this.TagHelp.Size = new System.Drawing.Size(384, 13);
            this.TagHelp.TabIndex = 29;
            this.TagHelp.Text = "You may still search for Sys Admins and Chairs to aquire their information in ord" +
    "er";
            // 
            // TagInfoContinued
            // 
            this.TagInfoContinued.AutoSize = true;
            this.TagInfoContinued.Location = new System.Drawing.Point(118, 271);
            this.TagInfoContinued.Name = "TagInfoContinued";
            this.TagInfoContinued.Size = new System.Drawing.Size(117, 13);
            this.TagInfoContinued.TabIndex = 30;
            this.TagInfoContinued.Text = "to contact them further.";
            // 
            // TagPersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 295);
            this.Controls.Add(this.TagInfoContinued);
            this.Controls.Add(this.TagHelp);
            this.Controls.Add(this.tagInfo);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.usersListView);
            this.Controls.Add(this.lblSecurityLevel);
            this.Controls.Add(this.positionLevel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TagPersonForm";
            this.Text = "EARS - Employee Selection (Tag)";
            this.Activated += new System.EventHandler(this.TagPersonForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TagPersonForm_FormClosed);
            this.Load += new System.EventHandler(this.TagPersonForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox positionLevel;
        private System.Windows.Forms.Label lblSecurityLevel;
        private System.Windows.Forms.ListView usersListView;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label tagInfo;
        private System.Windows.Forms.Label TagHelp;
        private System.Windows.Forms.Label TagInfoContinued;
    }
}