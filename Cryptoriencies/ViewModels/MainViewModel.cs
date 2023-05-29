using Cryptoriencies.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Windows;

namespace Cryptoriencies.ViewModels
{
	class MainViewModel
    {
		private static string address = "https://api.coincap.io/v2/assets";

		private static NumberFormatInfo numberFormat = new NumberFormatInfo()
		{
			NumberDecimalSeparator = "."
		};

		public static async void JSonCode(bool LanguageUkrain)
		{
			MainWindow._cryptaData = new ObservableCollection<Crypta>();

			HttpClient request = new HttpClient();

			string response;
			try
			{
				response = await request.GetStringAsync(address);

				var json = JObject.Parse(response);


				double? dSupply = 0, dMaxSupply = 0;

				var datas = json["data"];
				if (datas != null)
					foreach (var data in datas)
					{
						var name = data["name"];
						var rank = data["rank"];
						var symbol = data["symbol"];
						var supply = data["supply"];
						var maxSupply = data["maxSupply"];
						var marketCapUsd = data["marketCapUsd"];
						var volumeUsd24Hr = data["volumeUsd24Hr"];
						var priceUsd = data["priceUsd"];
						var changePercent24Hr = data["changePercent24Hr"];
						var vwap24Hr = data["vwap24Hr"];

						try { dSupply = double.Parse("" + supply as string, numberFormat); }
						catch { dSupply = null; }

						try { dMaxSupply = double.Parse("" + maxSupply as string, numberFormat); }
						catch { dMaxSupply = null; }

						MainWindow._cryptaData.Add(new Crypta()
						{
							Name = "" + name is string strName ? strName : null,
							Rank = "" + rank is string strRank ? strRank : null,
							Symbol = "" + symbol is string strSymbol ? strSymbol : null,
							Supply = Convert.ToString(dSupply),
							MaxSupply = Convert.ToString(dMaxSupply),
							MarketCapUsd = "$" + marketCapUsd is string strMarketCapUsd ? strMarketCapUsd : null,
							VolumeUsd24Hr = "$" + volumeUsd24Hr is string strVolumeUsd24Hris ? strVolumeUsd24Hris : null,
							PriceUsd = "$" + priceUsd is string strPriceUsd ? strPriceUsd : null,
							ChangePercent24Hr = changePercent24Hr + "%" is string strChangePercent24Hr ? strChangePercent24Hr : null,
							Vwap24Hr = "$" + vwap24Hr is string strVwap24Hr ? strVwap24Hr : null
						});
					}

				int i = 0;
				MainWindow._observablePoints = new double[MainWindow._cryptaData.Count];
				MainWindow._labels = new string[MainWindow._cryptaData.Count];

				foreach (var item in MainWindow._cryptaData)
				{
					MainWindow._observablePoints[i] = Double.Parse(item.PriceUsd == null ? "0" : item.PriceUsd.Remove(0, 1), numberFormat);
					MainWindow._labels[i++] = item.Name ?? "";

				}
				MainWindow.endAsyncJSonCode = true;
			}
			catch
			{
				if (LanguageUkrain)
					MessageBox.Show("проблема з Api");
				else
					MessageBox.Show("Api error");
			}
		}
	}
}
