using System;
using System.Collections.ObjectModel;

namespace Newsreader;

public class BindedData : Bindable
{
    private string? _userName;
    private ObservableCollection<string> _groups = new();
    private ObservableCollection<string> _articles = new();
    private ObservableCollection<String> _articleText = new();

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