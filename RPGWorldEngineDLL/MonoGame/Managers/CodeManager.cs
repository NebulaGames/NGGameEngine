using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;


namespace NebulaGames.RPGWorld.MonoGame.Managers
{
    public static class CodeManager
    {
        public static Dictionary<string, Assembly> _Compiled = new Dictionary<string, Assembly>();

        public static Assembly GetObject(string Name)
        {
            return _Compiled[Name];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SourceCode"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static object CompileInMemory(string Name, string SourceCode, params string[] assemblies)
        {
            try
            {
                // Add Method Code Here
                Dictionary<string, string> provOptions = new Dictionary<string, string>();

               // provOptions.Add("CompilerVersion", "v4");
                provOptions.Add("CompilerDirectoryPath", @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\");
                using (CodeDomProvider cc = CodeDomProvider.CreateProvider("cs", provOptions))
                {

                    CompilerParameters Parameters = new CompilerParameters();

                    // *** Start by adding any referenced assemblies
                    Parameters.ReferencedAssemblies.Add("System.dll");
                    Parameters.ReferencedAssemblies.Add("System.Data.Linq.dll");
                    Parameters.ReferencedAssemblies.Add("System.Data.dll");
                    Parameters.ReferencedAssemblies.Add("System.Core.dll");
                    Parameters.ReferencedAssemblies.Add("System.Xml.dll");
                    Parameters.ReferencedAssemblies.Add(Environment.GetEnvironmentVariable("XNAGSv4") + @"References\Windows\x86\Microsoft.Xna.Framework.dll");
                    Parameters.ReferencedAssemblies.Add(Environment.GetEnvironmentVariable("XNAGSv4") + @"References\Windows\x86\Microsoft.Xna.Framework.Game.dll");
                    Parameters.ReferencedAssemblies.Add(Environment.GetEnvironmentVariable("XNAGSv4") + @"References\Windows\x86\Microsoft.Xna.Framework.Graphics.dll");
                    Parameters.ReferencedAssemblies.Add("mscorlib.dll");
                    Parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                    Parameters.ReferencedAssemblies.Add("GG2DLib.dll");
                    Parameters.ReferencedAssemblies.Add("ICSharpCode.SharpZipLib.dll");
                   


                    if (assemblies != null)
                    {
                        foreach (string assembly in assemblies)
                        {
                            if (!Parameters.ReferencedAssemblies.Contains(assembly))
                            {
                                Parameters.ReferencedAssemblies.Add(assembly);
                            }
                        }
                    }
                    // *** Load the resulting assembly into memory
                    Parameters.GenerateInMemory = true;

                    // *** Now compile the whole thing
                    CompilerResults loCompiled = cc.CompileAssemblyFromSource(Parameters, SourceCode);

                    if (loCompiled.Errors.HasErrors)
                    {
                        string lcErrorMsg = "";

                        // *** Create Error String
                        lcErrorMsg = loCompiled.Errors.Count.ToString() + " Errors:";
                        for (int x = 0; x < loCompiled.Errors.Count; x++)
                            lcErrorMsg = lcErrorMsg + "\r\nLine: " + loCompiled.Errors[x].Line.ToString() + " - " +
                                loCompiled.Errors[x].ErrorText;

                        return string.Format("Compiler Error: {0}\r\n\r\n{1}", lcErrorMsg, SourceCode);
                    }

                    Assembly loAssembly = loCompiled.CompiledAssembly;

                    _Compiled.Add(Name, loAssembly);

                    return loAssembly;
                }
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> methodParams = new System.Collections.Generic.Dictionary<string, object>();
                methodParams.Add("SourceCode", SourceCode);
                methodParams.Add("assemblies", assemblies);

                ErrorLogable.LogError(ex, "Error Compiling: " + Name);
                throw ex;
            }
        }
    }
}
