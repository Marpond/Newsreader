using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Newsreader;

public partial class News : Window
{
    private Client _client;
    private BindedData _bindedData;
    public News(Client client, BindedData bindedData)
    {
        InitializeComponent();
        _client = client;
        _bindedData = bindedData;
    }

    private void ListBoxGroups_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (listBoxGroups.SelectedItem is null) return;
        var item = listBoxGroups.SelectedItem.ToString();
        _bindedData.Articles = _client.Foo($"listgroup {item}");
    }

    private void ListBoxArticles_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (listBoxArticles.SelectedItem is null) return;
        var item = listBoxArticles.SelectedItem.ToString();
        _bindedData.ArticleText = _client.Foo($"article {item}");
    }

    private void ListBoxArticleText_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Unselect the item
        listBoxArticleText.SelectedIndex = -1;
    }
}