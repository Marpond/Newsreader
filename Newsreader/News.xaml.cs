using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Newsreader;

public partial class News : Window
{
    private readonly List<string> _allGroups;
    private readonly BindedData _bindedData;
    private readonly Client _client;

    public News(Client client, BindedData bindedData)
    {
        InitializeComponent();
        _client = client;
        _bindedData = bindedData;
        _allGroups = _client.GetObservableCollection("list").ToList();
        _bindedData.Groups = new ObservableCollection<string>(_allGroups);
        UpdateFavoriteGroups();
    }

    private void ListBoxBothGroups_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox listBox) return;
        var item = listBox.SelectedItem as string;
        var articleText = _client.GetObservableCollection($"listgroup {item}");
        _bindedData.Articles = articleText;
    }

    private void ListBoxArticles_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (listBoxArticles.SelectedItem is null) return;
        var item = listBoxArticles.SelectedItem.ToString();
        _bindedData.ArticleText = _client.GetObservableCollection($"article {item}");
    }

    private void ListBoxArticleText_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Unselect the item
        listBoxArticleText.SelectedIndex = -1;
    }

    private void TextBoxFilter_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        _bindedData.Groups =
            new ObservableCollection<string>(_allGroups.Where(group => group.Contains(textBoxFilter.Text)));
        var favoriteGroups = JsonHandler.GetFavoriteGroups(MainWindow.CurrentUsername);
        _bindedData.FavoriteGroups =
            new ObservableCollection<string>(favoriteGroups.Where(group => group.Contains(textBoxFilter.Text)));
    }

    private void ButtonFavoriteGroup_OnClick(object sender, RoutedEventArgs e)
    {
        JsonHandler.AddFavoriteGroup(listBoxGroups.SelectedItem.ToString()!, MainWindow.CurrentUsername);
        UpdateFavoriteGroups();
    }

    private void ButtonRemoveFavoriteGroup_OnClick(object sender, RoutedEventArgs e)
    {
        var index = listBoxFavoriteGroups.SelectedIndex;
        JsonHandler.RemoveFavoriteGroup(listBoxFavoriteGroups.SelectedItem.ToString()!, MainWindow.CurrentUsername);
        UpdateFavoriteGroups();
        // Move one index down with the selection
        if (listBoxFavoriteGroups.Items.Count > index)
            listBoxFavoriteGroups.SelectedIndex = index;
    }

    private void UpdateFavoriteGroups()
    {
        _bindedData.FavoriteGroups = JsonHandler.GetFavoriteGroups(MainWindow.CurrentUsername);
    }

    private void ButtonPost_OnClick(object sender, RoutedEventArgs e)
    {
        if (textBoxFrom.Text == "" || textBoxSubject.Text == "" || textBoxBody.Text == "")
        {
            MessageBox.Show("Please fill in all fields");
        }
        else
        {
            _client.Post(
                textBoxFrom.Text,
                textBoxSubject.Text,
                textBoxNewsgroups.Text,
                textBoxBody.Text);
            MessageBox.Show("Post successful");
        }
    }
}