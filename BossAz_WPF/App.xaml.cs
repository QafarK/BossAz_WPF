using BossAz_WPF.AppDBContext;
using System.Windows;
using SimpleInjector;
using BossAz_WPF.Windows;
using BossAz_WPF.Models.ViewModels;
using System.IO;
using BossAz_WPF.Models.ViewModels.WindowModels;
using BossAz_WPF.Models.ViewModels.PageModels;
using BossAz_WPF.Pages;
using BossAz_WPF.Models.DataBaseModels;

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
        public static string filePath_DataBaseFileWorkers = Path.Combine(folderPath_DataBase, "DataBaseFileWorkers.txt");
        public static string filePath_DataBaseFileEmployers = Path.Combine(folderPath_DataBase, "DataBaseFileEmployers.txt");
        public static string filePath_DataBaseFileCv = Path.Combine(folderPath_DataBase, "Cv.txt");
        public static string filePath_DataBaseFileVacancia = Path.Combine(folderPath_DataBase, "Vacancia.txt");
        public static string filePath_DataBaseFileEmployerNotifications = Path.Combine(folderPath_DataBase, "EmployerNotifications.txt");
        public static string filePath_DataBaseFileWorkerNotifications = Path.Combine(folderPath_DataBase, "WorkerNotifications.txt");
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

            #region Worker
            Container.RegisterSingleton<WorkerViewModel>();
            Container.RegisterSingleton<WorkerEditProfileViewModel>();
            Container.RegisterSingleton<WorkerNotificationsViewModel>();
            Container.RegisterSingleton<WorkerShowVacanciasViewModel>();
            #endregion

            #region Employer
            Container.RegisterSingleton<EmployerViewModel>();
            Container.RegisterSingleton<EmployerEditProfileViewModel>();
            Container.RegisterSingleton<EmployerNotificationsViewModel>();
            Container.RegisterSingleton<EmployerApplyVacanciasViewModel>();
            #endregion
        }

        private void RegisterViews()
        {
            Container.RegisterSingleton<MainWindow>();
            Container.RegisterSingleton<SignUpWindow>();
            Container.RegisterSingleton<SignUpUsernameWindow>();

            #region Worker
            Container.RegisterSingleton<WorkerWindow>();
            Container.RegisterSingleton<WorkerEditProfilePage>();
            Container.RegisterSingleton<WorkerNotificationsPage>();
            Container.RegisterSingleton<WorkerShowVacanciasPage>();
            #endregion

            #region Employer
            Container.RegisterSingleton<EmployerWindow>();
            Container.RegisterSingleton<EmployerEditProfilePage>();
            Container.RegisterSingleton<EmployerNotificationsPage>();
            Container.RegisterSingleton<EmployerApplyVacanciasPage>();
            #endregion
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var view = Container.GetInstance<MainWindow>();
            view.DataContext = Container.GetInstance<MainViewModel>();
            //var view = Container.GetInstance<WorkerWindow>();
            //view.DataContext = Container.GetInstance<WorkerViewModel>();
            view.Show();

            base.OnStartup(e);

        }
    }

}
