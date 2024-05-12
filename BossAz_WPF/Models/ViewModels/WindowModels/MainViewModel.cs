using BossAz_WPF.Models.ViewModels.WindowModels;
using BossAz_WPF.Windows;
using BossAzWPF;
using BossAzWPF.Commands;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace BossAz_WPF.Models.ViewModels;

public class MainViewModel : BaseClass
{
    string? _username;
    string? _password;
    string? _errorLogin;

    public string? Username { get => _username; set { _username = value; OnPropertyChange(); } }
    public string? Password { get => _password; set { _password = value; OnPropertyChange(); } }
    public string? ErrorLogin { get => _errorLogin; set { _errorLogin = value; OnPropertyChange(); } }
    public ICommand LoginCommand { get; set; }
    public ICommand SignUpCommand { get; set; }
    public MainViewModel()
    {
        LoginCommand = new RelayCommand(LoginExecute);
        SignUpCommand = new RelayCommand(SignUpExecute);
    }
    public void SignUpExecute(object? obj)
    {
        App.Container.GetInstance<MainWindow>().Hide();

        var view = App.Container.GetInstance<SignUpWindow>();
        view.DataContext = App.Container.GetInstance<SignUpViewModel>();
        view.Show();
    }
    public void LoginExecute(object? obj)
    {
        if (Username is null || Password is null)
            MessageBox.Show("Please fill the username and password box");
        else
        {
            if (File.Exists(App.filePath_DataBaseFile))
            {
                using FileStream fileRead = new(App.filePath_DataBaseFile, FileMode.Open, FileAccess.Read);
                using StreamReader reader = new StreamReader(fileRead);

                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                do
                {
                    string? line = reader.ReadLine();
                    if (line is not null)
                    {
                        var data = line.Split(' ');
                        if (data[3].Equals(Username))
                        {
                            if (data[5].Equals(Password))
                            {
                                ErrorLogin = null;
                                if (data[7].Equals("Worker"))
                                {
                                    App.Container.GetInstance<MainWindow>().Hide();
                                    var view = App.Container.GetInstance<WorkerWindow>();
                                    view.DataContext = App.Container.GetInstance<WorkerViewModel>();
                                    view.Show();
                                }
                                else if (data[7].Equals("Employer"))
                                {
                                    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                }
                                return; //////
                            }
                            else
                                break;
                        }
                    }
                } while (!reader.EndOfStream);
                ErrorLogin = "*Username or password is incorrect";
            }
        }
    }
}
