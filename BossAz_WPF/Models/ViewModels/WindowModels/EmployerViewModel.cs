using BossAz_WPF.Models.ViewModels.PageModels;
using BossAz_WPF.Pages;
using BossAzWPF.Commands;
using BossAzWPF;
using System.Windows.Controls;
using System.Windows.Input;
using BossAz_WPF.AppDBContext;
using BossAz_WPF.Models.DataBaseModels;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;

namespace BossAz_WPF.Models.ViewModels.WindowModels;

public class EmployerViewModel : BaseClass
{
    private Page? currentView;
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

    public EmployerViewModel()
    {
        VacanciasCommand = new RelayCommand(VacanciasExecute);
        NotificationsCommand = new RelayCommand(NotificationsExecute);
        EditProfileCommand = new RelayCommand(EditProfileExecute);
    }
    private void VacanciasExecute(object? obj)
    {
        CurrentView = App.Container.GetInstance<EmployerApplyVacanciasPage>();
        CurrentView.DataContext = App.Container.GetInstance<EmployerApplyVacanciasViewModel>();
    }
    private void NotificationsExecute(object? obj)
    {
        CurrentView = App.Container.GetInstance<EmployerNotificationsPage>();
        CurrentView.DataContext = App.Container.GetInstance<EmployerViewModel>();
        using FileStream fileRead = new(App.filePath_DataBaseFileEmployerNotifications, FileMode.Open, FileAccess.Read);
        using StreamReader reader = new StreamReader(fileRead);
        do
        {
            string? line = reader.ReadLine();
            if (line is not null)
            {
                var data = line.Split('*');
                if (data[1].Equals(App.Container.GetInstance<PersonalInformation>().Id))
                {
                    var worker = AppDbContext.Users.FirstOrDefault(x => x.Split(' ')[1] == data[0]);
                    if (!Notifications.Contains(worker))
                        Notifications.Add(worker.Split(' ')[3] + " accepted your apply");
                }
                MessageBox.Show(data[1] + App.Container.GetInstance<PersonalInformation>().Id);
            }
        } while (!reader.EndOfStream);
    }

    private void EditProfileExecute(object? obj)
    {
        CurrentView = App.Container.GetInstance<EmployerEditProfilePage>();
        CurrentView.DataContext = App.Container.GetInstance<EmployerEditProfileViewModel>();
    }

}
