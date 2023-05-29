using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Cryptoriencies
{
	/// <summary>
	/// Interaction logic for RegisterControl.xaml
	/// </summary>
	public partial class RegisterControl : UserControl
	{
		public bool UkrLanguageMainWindow = true;
		public RegisterControl()
		{
			InitializeComponent();

			SwitchLanguage("ua");
		}
		private void Button_Reg_Click(object sender, RoutedEventArgs e)
		{
			bool value = true;
			string login = textBoxLogin.Text.Trim();
			string pass = PassBox.Password.Trim();
			string pass2 = PassBox2.Password.Trim();
			string email = textBoxEmail.Text.Trim().ToLower();

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
				UnCorrectLogin.Content = "";
				textBoxLogin.ToolTip = "";
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
				UnCorrectPassword.Content = "";
				if (value)
					value = true;
			}
			if (pass2 != pass || pass.Length < 5)
			{
				if (UkrLanguageMainWindow)
					UnCorrectPassword2.Content = "Не співпадають паролі";
				else
					UnCorrectPassword2.Content = "Password don't match";
				value = false;
			}
			else
			{
				UnCorrectPassword2.Content = "";
				if (value)
					value = true;
			}
			if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
			{
				if (email.Length < 5)
					if (UkrLanguageMainWindow)
						UnCorrectEmail.Content = "Закороткий email";
					else
						UnCorrectEmail.Content = "Too shrot email";
				else
					if (!email.Contains("@") || !email.Contains("."))
					if (UkrLanguageMainWindow)
						UnCorrectEmail.Content = "Не використані спецсимволи електронної адреси";
					else
						UnCorrectEmail.Content = "Special characters of the e-mail address aren't used";
				value = false;
			}
			else
			{
				UnCorrectEmail.Content = "";
				if (value)
					value = true;
			}
			if (value)
			{
				if (isUsersExists())
					return;
				DB db = new DB();

				MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `email`) VALUES (@login,@pass,@email)", db.getConnection());

				command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
				command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = pass;
				command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

				db.openConnection();

				if (command.ExecuteNonQuery() == 1)
				{
					MainWindow.isReg = true;
				}
				else
					if (UkrLanguageMainWindow)
					UnCorrectData.Content = "Некоректні данні";
				else
					UnCorrectData.Content = "Uncorrect data";
				db.closeConnection();
			}
		}
		public bool isUsersExists()
		{
			DB db = new DB();

			DataTable table = new DataTable();

			MySqlDataAdapter adapter = new MySqlDataAdapter();

			MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login`=@uL OR `email`=@uE", db.getConnection());

			command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = textBoxLogin.Text;
			command.Parameters.Add("@uE", MySqlDbType.VarChar).Value = textBoxEmail.Text;

			adapter.SelectCommand = command;
			try
			{
				adapter.Fill(table);

				if (table.Rows.Count > 0)
				{
					if (UkrLanguageMainWindow)
						UnCorrectData.Content = "Такий акаунт існує";
					else
						UnCorrectData.Content = "The same user exists";
					return true;
				}
				else
					return false;
			}
			catch
			{
				if (UkrLanguageMainWindow)
					MessageBox.Show("Невідома База данних");
				else
					MessageBox.Show("Unknown Data base");
				return true;
			}
		}

		public void SwitchLanguage(string laguageCode)
		{
			ResourceDictionary resourceDictionary = new ResourceDictionary();
			switch (laguageCode)
			{
				case "en":
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageRegisterDictionary.en.xaml", UriKind.Relative);
					break;
				case "ua":
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageRegisterDictionary.ua.xaml", UriKind.Relative);
					break;
				default:
					resourceDictionary.Source = new Uri("..\\SwitchLanguage\\LanguageRegisterDictionary.ua.xaml", UriKind.Relative);
					break;
			}
			this.Resources.MergedDictionaries.Add(resourceDictionary);
		}

		private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (PassBox.Password.Length < 5)
				PassBox2.IsEnabled = false;
			else PassBox2.IsEnabled = true;
		}
	}
}
