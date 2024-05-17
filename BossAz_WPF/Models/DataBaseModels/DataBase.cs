using System.Windows;

namespace BossAz_WPF.Models.DataBaseModels;

public class DataBase
{
    string? _id;
    string? _username;
    string? _password;
    string? _isWorkerOrEmployer;
    public string? Id { get => _id; set => _id = value; }
    public string? Username { get => _username; set => _username = value; }
    public string? Password { get => _password; set => _password = value; }
    public string? IsWorkerOrEmployer { get => _isWorkerOrEmployer; set => _isWorkerOrEmployer = value; }
    public DataBase(string? id, string? username, string? password, string? isWorkerOrEmployer)
    {
        Id = id;
        Username = username;
        Password = password;
        if (isWorkerOrEmployer is not null && (isWorkerOrEmployer.Equals("Worker") || isWorkerOrEmployer.Equals("Employer")))
            IsWorkerOrEmployer = isWorkerOrEmployer;
        else
        {
            MessageBox.Show("Error: IsWorkerOrEmployer in DataBase");
            throw new KeyNotFoundException();
        }

    }
    //public DataBase(string? id, string? username, string? password, string? isWorkerOrEmployer, Vacancia? vacancia)
    //{
    //    Id = id;
    //    Username = username;
    //    Password = password;
    //    Vacancia = vacancia;
    //    if (isWorkerOrEmployer is not null && (isWorkerOrEmployer.Equals("Worker") || isWorkerOrEmployer.Equals("Employer")))
    //        IsWorkerOrEmployer = isWorkerOrEmployer;
    //    else
    //        throw new KeyNotFoundException("Must be Worker or Employer");
    //}

    //public DataBase(string? id, string? username, string? password, string? isWorkerOrEmployer, CV? cv)
    //{
    //    Id = id;
    //    Username = username;
    //    Password = password;
    //    Cv = cv;
    //    if (isWorkerOrEmployer is not null && (isWorkerOrEmployer.Equals("Worker") || isWorkerOrEmployer.Equals("Employer")))
    //        IsWorkerOrEmployer = isWorkerOrEmployer;
    //    else
    //        throw new KeyNotFoundException("Must be Worker or Employer");
    //}
    public override string ToString() => $"Id: {Id} Username: {Username} Password: {Password} IsWorkerOrEmployer: {IsWorkerOrEmployer}";
}