using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace VisualSVNAuthzEditor
{
	public partial class FormMain : Form
	{
		const string ReposURL = "https://srv-tfs.ihs.internal.corp:8443/svn/gf";

		readonly List<SecuredObjectListItem> _securedObjectsList = new List<SecuredObjectListItem>();

		public FormMain()
		{
			InitializeComponent();

			var sdClass = new ManagementClass(@"\root\VisualSVN", "VisualSVN_SecurityDescriptor", null);

			foreach (var sd in sdClass.GetInstances().Cast<ManagementObject>())
			{
				var assocObjectRef = (string)sd.Properties["AssociatedObject"].Value;

				var assocObject = new ManagementObject(@"\root\VisualSVN", assocObjectRef, null);
				var assocClassName = assocObject.ClassPath.ClassName;

				var permissions = sd.Properties["Permissions"].Value as ManagementBaseObject[];

				var securedObject = new SecuredObjectListItem { AccociatedObject = assocObject };
				foreach (var permObj in permissions)
				{
					var level = (uint)permObj.Properties["AccessLevel"].Value;
					var sid = (string)((ManagementBaseObject)permObj.Properties["Account"].Value).Properties["SID"].Value;

					securedObject.Permissions.Add(new PermissionsListItem { SID = sid, PermissionsObject = permObj, Parent = securedObject });
				}

				_securedObjectsList.Add(securedObject);
			}

			listBoxSecuredObjects.Items.AddRange(_securedObjectsList.Cast<object>().ToArray());

			/*
			var repoClass = new ManagementClass(@"\root\VisualSVN", "VisualSVN_Repository", null);
			var repo = repoClass
				.GetInstances()
				.Cast<ManagementObject>()
				.First(r => r.Properties["Name"].Value.ToString() == "test")
				//.ToArray()
			;

			repo.GetMethodParameters("GetSecurity");
			*/
			/*
var repoClass = new ManagementClass(@"\\bym1d048\root\VisualSVN", "VisualSVN_Repository", null);

var inst = repoClass.CreateInstance();

//inst.Dump();
inst.SetPropertyValue("Name", "docs");

var gs1 = repoClass.InvokeMethod("GetGlobalSecurity", null, null);
var gs2 = inst.InvokeMethod("GetGlobalSecurity", null, null);

var sids = (gs2.Properties["Permissions"].Value as ManagementBaseObject[]).Select(p => (string)(p.Properties["Account"].Value as ManagementBaseObject).Properties["SID"].Value).ToArray();

var sid = new SecurityIdentifier(sids[0]);
sid.Translate(typeof(System.Security.Principal.NTAccount)).Dump();
			 */
		}
	}
}
