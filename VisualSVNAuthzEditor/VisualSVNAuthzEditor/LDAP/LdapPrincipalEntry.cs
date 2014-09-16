using System.Security.Principal;

namespace VisualSVNAuthzEditor.LDAP
{
	public class LdapPrincipalEntry
	{
		public bool IsHistorySID;
		public string SID;
		public string LdapDisplayName;
		public NTAccount Account;
	}
}
