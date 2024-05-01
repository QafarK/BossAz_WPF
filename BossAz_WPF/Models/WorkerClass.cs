using BossAz_WPF.Models;

namespace BossAzWPF.Models;

public class Worker:PersonalInformationAbstract
{
    CV _cv;

    public CV Cv { get => _cv; set => _cv = value; }

    public Worker(string? name, string? surname, string? city, string? phone, int age, CV cv):base(name,surname,city,phone,age)
    {
        Cv = cv;
    }

    public override string ToString() => $"Id: {Id} Name: {Name} Surname: {Surname} City: {City} Phone: {Phone} Age: {Age} {Cv.ToString2()}";

    public string ToStringDropn() => $"Id: {Id}\n Name: {Name}\n Surname: {Surname}\n City: {City}\n Phone: {Phone}\n Age: {Age}\n {Cv.ToStringDropn()}";
}

public class CV
{
    string? _profession;
    int _school;
    int _universityPoint;
    string? _skills;
    string? _companiesWorked;
    string _beginingWorkTime;
    string _endingWorkTime;
    string? _languages;
    bool _redDegree;
    string? _githubLink;
    string? _linkedinLink;

    public string? Profession
    {
        get => _profession;
        set
        {
            if (value.Contains(' '))
            {
                value = value.Replace(' ', '-');
                _profession = value;
            }
            else
                _profession = value;
        }
    }
    public int School { get => _school; set => _school = value; }
    public int UniversityPoint { get => _universityPoint; set => _universityPoint = value; }
    public string? Skills
    {
        get => _skills;
        set
        {
            if (value.Contains(' '))
            {
                value = value.Replace(' ', '-');
                _skills = value;
            }
            else
                _skills = value;
        }
    }
    public string? CompaniesWorked
    {
        get => _companiesWorked;
        set
        {
            if (value.Contains(' '))
            {
                value = value.Replace(' ', '-');
                _companiesWorked = value;
            }
            else
                _companiesWorked = value;
        }
    }
    public string BeginingWorkTime { get => _beginingWorkTime; set => _beginingWorkTime = value; }
    public string EndingWorkTime { get => _endingWorkTime; set => _endingWorkTime = value; }
    public string? Languages
    {
        get => _languages;
        set
        {
            if (value.Contains(' '))
            {
                value = value.Replace(' ', '-');
                _languages = value;
            }
            else
                _languages = value;
        }
    }
    public string? GithubLink { get => _githubLink; set => _githubLink = value; }
    public string? LinkedinLink { get => _linkedinLink; set => _linkedinLink = value; }

    public CV(string? profession, int school, int universityPoint, string? skills, string? companiesWorked,
        string beginingWorkTime, string endingWorkTime,
        string? languages, bool redDegree, string? githubLink, string? linkedinLink)
    {
        Profession = profession;
        School = school;
        UniversityPoint = universityPoint;
        Skills = skills;
        CompaniesWorked = companiesWorked;
        BeginingWorkTime = beginingWorkTime;
        EndingWorkTime = endingWorkTime;
        Languages = languages;
        _redDegree = redDegree;
        GithubLink = githubLink;
        LinkedinLink = linkedinLink;
    }
    public override string ToString() => @$"Profession: {_profession} School: {_school} University Point: {_universityPoint} Skills: {_skills} Companies Worked: {_companiesWorked} Beginning Work Time: {_beginingWorkTime} Ending Work Time: {_endingWorkTime} Languages: {_languages} Red Degree: {_redDegree} Github Link: {_githubLink} Linkedin Link: {_linkedinLink}";
    public string ToString2() => @$"Profession: {_profession} School: {_school} University-Point: {_universityPoint} Skills: {_skills} Companies-Worked: {_companiesWorked} Beginning-Work-Time: {_beginingWorkTime} Ending-Work-Time: {_endingWorkTime} Languages: {_languages} Red-Degree: {_redDegree} Github-Link: {_githubLink} Linkedin-Link: {_linkedinLink}";
    public string ToStringDropn() => $"Profession: {_profession}\n School: {_school}\n University-Point: {_universityPoint}\n Skills: {_skills}\n Companies-Worked: {_companiesWorked}\n Beginning-Work-Time: {_beginingWorkTime}\n Ending-Work-Time: {_endingWorkTime}\n Languages: {_languages}\n Red-Degree: {_redDegree}\n Github-Link: {_githubLink}\n Linkedin-Link: {_linkedinLink}";

}

record WorkerRCD
{
    string? _id;
    string? _name;
    string? _surname;
    string? _city;
    string? _phone;
    int _age;
    CV_RCD _cv;

    public string? Id { get => _id; set => _id = value; }
    public string? Name { get => _name; set => _name = value; }
    public string? Surname { get => _surname; set => _surname = value; }
    public string? City { get => _city; set => _city = value; }
    public string? Phone { get => _phone; set => _phone = value; }
    public int Age { get => _age; set => _age = value; }
    public CV_RCD Cv { get => _cv; set => _cv = value; }

    public WorkerRCD(string? id, string? name, string? surname, string? city, string? phone, int age, CV_RCD cv)
    {
        Id = id;
        Name = name;
        Surname = surname;
        City = city;
        Phone = phone;
        Age = age;
        Cv = cv;
    }
    public override string ToString() => $"Id: {_id} Name: {_name} Surname: {_surname} City: {_city} Phone: {_phone} Age: {_age}";
    public string ToStringDropn() => $"Id: {_id}\nName: {_name}\nSurname: {_surname}\nCity: {_city}\nPhone: {_phone}\nAge: {_age}\n{Cv.ToStringDropn()}";
    public string ToStringVS() => $"Name: {_name}\nSurname: {_surname}\nCity: {_city}\nPhone: {_phone}\nAge: {_age}\n{Cv.ToStringDropn()}";
}

record CV_RCD
{
    string? _profession;
    int _school;
    int _universityPoint;
    string? _skills;
    string? _companiesWorked;
    string _beginingWorkTime;
    string _endingWorkTime;
    string? _languages;
    bool _redDegree;
    string? _githubLink;
    string? _linkedinLink;

    public string? Profession { get => _profession; set => _profession = value; }
    public int School { get => _school; set => _school = value; }
    public int UniversityPoint { get => _universityPoint; set => _universityPoint = value; }
    public string? Skills { get => _skills; set => _skills = value; }
    public string? CompaniesWorked { get => _companiesWorked; set => _companiesWorked = value; }
    public string BeginingWorkTime { get => _beginingWorkTime; set => _beginingWorkTime = value; }
    public string EndingWorkTime { get => _endingWorkTime; set => _endingWorkTime = value; }
    public string? Languages { get => _languages; set => _languages = value; }
    public bool RedDegree { get => _redDegree; set => _redDegree = value; }
    public string? GithubLink { get => _githubLink; set => _githubLink = value; }
    public string? LinkedinLink { get => _linkedinLink; set => _linkedinLink = value; }

    public CV_RCD(string? profession, int school, int universityPoint, string? skills, string? companiesWorked,
    string beginingWorkTime, string endingWorkTime, string? languages,
    bool redDegree, string? githubLink, string? linkedinLink)
    {
        Profession = profession;
        School = school;
        UniversityPoint = universityPoint;
        Skills = skills;
        CompaniesWorked = companiesWorked;
        BeginingWorkTime = beginingWorkTime;
        EndingWorkTime = endingWorkTime;
        Languages = languages;
        RedDegree = redDegree;
        GithubLink = githubLink;
        LinkedinLink = linkedinLink;
    }
    public override string ToString() => @$"Profession: {_profession} School: {_school} University Point: {_universityPoint} Skills: {_skills} Companies Worked: {_companiesWorked} Beginning Work Time: {_beginingWorkTime} Ending Work Time: {_endingWorkTime} Languages: {_languages} Red Degree: {_redDegree} Github Link: {_githubLink} Linkedin Link: {_linkedinLink}";
    public string ToStringDropn() => $"Profession: {_profession}\nSchool: {_school}\nUniversity-Point: {_universityPoint}\nSkills: {_skills}\nCompanies-Worked: {_companiesWorked}\nBeginning-Work-Time: {_beginingWorkTime}\nEnding-Work-Time: {_endingWorkTime}\nLanguages: {_languages}\nRed-Degree: {_redDegree}\nGithub-Link: {_githubLink}\nLinkedin-Link: {_linkedinLink}";

}