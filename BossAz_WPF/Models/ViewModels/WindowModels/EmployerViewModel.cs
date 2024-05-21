using BossAz_WPF.Models.ViewModels.PageModels;
using BossAz_WPF.Pages;
using BossAzWPF.Commands;
using BossAzWPF;
using System.Windows.Controls;
using System.Windows.Input;

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
        CurrentView.DataContext = App.Container.GetInstance<EmployerNotificationsViewModel>();
    }

    private void EditProfileExecute(object? obj)
    {
        CurrentView = App.Container.GetInstance<EmployerEditProfilePage>();
        CurrentView.DataContext = App.Container.GetInstance<EmployerEditProfileViewModel>();
    }

}
