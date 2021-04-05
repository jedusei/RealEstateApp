using RealEstate.Services;
using RealEstate.Themes;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

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
        static ResourceDictionary _lightTheme = new LightTheme();
        static ResourceDictionary _darkTheme = new DarkTheme();

        public static AppStatus Status { get; private set; }
        public static new IPlatform Platform { get; private set; }
        public static event Action Exit;

        public App() : this(() => Current.Quit())
        {
        }

        public App(Action exitAction)
        {
            InitializeComponent();
            _exitAction = exitAction;

            if (!DesignMode.IsDesignModeEnabled)
            {
                if (Platform == null)
                    Platform ??= DependencyService.Get<IPlatform>();

                Resources.MergedDictionaries.Add(_lightTheme);
                //Resources.MergedDictionaries.Add(RequestedTheme == OSAppTheme.Dark ? _darkTheme : _lightTheme);
                //RequestedThemeChanged += OnRequestedThemeChanged;
            }

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDE0ODkzQDMxMzgyZTM0MmUzMGlQc3dpYzlhaEZxTk9hV2FtTjNaZ1FZSndCOWFucGc4THVoWVdab2FHZ2s9");

            On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

           On<Xamarin.Forms.PlatformConfiguration.iOS>()
                .SetHandleControlUpdatesOnMainThread(true);
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

        void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            ResourceDictionary previousTheme, currentTheme;
            if (e.RequestedTheme == OSAppTheme.Dark)
            {
                previousTheme = _lightTheme;
                currentTheme = _darkTheme;
            }
            else
            {
                previousTheme = _darkTheme;
                currentTheme = _lightTheme;
            }

            var mergedDictionaries = Resources.MergedDictionaries;
            mergedDictionaries.Remove(previousTheme);
            mergedDictionaries.Add(currentTheme);
        }
    }
}
