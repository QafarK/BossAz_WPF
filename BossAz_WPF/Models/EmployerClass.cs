using BossAz_WPF.Models;

namespace BossAzWPF.Models;

public class Employer:PersonalInformationAbstract
{

    Vacancia _vacancia;
    public Employer():base()
    {
        
    }
    public Employer(string? name, string? surname, string? city, string? phone, int age, Vacancia vacancia) : base(name, surname, city, phone, age)
    {
        Vacancia = vacancia;
    }
    public Vacancia Vacancia { get => _vacancia; set => _vacancia = value; }
    public override string ToString() => $"Id: {Id} Name: {Name} Surname: {Surname} City: {City}  Phone:  {Phone} Age: {Age} {Vacancia}";
}

public class Vacancia
{
    string? _title;
    string? _text;
    public string? Title
    {
        get => _title;
        set
        {
            if (value!.Contains(' '))
            {
                value = value.Replace(' ', '-');
                _title = value;
            }
            else
                _title = value;
        }
    }
    public string? Text
    {
        get => _text;
        set
        {
            if (value!.Contains(' '))
            {
                value = value.Replace(' ', '-');
                _text = value;
            }
            else
                _text = value;
        }
    }

    public Vacancia(string? title, string? text)
    {
        Title = title;
        Text = text;
    }
    public override string ToString() => $"Title: {Title} Text: {Text}";

}
record EmployerRCD
{
    string? _id;
    string? _name;
    string? _surname;
    string? _city;
    string? _phone;
    int _age;
    VacanciaRCD _vacancia;

    public string? Id { get => _id; set => _id = value; }
    public string? Name { get => _name; set => _name = value; }
    public string? Surname { get => _surname; set => _surname = value; }
    public string? City { get => _city; set => _city = value; }
    public string? Phone { get => _phone; set => _phone = value; }
    public int Age { get => _age; set => _age = value; }
    public VacanciaRCD Vacancia { get => _vacancia; set => _vacancia = value; }

    public EmployerRCD(string? id, string? name, string? surname, string? city, string? phone, int age, VacanciaRCD vacancia)
    {
        Id = id;
        Name = name;
        Surname = surname;
        City = city;
        Phone = phone;
        Age = age;

        Vacancia = vacancia;
    }
    public override string ToString() => $"Id: {_id} Name: {_name} Surname: {_surname} City: {_city} Phone: {_phone} Age: {_age} {Vacancia}";
    public string ToStringVS() => $"{Vacancia}";
    public string ToStringDropn() => $"Id: {_id}\nName: {_name}\nSurname: {_surname}\nCity: {_city}\nPhone: {_phone}\nAge: {_age}\n{Vacancia.ToStringDropn()}";
}

record VacanciaRCD
{
    string? _title;
    string? _text;
    public string? Title { get => _title; set => _title = value; }
    public string? Text { get => _text; set => _text = value; }
    public VacanciaRCD(string? title, string? text)
    {
        Title = title;
        Text = text;
    }
    public override string ToString() => $"Title: {Title} Text: {Text}";
    public string ToStringDropn() => $"Title: {Title}\nText: {Text}";
}