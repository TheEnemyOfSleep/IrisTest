using IrisWpfApp1.Models;
using IrisWpfApp1.ViewModels;
using IrisWpfApp1.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IrisWpfApp1.Services;
using IrisWpfApp1.DbContexts;
using Microsoft.EntityFrameworkCore;
using IrisWpfApp1.Services.SensorProviders;
using IrisWpfApp1.Services.SensorCreators;
using IrisWpfApp1.Services.SensorEditor;
using IrisWpfApp1.Services.SensorConflictValidators;
using IrisWpfApp1.Services.SensorRemover;
using IrisWpfApp1.Services.StateProvider;
using IrisWpfApp1.Services.SignalProvider;
using IrisWpfApp1.Services.SignalCreator;
using IrisWpfApp1.Services.SignalConflictValidator;
using IrisWpfApp1.Services.SignalRemover;
using IrisWpfApp1.Services.SignalEditor;
using IrisWpfApp1.Services.HistoryProvider;
using IrisWpfApp1.Services.HistoryCreator;

namespace IrisWpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly string _connectionString;
        private readonly NavigationStore _navigationSensorStore;
        private readonly NavigationStore _navigationSignalStore;
        private readonly MainDbContextFactory _mainDbContextFactory;

        private readonly Sensors _sensors;
        private readonly Signals _signals;
        private readonly Stories _stories;

        private readonly SensorsStore _sensorsStore;
        private readonly SignalsStore _signalsStore;
        private readonly HistoryStore _historyStore;

        public App()
        {
            _connectionString = ConfigurationManager.AppSettings["connectionString"];

            _mainDbContextFactory = new MainDbContextFactory(_connectionString);
            //Sensor Providers
            ISensorProvider sensorProvider = new DatabaseSensorProvider(_mainDbContextFactory);
            IStateProvider stateProvider = new DatabaseStateProvider(_mainDbContextFactory);
            ISensorCreator sensorCreator = new DatabaseSensorCreator(_mainDbContextFactory);
            ISensorEditor sensorEditor = new DatabaseSensorEditor(_mainDbContextFactory);
            ISensorRemover sensorRemover = new DatabaseSensorRemover(_mainDbContextFactory);
            ISensorConflictValidator sensorValidator = new DatabaseSensorConflictValidator(_mainDbContextFactory);

            //Signal Providers
            ISignalProvider signalProvider = new DatabaseSignalProvider(_mainDbContextFactory);
            ISignalCreator signalCreator = new DatabaseSignalCreator(_mainDbContextFactory);
            ISignalEditor signalEditor = new DatabaseSignalEditor(_mainDbContextFactory);
            ISignalRemover signalRemover = new DatabaseSignalRemover(_mainDbContextFactory);
            ISignalConflictValidator signalValidator = new DatabaseSignalConflictValidator(_mainDbContextFactory);

            //History Providers
            IHistoryProvider historyProvider = new DatabaseHistoryProvider(_mainDbContextFactory);
            IHistoryCreator historyCreator = new DatabaseHistoryCreator(_mainDbContextFactory);

            _sensors = new Sensors(sensorProvider, stateProvider, sensorCreator, sensorEditor, sensorRemover, sensorValidator);
            _signals = new Signals(signalProvider, signalCreator, signalEditor, signalRemover, signalValidator, sensorProvider, sensorValidator);
            _stories = new Stories(historyProvider, historyCreator);

            _navigationSensorStore = new NavigationStore();
            _navigationSignalStore = new NavigationStore();

            _sensorsStore = new SensorsStore(_sensors);
            _signalsStore = new SignalsStore(_signals);
            _historyStore = new HistoryStore(_stories);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseNpgsql(_connectionString).Options;

            using (DbContexts.MainDbContext dbContext = new DbContexts.MainDbContext(options))
            {
                RelationalDatabaseFacadeExtensions.Migrate(dbContext.Database);
            }

            _navigationSensorStore.CurrentViewModel = SensorViewModel();
            _navigationSignalStore.CurrentViewModel = SignalViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationSensorStore, _navigationSignalStore, HistoryListingViewModel.LoadViewModel(_historyStore))
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private AddSensorViewModel AddSensorViewModelMethod()
        {
            return AddSensorViewModel.LoadViewModel(_sensorsStore, _historyStore, new NavigationService(_navigationSensorStore, SensorViewModel));
        }

        private EditSensorViewModel EditSensorViewModelMetohd()
        {
            return EditSensorViewModel.LoadViewModel(_sensorsStore, _historyStore, new NavigationService(_navigationSensorStore, SensorViewModel));
        }

        private SensorsListingViewModel SensorViewModel()
        {
            return SensorsListingViewModel.LoadViewModel(_sensorsStore,
                                                         _signalsStore,
                                                         _historyStore,
                                                         new NavigationService(_navigationSensorStore, AddSensorViewModelMethod),
                                                         new NavigationService(_navigationSensorStore, EditSensorViewModelMetohd));
        }

        private CreateSignalViewModel CreateSignalViewModelMethod()
        {
            return new CreateSignalViewModel(_signalsStore, new NavigationService(_navigationSignalStore, SignalViewModel));
        }

        private EditSignalViewModel EditSignalViewModelMethod()
        {
            return new EditSignalViewModel(_signalsStore, new NavigationService(_navigationSignalStore, SignalViewModel));
        }

        private SignalsListingViewModel SignalViewModel()
        {
            return SignalsListingViewModel.LoadViewModel(_signalsStore,
                                                         new NavigationService(_navigationSignalStore, CreateSignalViewModelMethod),
                                                         new NavigationService(_navigationSignalStore, EditSignalViewModelMethod));
        }
    }
}
