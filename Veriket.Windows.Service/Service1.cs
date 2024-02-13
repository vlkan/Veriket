using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Veriket.Windows.Service
{
    public partial class Service1 : ServiceBase
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken;


        public Service1()
        {
            InitializeComponent();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);

            var timer = new System.Threading.Timer((e) =>
            {
                LogWriter();
            }, null, startTimeSpan, periodTimeSpan);

        }

        public static void LogWriter()
        {
            //Directory
            var rootDir = Path.GetPathRoot(Environment.SystemDirectory);
            var servicePath = $"{rootDir}\\ProgramData\\VeriketApp";
            var logPath = $"{servicePath}\\VeriketAppTest.txt";

            //Current Sessions
            var machineName = System.Environment.MachineName;
            var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            //Dir Check
            bool dirExists = System.IO.Directory.Exists(servicePath);

            if (!dirExists)
                System.IO.Directory.CreateDirectory(servicePath);

            //StreamWriter
            using (StreamWriter streamWriter = (File.Exists(logPath)) ? File.AppendText(logPath) : File.CreateText(logPath))
            {
                streamWriter.WriteLine($"{DateTime.Now}|{machineName}|{userName}");
                streamWriter.Flush();
            }
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {
            cancellationTokenSource.Cancel();
        }
    }
}
