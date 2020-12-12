using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using RealEstate.Services;
using System.Windows.Input;
using MvvmHelpers.Commands;

namespace RealEstate.ViewModels
{
    public enum LoadStatus
    {
        Loading,
        Loaded,
        Failed
    }

    public class BaseViewModel : BindableObject
    {
        protected readonly INavigationService _navigationService;
        protected LoadStatus _loadStatus = LoadStatus.Loaded;

        public LoadStatus LoadStatus
        {
            get => _loadStatus;
            protected set => SetProperty(ref _loadStatus, value);
        }
        public ICommand GoBackCommand { get; private set; }

        public BaseViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            GoBackCommand = new AsyncCommand(() => _navigationService.GoBackAsync());
        }

        public virtual void Initialize(object navigationData) { }
        public virtual void OnStart() { }
        public virtual void OnResume(object navigationData = null) { }
        public virtual void OnPause() { }
        public virtual void OnDestroy() { }
        public virtual bool OnBackButtonPressed() => false;

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
