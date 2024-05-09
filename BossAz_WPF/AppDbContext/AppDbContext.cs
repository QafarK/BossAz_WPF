using BossAz_WPF.Models;
using System.Collections.ObjectModel;

namespace BossAz_WPF.AppDBContext;

public class AppDbContext
{
    public ObservableCollection<PersonalInformation> Users = [];

}
