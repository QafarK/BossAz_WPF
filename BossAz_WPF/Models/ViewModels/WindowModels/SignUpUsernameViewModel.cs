﻿using BossAz_WPF.Models.DataBaseModels;
using BossAz_WPF.Windows;
using BossAzWPF;
using BossAzWPF.Commands;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BossAz_WPF.Models.ViewModels;

public class SignUpUsernameViewModel : BaseClass
{
    PersonalInformation? _personalInformation;
    string? _colorProgressBar;
    int _valueProgressBar;
    string? _username;
    string? _password;
    string? _repassword;
    string? _isWorkerOrEmployer;
    public string? IsWorkerOrEmployer
    {
        get => _isWorkerOrEmployer;
        set
        {
            if (value is not null && (value.Equals("Worker") || value.Equals("Employer")))
                _isWorkerOrEmployer = value;
            else
            {
                MessageBox.Show("Error: IsWorkerOrEmployer in SignUpUsernameViewModel");
                throw new KeyNotFoundException();
            }
        }
    }
    public string? ColorProgressBar { get => _colorProgressBar; set { _colorProgressBar = value; OnPropertyChange(); } }
    public int ValueProgressBar { get => _valueProgressBar; set { _valueProgressBar = value; OnPropertyChange(); } }
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
    public PersonalInformation? PersonalInformation { get => _personalInformation; set => _personalInformation = value; }

    public ICommand? SignUpCommand { get; set; }
    public ICommand? BackCommand { get; set; }


    public SignUpUsernameViewModel()
    {
        BackCommand = new RelayCommand(BackExecute);
        SignUpCommand = new RelayCommand(SignUpExecute, CanSignUp);
    }
    public bool CanSignUp(object? obj)
    {
        if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Repassword))
            return true;
        return false;
    }
    public void BackExecute(object? obj)
    {
        var thisView = App.Container.GetInstance<SignUpUsernameWindow>();
        thisView.Hide();

        var view = App.Container.GetInstance<SignUpWindow>();
        App.Container.GetInstance<SignUpViewModel>().NewPerson = PersonalInformation;
        view.DataContext = App.Container.GetInstance<SignUpViewModel>();
        view.Show();
    }
    public void SignUpExecute(object? obj)
    {
        Regex regex;
        try
        {
            if (File.Exists(App.filePath_DataBaseFile) && App.filePath_DataBaseFile.Length > 0)
            {
                string usernamePattern = @"^[a-z][a-z0-9]+$";
                regex = new(usernamePattern);
                using FileStream fileRead = new(App.filePath_DataBaseFile, FileMode.Open, FileAccess.Read);
                using StreamReader reader = new StreamReader(fileRead);

                bool let = true;

                while (true)
                {

                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    do
                    {
                        string? line = reader.ReadLine();
                        if (line is not null)
                        {
                            var data = line.Split(' ');
                            if (data[3].Equals(Username))
                            {
                                let = false;
                                break;
                            }
                        }
                        else
                            break;
                    } while (!reader.EndOfStream);

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
                reader.Close();
            }

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

            //-------------------------------------------------------
            //                      EXECUTE


            if (ErrorUsername is null && ErrorPassword is null && ErrorRepassword is null && IsWorkerOrEmployer is not null)
            {
                #region DataBase_To_JSONSerialize
                List<DataBase> DBlist = [];

                using FileStream fileReadDB = new(App.filePath_DataBaseFile, FileMode.Open, FileAccess.Read);
                using StreamReader readerDB = new StreamReader(fileReadDB);
                do
                {
                    string? line = readerDB.ReadLine();
                    if (line is not null)
                    {
                        var data = line.Split(' ');
                        DataBase dataBaseValue = new(data[1], data[3], data[5], data[7]);
                        DBlist.Add(dataBaseValue);
                    }
                    else
                        break;
                } while (!readerDB.EndOfStream);
                readerDB.Close();

                DataBase signUpUser = new(PersonalInformation!.Id, Username, Password, IsWorkerOrEmployer!);
                DBlist.Add(signUpUser);

                string json = JsonConvert.SerializeObject(DBlist, Formatting.Indented);
                File.WriteAllText(App.jsonPath_DataBase, json);
                #endregion

                #region NewUser_To_DataBaseFile
                using FileStream fileWriteDB = new(App.filePath_DataBaseFile, FileMode.Append, FileAccess.Write);
                using StreamWriter writerDB = new StreamWriter(fileWriteDB);
                writerDB.WriteLine(signUpUser.ToString());
                #endregion


                if (signUpUser.IsWorkerOrEmployer!.Equals("Worker"))
                {
                    using FileStream fileWriteDB1 = new(App.filePath_DataBaseFileWorkers, FileMode.Append, FileAccess.Write);
                    using StreamWriter writerDB1 = new StreamWriter(fileWriteDB1);
                    Worker worker = new(PersonalInformation, null);
                    writerDB1.WriteLine(worker.ToString());
                }
                else if (signUpUser.IsWorkerOrEmployer!.Equals("Employer"))
                {
                    using FileStream fileWriteDB1 = new(App.filePath_DataBaseFileEmployers, FileMode.Append, FileAccess.Write);
                    using StreamWriter writerDB1 = new StreamWriter(fileWriteDB1);
                    Employer employer = new(PersonalInformation, null);
                    writerDB1.WriteLine(employer.ToString());
                }



                MessageBox.Show("Account has been successfully created.");
                App.Container.GetInstance<SignUpUsernameWindow>().Hide();
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }



}