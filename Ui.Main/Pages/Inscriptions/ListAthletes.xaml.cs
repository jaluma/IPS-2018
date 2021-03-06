﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using System.Windows.Input;
using Logic.Db.Dto;
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Inscriptions
{
    /// <summary>
    ///     Lógica de interacción para ListAthletes.xaml
    /// </summary>
    public partial class ListAthletes : Page
    {
        private CompetitionDto _competition;
        private readonly CompetitionService _competitionService;
        private EnrollService _enroll;
        private readonly List<int> _ids;
        private DataTable _table;

        public ListAthletes() {
            InitializeComponent();

            _competitionService = new CompetitionService();
            _table = _competitionService.SelectAllCompetitions();

            var index = _table.Columns.IndexOf("Competition_Name");
            var list = new List<string>();
            _ids = new List<int>();

            foreach (DataRow row in _table.Rows) {
                _ids.Add(int.Parse(row[_table.Columns.IndexOf("Competition_ID")].ToString()));
                list.Add(row[_table.Columns.IndexOf("Competition_Name")].ToString());
            }

            CompetitionList.ItemsSource = list;

            if (list.Count > 0)
                CompetitionList.SelectedIndex = 0;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(DateTime))
                ((DataGridTextColumn) e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void GenerateTable() {
            _enroll = new EnrollService(_competition);
            _table = _enroll.SelectAthlete();

            _table.Columns[0].ColumnName = Properties.Resources.AthleteDni;
            _table.Columns[1].ColumnName = Properties.Resources.Competition_Id;
            _table.Columns[2].ColumnName = Properties.Resources.Competition_Status;
            //_table.Columns[3].ColumnName = Properties.Resources.Competition_Date;
            _table.Columns[3].ColumnName = Properties.Resources.AthleteDorsal;

            _table.Columns.Remove(Properties.Resources.Competition_Id);

            DataGridCompetition.ItemsSource = _table.DefaultView;
        }

        private void CompetitionList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            _competition = new CompetitionDto {
                ID = _ids[CompetitionList.SelectedIndex]
            };

            GenerateTable();
        }

        private void DataGridCompetition_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
        }
    }
}
