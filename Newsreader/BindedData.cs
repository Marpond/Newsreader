using System.Collections.ObjectModel;

namespace Newsreader;

public class BindedData : Bindable
{
    private ObservableCollection<string> _articles = new();
    private ObservableCollection<string> _articleText = new();
    private ObservableCollection<string> _favoriteGroups = new();
    private ObservableCollection<string> _groups = new();
    private string? _username;

    public BindedData()
    {
        _username = JsonHandler.GetRememberedUsername();
    }

    public string? Username
    {
        get => _username;
        set
        {
            _username = value;
            PropertyIsChanged();
        }
    }

    public ObservableCollection<string> Groups
    {
        get => _groups;
        set
        {
            _groups = value;
            PropertyIsChanged();
        }
    }

    public ObservableCollection<string> Articles
    {
        get => _articles;
        set
        {
            _articles = value;
            PropertyIsChanged();
        }
    }

    public ObservableCollection<string> ArticleText
    {
        get => _articleText;
        set
        {
            _articleText = value;
            PropertyIsChanged();
        }
    }

    public ObservableCollection<string> FavoriteGroups
    {
        get => _favoriteGroups;
        set
        {
            _favoriteGroups = value;
            PropertyIsChanged();
        }
    }
}