using System.Windows.Controls;

namespace BossAz_WPF.Models.DataBaseModels;

public class Employer : PersonalInformation
{

    Vacancia? _vacancia;
    public Vacancia? Vacancia { get => _vacancia; set => _vacancia = value; }
    public Employer(PersonalInformation personalInformation, Vacancia? vacancia = null) : base(personalInformation)
    {
        Vacancia = vacancia;
    }
    public Employer(string? name, string? surname, string? city, string? phone, DateTime age, bool genderMale, bool genderFemale, Vacancia? vacancia) : base(name, surname, city, phone, age, genderMale, genderFemale)
    {
        Vacancia = vacancia;
    }
    public override string ToString() => $"Id: {Id} Name: {Name} Surname: {Surname} City: {(City!.Contains(' ') ? City!.Split(' ')[1] : City)} Phone: {Phone!.Replace(' ', '-')} BirthDate: {BirthDate.ToString().Split(' ')[0]} Gender: {(GenderMale is true ? "Male" : "Female")} Vacancia: {(Vacancia is not null ? Vacancia.ToString2() : "null")}";
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
                _title = value;
        }
    }
    public string? Text
    {
        get => _text;
        set
        {
                _text = value;
        }
    }

    public Vacancia(string? title, string? text)
    {
        Title = title;
        Text = text;
    }
    public Vacancia()
    {
        
    }
    public override string ToString() => $"Title: {Title} Text: {Text}";
    public  string ToString2() => $"Title: {Title!.Replace(' ', '-')} Text: {Text!.Replace(' ', '-')}";

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