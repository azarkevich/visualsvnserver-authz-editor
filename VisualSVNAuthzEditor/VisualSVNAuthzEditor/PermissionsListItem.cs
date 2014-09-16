using System.Management;

namespace VisualSVNAuthzEditor
{
	public class PermissionsListItem
	{
		public string SID;
		public ManagementBaseObject PermissionsObject;
		public SecuredObjectListItem Parent;

		public override string ToString()
		{
			return SID;
		}
	}
}