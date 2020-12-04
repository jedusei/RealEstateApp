﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Threading.Tasks;
using RealEstate.Services;

namespace RealEstate.ViewModels
{
    public class BaseViewModel : BindableObject
    {
        protected readonly INavigationService _navigationService;

        public BaseViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData) => Task.CompletedTask;
        public virtual void OnStart() { }
        public virtual void OnResume(object navigationData = null) { }
        public virtual void OnPause() { }
        public virtual void OnDestroy() { }

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