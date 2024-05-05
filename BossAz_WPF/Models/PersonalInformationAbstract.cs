using BossAzWPF.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace BossAz_WPF.Models;

public class PersonalInformationAbstract
{
    string? _name;
    string? _surname;
    string? _city;
    string? _phone = "(+994)";
    DateTime _birthDate=new(1900,1,1);
    bool _genderMale;
    bool _genderFemale;
    public string? Id { get; set; }
    public string? Name { get => _name; set => _name = value; }
    public string? Surname { get => _surname; set => _surname = value; }
    public string? City
    {
        get => _city;
        set
        {
            if (value.Contains(' '))
            {
                value = value.Replace(' ', '-');
                _city = value;
            }
            else
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

    public PersonalInformationAbstract()
    {
        Id = Guid.NewGuid().ToString();

    }
    protected PersonalInformationAbstract(string? name, string? surname, string? city, string? phone, DateTime birthDate, bool genderMale, bool genderFemale)
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
