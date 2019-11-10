using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace MiDeskLampBand
{
    class RegCode
    {
        /// <summary>
        /// 注册程序集到系统
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public int RegistAssembly(string assemblyName)
        {
            if (!IsAdministrator())
            {
                return RunAsAdmin(assemblyName, "-regist");
            }
            Assembly assembly = Assembly.UnsafeLoadFrom(assemblyName);
            RegistrationServices registrationServices = new RegistrationServices();
            try
            {
                bool flag3 = registrationServices.RegisterAssembly(assembly, AssemblyRegistrationFlags.SetCodeBase);
                if (!flag3) return -3;
            }
            catch (UnauthorizedAccessException ex)
            {
                // 无权限
                Console.Error.Write(ex.Message);
                return -1;
            }catch(Exception ex)
            {
                // 其他错误
                Console.Error.Write(ex.Message);
                return -2;
            }
            return 0;
        }
        /// <summary>
        /// 从系统中卸载程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public int UnRegistAssembly(string assemblyName)
        {
            if (!IsAdministrator())
            {
                return RunAsAdmin(assemblyName, "-unregist");
            }
            Assembly assembly = Assembly.UnsafeLoadFrom(assemblyName);
            RegistrationServices registrationServices = new RegistrationServices();
            try
            {
                bool flag3 = registrationServices.UnregisterAssembly(assembly);
                if (!flag3) return -3;
            }
            catch (UnauthorizedAccessException ex)
            {
                // 无权限
                Console.Error.Write(ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                // 其他错误
                Console.Error.Write(ex.Message);
                return -2;
            }
            return 0;
        }
        /// <summary>
        /// 程序集是否已经注册
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool IsRegistered(string guid)
        {
            RegistryKey classesRoot = Registry.ClassesRoot;
            RegistryKey clsid = classesRoot.OpenSubKey("CLSID");
            guid = "{" + guid.ToUpper() + "}";
            foreach (string Keyname in clsid.GetSubKeyNames())
            {
                if (Keyname == guid) return true;
            }
            return false;
        }
        /// <summary>
        /// 检查当前用户是否为管理员
        /// </summary>
        /// <returns></returns>
        public bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }
        /// <summary>
        /// 以管理员模式运行程序
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns>是否成功</returns>
        public int RunAsAdmin(string assemblyName,string args)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = assemblyName;
            psi.Arguments = args;
            psi.Verb = "runas";

            var process = new Process();
            process.StartInfo = psi;
            process.Start();
            process.WaitForExit();
            
            return process.ExitCode;
        }
        
    }
}
