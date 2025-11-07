using SmartDyeing.FADM_Form;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing
{
    //internal static class Program
    //{
    //    /// <summary>
    //    /// 应用程序的主入口点。
    //    /// </summary>
    //    [STAThread]
    //    static void Main()
    //    {
    //        Application.EnableVisualStyles();
    //        Application.SetCompatibleTextRenderingDefault(false);
    //        Application.Run(new Login());
    //    }
    //}

    internal static class Program
    {
        /// <summary> 
        /// 该函数设置由不同线程产生的窗口的显示状态。 
        /// </summary> 
        /// <param name="hWnd">窗口句柄</param> 
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param> 
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary> 
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary> 
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param> 
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 检查当前进程是否具有管理员权限
        /// </summary>
        /// <returns>true表示已具有管理员权限，false表示没有</returns>
        public static bool IsAdministrator()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                if (identity == null)
                    return false;

                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        /// <summary>
        /// 以管理员权限重新启动应用程序
        /// </summary>
        /// <param name="preserveArgs">是否保留原命令行参数</param>
        /// <param name="showErrorMessage">当提升失败时是否显示错误消息</param>
        /// <returns>如果成功启动管理员权限进程则返回true，否则返回false</returns>
        public static bool RestartWithAdminRights(bool preserveArgs = true, bool showErrorMessage = true)
        {
            try
            {
                // 获取当前应用程序路径
                string exePath = Process.GetCurrentProcess().MainModule.FileName;

                // 创建进程启动信息
                var startInfo = new ProcessStartInfo(exePath)
                {
                    Verb = "runas",  // 请求管理员权限
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory
                };

                // 处理命令行参数
                if (preserveArgs && Environment.GetCommandLineArgs().Length > 1)
                {
                    // 排除第一个参数（程序路径），保留其他参数
                    string args = string.Join(" ",
                        Environment.GetCommandLineArgs()
                                   .Skip(1)
                                   .Select(arg => $"\"{arg.Replace("\"", "\\\"")}\""));
                    startInfo.Arguments = args;
                }

                // 启动新进程
                Process.Start(startInfo);
                return true;
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                // 1223表示用户取消了UAC提示
                if (ex.NativeErrorCode == 1223)
                {
                    if (showErrorMessage)
                    {
                        MessageBox.Show(
                            "需要管理员权限才能执行此操作。\n请在弹出的用户账户控制窗口中点击\"是\"授予权限。",
                            "权限不足",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                else if (showErrorMessage)
                {
                    MessageBox.Show(
                        $"无法获取管理员权限：{ex.Message}\n错误代码：{ex.NativeErrorCode}",
                        "权限提升失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                if (showErrorMessage)
                {
                    MessageBox.Show(
                        $"发生错误：{ex.Message}",
                        "操作失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                return false;
            }
        }

        /// <summary>
        /// 确保程序以管理员权限运行，如果不是则尝试提升权限
        /// </summary>
        /// <returns>如果当前已是管理员或成功提升权限则返回true，否则返回false</returns>
        public static bool EnsureAdminRights()
        {
            if (IsAdministrator())
                return true;

            // 尝试提升权限
            bool elevated = RestartWithAdminRights();

            // 如果提升成功，关闭当前非管理员进程
            if (elevated)
            {
                Environment.Exit(0);
            }

            return elevated;
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!EnsureAdminRights())
            {
                // 权限提升失败，根据需要处理
                Console.WriteLine("无法获取管理员权限，程序将退出。");
                return;
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //获取参数
            try
            {
                Lib_Card.Configure.Parameter parameter = new Lib_Card.Configure.Parameter();
                string sPath = Environment.CurrentDirectory + "\\Config\\parameter.ini";
                foreach (PropertyInfo info in parameter.GetType().GetProperties())
                {
                    char[] separator = { '_' };
                    string head = info.Name.Split(separator)[0];
                    Console.WriteLine(info.Name);
                    if (info.Name.Equals("CylinderVersion")) {
                        
                    }
                    if (info.PropertyType == typeof(int))
                    {
                        int value = Convert.ToInt32(Lib_File.Ini.GetIni(head, info.Name, sPath));
                        parameter.GetType().GetProperty(info.Name).SetValue(parameter, value);

                    }
                    else if (info.PropertyType == typeof(double))
                    {
                        double value = Convert.ToDouble(Lib_File.Ini.GetIni(head, info.Name, sPath));
                        parameter.GetType().GetProperty(info.Name).SetValue(parameter, value);
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "parameter", MessageBoxButtons.OK, false);
                System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id).Kill();
            }


            Process instance = RunningInstance();
            if (instance == null)
            {
                string sLanguage = Lib_Card.Configure.Parameter.Other_Language  == 0 ? "zh":"en";
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(sLanguage);

                FADM_Form.Login login = new Login();

                //FADM_Form.ReportTest r = new ReportTest("202312040034", "6");
                //r.ShowDialog();

                //界面转换
                login.ShowDialog();

                if (login.DialogResult == DialogResult.OK)
                {
                    login.Dispose();

                    //FADM_Form.Form2 form2 = new Form2();
                    //form2.ShowDialog();

                    FADM_Form.Main main = new FADM_Form.Main();
                    Application.Run(main);
                }

                else
                {
                    System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id).Kill();
                    return;
                }
            }
            else
            {
                HandleRunningInstance(instance);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"未处理的线程异常: {e.Exception.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Lib_Log.Log.writeLogException("未处理的线程异常" + e.ToString());
            // 记录异常详细信息到日志文件或其他存储
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            MessageBox.Show($"未处理的域异常: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Lib_Log.Log.writeLogException("未处理的域异常" + ex.ToString());
            // 记录异常详细信息到日志文件或其他存储
        }

        /// <summary> 
        /// 获取正在运行的实例，没有运行的实例返回null; 
        /// </summary> 
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") != current.MainModule.FileName)
                    {
                        continue;
                    }
#pragma warning restore CS8602 // 解引用可能出现空引用。
                    return process;
                }
            }
#pragma warning disable CS8603 // 可能返回 null 引用。
            return null;
#pragma warning restore CS8603 // 可能返回 null 引用。
        }

        /// <summary> 
        /// 显示已运行的程序。 
        /// </summary> 
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, 3); //显示，可以注释掉 
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端 
        }
    }
}
