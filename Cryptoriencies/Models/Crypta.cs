namespace Cryptoriencies.Models
{
	public class Crypta
	{
		private string? _name;
		private string? _rank;
		private string? _symbol;
		private string? _supply;
		private string? _maxSupply;
		private string? _marketCapUsd;
		private string? _volumeUsd24Hr;
		private string? _priceUsd;
		private string? _changePercent24Hr;
		private string? _vwap24Hr;
		private string? _explorer;
		public string? Name { get { return _name; } set { _name = value; } }
		public string? Rank { get { return _rank; } set { _rank = value; } }
		public string? Symbol { get { return _symbol; } set { _symbol = value; } }
		public string? Supply { get { return _supply; } set { _supply = value; } }
		public string? MaxSupply { get { return _maxSupply; } set { _maxSupply = value; } }
		public string? MarketCapUsd { get { return _marketCapUsd; } set { _marketCapUsd = value; } }
		public string? VolumeUsd24Hr { get { return _volumeUsd24Hr; } set { _volumeUsd24Hr = value; } }
		public string? PriceUsd { get { return _priceUsd; } set { _priceUsd = value; } }
		public string? ChangePercent24Hr { get { return _changePercent24Hr; } set { _changePercent24Hr = value; } }
		public string? Vwap24Hr { get { return _vwap24Hr; } set { _vwap24Hr = value; } }
		public string? Explorer { get { return _explorer; } set { _explorer = value; } }

	}
}
