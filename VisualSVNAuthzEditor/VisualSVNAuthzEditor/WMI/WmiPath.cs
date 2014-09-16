using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualSVNAuthzEditor.WMI
{
	class WmiPath
	{
		readonly static Tuple<string, string>[] NoKeys = new Tuple<string, string>[0];

		public readonly string ObjectName;
		public readonly Tuple<string, string>[] Keys = NoKeys;

		public WmiPath(string objectName)
		{
			ObjectName = objectName;
		}

		public WmiPath(string objectName, Tuple<string, string>[] keys)
		{
			ObjectName = objectName;
			Keys = keys;
		}

		static int GetNonEscapedIndexOf(string s, char ch)
		{
			var len = s.Length;

			for (var i = 0; i < len; i++)
			{
				if (s[i] == '\\')
				{
					i++;
					continue;
				}

				if (s[i] == ch)
					return i;
			}

			return -1;
		}

		public static WmiPath Parse(string relativePath)
		{
			var dot = GetNonEscapedIndexOf(relativePath, '.');
			if (dot == -1)
				return new WmiPath(relativePath);

			var keys = new List<Tuple<string, string>>();

			var keysStr = relativePath.Substring(dot + 1);

			while (true)
			{
				var comma = GetNonEscapedIndexOf(keysStr, ',');
				if(comma == -1)
				{
					keys.Add(ParseKeyValue(keysStr));
					break;
				}

				var pair = keysStr.Substring(0, comma);
				keys.Add(ParseKeyValue(pair));

				keysStr = keysStr.Substring(comma + 1);
			}

			return new WmiPath(relativePath.Substring(0, dot), keys.ToArray());
		}

		static string Unescape(string s)
		{
			var sb = new StringBuilder(s.Length);

			for (var i = 0; i < s.Length; i++)
			{
				if(s[i] == '\\')
					i++;
				sb.Append(s[i]);
			}

			return sb.ToString();
		}

		static Tuple<string, string> ParseKeyValue(string pair)
		{
			var eq = GetNonEscapedIndexOf(pair, '=');
			if(eq == -1)
				throw new ApplicationException("Incorrect WMI path");

			var value = pair.Substring(eq + 1);
			
			if (value.StartsWith("\"") && value.EndsWith("\""))
				value = value.Substring(1, value.Length - 2);

			return new Tuple<string, string>(Unescape(pair.Substring(0, eq)), Unescape(value));
		}

		public static string Assemble(string objectName, params Tuple<string, string>[] prms)
		{
			var sb = new StringBuilder();

			sb.Append(objectName);

			if(prms.Length > 0)
			{
				sb.Append(".");
				sb.Append(string.Join(",", prms.Select(AssemblePair)));
			}

			return sb.ToString();
		}

		static string AssemblePair(Tuple<string, string> pair)
		{
			return string.Format("{0}=\"{1}\"", Escape(pair.Item1), Escape(pair.Item2));
		}

		static string Escape(string s)
		{
			var sb = new StringBuilder(s.Length * 2);

			for (var i = 0; i < s.Length; i++)
			{
				if (s[i] == '\\' || s[i] == ',' || s[i] == '=' || s[i] == '"')
					sb.Append('\\');

				sb.Append(s[i]);
			}

			return sb.ToString();
		}
	}
}
