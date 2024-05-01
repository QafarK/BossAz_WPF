using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BossAz_WPF.Models;

public class Login : INotifyPropertyChanged
{
    string? _username;
    string? _password;

    public string? Username { get => _username; set { _username = value; OnPropertyChange(); } }
    public string? Password { get => _password; set { _password = value; OnPropertyChange(); } }

    //----------------------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChange([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
