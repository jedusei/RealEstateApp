using RealEstate.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace RealEstate
{
    public enum AppStatus
    {
        Starting,
        Running,
        Paused,
        Stopped
    }

    public partial class App : Xamarin.Forms.Application
    {
        static Action _exitAction;
        public static AppStatus Status { get; private set; }
        public static event Action Exit;

        public App(Action exitAction = null)
        {
            InitializeComponent();
            _exitAction = exitAction ?? Current.Quit;

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU5ODk4QDMxMzgyZTMzMmUzMG9BL2xkNGVFZlZjWmo4Y0RFQ0FFMmUxN0ozVlBzQ282blRQZWJGdWdHS2s9");

            On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        public static Task NextTickAsync()
        {
            var tcs = new TaskCompletionSource<bool>();

            Device.StartTimer(TimeSpan.FromSeconds(0), () =>
            {
                Device.BeginInvokeOnMainThread(() => tcs.SetResult(true));
                return false;
            });

            return tcs.Task;
        }

        public new static void Quit()
        {
            if (Status != AppStatus.Stopped && _exitAction != null)
                _exitAction.Invoke();
        }

        protected override async void OnStart()
        {
            await DependencyService.Get<INavigationService>().InitializeAsync();
            Status = AppStatus.Running;
        }

        protected override void OnSleep()
        {
            Status = AppStatus.Paused;
        }

        protected override async void OnResume()
        {
            await NextTickAsync();
            Status = AppStatus.Running;
        }

        public void OnExit()
        {
            Status = AppStatus.Stopped;
            Exit?.Invoke();
            Exit = null;
        }
    }
}
