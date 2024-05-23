using BossAz_WPF.Models.DataBaseModels;
using BossAzWPF;
using BossAzWPF.Commands;
using System.Diagnostics.Metrics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BossAz_WPF.Models.ViewModels.PageModels;
public class EmployerApplyVacanciasViewModel : BaseClass
{
    List<string> notifications = [];
    public List<string> Notifications { get => notifications; set { notifications = value; OnPropertyChange(); } }
    List<CV> cv = [];
    public List<CV> Cv { get => cv; set { cv = value; OnPropertyChange(); } }

    List<string> ids = [];
    public List<string> Ids { get => ids; set => ids = value; }
    public ICommand AcceptCommand { get; set; }

    public EmployerApplyVacanciasViewModel()
    {
        AcceptCommand = new RelayCommand(AcceptCv);
        LoadFiles();
    }



    private void AcceptCv(object? obj)
    {
        if (obj is null)
            return;
        var _cv = Cv.FirstOrDefault(cv => cv.CompaniesWorked == obj.ToString());
        if (_cv is not null)
        {
            using FileStream fileWrite = new(App.filePath_DataBaseFileWorkerNotifications, FileMode.Append, FileAccess.Write);
            using StreamWriter writer = new StreamWriter(fileWrite);
            writer.WriteLine(App.Container.GetInstance<PersonalInformation>().Id + '*' + Ids[Cv.IndexOf(_cv)] + '*');
        }

    }

    private void LoadFiles()
    {
        using FileStream fileRead = new(App.filePath_DataBaseFileEmployerNotifications, FileMode.Open, FileAccess.Read);
        using StreamReader reader = new StreamReader(fileRead);

        do
        {
            string? line = reader.ReadLine();
            if (line is not null)
                Notifications.Add(line);
        } while (!reader.EndOfStream);

        using FileStream fileRead1 = new(App.filePath_DataBaseFileCv, FileMode.Open, FileAccess.Read);
        using StreamReader reader1 = new StreamReader(fileRead1);
        do
        {
            string? line = reader1.ReadLine();
            if (line is not null)
            {
                var data = line.Split(' ');
                Cv.Add(new(data[3], data[5], data[7], new(Convert.ToInt32(data[9].Split('/')[2]), Convert.ToInt32(data[9].Split('/')[0]), Convert.ToInt32(data[9].Split('/')[1])), new(Convert.ToInt32(data[11].Split('/')[2]), Convert.ToInt32(data[11].Split('/')[0]), Convert.ToInt32(data[11].Split('/')[1]))));
                Ids.Add(data[1]);
            }
        } while (!reader.EndOfStream);
    }
}
