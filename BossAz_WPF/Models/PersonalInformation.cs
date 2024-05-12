namespace BossAz_WPF.Models;

public class PersonalInformation
{
    string? _name;
    string? _surname;
    string? _city;
    string? _phone;
    DateTime _birthDate=new(DateTime.Now.Year,1,1);
    bool _genderMale=true;
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
