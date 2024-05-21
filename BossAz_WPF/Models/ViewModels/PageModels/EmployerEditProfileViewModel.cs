using BossAz_WPF.Models.DataBaseModels;
using BossAzWPF.Commands;
using BossAzWPF;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace BossAz_WPF.Models.ViewModels.PageModels;
public class EmployerEditProfileViewModel:BaseClass
{
    Vacancia? vacancia;

    private PersonalInformation? newPerson;
    public PersonalInformation? NewPerson { get => newPerson; set { newPerson = value; OnPropertyChange(); } }
    public PersonalInformation? ComparePerson { get; set; }
    public Vacancia? Vacancia { get => vacancia; set => vacancia = value; }

    string? errorName;
    string? errorSurname;
    string? errorBirthDate;
    string? errorPhone;
    public string? ErrorName { get => errorName; set { errorName = value; OnPropertyChange(); } }
    public string? ErrorSurname { get => errorSurname; set { errorSurname = value; OnPropertyChange(); } }
    public string? ErrorBirthDate { get => errorBirthDate; set { errorBirthDate = value; OnPropertyChange(); } }
    public string? ErrorPhone { get => errorPhone; set { errorPhone = value; OnPropertyChange(); } }

    string? errorProfession;
    string? errorSkills;
    string? errorCompaniesWorked;
    string? errorBeginingWorkTime;
    string? errorEndingWorkTime;

    public string? ErrorProfession { get => errorProfession; set => errorProfession = value; }
    public string? ErrorSkills { get => errorSkills; set => errorSkills = value; }
    public string? ErrorCompaniesWorked { get => errorCompaniesWorked; set => errorCompaniesWorked = value; }
    public string? ErrorBeginingWorkTime { get => errorBeginingWorkTime; set => errorBeginingWorkTime = value; }
    public string? ErrorEndingWorkTime { get => errorEndingWorkTime; set => errorEndingWorkTime = value; }
    public EmployerEditProfileViewModel()
    {
        NewPerson = App.Container.GetInstance<PersonalInformation>();
        OkProfileCommand = new RelayCommand(EditProfileExecute, CanProfileEdit);
        OkVacanciaCommand = new RelayCommand(EditVacanicaExecute, CanVacanicaEdit);
    }

    public ICommand OkProfileCommand { get; set; }
    public ICommand OkVacanciaCommand { get; set; }

    #region VacanciaCommand
    public bool CanVacanicaEdit(object? obj)
    {
        if (Vacancia is not null && !string.IsNullOrEmpty(Vacancia.Title) && !string.IsNullOrEmpty(Vacancia.Text))
            return true;
        return false;
    }
    public void EditVacanicaExecute(object? obj)
    {
        try
        {

            StringBuilder stringBuilder = new StringBuilder();
            using FileStream fileRead = new(App.filePath_DataBaseFileEmployers, FileMode.Open, FileAccess.Read);
            using StreamReader reader = new StreamReader(fileRead);
            do
            {
                string? line = reader.ReadLine();
                if (line is not null)
                {
                    var data = line.Split(' ');
                    if (NewPerson!.Id!.Equals(data[1]))
                        stringBuilder.AppendLine(new Employer(NewPerson, Vacancia).ToString());
                    else
                        stringBuilder.AppendLine(line);
                }
            } while (!reader.EndOfStream);
            fileRead.Close();
            reader.Close();
            using FileStream fileWrite = new(App.filePath_DataBaseFileEmployers, FileMode.Create, FileAccess.Write);
            using StreamWriter writer = new StreamWriter(fileWrite);
            writer.Write(stringBuilder);

            using FileStream fileWrite1 = new(App.filePath_DataBaseFileVacancia, FileMode.Append, FileAccess.Write);
            using StreamWriter writer1 = new StreamWriter(fileWrite1);
            writer1.WriteLine("Id: " + NewPerson!.Id + " " + Vacancia!.ToString2());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion
    #region ProfileCommand
    public bool CanProfileEdit(object? obj)
    {
        if (NewPerson is not null && !string.IsNullOrEmpty(NewPerson.Name) && !string.IsNullOrEmpty(NewPerson.Surname) && !string.IsNullOrEmpty(NewPerson.City)
            && !string.IsNullOrEmpty(NewPerson.City) && !string.IsNullOrEmpty(NewPerson.Phone) && (NewPerson.GenderMale == true || NewPerson.GenderFemale == true))
            return true;
        return false;
    }

    public void EditProfileExecute(object? obj)
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
        if (ErrorName is null && ErrorSurname is null && ErrorPhone is null && ErrorBirthDate is null)
        {
            try
            {
                if (ComparePerson!.CompareTo(NewPerson).Equals(0))
                    return;
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    using FileStream fileRead = new(App.filePath_DataBaseFileWorkers, FileMode.Open, FileAccess.Read);
                    using StreamReader reader = new StreamReader(fileRead);
                    do
                    {
                        string? line = reader.ReadLine();
                        if (line is not null)
                        {
                            var data = line.Split(' ');
                            if (NewPerson.Id!.Equals(data[1]))
                                stringBuilder.AppendLine(NewPerson.ToString() + (!string.IsNullOrEmpty(Vacancia!.Title) ? Vacancia.ToString2() : "CV: null"));
                            else
                                stringBuilder.AppendLine(line);
                        }
                    } while (!reader.EndOfStream);
                    reader.Close();
                    using FileStream fileWrite = new(App.filePath_DataBaseFileWorkers, FileMode.Create, FileAccess.Write);
                    using StreamWriter writer = new StreamWriter(fileWrite);
                    writer.Write(stringBuilder);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in WorkerEditProfileViewModel");
            }
        }
    }
    #endregion


}

