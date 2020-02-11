using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Domain.Gtfs.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Windows;

namespace Chilicki.Ptsa.Search.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppSettings appSettings;
        private GtfsImportService importService;

        public MainWindow(
            IOptions<AppSettings> appSettings,
            GtfsImportService importService)
        {
            InitializeComponent();
            this.appSettings = appSettings.Value;
            this.importService = importService;
        }

        private void ImportGtfs1_Click(object sender, RoutedEventArgs e)
        {
            var gtfsFolderPath = appSettings.ImportGtfsPath1;
            importService.ImportGtfs(gtfsFolderPath);
        }

        private void ImportGtfs2_Click(object sender, RoutedEventArgs e)
        {
            var gtfsFolderPath = appSettings.ImportGtfsPath2;
            importService.ImportGtfs(gtfsFolderPath);
        }
    }
}
