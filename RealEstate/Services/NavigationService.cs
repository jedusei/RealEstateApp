using RealEstate.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(RealEstate.Services.NavigationService))]
namespace RealEstate.Services
{
    class NavigationService : INavigationService
    {
        public async Task InitializeAsync()
        {
            await GoToPageAsync<MainPage>();
        }

        public async Task GoToPageAsync<TPage>(object navigationData = null, bool clearHistory = false, bool removeCurrentPage = false) where TPage : BasePage
        {
            BasePage targetPage = null;
            if (Application.Current.MainPage == null)
            {
                targetPage = CreatePage(typeof(TPage), navigationData);
                Application.Current.MainPage = new NavigationPage(targetPage);
            }
            else
            {
                var navigation = Application.Current.MainPage.Navigation;
                var pageType = typeof(TPage);
                bool isSingleton = pageType.GetCustomAttributes(typeof(SingletonPageAttribute), false).Length > 0;
                if (isSingleton)
                {
                    for (int i = 0; i < navigation.NavigationStack.Count; i++)
                    {
                        var page = navigation.NavigationStack[i];
                        if (page.GetType() == pageType)
                        {
                            targetPage = page as BasePage;
                            break;
                        }
                    }

                    if (targetPage == navigation.NavigationStack[^1])
                    {
                        targetPage.SetNavigationData(navigationData);
                        return;
                    }
                }

                if (targetPage == null)
                    targetPage = CreatePage(pageType, navigationData);
                else
                {
                    navigation.RemovePage(targetPage);
                    targetPage.SetNavigationData(navigationData);
                }

                await navigation.PushAsync(targetPage);

                if (!clearHistory)
                {
                    if (removeCurrentPage)
                        navigation.RemovePage(navigation.NavigationStack[^2]);
                }
                else
                {
                    for (int j = navigation.NavigationStack.Count - 2; j >= 0; j--)
                        navigation.RemovePage(navigation.NavigationStack[j]);
                }
            }
        }

        public async Task GoBackAsync(bool animated = true)
        {
            if (Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
                await Application.Current.MainPage.Navigation.PopAsync(animated);
            else
                App.Quit();
        }

        BasePage CreatePage(Type pageType, object navigationData)
        {
            var page = Activator.CreateInstance(pageType) as BasePage;
            page.ViewModel?.Initialize(navigationData);
            return page;
        }

        public Task PopToRootAsync(bool animated = true)
        {
            var navigation = Application.Current.MainPage.Navigation;
            if (navigation.NavigationStack.Count > 1)
                return navigation.PopToRootAsync(animated);
            else
                return Task.CompletedTask;
        }
    }
}
