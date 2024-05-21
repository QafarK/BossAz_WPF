using BossAz_WPF.Models.DataBaseModels;
using BossAz_WPF.Models.ViewModels.PageModels;
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
                                    ShowWorker(data[1]);
                                else if (data[7].Equals("Employer"))
                                    ShowEmployer(data[1]);
                                return;
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

    private void ShowEmployer(string id)
    {
        try
        {
            using FileStream fileRead = new(App.filePath_DataBaseFileEmployers, FileMode.Open, FileAccess.Read);
            using StreamReader reader = new StreamReader(fileRead);
            do
            {
                string? line = reader.ReadLine();
                if (line is not null)
                {
                    var data = line.Split(' ');
                    if (id.Equals(data[1]))
                    {
                        reader.Close();
                        App.Container.GetInstance<MainWindow>().Hide();
                        var view = App.Container.GetInstance<EmployerWindow>();
                        view.DataContext = App.Container.GetInstance<EmployerViewModel>();
                        view.Show();

                        #region PersonalInformation
                        var person = App.Container.GetInstance<PersonalInformation>();
                        person.Id = data[1];
                        person.Name = data[3];
                        person.Surname = data[5];
                        person.City = data[7];
                        person.Phone = data[9].Replace('-', ' ');
                        person.BirthDate = new(Convert.ToInt32(data[11].Split('/')[2]), Convert.ToInt32(data[11].Split('/')[0]), Convert.ToInt32(data[11].Split('/')[1]));
                        if (data[13].Equals("Male"))
                        {
                            person.GenderMale = true;
                            person.GenderFemale = false;
                        }
                        else
                        {
                            person.GenderMale = false;
                            person.GenderFemale = true;
                        }
                        #endregion

                        if (!data[15].Equals("null"))
                            App.Container.GetInstance<EmployerEditProfileViewModel>().Vacancia = new(data[16].Replace('-',' '), data[18].Replace('-', ' '));
                        else
                        {
                            App.Container.GetInstance<EmployerEditProfileViewModel>().Vacancia = new();
                            MessageBox.Show("Your Vacancia is empty. Please fill it in \"EditProfile\"");
                        }

                        App.Container.GetInstance<EmployerEditProfileViewModel>().ComparePerson = App.Container.GetInstance<PersonalInformation>().Clone() as PersonalInformation;

                        return;

                    }
                }
            } while (!reader.EndOfStream);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + " in MainViewModel");
        }
    }

    private void ShowWorker(string id)
    {
        try
        {

            using FileStream fileRead = new(App.filePath_DataBaseFileWorkers, FileMode.Open, FileAccess.Read);
            using StreamReader reader = new StreamReader(fileRead);
            do
            {
                string? line = reader.ReadLine();
                if (line is not null)
                {
                    var data = line.Split(' ');
                    if (id.Equals(data[1]))
                    {
                        reader.Close();
                        App.Container.GetInstance<MainWindow>().Hide();
                        var view = App.Container.GetInstance<WorkerWindow>();
                        view.DataContext = App.Container.GetInstance<WorkerViewModel>();
                        view.Show();

                        #region PersonalInformation
                        var person = App.Container.GetInstance<PersonalInformation>();
                        person.Id = data[1];
                        person.Name = data[3];
                        person.Surname = data[5];
                        person.City = data[7];
                        person.Phone = data[9].Replace('-', ' ');
                        person.BirthDate = new(Convert.ToInt32(data[11].Split('/')[2]), Convert.ToInt32(data[11].Split('/')[0]), Convert.ToInt32(data[11].Split('/')[1]));
                        if (data[13].Equals("Male"))
                        {
                            person.GenderMale = true;
                            person.GenderFemale = false;
                        }
                        else
                        {
                            person.GenderMale = false;
                            person.GenderFemale = true;
                        }

                        #endregion

                        if (!data[15].Equals("null"))
                        {
                            App.Container.GetInstance<WorkerEditProfileViewModel>().Cv = new(data[16], data[18], data[20],
                            new(Convert.ToInt32(data[22].Split('/')[2]), Convert.ToInt32(data[22].Split('/')[0]), Convert.ToInt32(data[22].Split('/')[1])),
                                new(Convert.ToInt32(data[24].Split('/')[2]), Convert.ToInt32(data[24].Split('/')[0]), Convert.ToInt32(data[24].Split('/')[1])));
                        }
                        else
                        {
                            App.Container.GetInstance<WorkerEditProfileViewModel>().Cv = new();
                            MessageBox.Show("Your CV is empty. Please fill it in \"EditProfile\"");
                        }

                        App.Container.GetInstance<WorkerEditProfileViewModel>().ComparePerson = App.Container.GetInstance<PersonalInformation>().Clone() as PersonalInformation;

                        return;

                    }
                }
            } while (!reader.EndOfStream);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message+" in MainViewModel");
        }
    }
}