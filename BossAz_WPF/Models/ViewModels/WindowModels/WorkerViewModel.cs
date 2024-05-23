using BossAz_WPF.AppDBContext;
using BossAz_WPF.Models.DataBaseModels;
using BossAz_WPF.Models.ViewModels.PageModels;
using BossAz_WPF.Pages;
using BossAzWPF;
using BossAzWPF.Commands;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace BossAz_WPF.Models.ViewModels.WindowModels;

public class WorkerViewModel : BaseClass
{
    private Page? currentView;
    //public int MyProperty { get; set { currentView.Height}; }
    //private int _width = 800;
    //private int _height = 450;
    //public int Width { get => _width; set { _width = value; OnPropertyChange(); } }
    //public int Height { get => _height; set { _height = value; OnPropertyChange(); } }
    public Page? CurrentView
    {
        get { return currentView; }
        set { currentView = value; OnPropertyChange(); }
    }
    public ICommand VacanciasCommand { get; set; }
    public ICommand NotificationsCommand { get; set; }
    public ICommand EditProfileCommand { get; set; }

    ObservableCollection<string> notifications = [];
    public ObservableCollection<string> Notifications { get => notifications; set => notifications = value; }
    public WorkerViewModel()
    {
        

        VacanciasCommand = new RelayCommand(VacanciasExecute);
        NotificationsCommand = new RelayCommand(NotificationsExecute);
        EditProfileCommand = new RelayCommand(EditProfileExecute);
    }
    private void VacanciasExecute(object? obj)
    {
        CurrentView = App.Container.GetInstance<WorkerShowVacanciasPage>();
        CurrentView.DataContext = App.Container.GetInstance<WorkerShowVacanciasViewModel>();
    }
    private void NotificationsExecute(object? obj)
    {
        CurrentView = App.Container.GetInstance<WorkerNotificationsPage>();
        CurrentView.DataContext = App.Container.GetInstance<WorkerViewModel>();

        using FileStream fileRead = new(App.filePath_DataBaseFileWorkerNotifications, FileMode.Open, FileAccess.Read);
        using StreamReader reader = new StreamReader(fileRead);
        do
        {
            string? line = reader.ReadLine();
            if (line is not null)
            {
                var data = line.Split('*');
                if (data[1].Equals(App.Container.GetInstance<PersonalInformation>().Id))
                {
                    var employer = AppDbContext.Users.FirstOrDefault(x => x.Split(' ')[1] == data[0]);
                    if (!Notifications.Contains(employer))
                        Notifications.Add(employer.Split(' ')[3] + " accepted your apply");
                }
            }
        } while (!reader.EndOfStream);
    }

    private void EditProfileExecute(object? obj)
    {
        CurrentView = App.Container.GetInstance<WorkerEditProfilePage>();
        CurrentView.DataContext = App.Container.GetInstance<WorkerEditProfileViewModel>();
    }



}
