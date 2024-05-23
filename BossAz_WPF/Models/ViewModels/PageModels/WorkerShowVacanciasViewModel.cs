using BossAz_WPF.Models.DataBaseModels;
using BossAzWPF;
using BossAzWPF.Commands;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BossAz_WPF.Models.ViewModels.PageModels;
public class WorkerShowVacanciasViewModel : BaseClass
{
    public ICommand ApplyCommand { get; set; }
    List<Vacancia> vacancias = [];
    public List<Vacancia> Vacancias { get => vacancias; set { vacancias = value; OnPropertyChange(); } }

    List<string> ids = [];
    public List<string> Ids { get => ids; set => ids = value; }


    public WorkerShowVacanciasViewModel()
    {
        ApplyCommand = new RelayCommand(ApplyCv);
        LoadFile();
    }

    private void ApplyCv(object? obj)
    {
        string id = "null";
        if (obj is null)
            return;
        var apply = Vacancias.FirstOrDefault(vacancia => vacancia.Text == obj.ToString());
        if (apply is not null)
        {
            id = Ids[Vacancias.IndexOf(apply)];
            using FileStream fileWrite = new(App.filePath_DataBaseFileEmployerNotifications, FileMode.Append, FileAccess.Write);
            using StreamWriter writer = new StreamWriter(fileWrite);
            writer.WriteLine(App.Container.GetInstance<PersonalInformation>().Id + '*' + id);
        }
    }

    public void LoadFile()
    {
        using FileStream fileRead = new(App.filePath_DataBaseFileVacancia, FileMode.Open, FileAccess.Read);
        using StreamReader reader = new StreamReader(fileRead);
        do
        {
            string? line = reader.ReadLine();
            if (line is not null)
            {
                var data = line.Split(' ');
                Vacancia vacancia = new(data[3], data[5]);
                Vacancias.Add(vacancia);
                Ids.Add(data[1]);
            }
        } while (!reader.EndOfStream);

    }
}
