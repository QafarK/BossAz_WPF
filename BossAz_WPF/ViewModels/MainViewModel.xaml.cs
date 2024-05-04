using BossAz_WPF.ViewModels;
using BossAz_WPF.Views;
using System.ComponentModel;
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
        // if(json file
        Hide();
        LoginWindow loginWindow = new();
        loginWindow.ShowDialog();
    }
    private void SignUp_Button_Click(object sender, RoutedEventArgs e)
    {
        SignUpViewModel? signUpWindow = null;

        if (worker.IsChecked == true || employer.IsChecked == true)
        {
            Hide();
            signUpWindow = new SignUpViewModel();
            signUpWindow.ShowDialog();

            if (signUpWindow.DialogResult == false)
                Show();
        }
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
            textBoxUsername.Background = Brushes.White;
            textBoxPassword.Background = Brushes.White;
        }
        else
        {
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF673AB7");
            SolidColorBrush brush = new SolidColorBrush(color);

            usernameLabel.Foreground = brush;
            passwordLabel.Foreground = brush;
            textBoxUsername.Foreground = brush;
            textBoxPassword.Foreground = brush;
            textBoxUsername.Background = Brushes.AliceBlue;
            textBoxPassword.Background = Brushes.AliceBlue;
        }
    }

    private void RadioButton_Click(object sender, RoutedEventArgs e)
    {
        RadioButton? radioButton = sender as RadioButton;
        if (radioButton is not null && radioButton.IsChecked == true)
            signUp.IsEnabled = true;
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