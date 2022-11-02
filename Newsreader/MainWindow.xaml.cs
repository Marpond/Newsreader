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
    public static string CurrentUsername;
    private readonly BindedData _bindedData = new();
    private readonly Client _client = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _bindedData;
        saveUsernameCheckBox.IsChecked = !_bindedData.Username!.Equals(string.Empty);
    }

    private void Login_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            CurrentUsername = Username.Text;
            _client.Login(CurrentUsername, Password.Password);
            MessageBox.Show("Logged in!");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
            return;
        }

        if (JsonHandler.GetUsers() is null ||
            JsonHandler.GetUsers()!.All(user => !user.Username.Equals(CurrentUsername)))
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