using BossAz_WPF.AppDBContext;
using System.Windows;
using SimpleInjector;
using BossAz_WPF.Windows;
using BossAz_WPF.Models.ViewModels;
using BossAz_WPF.Models;
using System.IO;
using BossAz_WPF.Models.ViewModels.WindowModels;

namespace BossAzWPF
{
    public partial class App : Application
    {
        #region FILES
        // FOLDERS
        public static string folderPath_DataBase = Path.Combine("..\\..\\..\\", "DataBase");
        public static string folderPath_ModelsConvertedFile = Path.Combine("..\\..\\..\\", "ModelsConvertedFile");
        public static string folderPath_JSON = Path.Combine("..\\..\\..\\", "JSON");
        public static string folderPath_Notification = Path.Combine("..\\..\\..\\", "Notification");

        //FILES & JSON
        public static string filePath_DataBaseFile = Path.Combine(folderPath_DataBase, "DataBaseFile.txt");
        public static string jsonPath_DataBase = Path.Combine(folderPath_DataBase, "DataBase.json");
        public static string filePath_WorkerClassInfos = Path.Combine(folderPath_ModelsConvertedFile, "WorkerClassInfos.txt");
        public static string filePath_EmployerClassInfos = Path.Combine(folderPath_ModelsConvertedFile, "EmployerClassInfos.txt");
        public static string jsonPath_UsersDataAboutWorker = Path.Combine(folderPath_JSON, "UsersDataAboutWorker.json");
        public static string jsonPath_UsersDataAboutEmployer = Path.Combine(folderPath_JSON, "UsersDataAboutEmployer.json");
        public static string filePath_Notification = Path.Combine(folderPath_Notification, "Notifcation.txt");
        #endregion
        public static Container Container { get; set; } = new Container();
        public App()
        {
            Container.RegisterSingleton<AppDbContext>();
            RegisterViews();
            RegisterViewModels();
            RegisterModels();

        }

        private void RegisterModels()
        {
            Container.RegisterSingleton<PersonalInformation>();
        }
        private void RegisterViewModels()
        {
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<SignUpViewModel>();
            Container.RegisterSingleton<SignUpUsernameViewModel>();
            Container.RegisterSingleton<WorkerViewModel>();
        }

        private void RegisterViews()
        {
            Container.RegisterSingleton<MainWindow>();
            Container.RegisterSingleton<SignUpWindow>();
            Container.RegisterSingleton<SignUpUsernameWindow>();
            Container.RegisterSingleton<WorkerWindow>();
            Container.RegisterSingleton<EmployerWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var view = Container.GetInstance<MainWindow>();
            view.DataContext = Container.GetInstance<MainViewModel>();
            view.Show();

            base.OnStartup(e);
        }
    }

}
