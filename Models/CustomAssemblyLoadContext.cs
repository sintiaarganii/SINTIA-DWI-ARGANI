using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SINTIA_DWI_ARGANI
{
    public class CustomAssemblyLoadContext : System.Runtime.Loader.AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            // Cari file library di folder aplikasi
            var libraryPath = Path.Combine(AppContext.BaseDirectory, unmanagedDllName);
            if (File.Exists(libraryPath))
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            // Coba load dari system
            return LoadUnmanagedDllFromPath(unmanagedDllName);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            // Default implementation
            return null;
        }
    }
}