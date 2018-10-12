﻿using Logic.Db.Dto;
using Logic.Db.Util.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ui.Main.Pages.Inscriptions
{
    /// <summary>
    /// Lógica de interacción para CompetitionSelectionPage.xaml
    /// </summary>
    public partial class CompetitionSelectionWindow : Window
    {
        private readonly CompetitionService _competitionService;
        private readonly AthleteDto _athlete;

        private CompetitionDto _competition;

        private List<long> _columnIds;

        public CompetitionSelectionWindow(AthleteDto athlete)
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            _athlete = athlete;

            TxNameSurname.Content = _athlete.Name + " " + _athlete.Surname;
            TxDni.Content = _athlete.Dni;
            TxBirthDate.Content = _athlete.BirthDate.ToShortDateString();
            TxGender.Content = _athlete.Gender.ToString();


            _competitionService = new CompetitionService();
            DataTable table = _competitionService.ListCompetitionsToInscribe();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Type;
            table.Columns[3].ColumnName = Properties.Resources.Competition_Km;
            table.Columns[4].ColumnName = Properties.Resources.Competition_Price;
            table.Columns[5].ColumnName = Properties.Resources.InscriptionOpen;
            table.Columns[6].ColumnName = Properties.Resources.InscriptionClose;
            table.Columns[7].ColumnName = Properties.Resources.Competition_Date;
            
            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();

            table.Columns.RemoveAt(0);

            CompetitionsToSelect.ItemsSource = table.DefaultView;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                ((DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void BtFinish_Click(object sender, RoutedEventArgs e)
        {
            if (CompetitionsToSelect.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.NothingSelected);
                return;
            }
            EnrollService enrollService = new EnrollService(_competition);
            enrollService.InsertAthleteInCompetition(_athlete, _competition);
            Content = new Frame()
            {
                Content = new InscriptionProofWindow(_athlete, _competition)
            };
        }

        private void CompetitionsToSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexSeletected = CompetitionsToSelect.SelectedIndex;
            
            _competition = new CompetitionDto()
            {
                ID = (int)_columnIds[indexSeletected]
            };
        }
    }
}