﻿using Logic.Db.Dto;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Ui.Main.Pages.Inscriptions.Payment
{
    /// <summary>
    /// Lógica de interacción para WireTransferWindow.xaml
    /// </summary>
    public partial class WireTransferWindow : Window
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;

        public WireTransferWindow(AthleteDto athlete, CompetitionDto competition)
        {
            InitializeComponent();

            _athlete = athlete;
            _competition = competition;

            string cont = Properties.Resources.PaymentAmount +" " + _competition.Price + " €";
            LbPaymentAmount.Content = cont;
        }

        private void BtNext_Click(object sender, RoutedEventArgs e)
        {
            new InscriptionProofWindow(_athlete, _competition).Show();
            this.Close();
        }
    }
}
