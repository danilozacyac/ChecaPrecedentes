using System;
using System.Linq;
using System.Windows;

namespace ChecaPrecedentes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var watch = Stopwatch.StartNew();
            //// the code that you want to measure comes here

            //ObservableCollection<Tesis> listaTesisNoConcuerdan = new TesisModel().GetTesisSql();

            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;

            //MessageBox.Show(elapsedMs.ToString());

            //LstRegs.DataContext = listaTesisNoConcuerdan;
            ////listaTesisNoConcuerdan = new TesisModel().GetTesisAccess(listaTesisNoConcuerdan);

            ////LstRegs.DataContext = (from n in listaTesisNoConcuerdan
            ////                       where n.QDifiere != String.Empty
            ////                       select n);

            //LblTotal.Content = LstRegs.Items.Count;
            
        }

        //private Tesis tesisSeleccionada;
        private void LstRegs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //tesisSeleccionada = LstRegs.SelectedItem as Tesis;

            //TxtRSql.Text = tesisSeleccionada.RubroSql;
            //TxtTSql.Text = tesisSeleccionada.TextoSql;
            //TxtPSql.Text = tesisSeleccionada.PrecedenteSql;
            //TxtNSql.Text = tesisSeleccionada.NotaPublicaSql;

            //TxtRAccess.Text = tesisSeleccionada.RubroAccess;
            //TxtTAccess.Text = tesisSeleccionada.TextoAccess;
            //TxtPAccess.Text = tesisSeleccionada.PrecedenteAccess;
            //TxtNAccess.Text = tesisSeleccionada.NotaPublicaAccess;

            //LblModifica.Content = tesisSeleccionada.QDifiere;
        }

        private void BtnVerificar_Click(object sender, RoutedEventArgs e)
        {

            //if(ChkDecima.IsChecked == true)
            //    new TesisModel().GetTesisSqlPorEpoca(ConstMant.IniciaDecima, ConstMant.FinDecima);
            //if (ChkNovena.IsChecked == true)
            //    new TesisModel().GetTesisSqlPorEpoca(ConstMant.IniciaNovena, ConstMant.FinNovena);
            //if (ChkOctava.IsChecked == true)
            //    new TesisModel().GetTesisSqlPorEpoca(ConstMant.IniciaOctava, ConstMant.FinOctava);
            //if (ChkSeptima.IsChecked == true)
            //    new TesisModel().GetTesisSqlPorEpoca(ConstMant.IniciaSeptima, ConstMant.FinOctava);
            //if (ChkSexta.IsChecked == true)
            //    new TesisModel().GetTesisSqlPorEpoca(ConstMant.IniciaSexta, ConstMant.FinSeptima);
            //if (ChkQuinta.IsChecked == true)
            //    new TesisModel().GetTesisSqlPorEpoca(ConstMant.Iniciaquinta, ConstMant.FinQuinta);
                



            // LstRegs.DataContext = TesisModel.ListaTesis;
            // LblTotal.Content = LstRegs.Items.Count;
        }

        private void TabControl1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TabControl2.SelectedIndex = TabControl1.SelectedIndex;
        }

        private void TabControl2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TabControl1.SelectedIndex = TabControl2.SelectedIndex;
        }

        private void BtnEmpatar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
