using System.Collections.Generic;
using System.Management;

namespace VisualSVNAuthzEditor
{
	public class SecuredObjectListItem
	{
		public string DisplayName;
		public string WmiReference;
		public ManagementObject AccociatedObject;
		public readonly List<PermissionsListItem> Permissions = new List<PermissionsListItem>();
		//public ManagementBaseObject[] WmiPermissions;

		public override string ToString()
		{
			return DisplayName;
		}
	}
}