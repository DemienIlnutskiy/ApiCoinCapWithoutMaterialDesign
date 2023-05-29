using System;
using System.Windows;

namespace Cryptoriencies.ViewModels
{
	static class ChangeTheme
	{
		public static void ChangeThemeDark(bool darkMode=true)
		{
			Uri uri;
			if (darkMode)
			{
				uri = new Uri(@"Themes/DarkTheme.xaml", UriKind.Relative);

			}
			else
			{
				uri = new Uri(@"Themes/LightTheme.xaml", UriKind.Relative);
			}
			ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
			Application.Current.Resources.Clear();
			Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
		}
	}
}
