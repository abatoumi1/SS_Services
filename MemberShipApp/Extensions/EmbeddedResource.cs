using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Extensions
{
	// Taken from http://jopinblog.wordpress.com/2007/11/12/embedded-resource-queries-or-how-to-manage-sql-code-in-your-net-projects/
	/// <summary>
	/// Allows access to embedded resources in the application
	/// </summary>
	public static class EmbeddedResource
	{
		/// <summary>
		/// Gets a stream for the named embedded resource
		/// </summary>
		/// <param name="assembly">The assembly that has the resource</param>
		/// <param name="name">The name of the embedded resource</param>
		public static StreamReader GetStream(System.Reflection.Assembly assembly, string name)
		{
			foreach (string resName in assembly.GetManifestResourceNames())
			{
				if (resName.EndsWith(name))
				{
					return new StreamReader(assembly.GetManifestResourceStream(resName));
				}
			}
			return null;
		}

		/// <summary>
		/// Gets a string representation for the named embedded resource
		/// </summary>
		/// <param name="assembly">The assembly that has the resource</param>
		/// <param name="name">The name of the embedded resource</param>
		public static string GetString(System.Reflection.Assembly assembly, string name)
		{
			StreamReader sr = EmbeddedResource.GetStream(assembly, name);
			string data = sr.ReadToEnd();
			sr.Close();
			return data;
		}

		/// <summary>
		/// Gets a string representation for the named embedded resource using the default assembly
		/// </summary>
		/// <param name="name">The name of the embedded resource</param>
		public static string GetString(string name)
		{
			return EmbeddedResource.GetString(typeof(EmbeddedResource).Assembly, name);
		}
	}
}