using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace ProcessUtility
{
    internal class ElevatedProcess
    {
        private Process process;

        /// <summary>
        /// 开始提升执行权限
        /// </summary>
        /// <returns>提升是否成功</returns>
        public bool begin()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = Assembly.GetExecutingAssembly().Location;
            processStartInfo.WorkingDirectory = Environment.CurrentDirectory;
            string text = ElevatedProcess.elevatedArg();
            processStartInfo.Arguments = text;
            processStartInfo.UseShellExecute = true;
            processStartInfo.Verb = "runas";
            try
            {
                this.process = Process.Start(processStartInfo);
            }
            catch (Win32Exception ex)
            {
                this.kill();
                return false;
            }
            return true;
        }

        public bool wait()
        {
            try
            {
                this.process.WaitForExit();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void kill()
        {
            try
            {
                this.process.Kill();
            }
            catch (Exception)
            {
            }
        }

        public static string elevatedArg()
        {
            return "elevated";
        }
    }
}
