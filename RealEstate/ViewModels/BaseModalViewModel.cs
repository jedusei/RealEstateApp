using MvvmHelpers.Commands;
using System;

namespace RealEstate.ViewModels
{
    public abstract class BaseModalViewModel : BaseViewModel
    {
        Action<object> _setResultAction;

        public BaseModalViewModel(Action<object> setResultAction)
        {
            _setResultAction = setResultAction;
            GoBackCommand = new AsyncCommand(() => _navigationService.PopModalAsync());
        }

        protected void SetResult(object result) => _setResultAction(result);
    }
}
