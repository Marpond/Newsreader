using System;
using System.Diagnostics;
using System.Windows;

namespace Newsreader;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly Client _client = new();
    private readonly BindedData _bindedData = new();
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = _bindedData;
        if (!_bindedData.UserName!.Equals(String.Empty)) checkBox.IsChecked = true;
    }

    private void Login_OnClick(object sender, RoutedEventArgs e)
    {
        try 
        {
            _client.Login(Username.Text, Password.Password);
            MessageBox.Show("Logged in!");
            if (checkBox.IsChecked is true)
            {
                Newsreader.Username.Set(Username.Text);
            }
            else
            {
                Newsreader.Username.Delete();
            }

            _bindedData.Groups = _client.Foo("list");
                // Switch to News.xaml
            var news = new News(_client,_bindedData);
            Content = news.Content;
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

}