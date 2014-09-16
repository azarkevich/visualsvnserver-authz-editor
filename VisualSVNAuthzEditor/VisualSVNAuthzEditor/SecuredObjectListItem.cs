using System.Collections.Generic;
using System.Management;

namespace VisualSVNAuthzEditor
{
	public class SecuredObjectListItem
	{
		public ManagementObject AccociatedObject;
		public readonly List<PermissionsListItem> Permissions = new List<PermissionsListItem>();

		public override string ToString()
		{
			return (AccociatedObject.Properties["Name"].Value as string) ?? "NoName";
		}
	}
}