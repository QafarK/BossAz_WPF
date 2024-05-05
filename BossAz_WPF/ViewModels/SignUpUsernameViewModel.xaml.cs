using BossAz_WPF.Models;
using BossAzWPF.Commands;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BossAz_WPF.ViewModels;

public partial class SignUpUsernameViewModel : Window, INotifyPropertyChanged
{
    #region File & Folders
    // FOLDERS
    static string folderPath_DataBase = Path.Combine("..\\..\\..\\", "DateBase");
    static string folderPath_ModelsConvertedFile = Path.Combine("..\\..\\..\\", "ModelsConvertedFile");
    static string folderPath_JSON = Path.Combine("..\\..\\..\\", "JSON");
    static string folderPath_Notification = Path.Combine("..\\..\\..\\", "Notification");

    //FILES & JSON
    static string filePath_DataBaseFile = Path.Combine(folderPath_DataBase, "DataBaseFile.txt");
    static string jsonPath_DataBase = Path.Combine(folderPath_DataBase, "DataBase.json");
    static string filePath_WorkerClassInfos = Path.Combine(folderPath_ModelsConvertedFile, "WorkerClassInfos.txt");
    static string filePath_EmployerClassInfos = Path.Combine(folderPath_ModelsConvertedFile, "EmployerClassInfos.txt");
    static string jsonPath_UsersDataAboutWorker = Path.Combine(folderPath_JSON, "UsersDataAboutWorker.json");
    static string jsonPath_UsersDataAboutEmployer = Path.Combine(folderPath_JSON, "UsersDataAboutEmployer.json");
    static string filePath_Notification = Path.Combine(folderPath_Notification, "Notifcation.txt");
    #endregion
    PersonalInformationAbstract? _personalInformation;
    string _colorProgressBar;
    int _valueProgressBar;
    public string ColorProgressBar { get => _colorProgressBar; set { _colorProgressBar = value; OnPropertyChange(); } }
    public int ValueProgressBar { get => _valueProgressBar; set { _valueProgressBar = value; OnPropertyChange(); } }
    string? _username;
    string? _password;
    string? _repassword;
    public string? Username { get => _username; set { _username = value; OnPropertyChange(); } }
    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChange();
            if (value is not null && value.Length <= 3)
            {
                ValueProgressBar = value.Length * 10;
                ColorProgressBar = "Red";
            }
            else if (value is not null && value.Length > 3 && value.Length < 8)
            {
                ValueProgressBar = value.Length * 10;
                ColorProgressBar = "Yellow";
            }
            else if (value is not null && value.Length > 7)
            {
                bool have = false;
                for (int i = 0; i < 10; i++)
                {
                    if (value.Contains(i.ToString()))
                    {
                        have = true;
                        break;
                    }
                }
                if (have == false)
                {
                    ValueProgressBar = 75;
                    ColorProgressBar = "Yellow";
                    ErrorPassword = "*Password must be minimum 8 characters, at least 1 number";
                }
                else
                {
                    ValueProgressBar = 80;
                    ColorProgressBar = "Green";
                    ErrorPassword = null;
                }
            }

        }
    }
    public string? Repassword { get => _repassword; set { _repassword = value; } }

    string? errorUsername;
    string? errorPassword;
    string? errorRepassword;
    public string? ErrorUsername { get => errorUsername; set { errorUsername = value; OnPropertyChange(); } }
    public string? ErrorPassword { get => errorPassword; set { errorPassword = value; OnPropertyChange(); } }
    public string? ErrorRepassword { get => errorRepassword; set { errorRepassword = value; OnPropertyChange(); } }
    public PersonalInformationAbstract? PersonalInformation { get => _personalInformation; set => _personalInformation = value; }

    public ICommand? SignUpCommand { get; set; }
    public ICommand? BackCommand { get; set; }


    public SignUpUsernameViewModel(PersonalInformationAbstract personalInformation)
    {
        InitializeComponent();
        DataContext = this;
        _personalInformation = personalInformation;
        BackCommand = new RelayCommand(BackExecute);
        SignUpCommand = new RelayCommand(SignUpExecute, CanSignUp);
    }

    //private void Button_Click(object sender, RoutedEventArgs e)
    //{
    //    MessageBox.Show(ValueProgressBar.ToString());
    //}
    public bool CanSignUp(object? obj)
    {
        if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Repassword))
        {


            return true;
        }
        return false;
    }

    public void SignUpExecute(object? obj)
    {
        //Regex ile password qoy



        if (File.Exists(filePath_DataBaseFile))
        {
            string usernamePattern = @"^[a-z][a-z0-9]+$";
            Regex regex = new(usernamePattern);
            using FileStream fileRead1 = new(filePath_DataBaseFile, FileMode.Open, FileAccess.Read);
            using StreamReader reader1 = new StreamReader(fileRead1);

            bool let = true;

            while (true)
            {

                reader1.BaseStream.Seek(0, SeekOrigin.Begin);
                do
                {
                    string? line = reader1.ReadLine();
                    var data = line.Split(' ');
                    if (data[3].Equals(Username))
                    {
                        let = false;
                        break;
                    }
                } while (!reader1.EndOfStream);

                if (regex.IsMatch(Username!))
                {
                    if (let is false)
                    {
                        let = true;
                        ErrorUsername = "*This username already exists";
                        break;
                    }
                    else
                    {
                        ErrorUsername = null;
                        break;
                    }
                }
                else
                {
                    ErrorUsername = "*Wrong characters in username";
                    break;
                }
            }
            reader1.Close();

            string passwordPattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[a-zA-Z]).{8,}$";
            regex = new(passwordPattern);

            if (!regex.IsMatch(Password!) || Password!.Contains(' '))
                ErrorPassword = "*Password must be minimum 8 characters, at least 1 number";
            else
                ErrorPassword = null;
            if (!Password!.Equals(Repassword))
                ErrorRepassword = "*Password is not matching";
            else
                ErrorRepassword = null;

            //------------------------------------------

            //using FileStream fileRead = new(filePath_DataBaseFile, FileMode.Open, FileAccess.Read);
            //using StreamReader reader = new StreamReader(fileRead);

            //bool let = false;

            //while (true)
            //{
            //    reader.BaseStream.Seek(0, SeekOrigin.Begin);
            //    string[] data;

            //    do
            //    {

            //        string? line = reader.ReadLine();
            //        data = line.Split(' ');
            //        if (data[3].Equals(username) && data[5].Equals(password))
            //        {
            //            let = true;
            //            break;
            //        }
            //    } while (!reader.EndOfStream);

            //    if (let is true)
            //    {
            //        reader.Close();

            //        if (data[7].Equals("Worker"))
            //            WorkerAbility(username, data[1], data[3]);
            //        else if (data[7].Equals("Employer"))
            //            EmployerAbility(username, data[1], data[3]);
            //        else
            //            throw new KeyNotFoundException("Error in DataBaseFile");
            //    }
            //    else
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine("Username or Password is incorrect\n");
            //        Console.ForegroundColor = ConsoleColor.White;
            //        regex = new(usernamePattern);
            //        do
            //        {
            //            Console.Write("Username: ");
            //            username = Console.ReadLine();
            //        } while (!regex.IsMatch(username));

            //        regex = new(passwordPattern);
            //        do
            //        {
            //            Console.Write("Password: ");
            //            password = Console.ReadLine();
            //        } while (!regex.IsMatch(password) || password.Contains(' '));
        }
    }



    public void BackExecute(object? obj)
    {
        Hide();
        SignUpViewModel signUp = new(_personalInformation!);
        signUp.ShowDialog();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChange([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

