using System.Collections.Generic;
using System.DirectoryServices;
using System.Security.Principal;
using VisualSVNAuthzEditor.Properties;

namespace VisualSVNAuthzEditor.LDAP
{
	class LDAPHelper
	{
		readonly Dictionary<string, LdapPrincipalEntry> _mapping = new Dictionary<string, LdapPrincipalEntry>();

		string ConvertSid(byte[] sid)
		{
			return new SecurityIdentifier(sid, 0).ToString();
		}

		public void LoadMapping()
		{
			var dirEnt = new DirectoryEntry(Settings.Default.LDAP_DirEntry);
			using (var searcher = new DirectorySearcher(dirEnt, Settings.Default.LDAP_Filter))
			{
				searcher.SizeLimit = 0;
				searcher.PageSize = 1000;
				searcher.SearchScope = SearchScope.OneLevel;

				searcher.PropertiesToLoad.Add("objectSid");
				searcher.PropertiesToLoad.Add("sidHistory");
				searcher.PropertiesToLoad.Add(Settings.Default.LDAP_DisplayNameAttribute);

				var results = searcher.FindAll();

				foreach (SearchResult result in results)
				{
					var ldapEntry = new LdapPrincipalEntry();
					ldapEntry.LdapDisplayName = (string)result.Properties[Settings.Default.LDAP_DisplayNameAttribute][0];
					ldapEntry.SID = ConvertSid((byte[])result.Properties["objectSid"][0]);
					_mapping[ldapEntry.SID] = ldapEntry;

					var histSidProp = result.Properties["sidHistory"];
					if (histSidProp != null && histSidProp.Count > 0)
					{
						var histLdapEntry = new LdapPrincipalEntry { IsHistorySID = true, LdapDisplayName = ldapEntry.LdapDisplayName };
						histLdapEntry.SID = ConvertSid((byte[])result.Properties["sidHistory"][0]);
						_mapping[histLdapEntry.SID] = histLdapEntry;
					}
				}
			}
		}

		public LdapPrincipalEntry ConvertSid(string sid)
		{
			if (!_mapping.ContainsKey(sid))
			{
				var ntAcc = (NTAccount)new SecurityIdentifier(sid).Translate(typeof(NTAccount));
				var entry = new LdapPrincipalEntry { Account = ntAcc };

				_mapping[sid] = entry;

				return entry;
			}
			return _mapping[sid];
		}
	}
}
