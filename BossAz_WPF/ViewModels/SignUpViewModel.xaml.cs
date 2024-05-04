using BossAzWPF.Commands;
using BossAzWPF.Models;
using System.ComponentModel;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BossAz_WPF.Views;

public partial class SignUpViewModel : Window, INotifyPropertyChanged
{
    private Employer? newEmployer;
    public Employer? NewEmployer { get => newEmployer; set { newEmployer = value; OnPropertyChange(); } }

    public ICommand SignUpCommand { get; set; }
    public SignUpViewModel()
    {
        InitializeComponent();
        DataContext = this;
        NewEmployer = new();
        //NewEmployer.City = "ASDSAS";
        //NewEmployer.Name = "ASDSAS";
        //NewEmployer.Surname = "ASDSAS";
        //NewEmployer.Age = 11;

        //new RelayCommand
        SignUpCommand = new RelayCommand(SignUpExecute, CanSignUp);

    }

    //private void Button_Click(object sender, RoutedEventArgs e)
    //{

    //    MessageBox.Show(NewEmployer.Name);

    //}


    public bool CanSignUp(object? obj)
   {
        if (NewEmployer is not null && !string.IsNullOrEmpty(NewEmployer.Name) && !string.IsNullOrEmpty(NewEmployer.Surname)// Regex ile yoxla
            && NewEmployer.Age > 0 && !string.IsNullOrEmpty(NewEmployer.City) && !string.IsNullOrEmpty(NewEmployer.Phone) && (NewEmployer.GenderMale==true || NewEmployer.GenderFemale == true))
        {
            return true; }

        return false;
    }

    public void SignUpExecute(object? obj)
    {
        //textBoxPhone.BorderBrush = Brushes.Red;
    }






    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChange([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
