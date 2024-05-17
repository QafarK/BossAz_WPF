using BossAz_WPF.Models.DataBaseModels;
using BossAz_WPF.Windows;
using BossAzWPF;
using BossAzWPF.Commands;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BossAz_WPF.Models.ViewModels;

public class SignUpViewModel : BaseClass
{
    string? _isWorkerOrEmployer;
    public string? IsWorkerOrEmployer
    {
        get => _isWorkerOrEmployer;
        set
        {
            if (value is not null)
            {
                value = value.Split(' ')[1];
                if (value.Equals("Worker") || value.Equals("Employer"))
                    _isWorkerOrEmployer = value;
                else
                    throw new KeyNotFoundException();
            }
        }
    }
    private PersonalInformation? newPerson;
    public PersonalInformation? NewPerson { get => newPerson; set { newPerson = value; OnPropertyChange(); } }

    string? errorName;
    string? errorSurname;
    string? errorBirthDate;
    string? errorPhone;
    public string? ErrorName { get => errorName; set { errorName = value; OnPropertyChange(); } }
    public string? ErrorSurname { get => errorSurname; set { errorSurname = value; OnPropertyChange(); } }
    public string? ErrorBirthDate { get => errorBirthDate; set { errorBirthDate = value; OnPropertyChange(); } }
    public string? ErrorPhone { get => errorPhone; set { errorPhone = value; OnPropertyChange(); } }

    public ICommand SignUpCommand { get; set; }

    public SignUpViewModel()
    {
        NewPerson = App.Container.GetInstance<PersonalInformation>();
        SignUpCommand = new RelayCommand(SignUpExecute, CanSignUp);
    }
    public bool CanSignUp(object? obj)
    {
        if (NewPerson is not null && !string.IsNullOrEmpty(NewPerson.Name) && !string.IsNullOrEmpty(NewPerson.Surname) && !string.IsNullOrEmpty(NewPerson.City)
            && !string.IsNullOrEmpty(NewPerson.City) && !string.IsNullOrEmpty(NewPerson.Phone) && !string.IsNullOrEmpty(IsWorkerOrEmployer) && (NewPerson.GenderMale == true || NewPerson.GenderFemale == true))
            return true;
        return false;
    }

    public void SignUpExecute(object? obj)
    {
        Regex regex;
        string pattern = @"^[A-Z][a-z]+$";
        regex = new(pattern);
        if (string.IsNullOrEmpty(NewPerson!.Name) || NewPerson.Name.Length < 3 || !regex.IsMatch(NewPerson.Name))
            ErrorName = "*Must enter name, for example: Ali";
        else
            ErrorName = null;
        if (string.IsNullOrEmpty(NewPerson!.Surname) || NewPerson.Surname.Length < 3 || !regex.IsMatch(NewPerson.Surname))
            ErrorSurname = "*Must enter surname, for example: Aliyev";
        else
            ErrorSurname = null;
        pattern = @"^\(\+994\) (50|51|55|70) \d{3} \d{2} \d{2}$";
        regex = new(pattern);
        if (!regex.IsMatch(NewPerson.Phone!))
            ErrorPhone = "*Phone number must be (+994) (50|51|55|70) XXX XX XX";
        else
            ErrorPhone = null;
        int age;
        if (DateTime.Now.Day - NewPerson.BirthDate.Day > 0 && DateTime.Now.Month == NewPerson.BirthDate.Month)
            age = DateTime.Now.Year - NewPerson.BirthDate.Year;
        else if (DateTime.Now.Month - NewPerson.BirthDate.Month >= 0)
            age = DateTime.Now.Year - NewPerson.BirthDate.Year;
        else
            age = DateTime.Now.Year - NewPerson.BirthDate.Year - 1;
        if (age < 18 || age > 65)
            ErrorBirthDate = "*Age range must be 18-65";
        else
            ErrorBirthDate = null;
        //------------------------------------------------------------------------------------------------
        //                                             EXECUTE
        if (ErrorName is null && ErrorSurname is null && ErrorPhone is null && ErrorBirthDate is null && IsWorkerOrEmployer is not null)
        {
            var currentView = App.Container.GetInstance<SignUpWindow>();
            currentView.Hide();

            var view = App.Container.GetInstance<SignUpUsernameWindow>();
            App.Container.GetInstance<SignUpUsernameViewModel>().PersonalInformation = NewPerson;
            App.Container.GetInstance<SignUpUsernameViewModel>().IsWorkerOrEmployer = IsWorkerOrEmployer;
            view.DataContext = App.Container.GetInstance<SignUpUsernameViewModel>();
            view.Show();
        }
    }
}
