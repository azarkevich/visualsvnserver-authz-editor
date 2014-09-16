using System.Management;
using VisualSVNAuthzEditor.LDAP;

namespace VisualSVNAuthzEditor
{
	public class PermissionsListItem
	{
		public string SID;
		public LdapPrincipalEntry Principal;
		public ManagementBaseObject PermissionsObject;
		public SecuredObjectListItem Parent;

		public override string ToString()
		{
			if (Principal.SID == null)
			{
				if(Principal.Account != null)
					return SID + " (" + Principal.Account.Value + ")";

				return SID;
			}

			if (Principal.IsHistorySID)
				return Principal.LdapDisplayName + " (hist)";

			return Principal.LdapDisplayName;
		}
	}
}