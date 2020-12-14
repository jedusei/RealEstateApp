using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RealEstate.Views
{
    public class BaseModal : BasePage
    {
        TaskCompletionSource<object> _resultTcs = new TaskCompletionSource<object>();

        public bool IsCompleted => _resultTcs.Task.IsCompleted;
        public object Result => _resultTcs.Task.Result;

        protected override IReadOnlyList<Page> GetNavigationStack()
        {
            return Application.Current.MainPage.Navigation.ModalStack;
        }

        public Task<object> GetResultAsync() => _resultTcs.Task;
        protected void SetResult(object result) => _resultTcs.TrySetResult(result);

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (!IsCompleted)
                SetResult(null);
        }
    }
}
