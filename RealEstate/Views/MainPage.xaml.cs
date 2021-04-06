using System.Collections.Generic;
using Xamarin.Forms;

namespace RealEstate.Views
{
    public partial class MainPage : BasePage
    {
        int _previousIndex;
        List<int> visitedTabs;

        public MainPage()
        {
            InitializeComponent();
            visitedTabs = new List<int>(tabView.Items.Count);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            SetTabViewHeight();
        }

        void SetTabViewHeight()
        {
            if (AbsoluteLayout.GetLayoutBounds(tabView).Height != MaxSize.Height)
                AbsoluteLayout.SetLayoutBounds(tabView, new Rectangle(0, 0, 1, MaxSize.Height));
        }

        ITabContentView GetTab(int index)
        {
            return tabView.Items[index].Content as ITabContentView;
        }

        protected override void OnStart()
        {
            base.OnStart();
            _previousIndex = tabView.SelectedIndex;
            visitedTabs.Add(tabView.SelectedIndex);
            var currentTab = GetTab(tabView.SelectedIndex);
            currentTab?.OnStart();
        }
        protected override void OnRefresh()
        {
            base.OnRefresh();
            SetTabViewHeight();
            visitedTabs.Clear();
            OnStart();
        }
        protected override void OnPause()
        {
            base.OnPause();
            var currentTab = GetTab(tabView.SelectedIndex);
            currentTab?.OnPause();
        }
        protected override void OnResume(object navigationData)
        {
            base.OnResume(navigationData);
            var currentTab = GetTab(tabView.SelectedIndex);
            currentTab?.OnPause();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            for (int i = 0; i < tabView.Items.Count; i++)
            {
                var tab = GetTab(i);
                tab?.OnDestroy();
            }
        }

        void tabView_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            var currentIndex = e.Index;
            var previousTab = GetTab(_previousIndex);
            previousTab?.OnPause();

            var currentTab = GetTab(currentIndex);
            if (currentTab != null)
            {
                if (visitedTabs.Contains(currentIndex))
                    currentTab.OnResume();
                else
                {
                    visitedTabs.Add(currentIndex);
                    currentTab.OnStart();
                }
            }

            _previousIndex = currentIndex;
        }

        protected override bool OnBackButtonPressed()
        {
            if (tabView.SelectedIndex != 0)
            {
                tabView.SelectedIndex = 0;
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}
