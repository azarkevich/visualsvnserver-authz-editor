namespace VisualSVNAuthzEditor
{
	partial class FormMain
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
			this.listBoxSecuredObjects = new System.Windows.Forms.ListBox();
			this.listBoxPermissions = new System.Windows.Forms.ListBox();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonDeleteSecurityEntry = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBoxSecuredObjects
			// 
			this.listBoxSecuredObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxSecuredObjects.FormattingEnabled = true;
			this.listBoxSecuredObjects.Location = new System.Drawing.Point(12, 15);
			this.listBoxSecuredObjects.Name = "listBoxSecuredObjects";
			this.listBoxSecuredObjects.Size = new System.Drawing.Size(1065, 329);
			this.listBoxSecuredObjects.TabIndex = 0;
			this.listBoxSecuredObjects.SelectedIndexChanged += new System.EventHandler(this.listBoxSecuredObjects_SelectedIndexChanged);
			// 
			// listBoxPermissions
			// 
			this.listBoxPermissions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxPermissions.FormattingEnabled = true;
			this.listBoxPermissions.Location = new System.Drawing.Point(12, 355);
			this.listBoxPermissions.Name = "listBoxPermissions";
			this.listBoxPermissions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBoxPermissions.Size = new System.Drawing.Size(1065, 264);
			this.listBoxPermissions.TabIndex = 1;
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(1083, 355);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(91, 23);
			this.buttonDelete.TabIndex = 2;
			this.buttonDelete.Text = "Delete";
			this.buttonDelete.UseVisualStyleBackColor = true;
			// 
			// buttonDeleteSecurityEntry
			// 
			this.buttonDeleteSecurityEntry.Location = new System.Drawing.Point(1083, 15);
			this.buttonDeleteSecurityEntry.Name = "buttonDeleteSecurityEntry";
			this.buttonDeleteSecurityEntry.Size = new System.Drawing.Size(91, 23);
			this.buttonDeleteSecurityEntry.TabIndex = 2;
			this.buttonDeleteSecurityEntry.Text = "Delete";
			this.buttonDeleteSecurityEntry.UseVisualStyleBackColor = true;
			this.buttonDeleteSecurityEntry.Click += new System.EventHandler(this.buttonDeleteSecurityEntry_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1186, 638);
			this.Controls.Add(this.buttonDeleteSecurityEntry);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.listBoxPermissions);
			this.Controls.Add(this.listBoxSecuredObjects);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Authz Editor";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxSecuredObjects;
		private System.Windows.Forms.ListBox listBoxPermissions;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonDeleteSecurityEntry;
	}
}

