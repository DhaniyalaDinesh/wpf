using System.Windows;

namespace WPFUI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static bool IsDarkTheme { get; private set; }

    /// <summary>
    /// Dynamically switches the active theme resource dictionary between Light and Dark.
    /// </summary>
    public static void SetTheme(bool isDark)
    {
        IsDarkTheme = isDark;
        var relativePath = isDark ? "/WPFUI;component/Resources/Theme.Dark.xaml" : "/WPFUI;component/Resources/Theme.Light.xaml";
        var themeUri = new Uri(relativePath, UriKind.RelativeOrAbsolute);
        var newThemeDict = new ResourceDictionary { Source = themeUri };

        if (Current == null)
        {
            return;
        }

        var merged = Current.Resources.MergedDictionaries;
        for (int i = 0; i < merged.Count; i++)
        {
            var source = merged[i].Source?.OriginalString;
            if (source != null && (source.Contains("Theme.Light.xaml") || source.Contains("Theme.Dark.xaml")))
            {
                merged[i] = newThemeDict;
                return;
            }
        }

        // If not found, add it
        merged.Add(newThemeDict);
    }
}
