using System;
using System.Linq;

namespace CheckPrecedentes.Dao
{
    public class Tesis
    {
        private int regIus;
        private string baseAccess;
        private string rubroSql;
        private string textoSql;
        private string precedenteSql;
        private string notaPublicaSql;
        private string rubroAccess;
        private string textoAccess;
        private string precedenteAccess;
        private string notaPublicaAccess;
        private string qDifiere = String.Empty;

        public int RegIus
        {
            get
            {
                return this.regIus;
            }
            set
            {
                this.regIus = value;
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

        public string RubroSql
        {
            get
            {
                return this.rubroSql;
            }
            set
            {
                this.rubroSql = value;
            }
        }

        public string TextoSql
        {
            get
            {
                return this.textoSql;
            }
            set
            {
                this.textoSql = value;
            }
        }

        public string PrecedenteSql
        {
            get
            {
                return this.precedenteSql;
            }
            set
            {
                this.precedenteSql = value;
            }
        }

        public string NotaPublicaSql
        {
            get
            {
                return this.notaPublicaSql;
            }
            set
            {
                this.notaPublicaSql = value;
            }
        }

        public string RubroAccess
        {
            get
            {
                return this.rubroAccess;
            }
            set
            {
                this.rubroAccess = value;
            }
        }

        public string TextoAccess
        {
            get
            {
                return this.textoAccess;
            }
            set
            {
                this.textoAccess = value;
            }
        }

        public string PrecedenteAccess
        {
            get
            {
                return this.precedenteAccess;
            }
            set
            {
                this.precedenteAccess = value;
            }
        }

        public string NotaPublicaAccess
        {
            get
            {
                return this.notaPublicaAccess;
            }
            set
            {
                this.notaPublicaAccess = value;
            }
        }

        public string QDifiere
        {
            get
            {
                return this.qDifiere;
            }
            set
            {
                this.qDifiere = value;
            }
        }


    }
}
