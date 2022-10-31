using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Newsreader;

public abstract class Bindable : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void PropertyIsChanged([CallerMemberName] string memberName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
    }
}