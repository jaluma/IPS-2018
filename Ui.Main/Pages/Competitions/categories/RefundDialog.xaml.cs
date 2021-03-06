﻿using System;
using System.Collections.Generic;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Logic.Db.Dto;

namespace Ui.Main.Pages.Competitions.categories
{
    /// <summary>
    ///     Lógica de interacción para RefundDialog.xaml
    /// </summary>
    public partial class RefundDialog : ModernDialog
    {
        private readonly List<string> devoluciones = new List<string> {
            "100%",
            "75%",
            "50%",
            "25%",
            "0%"
        };

        private readonly InscriptionDatesDto limites = new InscriptionDatesDto();
        public List<RefundDto> refunds = new List<RefundDto>();

        public RefundDialog(InscriptionDatesDto limites) {
            InitializeComponent();
            this.limites = limites;
            Limite.DisplayDateEnd = limites.FechaFin;
            Limite.DisplayDateStart = limites.FechaInicio;
            CloseButton.Visibility = Visibility.Collapsed;
        }

        private void Devolucion_Initialized(object sender, EventArgs e) {
            Refund.ItemsSource = devoluciones;
        }

        private void BtPlazo_Click(object sender, RoutedEventArgs e) {
            if (Limite.SelectedDate != null && Refund.SelectedItem != null) {
                var nuevoRefund = new RefundDto {
                    date_refund = (DateTime) Limite.SelectedDate,
                    refund = double.Parse(Refund.SelectedItem.ToString().Replace("%", ""))
                };
                Plazos_list.Items.Add(nuevoRefund);
                Limite.SelectedDate = null;
                if (nuevoRefund.date_refund.Date.Year == limites.FechaFin.Date.Year &&
                    nuevoRefund.date_refund.Date.Month == limites.FechaFin.Date.Month &&
                    nuevoRefund.date_refund.Date.Day == limites.FechaFin.Date.Day)
                    Limite.IsEnabled = false;

                else
                    Limite.DisplayDateStart = nuevoRefund.date_refund.AddDays(1);
                Refund.SelectedItem = null;
            }
        }

        private void BtModificar_Click(object sender, RoutedEventArgs e) {
            Plazos_list.Items.Clear();
            Limite.DisplayDateEnd = limites.FechaFin;
            Limite.DisplayDateStart = limites.FechaInicio;
            Limite.IsEnabled = true;
            Refund.ItemsSource = devoluciones;
        }

        private void BtAceptar_Click(object sender, RoutedEventArgs e) {
            foreach (var c in Plazos_list.Items)
                refunds.Add((RefundDto) c);

            Close();
        }
    }
}
