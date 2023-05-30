using Cryptoriencies.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Cryptoriencies
{
	/// <summary>
	/// Interaction logic for AuthControl.xaml
	/// </summary>
	public partial class Calculator : UserControl
	{
		NumberFormatInfo numberFormat = new NumberFormatInfo()
		{
			NumberDecimalSeparator = "."
		};

		public Crypta? selectItemCalculator;
		public bool UkrLanguageClaculator = true, openDiagram = false;
		private bool price = false,price2=false;
		public Calculator()
		{
			InitializeComponent();

			SwitchLanguage("ua");
		}

		public void SwitchLanguage(string laguageCode)
		{
			ResourceDictionary resourceDictionary = new ResourceDictionary();
			switch (laguageCode)
			{
				case "en":
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageCalculatorDictionary.en.xaml", UriKind.Relative);
					break;
				case "ua":
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageCalculatorDictionary.ua.xaml", UriKind.Relative);
					break;
				default:
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageCalculatorDictionary.ua.xaml", UriKind.Relative);
					break;
			}
			this.Resources.MergedDictionaries.Add(resourceDictionary);
		}

		private void Menu_Button_Click(object sender, RoutedEventArgs e)
		{
			openDiagram = false;
			this.Visibility = Visibility.Collapsed;

			TextSum.Text = "";
			SwitchCrypta.Text = "";
			SwitchCrypta2.Text = "";
			CBcryptocurrency.Text="";
			CBcryptocurrency2.Text = "";
		}

		public void Diagram_Button_Click(object sender, RoutedEventArgs e)
		{
			openDiagram = true;
			this.Visibility = Visibility.Collapsed;

			TextSum.Text = "";
			SwitchCrypta.Text = "";
			SwitchCrypta2.Text = "";
			CBcryptocurrency.Text = "";
			CBcryptocurrency2.Text = "";
		}

		private void CBcryptocurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Crypta selectItem = (Crypta)CBcryptocurrency.SelectedItem;

			if (selectItem != null)
			{
				SwitchCrypta.Text = selectItem.PriceUsd == null ? "" : selectItem.PriceUsd.ToString();
				price = true;

				if (price && price2)
					Sum.IsEnabled = true;
				else
					Sum.IsEnabled = false;
				selectItemCalculator = selectItem;
			}
		}

		private void CBcryptocurrency2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Crypta selectItem = (Crypta)CBcryptocurrency2.SelectedItem;

			if (selectItem != null)
			{
				SwitchCrypta2.Text = selectItem.PriceUsd == null ? "" : selectItem.PriceUsd.ToString();
				price2 = true;
				if (price && price2)
					Sum.IsEnabled = true;
				else
					Sum.IsEnabled = false;
				selectItemCalculator = selectItem;
			}
		}

		private void Sum_Click(object sender, RoutedEventArgs e)
		{
			Crypta selectItem = (Crypta)CBcryptocurrency.SelectedItem;
			Crypta selectItem2 = (Crypta)CBcryptocurrency2.SelectedItem;
			UnCorrectData.Content = "";
			decimal crypta1,crypta2;
			crypta1 = Decimal.Parse(SwitchCrypta.Text.Remove(0, 1), numberFormat);
			crypta2 = Decimal.Parse(SwitchCrypta2.Text.Remove(0, 1), numberFormat);
			TextSum.Text ="1 "+ selectItem.Name+" = "+ Convert.ToString(crypta1/crypta2)+" "+ selectItem2.Name;
		}
	}
}
