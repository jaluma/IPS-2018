﻿using Logic.Db.Dto;
using Logic.Db.Util.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using Ui.Main.Pages.Competitions.categories;
using Control = System.Windows.Controls.Control;
using Cursors = System.Windows.Input.Cursors;
using MessageBox = System.Windows.Forms.MessageBox;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using SelectionMode = System.Windows.Controls.SelectionMode;

namespace Ui.Main.Pages.Competitions
{
    /// <summary>
    /// Lógica de interacción para ConfigureCompetition.xaml
    /// </summary>
    public partial class ConfigureCompetition : Page
    {

        private CompetitionService _serviceComp = new CompetitionService();
        private EnrollService _serviceEnroll;
        private CompetitionService _serviceCompCat = new CompetitionService();
        private CompetitionService _serviceCategories = new CompetitionService();
        private CompetitionDto _competition = new CompetitionDto();
        public List<RefundDto> refundsList = new List<RefundDto>();
        byte[] bytes;
        private IEnumerable<AbsoluteCategory> list;
        public List<AbsoluteCategory> absolutes = new List<AbsoluteCategory>();

        public ConfigureCompetition()
        {
            InitializeComponent();
            GridMountain.Visibility = Visibility.Collapsed;
            InicioPlazo.IsEnabled = false;
            FinPlazo.IsEnabled = false;
            FechaCompeticion.DisplayDateStart= DateTime.Now;

        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog dialog = new OpenFileDialog {
                Filter = @"PDF Files|*.pdf",
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                string path = dialog.FileName;
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) 
                {
                    Reglamento.Text = dialog.SafeFileName ?? throw new InvalidOperationException();
                }

                bytes = File.ReadAllBytes(System.IO.Path.GetFullPath(path));
            }
        }
        
        private void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Control component)
                component.Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Control component)
                component.Cursor = null;
        }

        private bool MountainIsChecked()
        {
            return (bool)RBMountain.IsChecked;
        }

        private bool AsphaltIsChecked()
        {
            return (bool)RBAsphalt.IsChecked;
        }

        private void CalculateDesnivel() {

            if (DPos.Text == "")
                DPos.Text = "0";
            if (DNeg.Text == "")
                DNeg.Text = "0";

            double cant= (Double.Parse(DPos.Text) - Double.Parse(DNeg.Text));
            DTotal.Text = cant.ToString();

        }

        private void CheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            if (MountainIsChecked())
            
                GridMountain.Visibility = Visibility.Visible;               
            
            else
                GridMountain.Visibility = Visibility.Collapsed;
        }
        
        private void DPos_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!DPos.IsFocused)
                CalculateDesnivel();
        }

        private void DNeg_LostFocus(object sender, RoutedEventArgs e)
        {
            if(!DNeg.IsFocused)
            CalculateDesnivel();
        }

        private void ComboBox_Initialized(object sender, EventArgs e)
        { 
            
            list= _serviceCompCat.SelectAllCategories();

            foreach (var c in list)
            {

                Categories.SelectionMode = SelectionMode.Single;
                Categories.Items.Add(c);
            }

        }

        private void BtModificar_Click(object sender, RoutedEventArgs e)
        {
            if (Categories.SelectedItem != null)
            {
                CategoriesDialog catD = new CategoriesDialog((AbsoluteCategory)Categories.SelectedItem, absolutes);
                catD.ShowDialog();
                this.absolutes = catD.absolutes;
                Categories.Items.Refresh();
            }
        }
        private void BtReset_Click(object sender, RoutedEventArgs e)
        {
            list = _serviceCompCat.SelectAllCategories();

            Categories.Items.Clear();
            
            foreach (var c in list)
            {
                Categories.SelectionMode = SelectionMode.Single;
                Categories.Items.Add(c);
            }
        }

        private void BtPlazo_Click(object sender, RoutedEventArgs e)
        {
            
            if (Plazos_list.SelectedItem == null)
            {

                InscriptionDatesDto plazos = new InscriptionDatesDto
                {
                    fechaInicio = (DateTime)InicioPlazo.SelectedDate,
                    fechaFin = (DateTime)FinPlazo.SelectedDate,
                    precio = Double.Parse(PrecioInscripcion.Text)
                };

                Plazos_list.SelectionMode = SelectionMode.Single;
                Plazos_list.Items.Add(plazos);
                FinPlazo.SelectedDate = null;
                InicioPlazo.SelectedDate = plazos.fechaFin.AddDays(1);
                InicioPlazo.DisplayDateStart = plazos.fechaFin;
                PrecioInscripcion.Text = null;
                InicioPlazo.IsEnabled = false;
                FinPlazo.DisplayDateStart = InicioPlazo.SelectedDate;

            }
        }
        private void InicioPlazo_GotFocus(object sender, RoutedEventArgs e)
        {
             FinPlazo.SelectedDate = null;
             FinPlazo.DisplayDateStart = InicioPlazo.SelectedDate;
           
        }

        private void InicioPlazo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (InicioPlazo.SelectedDate != null)
            {
                FinPlazo.IsEnabled = true;
                FinPlazo.DisplayDateStart = InicioPlazo.SelectedDate;
            }
        }

        private void BtRefund_Click(object sender, RoutedEventArgs e)
        {
            if (Plazos_list.Items.Count > 0 && FechaCompeticion.SelectedDate!=null)
            {
                InscriptionDatesDto nuevo = new InscriptionDatesDto();
                nuevo = (InscriptionDatesDto)Plazos_list.Items.GetItemAt(0);
                nuevo.fechaFin = (DateTime)FechaCompeticion.SelectedDate;
                RefundDialog refunds = new RefundDialog(nuevo);
                refunds.ShowDialog();
                refundsList = refunds.refunds;
            }

            else
            {
                MessageBox.Show("Por favor, introduzca primero los plazos de inscripción y la fecha de competición");
                return;
            }

        }

        private void FechaCompeticion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FechaCompeticion.SelectedDate != null && FechaCompeticion.Text != "")
            {
                InicioPlazo.IsEnabled = true;
                InicioPlazo.DisplayDateStart = DateTime.Now;
                InicioPlazo.DisplayDateEnd = FechaCompeticion.SelectedDate;
                FinPlazo.DisplayDateEnd = FechaCompeticion.SelectedDate;
            }            
            else
                InicioPlazo.IsEnabled = false;

        }

        private void BtBorrar_Click(object sender, RoutedEventArgs e)
        {
            Plazos_list.Items.Clear();
            InicioPlazo.SelectedDate = null;
            FinPlazo.SelectedDate = null;
            InicioPlazo.IsEnabled = true;
            FinPlazo.IsEnabled = false;
            InicioPlazo.DisplayDateStart = DateTime.Now;

        }
       
        private void OnMouseEnter(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.Hand;
        }
        private void OnMouseEnterBeam(object sender, System.Windows.Input.MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.IBeam;
        }
        private void OnMouseLeave(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }
        private void OnMouseLeaveBeam(object sender, System.Windows.Input.MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }
        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {

            if (checkAll())
            {

                _competition.Date = (DateTime)FechaCompeticion.SelectedDate;
                _competition.Km = Double.Parse(Km.Text);
                _competition.Name = Nombre.Text;
                _competition.Milestone = 5;
                if (MountainIsChecked())
                {
                    _competition.Type = TypeCompetition.Mountain;
                    _competition.Slope = Double.Parse(DTotal.Text);
                }
                else
                {
                    _competition.Type = TypeCompetition.Asphalt;
                }
                _competition.NumberPlaces = int.Parse(NumeroPlazas.Text);
                // _competition.Price = 100;
                _competition.Rules = bytes;
                _competition.Status = "OPEN";

                //METER PLAZOS en inscription dates

                foreach (InscriptionDatesDto p in Plazos_list.Items)
                    _serviceComp.AddInscriptionDate(p);

                //METER CATEGORIAS 
                foreach (AbsoluteCategory c in Categories.Items) //modificar las categorias que te devuelve el dialogo no el listbox
                {
                    
                    _serviceCategories.AddCategory(c.CategoryF);
                    _serviceCategories.AddCategory(c.CategoryM);

                }

                //añadir absoluta categoria vincular

                foreach (var c in absolutes)
                {
                    CategoryDto idm = _serviceCategories.getCategory(c.CategoryM);
                    CategoryDto idf = _serviceCategories.getCategory(c.CategoryF);
                    AbsoluteCategory nueva = new AbsoluteCategory
                    {
                        Name = c.Name,
                        CategoryF = idf,
                        CategoryM = idm
                    };

                    _serviceCategories.AddAbsoluteCategory(nueva);
                }

                //añadir competicion

                _serviceComp.AddCompetition(_competition);


                //vincular competicion y plazos con precio

                _competition.ID = _serviceComp.getIdCompetition(_competition);
                _serviceEnroll = new EnrollService(_competition);
                foreach (var c in Plazos_list.Items)
                {
                    _serviceEnroll.EnrollCompetitionDates(_competition.ID, (InscriptionDatesDto)c);


                }
                //vincular competicion y categorias
                foreach (var c in absolutes)
                {
                    long id = _serviceComp.getIdAbsolute((AbsoluteCategory)c);

                    _serviceEnroll.EnrollAbsoluteCompetition(_competition.ID, id);

                }
                //vincular refunds y competicion
                foreach (var c in refundsList)
                {
                    _serviceEnroll.EnrollRefundsCompetition(_competition.ID, c.date_refund, c.refund);
                }
            }

            else
                MessageBox.Show("Por favor, revise que todos los campos se han introducido correctamente");
                return;
        }


        private bool checkAll() {
            double result;
            int resut;

            if (FechaCompeticion.SelectedDate == null | Km.Text == ("") || !Double.TryParse(Km.Text, out result) || result <0 || Nombre.Text == ("") ||
                (!MountainIsChecked() && !AsphaltIsChecked()) || (MountainIsChecked() && DTotal.Text == ("")) || NumeroPlazas.Text == ("") || !int.TryParse(NumeroPlazas.Text, out resut) 
                || Plazos_list.Items.IsEmpty || !Double.TryParse(PrecioInscripcion.Text, out result) || result <0)
                return false;

            

            return true;

        }

        
    }
}
