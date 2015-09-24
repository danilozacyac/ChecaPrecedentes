using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CheckPrecedentes.Dao;
using CheckPrecedentes.Models;

namespace CheckPrecedentes
{
    /// <summary>
    /// Lógica de interacción para RevisaTotales.xaml
    /// </summary>
    public partial class RevisaTotales : Window
    {
        private int numTotalAccess;
        private int numTotalServer;

        private ObservableCollection<Tesis> tesisAccess;
        private ObservableCollection<Tesis> tesisServer;

        private ObservableCollection<Tesis> diferencias;

        TesisModel model = new TesisModel();
        Epocas epocaSelect;

        public RevisaTotales()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CbxEpoca.DataContext = new Epocas().GetEpocasForRevision();
        }

        private void BtnInicia_Click(object sender, RoutedEventArgs e)
        {
            if (CbxEpoca.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona la epoca que quieres revisar");
                return;
            }

            epocaSelect = CbxEpoca.SelectedItem as Epocas;



            numTotalAccess = model.GetTesisCountAccess(epocaSelect.BaseAccess);
            numTotalServer = model.GetTesisCountSql(epocaSelect.IdEpocaServer);

            if (numTotalAccess != numTotalServer)
                BtnMuestra.IsEnabled = true;

            TxtAccess.Text = numTotalAccess.ToString();
            TxtServer.Text = numTotalServer.ToString();
        }

        private void BtnMuestra_Click(object sender, RoutedEventArgs e)
        {
            diferencias = new ObservableCollection<Tesis>();
            tesisAccess = model.GetTesisAccess(epocaSelect.BaseAccess);
            tesisServer = model.GetTesisServer(epocaSelect.IdEpocaServer);

            if (numTotalAccess > numTotalServer)
            {
                foreach (Tesis tesis in tesisAccess)
                {
                    var encuentra = (from n in tesisServer
                                     where n.RegIus == tesis.RegIus
                                     select n);

                    if (encuentra == null)
                        diferencias.Add(tesis);
                }
            }
            else
            {
                foreach (Tesis tesis in tesisServer)
                {
                    try
                    {
                        Tesis encuentra = (from n in tesisAccess
                                           where n.RegIus == tesis.RegIus
                                           select n).ToList()[0];
                    }
                    catch (Exception)
                    {
                        diferencias.Add(tesis);
                    }
                }
            }

            dataGrid1.DataContext = diferencias;
        }
    }
}
