using RealEstate.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RealEstate.Views
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SingletonPageAttribute : Attribute
    {
    }

    public abstract class BasePage : ContentPage
    {
        const int TRANSITION_DURATION = 250;
        const int DESTROY_DELAY = 2000;
        bool _hasLoaded;
        object _newNavigationData;
        BaseViewModel _viewModel;

        protected bool IsPaused { get; private set; }
        public BaseViewModel ViewModel
        {
            get => _viewModel;
            set => BindingContext = value;
        }

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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (IsPaused)
                return;

            if (_hasLoaded)
            {
                OnResume(_newNavigationData);
                _newNavigationData = null;
            }
            else
            {
                await App.NextTickAsync();
                _hasLoaded = true;
                IsPaused = false;
                OnStart();
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (IsPaused)
                return;

            OnPause();

            await Task.Delay(TRANSITION_DURATION);

            IsPaused = false;
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
    }
}
