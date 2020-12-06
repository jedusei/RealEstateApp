using RealEstate.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RealEstate.Views
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SingletonPageAttribute : Attribute
    {
    }

    public abstract class BasePage : ContentPage
    {
        public static readonly BindableProperty StatusBarColorProperty = BindableProperty.Create(nameof(StatusBarColor), typeof(Color), typeof(BasePage), propertyChanged: OnStatusBarColorChanged);

        public const int TRANSITION_DURATION = 250;
        const int KEYBOARD_MIN_HEIGHT = 200;
        const int DESTROY_DELAY = 2000;
        bool _hasLoaded;
        object _newNavigationData;
        BaseViewModel _viewModel;

        protected bool IsPaused { get; private set; }
        public Color StatusBarColor
        {
            get => (Color)GetValue(StatusBarColorProperty);
            set => SetValue(StatusBarColorProperty, value);
        }
        public BaseViewModel ViewModel
        {
            get => _viewModel;
            set => BindingContext = value;
        }
        public bool IsKeyboardShowing { get; private set; }
        public Size MaxSize { get; private set; }

        public BasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void SetNavigationData(object navigationData)
        {
            _newNavigationData = navigationData;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            try
            {
                _viewModel = BindingContext as BaseViewModel;
            }
            catch
            {
                _viewModel = null;
            }
        }

        static void OnStatusBarColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            App.Platform.SetStatusBarColor((Color)newValue);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            var screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            IsKeyboardShowing = (screenHeight - height) >= KEYBOARD_MIN_HEIGHT;
            if (!IsKeyboardShowing)
            {
                var size = MaxSize;
                size.Width = width;
                size.Height = height;
                MaxSize = size;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_hasLoaded)
            {
                IsPaused = false;
                bool isHotReload = (App.Status == AppStatus.Running) && System.Diagnostics.Debugger.IsAttached;

                await App.NextTickAsync();

                if (isHotReload)
                    OnRefresh();
                else
                {
                    OnResume(_newNavigationData);
                    _newNavigationData = null;
                }
            }
            else
            {
                await App.NextTickAsync();
                _hasLoaded = true;
                OnStart();
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (IsPaused)
                return;

            IsPaused = true;
            OnPause();

            await Task.Delay(TRANSITION_DURATION);

            var isDetached = true;

            foreach (var p in Navigation.NavigationStack)
            {
                if (p == this)
                {
                    isDetached = false;
                    break;
                }

            }

            if (!isDetached)
            {
                await Task.Delay(DESTROY_DELAY);
                if (App.Status != AppStatus.Stopped)
                    return;
            }

            OnDestroy();
        }

        protected virtual void OnStart()
        {
            _viewModel?.OnStart();
        }
        protected virtual void OnRefresh() // Hot Reload
        {
        }
        protected virtual void OnResume(object navigationData)
        {
            _viewModel?.OnResume(navigationData);
        }
        protected virtual void OnPause()
        {
            _viewModel?.OnPause();
        }
        protected virtual void OnDestroy()
        {
            _viewModel?.OnDestroy();
        }

        protected override bool OnBackButtonPressed()
        {
            return _viewModel?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
        }
    }
}
