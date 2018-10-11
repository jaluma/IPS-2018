﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ui.Main.Pages.Competition.Times;
using Ui.Main.Pages.Inscriptions.HasRegistered;
using Ui.Main.Pages.OpenCompetitions;

namespace Ui.Main.Pages.MenuInitial {
    /// <summary>
    /// Lógica de interacción para MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page {
        public MainMenu() {
            InitializeComponent();
        }

        private void TileAthletes_Click(object sender, System.Windows.RoutedEventArgs e) {

            //NavigationService?.Navigate(new ...);
        }

        private void TileCompetition_Click(object sender, System.Windows.RoutedEventArgs e) {
            //Content = new Frame()
            //{
            //    Content = new ListOpenCompetition()
            //};
            ChangeMenuSelected(Properties.Resources.TileCompetition, Properties.Resources.CategoryMenuActive);
        }

        private void TileCompetitionFinish_Click(object sender, System.Windows.RoutedEventArgs e) {
            //Content = new Frame() {
            //    Content = new SelectionCompetition()
            //};
            ChangeMenuSelected(Properties.Resources.TileCompetition, Properties.Resources.CategoryMenuFinish);
        }

        private void TileInscriptionDorsal_Click(object sender, System.Windows.RoutedEventArgs e) {
            //Content = new Frame() {
            //    Content = new AddDorsalsAndRegisteredInCompetition()
            //};
            ChangeMenuSelected(Properties.Resources.TileInscriptionDorsal, Properties.Resources.TileInscriptionDorsal);
        }

        public static void ChangeMenuSelected(string menuName, string subMenuName) {
            var mainWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) as MainWindow;

            if (mainWindow != null) {
                var linkMenu = mainWindow.PrincipalWindow.MenuLinkGroups
                    .First(m => m.DisplayName.Equals(menuName)).Links;
                var linkSubMenu = linkMenu.First(l => l.DisplayName.Equals(subMenuName));
                mainWindow.PrincipalWindow.ContentSource = linkSubMenu.Source;
            }
        }

        
    }
}
