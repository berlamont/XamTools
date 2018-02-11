using System;
using System.IO;
using System.Threading.Tasks;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using eoTouchx.Services;
using Xamarin.Forms;

namespace eoTouchx.Droid.Services
{
    public class CrashService : ICrashService
    {
        const string ErrorFileName = "CrashReport.log";

        public CrashService()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            AndroidEnvironment.UnhandledExceptionRaiser += HandleAndroidException;
        }

        public Task ReportApplicationCrash()
        {
#if DEBUG
            ShowCrashReportDebug();
#endif
            return Task.FromResult(default(int));
        }

        static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = new Exception(nameof(CurrentDomainOnUnhandledException), e.ExceptionObject as Exception);
            LogUnhandledException(exception);
        }

        static void HandleAndroidException(object sender, RaiseThrowableEventArgs e)
        {
            e.Handled = true;
            var exception = new Exception(nameof(CurrentDomainOnUnhandledException), e.Exception);
            LogUnhandledException(exception);
        }

        static void LogUnhandledException(Exception exception)
        {
            try
            {
                var errorFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ErrorFileName);
                var errorMessage = $"Time: {DateTime.Now}\r\nError: Unhandled Exception\r\n{exception}";
                File.WriteAllText(errorFilePath, errorMessage);
                // Log to Android Device Logging.
                Log.Error("Crash Report", errorMessage);
            }
            catch
            {
                // do nothing
            }
        }

        static void ShowCrashReportDebug()
        {
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var errorFilePath = Path.Combine(libraryPath, ErrorFileName);

            if (!File.Exists(errorFilePath))
                return;

            var alert = new AlertDialog.Builder(Forms.Context)
                        .SetPositiveButton("Clear", (s, a) => File.Delete(errorFilePath)).SetNegativeButton("Close", (s, args) => {})
                        .SetMessage(File.ReadAllText(errorFilePath)).SetTitle("Crash Report");
            alert.Show();
        }

        static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = new Exception("TaskSchedulerOnUnobservedTaskException", e.Exception);
            LogUnhandledException(exception);
        }
    }
}