using RealEstate.ViewModels;
using System;
using System.Collections.Generic;
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
        public static readonly Color DefaultStatusBarColor = (Color)Application.Current.Resources["StatusBarColor"];
        public static readonly BindableProperty StatusBarColorProperty = BindableProperty.Create(nameof(StatusBarColor), typeof(Color), typeof(BasePage), DefaultStatusBarColor, propertyChanged: OnStatusBarColorChanged);

        public const int TRANSITION_DURATION = 250;
        const int KEYBOARD_MIN_HEIGHT = 200;
        const int DESTROY_DELAY = 2000;
        bool _hasLoaded;
        object _newNavigationData;
        BaseViewModel _viewModel;
#if DEBUG
        bool _hotReloadCheck;
#endif

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
        public bool IsPaused { get; private set; }

        public BasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            SetDynamicResource(BackgroundColorProperty, "PageBackgroundColor");
        }

        protected virtual IReadOnlyList<Page> GetNavigationStack()
        {
            return Application.Current.MainPage.Navigation.NavigationStack;
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

            App.Platform.SetStatusBarColor(StatusBarColor);

            if (_hasLoaded)
            {
                var isHotReload = false;
                var waitForNextTick = true;
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    isHotReload = _hotReloadCheck;
                    _hotReloadCheck = false;

                    if (!isHotReload && App.Status != AppStatus.Paused && System.Diagnostics.Debugger.IsAttached)
                    {
                        waitForNextTick = false;
                        await Task.Delay(TRANSITION_DURATION);
                        var navigationStack = GetNavigationStack();
                        if (navigationStack.Count == 0 || navigationStack[^1] != this)
                            isHotReload = true;
                    }
                }
#endif
                if (waitForNextTick)
                    await App.NextTickAsync();

                if (isHotReload)
                    OnRefresh();
                else
                {
                    IsPaused = false;
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
#if DEBUG
            _hotReloadCheck = true; // OnAppearing() will set this to false, if called right after this
            await App.NextTickAsync();
            if (!_hotReloadCheck)
                return; // Hot reload confirmed

            _hotReloadCheck = false;
#else
            await App.NextTickAsync();
#endif
            IsPaused = true;
            OnPause();

            await Task.Delay(TRANSITION_DURATION);

            var isDetached = true;

            foreach (var p in GetNavigationStack())
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
