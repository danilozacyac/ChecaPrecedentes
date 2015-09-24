using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CheckPrecedentes.Dao;
using CheckPrecedentes.Models;
using System.Windows.Threading;
using MantesisVerIusCommonObjects.Utilities;
using System.ComponentModel;
using System.Collections.Generic;

namespace CheckPrecedentes
{
    /// <summary>
    /// Lógica de interacción para ChecaPrecedentes.xaml
    /// </summary>
    public partial class ChecaPrecedentes : UserControl
    {
        /// <summary>
        /// Listado de tesis que se van a revisar
        /// </summary>
        private ObservableCollection<Tesis> listaPorChecar;

        /// <summary>
        /// Lista de las tesis que se encuentran desincronizadas entre las bases de datos de SQL Server y la de Access
        /// </summary>
        private ObservableCollection<Tesis> listaTesisNoConcuerdan;

        private TesisModel model;

        private List<int> epocasVerificar;

        public ChecaPrecedentes()
        {
            InitializeComponent();

            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            updatePbDelegate =
                new UpdateProgressBarDelegate(TesisProgress.SetValue);

        }

        private void BtnVerificar_Click(object sender, RoutedEventArgs e)
        {
            this.RBtnCancelar.IsEnabled = true;
            epocasVerificar = new List<int>();
            listaTesisNoConcuerdan = new ObservableCollection<Tesis>();
            listaPorChecar = new ObservableCollection<Tesis>();

            if (ChkTodo.IsChecked == true)
            {
                MessageBoxResult result = MessageBox.Show("Verificar completamente la base de datos será un proceso tardado. ¿Deseas continuar?",
                    "Atención", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                    epocasVerificar.Add(1000);
                else
                    ChkTodo.IsChecked = false;
            }
            else
            {

                if (ChkDecima.IsChecked == true)
                    epocasVerificar.Add(10);
                if (ChkNovena.IsChecked == true)
                    epocasVerificar.Add(9);
                if (ChkOctava.IsChecked == true)
                    epocasVerificar.Add(8);
                if (ChkSeptima.IsChecked == true)
                    epocasVerificar.Add(7);
                if (ChkSexta.IsChecked == true)
                    epocasVerificar.Add(6);
                if (ChkQuinta.IsChecked == true)
                    epocasVerificar.Add(5);
                if (ChkInformes.IsChecked == true)
                    epocasVerificar.Add(4);
                if (ChkAp6.IsChecked == true)
                    epocasVerificar.Add(26);
                if (ChkAp5.IsChecked == true)
                    epocasVerificar.Add(25);
                if (ChkAp4.IsChecked == true)
                    epocasVerificar.Add(24);
                if (ChkAp3.IsChecked == true)
                    epocasVerificar.Add(23);
                if (ChkAp2.IsChecked == true)
                    epocasVerificar.Add(22);
                if (ChkAp1.IsChecked == true)
                    epocasVerificar.Add(21);


            }


            LaunchBusyIndicator();
           
        }

        private UpdateProgressBarDelegate updatePbDelegate = null;

       

        private delegate void UpdateProgressBarDelegate(
            System.Windows.DependencyProperty dp, Object value);

        private void TabControl1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TabControl2.SelectedIndex = TabControl1.SelectedIndex;
        }

        private void TabControl2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TabControl1.SelectedIndex = TabControl2.SelectedIndex;
        }


        private Tesis tesisSeleccionada;
        private void LstRegs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tesisSeleccionada = LstRegs.SelectedItem as Tesis;

            TxtRSql.Text = tesisSeleccionada.RubroSql;
            TxtTSql.Text = tesisSeleccionada.TextoSql;
            TxtPSql.Text = tesisSeleccionada.PrecedenteSql;
            TxtNSql.Text = tesisSeleccionada.NotaPublicaSql;

            TxtRAccess.Text = tesisSeleccionada.RubroAccess;
            TxtTAccess.Text = tesisSeleccionada.TextoAccess;
            TxtPAccess.Text = tesisSeleccionada.PrecedenteAccess;
            TxtNAccess.Text = tesisSeleccionada.NotaPublicaAccess;

            LblModifica.Content = tesisSeleccionada.QDifiere;
        }

        #region Background Worker

        private BackgroundWorker worker = new BackgroundWorker();
        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            model = new TesisModel();

            if (epocasVerificar.Contains(1000))
            {

                model.GetTesisSql(listaPorChecar);
                
            }
            else
            {

                if (epocasVerificar.Contains(10))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.Decima);
                if (epocasVerificar.Contains(9))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.Novena);
                if (epocasVerificar.Contains(8))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.Octava);
                if (epocasVerificar.Contains(7))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.Septima);
                if (epocasVerificar.Contains(6))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.Sexta);
                if (epocasVerificar.Contains(5))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.Quinta);
                if (epocasVerificar.Contains(4))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.IniciaInformes, ConstMant.FinInformes);
                if (epocasVerificar.Contains(26))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.IniciaApendice6, ConstMant.FinApendice6);
                if (epocasVerificar.Contains(25))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.IniciaApendice5, ConstMant.FinApendice5);
                if (epocasVerificar.Contains(24))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.IniciaApendice4, ConstMant.FinApendice4);
                if (epocasVerificar.Contains(23))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.IniciaApendice3, ConstMant.FinApendice3);
                if (epocasVerificar.Contains(22))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.IniciaApendice2, ConstMant.FinApendice2);
                if (epocasVerificar.Contains(21))
                    model.GetTesisSqlPorEpoca(listaPorChecar, ConstMant.IniciaApendice1, ConstMant.FinApendice1);
                
            }
        }

        private void RBtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.worker.CancelAsync();
            this.RBtnCancelar.IsEnabled = false;
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BusyIndicator.IsBusy = false;

            TesisProgress.Visibility = Visibility.Visible;
            TesisProgress.Minimum = 0;
            TesisProgress.Maximum = listaPorChecar.Count();
            TesisProgress.Value = 0;

            this.Dispatcher.BeginInvoke((Action)(() => UpdateContentLabel("Loading items")));

            int counter = 1;
            foreach (Tesis tes in listaPorChecar)
            {
                if (worker.CancellationPending == true)
                    break; 

                TesisProgress.Value += 1;


                model.GetTesisAccess(tes, listaTesisNoConcuerdan);

                Dispatcher.CurrentDispatcher.Invoke(updatePbDelegate,
                        System.Windows.Threading.DispatcherPriority.Background,
                        new object[] { ProgressBar.ValueProperty, TesisProgress.Value });

                this.Dispatcher.BeginInvoke((Action)(() =>
                UpdateContentLabel("Verificando tesis:  " + counter + " de " + listaPorChecar.Count())));

                this.Dispatcher.BeginInvoke((Action)(() => UpdateListContent()));

                counter++;
            }

            LstRegs.DataContext = listaTesisNoConcuerdan;
            LblTotal.Content = LstRegs.Items.Count;
            TesisProgress.Visibility = Visibility.Collapsed;
        }

        private void LaunchBusyIndicator()
        {
            if (!worker.IsBusy)
            {
                BusyIndicator.IsBusy = true;
                worker.RunWorkerAsync();

            }
        }

        void UpdateContentLabel(string task)
        {
            LblProgreso.Content = task;
        }

        void UpdateListContent()
        {
            LstRegs.DataContext = listaTesisNoConcuerdan;
        }

        #endregion

       
    }
}
