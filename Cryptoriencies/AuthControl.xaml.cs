using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Cryptoriencies
{
	/// <summary>
	/// Interaction logic for AuthControl.xaml
	/// </summary>
	public partial class AuthControl : UserControl
	{
		public bool UkrLanguageMainWindow = true;
		public AuthControl()
		{
			InitializeComponent();

			SwitchLanguage("ua");
		}
		private void Button_Auth_Click(object sender, RoutedEventArgs e)
		{
			string login = textBoxLogin.Text.Trim();
			string pass = PassBox.Password.Trim();
			bool value = true;
			login = "aaaaaa";
			pass = "aaaaaa";
			if (login.Length < 5)
			{
				if (UkrLanguageMainWindow)
					UnCorrectLogin.Content = "Закороткий логін";
				else
					UnCorrectLogin.Content = "Too shrot login";
				value = false;
			}
			else
			{
				if (UkrLanguageMainWindow)
					UnCorrectLogin.Content = "";
				else
					UnCorrectLogin.Content = "";
				value = true;
			}
			if (pass.Length < 5)
			{
				if (UkrLanguageMainWindow)
					UnCorrectPassword.Content = "Закороткий пароль";
				else
					UnCorrectPassword.Content = "Too shrot password";	
				value = false;
			}
			else
			{
				if (UkrLanguageMainWindow)
					UnCorrectPassword.Content = "";
				else
					UnCorrectPassword.Content = "";
				value = true;
			}
			if (value)
			{
				DB db = new DB();

				DataTable table = new DataTable();

				MySqlDataAdapter adapter = new MySqlDataAdapter();

				MySqlCommand command = new MySqlCommand("SELECT * FROM `users`WHERE `login`=@uL AND `password`=@uP", db.getConnection());

				command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
				command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass;

				adapter.SelectCommand = command;
				try
				{
					adapter.Fill(table);

					if (table.Rows.Count > 0)
					{
						MainWindow.isLogin = true;
					}
					else
					if (UkrLanguageMainWindow)
						UnCorrectData.Content = "Неіснуючий користувач";
					else
						UnCorrectData.Content = "Non-existent user";
				}
				catch
				{
					if (UkrLanguageMainWindow)
						MessageBox.Show("Невідома База данних");
					else
						MessageBox.Show("Unknown Data base");
				}
			}
		}

		public void SwitchLanguage(string laguageCode)
		{
			ResourceDictionary resourceDictionary = new ResourceDictionary();
			switch (laguageCode)
			{
				case "en":
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageAuthDictionary.en.xaml", UriKind.Relative);
					break;
				case "ua":
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageAuthDictionary.ua.xaml", UriKind.Relative);
					break;
				default:
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageAuthDictionary.ua.xaml", UriKind.Relative);
					break;
			}
			this.Resources.MergedDictionaries.Add(resourceDictionary);
		}

		private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
		{

        }
    }
}
