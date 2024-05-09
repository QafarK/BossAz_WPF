using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using BossAzWPF;
using BossAz_WPF.Models.ViewModels;


namespace BossAz_WPF.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    private void LogIn_Button_Click(object sender, RoutedEventArgs e)
    {
        // if(json file
        Hide();
        //Login window=new()
    }
    private void SignUp_Button_Click(object sender, RoutedEventArgs e)
    {
        var view = App.Container.GetInstance<SignUpWindow>();
        view.DataContext = App.Container.GetInstance<SignUpViewModel>();

        var viewModel = App.Container.GetInstance<SignUpViewModel>();
        if (worker.IsChecked == true || employer.IsChecked == true)
        {
            Hide();

            if (worker.IsChecked == true)
                viewModel.IsWorkerOrEmployer = "Worker";
            else if (employer.IsChecked == true)
                viewModel.IsWorkerOrEmployer = "Employer";
            else
                throw new KeyNotFoundException();

            view.Show();
        }
    }

    #region Events
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
            Color color = (Color)ColorConverter.ConvertFromString("#FF673AB7");
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
    #endregion


}

