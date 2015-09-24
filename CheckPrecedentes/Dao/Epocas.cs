using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CheckPrecedentes.Dao
{
    public class Epocas
    {
        private string epoca;
        private string baseAccess;
        private int idEpocaServer;

        public string Epoca
        {
            get
            {
                return this.epoca;
            }
            set
            {
                this.epoca = value;
            }
        }

        public string BaseAccess
        {
            get
            {
                return this.baseAccess;
            }
            set
            {
                this.baseAccess = value;
            }
        }

        public int IdEpocaServer
        {
            get
            {
                return this.idEpocaServer;
            }
            set
            {
                this.idEpocaServer = value;
            }
        }


        public ObservableCollection<Epocas> GetEpocasForRevision()
        {
            ObservableCollection<Epocas> epocas = new ObservableCollection<Epocas>();

            epocas.Add(new Epocas { Epoca = "Décima", BaseAccess = "DECIMA.MDB", IdEpocaServer = 100 });
            epocas.Add(new Epocas { Epoca = "Novena", BaseAccess = "NOVENA.MDB", IdEpocaServer = 5 });

            return epocas;
        }
    }
}
