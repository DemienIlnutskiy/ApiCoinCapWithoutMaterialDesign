using System;
using System.Net;
using System.IO;

namespace Cryptoriencies
{
	class CoinCapAPI
	{
		private HttpWebRequest _request;

		public string? address;

		public string Response { get; set; }
		public CoinCapAPI(string? _address)
		{
			address = _address ;
		}
		public void Run()
		{
			_request = WebRequest.Create(address??"") as HttpWebRequest;
			if (_request != null)
				_request.Method = "GET";
			try
			{
				HttpWebResponse response = _request.GetResponse() as HttpWebResponse;
				if (response != null)
				{
					var stream = response.GetResponseStream();
					if (stream != null) Response = new StreamReader(stream).ReadToEnd();
				}
			}catch (Exception) { }
		}
	}
}
