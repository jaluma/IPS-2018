﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Ui.Main.Pages.MenuInitial;
using DataGridTextColumn = System.Windows.Controls.DataGridTextColumn;

namespace Ui.Main.Pages.Competition.Times
{
    /// <summary>
    ///     Lógica de interacción para SelectionCompetition.xaml
    /// </summary>
    public partial class SelectionCompetition : ModernFrame
    {
        private readonly CompetitionService _service;
        private readonly List<long> _columnIds;

        public SelectionCompetition() {
            InitializeComponent();
            // change title window

            // inicialize data table
            _service = new CompetitionService();
            var table = _service.SelectCompetitionFinish();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Type;
            table.Columns[3].ColumnName = Properties.Resources.Competition_Km;
            //table.Columns[4].ColumnName = Properties.Resources.Competition_Price;
            table.Columns[4].ColumnName = Properties.Resources.Competition_Date;
            table.Columns[5].ColumnName = Properties.Resources.Competition_Status;
            table.Columns[6].ColumnName = Properties.Resources.Competition_Inscritos;

            //columnIds = table.Columns[0];
            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();

            table.Columns.RemoveAt(0);
            table.Columns.Remove(Properties.Resources.Competition_Status);

            DataGridCompetition.ItemsSource = table.DefaultView;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var indexSeletected = DataGridCompetition.SelectedIndex;

            var id = (int) _columnIds[indexSeletected];


            var competition = new CompetitionDto {
                ID = id
            };

            TimesAthletes.Competition = competition;

            MainMenu.ChangeMenuSelected(Properties.Resources.TileTimes, Properties.Resources.TitleTimesCompetition);
        }

        private void DataGridCompetition_OnMouseEnter(object sender, MouseEventArgs e) {
            DataGridCompetition.Cursor = Cursors.Hand;
        }

        private void DataGridCompetition_OnMouseLeave(object sender, MouseEventArgs e) {
            DataGridCompetition.Cursor = null;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(DateTime))
                ((DataGridTextColumn) e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void DataGridCompetition_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
        }
    }
}
