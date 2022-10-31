using System.Collections.ObjectModel;

namespace Newsreader;

public class BindedData : Bindable
{
    private ObservableCollection<string> _articles = new();
    private ObservableCollection<string> _articleText = new();
    private ObservableCollection<string> _groups = new();
    private string? _userName;

    public BindedData()
    {
        _userName = Username.Get();
    }

    public string? UserName
    {
        get => _userName;
        set
        {
            _userName = value;
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
}