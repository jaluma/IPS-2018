﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Controls;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Button = System.Windows.Controls.Button;
using DataGrid = System.Windows.Controls.DataGrid;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Label = System.Windows.Controls.Label;
using PrintDialog = System.Windows.Controls.PrintDialog;

namespace Ui.Main.Pages.Competition.Times
{
    /// <summary>
    /// Lógica de interacción para AllTimes.xaml
    /// </summary>
    public partial class AllTimes : ModernDialog
    {

        private DataTable[,] _tables;
        private IEnumerable<AbsoluteCategory> _categories;
        private CompetitionDto _competition;
        private TimesService _service;

        private static string[] Genders = new string[2]{"M", "F"};

        public AllTimes(CompetitionDto competition, IEnumerable<AbsoluteCategory> categories) {
            _competition = competition;
            _categories = categories;
            _tables = new DataTable[_categories.Count(), 2];
            _service = new TimesService();
            InitializeComponent();

            //botones
            Button customButton = new Button() { Content = Properties.Resources.Close, Margin = new Thickness(4) };
            customButton.Click += (ss, ee) => { this.Close(); };
            Button printButton = new Button() { Content = Properties.Resources.Print, Margin = new Thickness(4) };
            printButton.Click += ButtonBase_OnClick;
            Buttons = new Button[] { printButton, customButton };

            for (int i = 0; i < _categories.Count(); i++) {
                ColumnDefinition columnCategories = new ColumnDefinition()
                {
                    Width = GridLength.Auto
                };
                Layout.ColumnDefinitions.Add(columnCategories);
            }

            for (int i = 0; i < 4; i++) {
                RowDefinition row = new RowDefinition()
                {
                    Height = GridLength.Auto
                };
                Layout.RowDefinitions.Add(row);
            }

            for (int i = 0; i < _categories.Count(); i++) {
                for (int j = 0; j < 2; j++) {
                    DataGrid dataGrid = new DataGrid();
                    dataGrid.Margin = new Thickness(10);
                    GenerateDatagrid(i, j);

                    if (_tables[i, j] != null) {
                        getLabel(i, j);

                        dataGrid.ItemsSource = _tables[i, j].DefaultView;
                        dataGrid.MinColumnWidth = 10;

                        Grid.SetColumn(dataGrid, i);
                        Grid.SetRow(dataGrid, j*2+1);

                        Layout.Children.Add(dataGrid);
                    }
                }
            }
        }

        private void getLabel(int i, int j) {
            Label label = new Label() {
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            Grid.SetColumn(label, i);
            Grid.SetRow(label, j*2);

            label.HorizontalContentAlignment = HorizontalAlignment.Center;

            label.Content = j == 0 ? _categories.ElementAt(i).CategoryM.Name.Replace('_', ' ') : _categories.ElementAt(i).CategoryF.Name.Replace('_', ' ');

            Layout.Children.Add(label);
        }

        private void GenerateDatagrid(int index, int rowGender) {
            DataColumn column = new DataColumn(Properties.Resources.AthletePosition, typeof(string))
            {
                AllowDBNull = true
            };

            ref DataTable tableRef = ref _tables[index, rowGender];
            tableRef = new DataTable();
            tableRef.Columns.Add(column);

            tableRef.Columns[Properties.Resources.AthletePosition].AutoIncrement = true;
            tableRef.Columns[Properties.Resources.AthletePosition].AutoIncrementSeed = 1;
            tableRef.Columns[Properties.Resources.AthletePosition].AutoIncrementStep = 1;

            tableRef.Merge(_service.SelectCompetitionTimes(_competition, _categories.ElementAt(index), Genders[rowGender]));
            if (tableRef.Rows.Count <= 0) {
                tableRef = null;
                return;
            }

            tableRef.Columns[1].ColumnName = Properties.Resources.AthleteDorsal;
            tableRef.Columns[2].ColumnName = Properties.Resources.AthleteDni;
            tableRef.Columns[3].ColumnName = Properties.Resources.AthleteName;
            tableRef.Columns[4].ColumnName = Properties.Resources.AthleteSurname;
            tableRef.Columns[5].ColumnName = Properties.Resources.AthleteGender;
            tableRef.Columns[6].ColumnName = Properties.Resources.InitialTime;
            tableRef.Columns[7].ColumnName = Properties.Resources.FinishTime;
            tableRef.Columns[8].ColumnName = Properties.Resources.Age;

            DataTable dtClone = tableRef.Clone();
            dtClone.Columns[6].ColumnName = Properties.Resources.Time;
            dtClone.Columns[Properties.Resources.Time].DataType = typeof(string);


            foreach (DataRow row in tableRef.Rows)
            {
                object[] dr = row.ItemArray as object[];
                if (dr[6] is DBNull || dr[7] is DBNull || (long)dr[7] == 0)
                {
                    dr[6] = "---";
                }
                else
                {
                    var seconds = (long)dr[7] - (long)dr[6];
                    var timespan = TimeSpan.FromSeconds(seconds);
                    dr[6] = timespan.ToString(@"hh\:mm\:ss");
                }

                var desRow = dtClone.NewRow();
                desRow.ItemArray = dr;
                dtClone.Rows.Add(desRow);
            }

            //_table.Columns.Remove(Properties.Resources.InitialTime);
            dtClone.Columns.Remove(Properties.Resources.FinishTime);
            dtClone.Columns.Remove(Properties.Resources.AthleteDni);
            dtClone.Columns.Remove(Properties.Resources.Age);
            dtClone.Columns.Remove(Properties.Resources.AthleteGender);
            dtClone.Columns.Remove(Properties.Resources.AthleteName);
            dtClone.Columns.Remove(Properties.Resources.AthleteSurname);

            tableRef = dtClone;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            Panel.ScrollToTop();

            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;

            var result = printDlg.ShowDialog();

            if ((bool) result) {
                Size pageSize = new Size { Height = printDlg.PrintableAreaHeight, Width = printDlg.PrintableAreaWidth };
                Panel.Measure(pageSize);
                Panel.UpdateLayout();
                printDlg.PrintVisual(Panel, "Tiempos");
            }
        }
    }
}