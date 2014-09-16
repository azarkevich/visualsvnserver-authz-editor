using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using VisualSVNAuthzEditor.LDAP;

namespace VisualSVNAuthzEditor
{
	public partial class FormMain : Form
	{
		readonly List<SecuredObjectListItem> _securedObjectsList = new List<SecuredObjectListItem>();

		public FormMain()
		{
			InitializeComponent();

			var ldapHelper = new LDAPHelper();
			ldapHelper.LoadMapping();

			var sdClass = new ManagementClass(@"\\srv-tfs\root\VisualSVN", "VisualSVN_SecurityDescriptor", null);

			foreach (var sd in sdClass.GetInstances().Cast<ManagementObject>())
			{
				var assocObjectRef = (string)sd.Properties["AssociatedObject"].Value;

				try
				{
					var assocObject = new ManagementObject(@"\\srv-tfs\root\VisualSVN", assocObjectRef, null);

					//var pds = assocObject.Properties.Cast<PropertyData>().ToArray();

					//var name = (string)assocObject.Properties["Name"].Value;
					var url = (string)assocObject.Properties["URL"].Value;

					var displayName = url + " (" + assocObject.ClassPath.ClassName + ")";

					var permissions = sd.Properties["Permissions"].Value as ManagementBaseObject[];

					var securedObject = new SecuredObjectListItem { AccociatedObject = assocObject, DisplayName = displayName };
					foreach (var permObj in permissions)
					{
						var level = (uint)permObj.Properties["AccessLevel"].Value;
						var sid = (string)((ManagementBaseObject)permObj.Properties["Account"].Value).Properties["SID"].Value;

						securedObject.Permissions.Add(new PermissionsListItem { SID = sid, Principal = ldapHelper.ConvertSid(sid), PermissionsObject = permObj, Parent = securedObject });
					}

					_securedObjectsList.Add(securedObject);
				}
				catch (Exception ex)
				{
					var securedObject = new SecuredObjectListItem { DisplayName = "Exception: " + ex.Message + " (" + assocObjectRef + ")" };
					_securedObjectsList.Add(securedObject);
				}
			}

			listBoxSecuredObjects.Items.AddRange(_securedObjectsList.Cast<object>().ToArray());
		}

		void listBoxSecuredObjects_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SecuredObjectSelectionChanged();
		}

		void SecuredObjectSelectionChanged()
		{
			var securedObject = _securedObjectsList[listBoxSecuredObjects.SelectedIndex];
			
			listBoxPermissions.Items.Clear();
			listBoxPermissions.Items.AddRange(securedObject.Permissions.Cast<object>().ToArray());
		}
	}
}
