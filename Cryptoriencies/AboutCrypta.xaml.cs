using Cryptoriencies.Models;
using Cryptoriencies.ViewModels;
using System;
using System.Windows;

namespace Cryptoriencies
{
	public class LogarithmicPoint
	{
		public double X { get; set; }
		public double Y { get; set; }
	}

	public partial class AboutCrypta : Window
	{
		private Crypta? crypta;

		public void OptionAboutCryptaWindow(Crypta referenceData, double width, double height, WindowState
			windowState, double top, double left, bool regimCheck, bool? switchLanguage)
		{
			if (referenceData.PriceUsd != null)
				crypta = referenceData;
			if (switchLanguage ?? true)
				SwitchLanguage("ua");
			else
				SwitchLanguage("en");
			LanguageUkrain.IsSelected = switchLanguage ?? true;
			LanguageEnglish.IsSelected = !switchLanguage ?? false;

			Application.Current.MainWindow = this;
			Application.Current.MainWindow.Width = width;
			Application.Current.MainWindow.Height = height;
			About.WindowState = windowState;
			About.WindowStartupLocation = WindowStartupLocation.Manual;
			About.Top = top;
			About.Left = left;

			DarkModeToggle.IsChecked = regimCheck;
			ChangeTheme.ChangeThemeDark(DarkModeToggle.IsChecked ?? true);

			if (referenceData.PriceUsd != null)
				try
				{
					if (switchLanguage ?? true)
						YourCrypta.Text = "Вибрана криптовалюта:" + " " + referenceData.Name;
					else
						YourCrypta.Text = "Curent cryptocurrency:" + " " + referenceData.Name;
				}
				catch
				{
					YourCrypta.Text = "Вибрана криптовалюта:" + " " + referenceData.Name;
				}
		}

		public AboutCrypta()
		{
			InitializeComponent();

			DarkModeToggle.Click += (s, e) =>
			{
				ChangeTheme.ChangeThemeDark(DarkModeToggle.IsChecked ?? true);
			};
		}
		private void Button_Main_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.OptionMainWindow(About.Width, About.Height, About.WindowState, About.Top, About.Left, DarkModeToggle.IsChecked ?? true, LanguageUkrain.IsSelected);
			mainWindow.Show();
			Close();
		}
		private void SwitchLanguage(string laguageCode)
		{
			ResourceDictionary resourceDictionary = new ResourceDictionary();
			switch (laguageCode)
			{
				case "en":
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageAboutCryptaDictionary.en.xaml", UriKind.Relative);
					break;
				case "ua":
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageAboutCryptaDictionary.ua.xaml", UriKind.Relative);
					break;
				default:
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageAboutCryptaDictionary.ua.xaml", UriKind.Relative);
					break;
			}
			this.Resources.MergedDictionaries.Add(resourceDictionary);
		}

		private void LanguageUkrain_Selected(object sender, RoutedEventArgs e)
		{
			SwitchLanguage("ua");
			if (crypta != null)
				YourCrypta.Text = "Вибрана криптовалюта:" + " " + Convert.ToString(crypta.Name);
		}

		private void LanguageEnglish_Selected(object sender, RoutedEventArgs e)
		{
			SwitchLanguage("en");
			if (crypta != null)
				YourCrypta.Text = "Curent cryptocurrency:" + " " + Convert.ToString(crypta.Name);
		}
	}
}
