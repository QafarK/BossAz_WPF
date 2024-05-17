using System.Xml.Linq;

namespace BossAz_WPF.Models.DataBaseModels;

public class PersonalInformation
{
    string? _name = "Qafar";
    string? _surname = "Kerimov";
    string? _city;
    string? _phone = "(+994) 50 555 55 55";
    DateTime _birthDate = new(DateTime.Now.Year - 20, 1, 1);
    bool _genderMale = true;
    bool _genderFemale;
    public string? Id { get; set; }
    public string? Name { get => _name; set => _name = value; }
    public string? Surname { get => _surname; set => _surname = value; }
    public string? City
    {
        get => _city;
        set
        {
            //if (value.Contains(' '))
            //{
            //    value = value.Replace(' ', '-');
            //    _city = value;
            //}
            //else
            _city = value;
        }
    }
    public string? Phone
    {
        get => _phone;
        set
        {
            //value = value!.Replace(' ', '-');
            _phone = value;
        }
    }
    public DateTime BirthDate { get => _birthDate; set => _birthDate = value; }
    public bool GenderMale { get => _genderMale; set { _genderMale = value; } }
    public bool GenderFemale { get => _genderFemale; set => _genderFemale = value; }

    public PersonalInformation()
    {
        Id = Guid.NewGuid().ToString();
        // Ele yaz ki cvni de data base elave ede bilek 
    }
    protected PersonalInformation(PersonalInformation personalInformation)
    {
        Id = personalInformation.Id;
        Name = personalInformation.Name;
        Surname = personalInformation.Surname;
        City = personalInformation.City;
        Phone = personalInformation.Phone;
        BirthDate = personalInformation.BirthDate;
        GenderMale = personalInformation.GenderMale;
        GenderFemale = personalInformation.GenderFemale;
    }
    protected PersonalInformation(string? name, string? surname, string? city, string? phone, DateTime birthDate, bool genderMale, bool genderFemale)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Surname = surname;
        City = city;
        Phone = phone;
        BirthDate = birthDate;
        GenderMale = genderMale;
        GenderFemale = genderFemale;
    }



}
