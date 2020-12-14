using Syncfusion.XForms.Border;
using System;
using Xamarin.Forms;

namespace RealEstate.Views
{
    public partial class BottomSheetModal : BaseModal
    {
        public static readonly BindableProperty CollapsedHeightProperty = BindableProperty.Create(nameof(CollapsedHeight), typeof(double), typeof(BottomSheetModal), -1.0, propertyChanged: OnCollapsedHeightChanged);
        public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create(nameof(IsExpanded), typeof(bool), typeof(BottomSheetModal));

        SfBorder _sheet;
        View _dragHandle;
        View _dragHandleBase;
        View _titleBar;
        double _initialHeight;
        double _gestureStartHeight;
        bool _isGestureRunning;
        bool _isAnimationRunning;
        const double THRESHOLD = 100;

        public double CollapsedHeight
        {
            get => (double)GetValue(CollapsedHeightProperty);
            set => SetValue(CollapsedHeightProperty, value);
        }
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            private set => SetValue(IsExpandedProperty, value);
        }

        public BottomSheetModal()
        {
            InitializeComponent();
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack[^1] as BasePage;
            if (currentPage != null)
            {
                var statusBarColor = currentPage.StatusBarColor;
                var backgroundColor = BackgroundColor;
                StatusBarColor = new Color(
                    Lerp(statusBarColor.R, backgroundColor.R, backgroundColor.A),
                    Lerp(statusBarColor.G, backgroundColor.G, backgroundColor.A),
                    Lerp(statusBarColor.B, backgroundColor.B, backgroundColor.A)
                );
            }
        }

        static async void OnCollapsedHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var modal = bindable as BottomSheetModal;
            var value = (double)newValue;
            modal._initialHeight = (value == -1) ? -1 : value + 45;
            if (!modal._isGestureRunning && !modal._isAnimationRunning)
            {
                modal._sheet.HeightRequest = modal._initialHeight;
                await App.NextTickAsync();
                modal.ResetDragHandlePosition();
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _sheet = GetTemplateChild("sheet") as SfBorder;
            _dragHandle = GetTemplateChild("dragHandle") as View;
            _dragHandleBase = GetTemplateChild("dragHandleBase") as View;
            _titleBar = GetTemplateChild("titleBar") as View;

            _sheet.SizeChanged += sheet_SizeChanged;
            (GetTemplateChild("backBtn") as Button).Clicked += (s, e) => Close();
            var tapGestureRecognizer = (GetTemplateChild("root") as AbsoluteLayout).GestureRecognizers[0] as TapGestureRecognizer;
            tapGestureRecognizer.Tapped += (s, e) => Close();

            var panGestureRecognizer = _dragHandle.GestureRecognizers[0] as PanGestureRecognizer;
            panGestureRecognizer.PanUpdated += PanGestureRecognizer_PanUpdated;
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            sheet_SizeChanged(this, new EventArgs());
            ResetDragHandlePosition();
        }

        void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _isGestureRunning = true;
                    StopSlideAnimation();
                    _gestureStartHeight = _sheet.Height;
                    _sheet.AbortAnimation("slide");
                    break;

                case GestureStatus.Running:
                    _sheet.HeightRequest = Math.Min(_gestureStartHeight - e.TotalY, MaxSize.Height);
                    break;

                default:
                    _isGestureRunning = false;
                    StartSlideAnimation();
                    break;
            }
        }

        void ResetDragHandlePosition()
        {
            var bounds = AbsoluteLayout.GetLayoutBounds(_dragHandle);
            bounds.Y = _sheet.Y;
            AbsoluteLayout.SetLayoutBounds(_dragHandle, bounds);
        }

        void sheet_SizeChanged(object sender, EventArgs e)
        {
            if (_sheet.Y == 0)
                IsExpanded = true;
            else if (_sheet.Height == 0)
            {
                Navigation.PopModalAsync();
                return;
            }

            if (!_isGestureRunning)
                ResetDragHandlePosition();

            if (_sheet.Y <= 70)
            {
                var t = _sheet.Y / 70f;
                _titleBar.HeightRequest = Lerp(70, 0, t);
                _dragHandleBase.HeightRequest = Lerp(20, 45, t);

                t = _sheet.Y / 20f;
                _dragHandleBase.Opacity = t;
                _titleBar.Opacity = 1 - t;
            }
            else
            {
                _titleBar.HeightRequest = 0;
                _titleBar.Opacity = 0;
                _dragHandleBase.HeightRequest = 45;
                _dragHandleBase.Opacity = 1;
            }

            if (_sheet.Y <= 25)
            {
                var newCornerRadius = Lerp(0, 25, _sheet.Y / 25f);
                var cornerRadius = _sheet.CornerRadius;
                cornerRadius.Left = cornerRadius.Top = newCornerRadius;
                _sheet.CornerRadius = cornerRadius;
            }
            else
            {
                var cornerRadius = _sheet.CornerRadius;
                cornerRadius.Left = cornerRadius.Top = 25;
                _sheet.CornerRadius = cornerRadius;
            }
        }

        void StartSlideAnimation()
        {
            var delta = _sheet.Height - _initialHeight;
            if (delta != 0)
            {
                _isAnimationRunning = true;
                var animationStartHeight = _sheet.Height;
                if (delta > THRESHOLD)
                {
                    _sheet.Animate("slide", (t) =>
                    {
                        _sheet.HeightRequest = Lerp(animationStartHeight, MaxSize.Height, t);
                    }, easing: Easing.SinIn, finished: (d, b) => _isAnimationRunning = false);
                }
                else if (delta < -THRESHOLD)
                {
                    _sheet.Animate("slide", (t) =>
                    {
                        _sheet.HeightRequest = Lerp(animationStartHeight, 0, t);
                    }, easing: Easing.SinIn, finished: (d, b) => _isAnimationRunning = false);
                }
                else
                {
                    _sheet.Animate("slide", (t) =>
                    {
                        _sheet.HeightRequest = Lerp(animationStartHeight, _initialHeight, t);
                    }, easing: Easing.SinIn, finished: (d, b) => _isAnimationRunning = false);
                }
            }
        }

        void StopSlideAnimation()
        {
            _sheet.AbortAnimation("slide");
        }

        double Lerp(double from, double to, double t)
        {
            return from + t * (to - from);
        }
    }
}
