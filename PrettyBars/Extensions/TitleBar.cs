using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace PrettyBars.Extensions
{
    public class TitleBar
    {
        private static CoreApplicationViewTitleBar _titleBar => CoreApplication.GetCurrentView().TitleBar;
        private static ApplicationViewTitleBar _viewTitleBar => ApplicationView.GetForCurrentView().TitleBar;

        public static readonly DependencyProperty ExtendViewInTitleBarProperty = DependencyProperty.RegisterAttached(
            "ExtendViewInTitleBar", typeof(bool), typeof(TitleBar), new PropertyMetadata(default(bool), (o, e) => _titleBar.ExtendViewIntoTitleBar = (bool)e.NewValue));

        public static void SetExtendViewInTitleBar(DependencyObject element, bool value)
        {
            element.SetValue(ExtendViewInTitleBarProperty, value);
        }

        public static bool GetExtendViewInTitleBar(DependencyObject element)
        {
            return (bool)element.GetValue(ExtendViewInTitleBarProperty);
        }

        public static readonly DependencyProperty ForegroundColorProperty = DependencyProperty.RegisterAttached(
            "ForegroundColor", typeof(Color), typeof(TitleBar), new PropertyMetadata(default(Color), (o, e) =>
            {
                UpdateTitleBar(o);
            }));

        public static void SetForegroundColor(DependencyObject element, Color value)
        {
            element.SetValue(ForegroundColorProperty, value);
        }

        public static Color GetForegroundColor(DependencyObject element)
        {
            return (Color)element.GetValue(ForegroundColorProperty);
        }

        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.RegisterAttached(
            "BackgroundColor", typeof(Color), typeof(TitleBar), new PropertyMetadata(default(Color), (o, e) =>
            {
                UpdateTitleBar(o);
            }));

        public static void SetBackgroundColor(DependencyObject element, Color value)
        {
            element.SetValue(BackgroundColorProperty, value);
        }

        public static Color GetBackgroundColor(DependencyObject element)
        {
            return (Color)element.GetValue(BackgroundColorProperty);
        }

        public static readonly DependencyProperty AccentColorProperty = DependencyProperty.RegisterAttached(
            "AccentColor", typeof(Color), typeof(TitleBar), new PropertyMetadata(default(Color), (o, e) =>
            {
                UpdateTitleBar(o);
            }));

        public static void SetAccentColor(DependencyObject element, Color value)
        {
            element.SetValue(AccentColorProperty, value);
        }

        public static Color GetAccentColor(DependencyObject element)
        {
            return (Color)element.GetValue(AccentColorProperty);
        }

        private static void UpdateTitleBar(DependencyObject o)
        {
            // prepare color
            var foreground = GetForegroundColor(o);
            var background = GetBackgroundColor(o);
            var accent = GetAccentColor(o);
            var modAccent = ColorUtils.IsLight(background) ? ColorUtils.Darker(accent) : ColorUtils.Lighter(accent);
            var highlight = ColorUtils.IsLight(accent) ? Colors.Black : Colors.White;
            if (ColorUtils.IsDark(background)) Swap(ref accent, ref modAccent);
            // update properties
            _viewTitleBar.ForegroundColor = foreground;
            _viewTitleBar.BackgroundColor = background;
            _viewTitleBar.InactiveForegroundColor = Color.FromArgb(0x80, foreground.R, foreground.G, foreground.B);
            _viewTitleBar.InactiveBackgroundColor = background;
            _viewTitleBar.ButtonForegroundColor = accent;
            _viewTitleBar.ButtonBackgroundColor = background;
            _viewTitleBar.ButtonHoverForegroundColor = highlight;
            _viewTitleBar.ButtonHoverBackgroundColor = accent;
            _viewTitleBar.ButtonInactiveForegroundColor = Color.FromArgb(0x80, accent.R, accent.G, accent.B);
            _viewTitleBar.ButtonInactiveBackgroundColor = background;
            _viewTitleBar.ButtonPressedForegroundColor = highlight;
            _viewTitleBar.ButtonPressedBackgroundColor = modAccent;
            // update properties for phone
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusbar = StatusBar.GetForCurrentView();
                statusbar.ForegroundColor = highlight;
                statusbar.BackgroundColor = modAccent;
            }
        }

        private static void Swap<T>(ref T lhs, ref T rhs)
        {
            var tmp = lhs;
            lhs = rhs;
            rhs = tmp;
        }
    }
}
