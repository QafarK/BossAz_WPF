using BossAzWPF.Models;

namespace BossAz_WPF.Models;

public abstract class PersonalInformationAbstract
{
    string? _name;
    string? _surname;
    string? _city;
    string? _phone= "(+994) ";
    int _age;

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
            value = value!.Replace(' ', '-');
            _phone = value;
        }
    }
    public int Age { get => _age; set => _age = value; }
    protected PersonalInformationAbstract()
    {
        Id = Guid.NewGuid().ToString();

    }
    protected PersonalInformationAbstract(string? name, string? surname, string? city, string? phone, int age)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Surname = surname;
        City = city;
        Phone = phone;
        Age = age;
    }
}
