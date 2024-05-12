using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BossAzWPF;
using BossAz_WPF.Models.ViewModels;


namespace BossAz_WPF.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_MouseMove(object sender, MouseEventArgs e)
    {
        Point currentPosition = e.GetPosition(this);

        if (currentPosition.Y > 440)
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

    private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Enter)
            App.Container.GetInstance<MainViewModel>().LoginCommand.Execute(null);
    }

    private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            App.Container.GetInstance<MainViewModel>().LoginCommand.Execute(null);
    }
}

