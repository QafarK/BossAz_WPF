﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BossAz_WPF.Services;

public class NotifyService : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChange([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
