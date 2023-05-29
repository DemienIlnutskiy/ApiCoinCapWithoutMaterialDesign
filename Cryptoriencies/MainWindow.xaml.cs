using Cryptoriencies.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Cryptoriencies.Models;

namespace Cryptoriencies
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static bool isLogin, isReg, endAsyncJSonCode=false;

		private bool asyncSearch = true;

		NumberFormatInfo numberFormat = new NumberFormatInfo()
		{
			NumberDecimalSeparator = "."
		};

		public static ObservableCollection<Crypta>? _cryptaData;

		private ObservableCollection<Crypta>? _cryptaSearchData=new ObservableCollection<Crypta>();

		public static double[]? _observablePoints { get; set; }
		public static double? observablePointsChose;
		public static string[]? _labels { get; set; }
		public static double? labelsChose;

		public void OptionMainWindow(double width, double height, WindowState windowState, double top, double left, bool regimCheck, bool switchLanguage)
		{
			Application.Current.MainWindow = this;
			Application.Current.MainWindow.Width = width;
			Application.Current.MainWindow.Height = height;
			MainWindowCrypta.WindowState = windowState;
			MainWindowCrypta.WindowStartupLocation = WindowStartupLocation.Manual;
			MainWindowCrypta.Top = top;
			MainWindowCrypta.Left = left;

			authControl.Visibility= Visibility.Collapsed;

			if ((bool)switchLanguage)
			{
				SwitchLanguage("ua");
				if (_cryptaData != null)
					CountCrypta.Text = "Криптовалюти: " + Convert.ToString(_cryptaData.Count);
			}
			else
			{
				SwitchLanguage("en");
				if (_cryptaData != null)
					CountCrypta.Text = "Cryptocurrencies: " + Convert.ToString(_cryptaData.Count);
			}
			LanguageUkrain.IsSelected = switchLanguage;
			LanguageEnglish.IsSelected = !switchLanguage;

			DarkModeToggle.IsChecked = regimCheck;
				ChangeTheme.ChangeThemeDark(DarkModeToggle.IsChecked ?? true);
		}

		public MainWindow()
		{
			InitializeComponent();

			DarkModeToggle.Click += (s, e) =>
			{
				ChangeTheme.ChangeThemeDark(DarkModeToggle.IsChecked ?? true);
			};
			registerControl.Log.GotFocus += (s, e) =>
			{
				authControl.Visibility = Visibility.Visible;
				registerControl.Visibility = Visibility.Collapsed;
				authControl.Log.IsSelected = true;
			};
			registerControl.rgButton.Click += (s, e) =>
			{
				if (isReg)
				{
					authControl.Visibility = Visibility.Visible;
					registerControl.Visibility = Visibility.Collapsed;
					isReg = false;
				}
			};
			authControl.Reg.GotFocus += (s, e) =>
			{
				registerControl.Visibility = Visibility.Visible;
				authControl.Visibility = Visibility.Collapsed;
				registerControl.Reg.IsSelected = true;
			};
			authControl.auth_Button.Click += (s, e) =>
			{
				if (isLogin)
				{
					authControl.Visibility = Visibility.Collapsed;
					isLogin = false;
				}
			};

			if (_cryptaData == null)
			{
				MainViewModel.JSonCode(LanguageUkrain.IsSelected);
				if (_cryptaData != null)
				{
					lvInfCript.ItemsSource = null;
					lvInfCript.ItemsSource = _cryptaData;
				}
			}
			else
			{
				lvInfCript.ItemsSource = _cryptaData;
			}

			CountCrypta.Text = "Криптовалюти: 100";
		}

		private void Button_Enter_Click(object sender, RoutedEventArgs e)
		{
			authControl.Visibility = Visibility.Visible;
			if (LanguageUkrain.IsSelected)
			{
				registerControl.SwitchLanguage("ua");
				authControl.SwitchLanguage("ua");
				registerControl.UkrLanguageMainWindow = LanguageUkrain.IsSelected;
				authControl.UkrLanguageMainWindow = LanguageUkrain.IsSelected;
			}
			else
			{
				registerControl.SwitchLanguage("en");
				authControl.SwitchLanguage("en");
				registerControl.UkrLanguageMainWindow = LanguageUkrain.IsSelected;
				authControl.UkrLanguageMainWindow = LanguageUkrain.IsSelected;
			}
		}

		private void Button_Reg_Click(object sender, RoutedEventArgs e)
		{
			registerControl.Visibility = Visibility.Visible;
			if ((bool)LanguageUkrain.IsSelected)
			{
				registerControl.SwitchLanguage("ua");
				authControl.SwitchLanguage("ua");
				registerControl.UkrLanguageMainWindow = LanguageUkrain.IsSelected;
				authControl.UkrLanguageMainWindow = LanguageUkrain.IsSelected;
			}
			else
			{
				registerControl.SwitchLanguage("en");
				authControl.SwitchLanguage("en");
				registerControl.UkrLanguageMainWindow = LanguageUkrain.IsSelected;
				authControl.UkrLanguageMainWindow = LanguageUkrain.IsSelected;
			}
		}

		private async void TBoxSearch_LostFocus(object sender, RoutedEventArgs e)
		{
			await Task.Run(() =>
			{
				lvInfCript2.SelectionChanged += (s, e) =>
				{
					lvInfCript2.Visibility = Visibility.Visible;
					GridRow3.Height = GridLength.Auto;

					Crypta selectItem = (Crypta)lvInfCript2.SelectedItem;

					labelsChose = double.Parse(selectItem.Rank ?? "0", numberFormat) - 1;
					observablePointsChose = double.Parse(selectItem.PriceUsd == null ? "0" : selectItem.PriceUsd.Remove(0, 1), numberFormat);

					AboutCrypta aboutCrypta = new AboutCrypta();
					aboutCrypta.OptionAboutCryptaWindow(selectItem, MainWindowCrypta.Width, MainWindowCrypta.Height,
						MainWindowCrypta.WindowState, MainWindowCrypta.Top, MainWindowCrypta.Left, DarkModeToggle.IsChecked ?? true, LanguageUkrain.IsSelected);
					asyncSearch = false;
					aboutCrypta.Show();
					Close();
				};
			});
			if (asyncSearch)
			{
				lvInfCript2.Visibility = Visibility.Hidden;
				GridRow3.Height = new GridLength(0);
				lvInfCript2.ItemsSource = null;
			}
		}

		private void TBoxSearch_GotFocus(object sender, RoutedEventArgs e)
		{
			if (TBoxSearch.Text != "")
			{
				lvInfCript2.ItemsSource = _cryptaSearchData;
				lvInfCript2.Visibility = Visibility.Visible;
				GridRow3.Height = GridLength.Auto;
			}
		}

		private void lvInfCript_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Crypta selectItem = (Crypta)lvInfCript.SelectedItem;

			observablePointsChose = double.Parse(selectItem.PriceUsd == null ? "0" : selectItem.PriceUsd.Remove(0, 1), numberFormat);
			labelsChose = double.Parse(selectItem.Rank ?? "0", numberFormat) - 1;

			AboutCrypta aboutCrypta = new AboutCrypta();
			aboutCrypta.OptionAboutCryptaWindow(selectItem, MainWindowCrypta.Width, MainWindowCrypta.Height,
				MainWindowCrypta.WindowState, MainWindowCrypta.Top, MainWindowCrypta.Left, DarkModeToggle.IsChecked ?? true, LanguageUkrain.IsSelected);
			aboutCrypta.Show();
			Close();
		}
		private void SwitchLanguage(string laguageCode)
		{
			ResourceDictionary resourceDictionary = new ResourceDictionary();
			switch (laguageCode)
			{
				case "en":
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageMainMenuDictionary.en.xaml", UriKind.Relative);
					break;
				case "ua":
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageMainMenuDictionary.ua.xaml", UriKind.Relative);
					break;
				default:
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageMainMenuDictionary.ua.xaml", UriKind.Relative);
					break;
			}
			this.Resources.MergedDictionaries.Add(resourceDictionary);
		}

		private void LanguageUkrain_Selected(object sender, RoutedEventArgs e)
		{
			SwitchLanguage("ua");
			if (_cryptaData != null)
				CountCrypta.Text = "Криптовалюти:" + " " + Convert.ToString(_cryptaData.Count);
		}

		private void LanguageEnglish_Selected(object sender, RoutedEventArgs e)
		{
			SwitchLanguage("en");
			if (_cryptaData != null)
				CountCrypta.Text = "Cryptocurrencies:" + " " + Convert.ToString(_cryptaData.Count);
		}

		private void Calculator_Click(object sender, RoutedEventArgs e)
		{
			CalculatorControl.CBcryptocurrency.ItemsSource = _cryptaData;
			CalculatorControl.CBcryptocurrency2.ItemsSource = _cryptaData;
			CalculatorControl.Visibility = Visibility.Visible;
			if ((bool)LanguageUkrain.IsSelected)
			{
				CalculatorControl.SwitchLanguage("ua");
				CalculatorControl.UkrLanguageClaculator = LanguageUkrain.IsSelected;
			}
			else
			{
				CalculatorControl.SwitchLanguage("en");
				CalculatorControl.UkrLanguageClaculator = LanguageUkrain.IsSelected;
			}
		}

		private void CalculatorControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (CalculatorControl.openDiagram && _cryptaData != null)
			{
				foreach (var item in _cryptaData)
				{
					if (item.PriceUsd == CalculatorControl.SwitchCrypta.Text)
						try
						{
							labelsChose = double.Parse(item.Rank ?? "0", numberFormat) - 1;
							observablePointsChose = double.Parse(item.PriceUsd == null ? "0" : item.PriceUsd.Remove(0, 1), numberFormat);
							break;
						}
						catch
						{
							labelsChose = null;
							observablePointsChose = null;
						}
					else
					{
						labelsChose = null;
						observablePointsChose = null;
					}
				}

				AboutCrypta aboutCrypta = new AboutCrypta();
				aboutCrypta.OptionAboutCryptaWindow(CalculatorControl.selectItemCalculator ?? new Crypta(), MainWindowCrypta.Width, MainWindowCrypta.Height,
					MainWindowCrypta.WindowState, MainWindowCrypta.Top, MainWindowCrypta.Left, DarkModeToggle.IsChecked ?? true, LanguageUkrain.IsSelected);
				aboutCrypta.Show();
				this.Close();
			}
		}

		private void Diagram_Click(object sender, RoutedEventArgs e)
		{
			labelsChose = null;
			observablePointsChose = null;

			AboutCrypta aboutCrypta = new AboutCrypta();
			aboutCrypta.OptionAboutCryptaWindow(new Crypta(), MainWindowCrypta.Width, MainWindowCrypta.Height,
					MainWindowCrypta.WindowState, MainWindowCrypta.Top, MainWindowCrypta.Left, DarkModeToggle.IsChecked ?? true, LanguageUkrain.IsSelected);
			aboutCrypta.Show();
			this.Close();
		}

		private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			_cryptaSearchData = new ObservableCollection<Crypta>();
			if (_cryptaData != null)
				if (TBoxSearch.Text.Length == 0)
				{
					lvInfCript.ItemsSource = _cryptaData;
					lvInfCript2.Visibility = Visibility.Hidden;
					GridRow3.Height = new GridLength(0);
					return;
				}
				else
				{
					string strSearchValue1, strSearchValue2;
					foreach (var item in _cryptaData)
					{
						if (item.Name != null && _cryptaSearchData != null)
						{
							strSearchValue1 = item.Name.ToLower();
							strSearchValue2 = TBoxSearch.Text.ToLower();

							if (strSearchValue1.StartsWith(strSearchValue2))
								_cryptaSearchData.Add(item);
						}
					}
					lvInfCript.ItemsSource = _cryptaSearchData;
					lvInfCript2.ItemsSource = _cryptaSearchData;
					lvInfCript2.Visibility = Visibility.Visible;
					GridRow3.Height = GridLength.Auto;
				}
		}
	}
}
