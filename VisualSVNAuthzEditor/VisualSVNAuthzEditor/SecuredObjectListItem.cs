using System.Collections.Generic;
using System.Management;

namespace VisualSVNAuthzEditor
{
	public class SecuredObjectListItem
	{
		public string DisplayName;
		public ManagementObject AccociatedObject;
		public readonly List<PermissionsListItem> Permissions = new List<PermissionsListItem>();

		public override string ToString()
		{
			return DisplayName;
		}
	}
}