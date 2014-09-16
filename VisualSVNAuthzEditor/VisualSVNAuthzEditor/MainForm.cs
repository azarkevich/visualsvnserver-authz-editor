using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using VisualSVNAuthzEditor.LDAP;
using VisualSVNAuthzEditor.WMI;

namespace VisualSVNAuthzEditor
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();

			var ldapHelper = new LDAPHelper();
			ldapHelper.LoadMapping();

			var securedObjectsList = new List<SecuredObjectListItem>();

			var sdClass = new ManagementClass(@"\root\VisualSVN", "VisualSVN_SecurityDescriptor", null);

			foreach (var sd in sdClass.GetInstances().Cast<ManagementObject>())
			{
				var assocObjectRef = (string)sd.Properties["AssociatedObject"].Value;

				try
				{
					var assocObject = new ManagementObject(@"\root\VisualSVN", assocObjectRef, null);

					//var name = (string)assocObject.Properties["Name"].Value;
					var url = (string)assocObject.Properties["URL"].Value;

					var displayName = url + " (" + assocObject.ClassPath.ClassName + ")";

					var permissions = (ManagementBaseObject[])sd.Properties["Permissions"].Value;

					var securedObject = new SecuredObjectListItem { AccociatedObject = assocObject, DisplayName = displayName, WmiReference = assocObjectRef };

					foreach (var permObj in permissions)
					{
						var sid = (string)((ManagementBaseObject)permObj.Properties["Account"].Value).Properties["SID"].Value;

						var permissionObjectUI = new PermissionsListItem { SID = sid, Principal = ldapHelper.ConvertSid(sid), PermissionsObject = permObj, Parent = securedObject };
						securedObject.Permissions.Add(permissionObjectUI);
					}

					securedObjectsList.Add(securedObject);
				}
				catch (Exception ex)
				{
					var securedObject = new SecuredObjectListItem { DisplayName = "Exception: " + ex.Message + " (" + assocObjectRef + ")", WmiReference = assocObjectRef };
					securedObjectsList.Add(securedObject);
				}
			}

			listBoxSecuredObjects.Items.AddRange(securedObjectsList.Cast<object>().ToArray());
		}

		void listBoxSecuredObjects_SelectedIndexChanged(object sender, EventArgs e)
		{
			SecuredObjectSelectionChanged();
		}

		void SecuredObjectSelectionChanged()
		{
			var securedObject = (SecuredObjectListItem)listBoxSecuredObjects.SelectedItem;
			
			listBoxPermissions.Items.Clear();
			if (securedObject != null)
			{
				listBoxPermissions.Items.AddRange(securedObject.Permissions.Cast<object>().ToArray());
			}
		}

		void buttonDeleteSecurityEntry_Click(object sender, EventArgs e)
		{
			var item = (SecuredObjectListItem)listBoxSecuredObjects.SelectedItem;
			var wmiObjectParsedPath = WmiPath.Parse(item.WmiReference);

			if (wmiObjectParsedPath.ObjectName != "VisualSVN_RepositoryEntry")
			{
				MessageBox.Show("Can't delete " + wmiObjectParsedPath.ObjectName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var repoName = wmiObjectParsedPath.Keys.First(k => k.Item1 == "RepositoryName").Item2;
			var repoPath = WmiPath.Assemble("VisualSVN_Repository", Tuple.Create("Name", repoName));

			var repo = new ManagementObject(@"\root\VisualSVN", repoPath, null);
			var prms = repo.GetMethodParameters("SetSecurity");
			prms["Path"] = wmiObjectParsedPath.Keys.First(k => k.Item1 == "Path").Item2;
			prms["Permissions"] = new ManagementObject[0];
			prms["ResetChildren"] = false;

			repo.InvokeMethod("SetSecurity", prms, null);

			var selInd = listBoxSecuredObjects.SelectedIndex;
			listBoxSecuredObjects.Items.RemoveAt(listBoxSecuredObjects.SelectedIndex);
			
			if (selInd >= listBoxSecuredObjects.Items.Count)
				selInd = listBoxSecuredObjects.Items.Count - 1;

			listBoxSecuredObjects.SelectedIndex = selInd;
		}
	}
}
