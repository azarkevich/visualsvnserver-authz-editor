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
			this.SuspendLayout();
			// 
			// listBoxSecuredObjects
			// 
			this.listBoxSecuredObjects.FormattingEnabled = true;
			this.listBoxSecuredObjects.Location = new System.Drawing.Point(12, 54);
			this.listBoxSecuredObjects.Name = "listBoxSecuredObjects";
			this.listBoxSecuredObjects.Size = new System.Drawing.Size(976, 134);
			this.listBoxSecuredObjects.TabIndex = 0;
			// 
			// listBoxPermissions
			// 
			this.listBoxPermissions.FormattingEnabled = true;
			this.listBoxPermissions.Location = new System.Drawing.Point(12, 224);
			this.listBoxPermissions.Name = "listBoxPermissions";
			this.listBoxPermissions.Size = new System.Drawing.Size(976, 134);
			this.listBoxPermissions.TabIndex = 1;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1000, 444);
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
	}
}

