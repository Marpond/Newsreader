using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Newsreader;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly BindedData _bindedData = new();
    private readonly Client _client = new();
    public static string CurrentUsername;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _bindedData;
        saveUsernameCheckBox.IsChecked = !_bindedData.Username!.Equals(string.Empty);
    }

    private void Login_OnClick(object sender, RoutedEventArgs e)
    {
            CurrentUsername = Username.Text;
            _client.Login(CurrentUsername, Password.Password);
            MessageBox.Show("Logged in!");
            
            
            if (JsonHandler.GetUsers() is null || JsonHandler.GetUsers()!.All(user => !user.Username.Equals(CurrentUsername)))
                JsonHandler.AddNewUser(CurrentUsername);
            Debug.Print($"Saved new user: {CurrentUsername}");
            
            if (saveUsernameCheckBox.IsChecked is true)
                JsonHandler.RememberUsername(CurrentUsername);
            else
                JsonHandler.ForgetUsername();

            // Switch to News.xaml
            var news = new News(_client, _bindedData);
            Content = news.Content;
    }
}