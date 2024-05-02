using BossAzWPF.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace BossAz_WPF.Views;

public partial class SignUpViewModel : Window, INotifyPropertyChanged
{
    private Employer? newEmployer;
    public Employer? NewEmployer { get => newEmployer; set { newEmployer = value; OnPropertyChange(); } }


    public SignUpViewModel()
    {
        InitializeComponent();
        DataContext = this;
        NewEmployer = new();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(NewEmployer.Name);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChange([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
