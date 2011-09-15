using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Debuggernaut
{
    static class DynamicAssemblyResolver
    {
        static DynamicAssemblyResolver()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(OnAssemblyResolve);
        }

        public static void AddAssembly(string fullName, byte[] rawAssembly)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new ArgumentNullException("fullName");

            if ((rawAssembly == null) || (rawAssembly.Length == 0))
                throw new ArgumentNullException("rawAssembly");

            _assemblies[new AssemblyName(fullName)] = rawAssembly;
        }

        static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            byte[] rawAssembly;
            if (_assemblies.TryGetValue(new AssemblyName(args.Name), out rawAssembly))
            {
                return Assembly.Load(rawAssembly);
            }
            else
            {
                return null;
            }
        }

        static Dictionary<AssemblyName, byte[]> _assemblies = new Dictionary<AssemblyName, byte[]>(new AssemblyNameComparer());
    }

    class AssemblyNameComparer : IEqualityComparer<AssemblyName>
    {
        public bool Equals(AssemblyName x, AssemblyName y)
        {
            return AssemblyName.ReferenceMatchesDefinition(x, y);
        }

        public int GetHashCode(AssemblyName assemblyName)
        {
            return assemblyName.Name.ToLowerInvariant().GetHashCode();
        }
    }
}
