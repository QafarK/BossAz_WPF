using BossAz_WPF.Models;
using BossAz_WPF.ViewModels;
using BossAzWPF.Commands;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BossAzWPF;

public partial class MainWindow : Window, INotifyPropertyChanged
{


    //Button? login;
    //ICommand Login;
    //RadioButton _employer;
    //RadioButton _worker;
    //public RadioButton Employer
    //{
    //    get => _employer; set { _employer = value; OnPropertyChange(); }
    //}

    //public RadioButton Worker
    //{
    //    get => _worker; set { _worker = value; Environment.Exit(0); OnPropertyChange();  }
    //}
    //bool let = false;
    //public ICommand RadioButtonCheckCommand { get; set; }
    //public ICommand SignUpCommand { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        //RadioButtonCheckCommand = new RelayCommand(LetSignUp, IsRadioButtonEnabled);
        //SignUpCommand = new RelayCommand(Button, ReturnLet);
        //MessageBox.Show(let.ToString());
        //textBoxUsername.BorderBrush = Brushes.Red;
    }
    private void LogIn_Button_Click(object sender, RoutedEventArgs e)
    {
        Hide();
        LoginWindow loginWindow = new();
        loginWindow.ShowDialog();
    }


    private void Window_MouseMove(object sender, MouseEventArgs e)
    {

        System.Windows.Point currentPosition = e.GetPosition(this);

        if (currentPosition.Y > 420)
        {
            usernameLabel.Foreground = Brushes.Gray;
            passwordLabel.Foreground = Brushes.Gray;
            textBoxUsername.Foreground = Brushes.Gray;
            textBoxPassword.Foreground = Brushes.Gray;
        }
        else
        {
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF673AB7");
            SolidColorBrush brush = new SolidColorBrush(color);

            usernameLabel.Foreground = brush;
            passwordLabel.Foreground = brush;
            textBoxUsername.Foreground = brush;
            textBoxPassword.Foreground = brush;
        }
    }

    private void RadioButton_Click(object sender, RoutedEventArgs e)
    {
        RadioButton? radioButton = sender as RadioButton;
        if(radioButton is not null && radioButton.IsChecked == true)
        {
            signUp.IsEnabled = true;
        }
    }


    //#region RadioButton
    //public bool IsRadioButtonEnabled(object? parameter)
    //{
    //    RadioButton? radioButton = parameter as RadioButton;

    //    if (radioButton is not null)
    //        return radioButton.IsEnabled;
    //    else
    //        return false;
    //}

    //private void LetSignUp(object? parameter)
    //{
    //    let = true;
    //}
    //#endregion

    //#region SignUpButton
    //private bool ReturnLet(object? parameter) => let;


    //#endregion
    //public Button? Login { get => login; set => login = value; }




    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChange([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }

}