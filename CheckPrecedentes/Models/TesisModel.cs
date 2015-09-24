using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using CheckPrecedentes.Dao;
using ScjnUtilities;

namespace CheckPrecedentes.Models
{
    public class TesisModel
    {
        readonly string serverConnectionString = ConfigurationManager.ConnectionStrings["MantesisSql"].ConnectionString;

        
        /// <summary>
        /// Obtiene todas las tesis que se encuentran en la base de datos de SQL Server y posteriormente las compara con 
        /// su similar que se encuentra en las bases de datos de Access para encontrar diferencias en su estructura
        /// </summary>
        /// <param name="listaTesisNoConcuerdan">Lista de tesis que presenta diferencias</param>
        public void GetTesisSql(ObservableCollection<Tesis> listaTesis)
        {

            SqlConnection connection = new SqlConnection(serverConnectionString);

            SqlCommand cmd;
            SqlDataReader reader = null;

            Tesis tesis = null;

            try
            {
                connection.Open();

                //Solo Checa decima
                //string sqlCadena = "SELECT * FROM Tesis WHERE Epoca = 100 and Parte <> 99 ORDER BY BD";
                string sqlCadena = "SELECT IUS,Rubro,Texto,Precedentes,NotaPublica,BD FROM Tesis WHERE Parte <> 99 ORDER BY BD";

                cmd = new SqlCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tesis = new Tesis();

                        tesis.RegIus = reader["IUS"] as int? ?? -1;
                        tesis.RubroSql = reader["Rubro"].ToString();
                        tesis.TextoSql = reader["Texto"].ToString();
                        tesis.PrecedenteSql = reader["precedentes"].ToString();
                        tesis.NotaPublicaSql = reader["NotaPublica"].ToString();
                        tesis.BaseAccess = reader["BD"].ToString();

                        this.GetTesisAccess(tesis,listaTesis);
                    }
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// Obtiene las tesis que pertenecen a la epoca seleccionada y que se encuentran en la base de datos de SQL Server
        /// y posteriormente las compara con su similar que se encuentra en las bases de datos de Access para encontrar
        /// diferencias en su estructura
        /// </summary>
        /// <param name="listaPorChecar">Lista de tesis que se revisarán</param>
        /// <param name="volumenInicio">Volumen inicial de la época seleccionada</param>
        /// <param name="volumenFinal">Volumen final de la época seleccionada</param>
        public void GetTesisSqlPorEpoca(ObservableCollection<Tesis> listaPorChecar, int volumenInicio, int volumenFinal)
        {
            SqlConnection connection = new SqlConnection(serverConnectionString);

            SqlCommand cmd;
            SqlDataReader reader = null;

            Tesis tesis = null;

            try
            {
                connection.Open();

                string sqlCadena = "SELECT IUS,Rubro,Texto,Precedentes,NotaPublica,BD FROM Tesis WHERE Parte <> 99 AND (Volumen BETWEEN @Inicio AND @Fin) ORDER BY BD";

                cmd = new SqlCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@Inicio", volumenInicio);
                cmd.Parameters.AddWithValue("@Fin", volumenFinal);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tesis = new Tesis();

                        tesis.RegIus = Convert.ToInt32(reader["IUS"]);
                        tesis.RubroSql = reader["Rubro"].ToString();
                        tesis.TextoSql = reader["Texto"].ToString();
                        tesis.PrecedenteSql = reader["precedentes"].ToString();
                        tesis.NotaPublicaSql = reader["NotaPublica"].ToString().Trim();
                        tesis.BaseAccess = reader["BD"].ToString();

                        listaPorChecar.Add(tesis);
                        //this.GetTesisAccess(tesis,listaTesisNoConcuerdan);
                    }

                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            finally
            {
                connection.Close();
            }
        }

        public void GetTesisSqlPorEpoca(ObservableCollection<Tesis> listaPorChecar, int epoca)
        {
            SqlConnection connection = new SqlConnection(serverConnectionString);

            SqlCommand cmd;
            SqlDataReader reader = null;

            Tesis tesis = null;

            try
            {
                connection.Open();

                string sqlCadena = "SELECT IUS,Rubro,Texto,Precedentes,NotaPublica,BD FROM Tesis WHERE Parte <> 99 AND Epoca = @Epoca ORDER BY BD";

                cmd = new SqlCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@Epoca", epoca);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tesis = new Tesis();

                        tesis.RegIus = Convert.ToInt32(reader["IUS"]);
                        tesis.RubroSql = reader["Rubro"].ToString();
                        tesis.TextoSql = reader["Texto"].ToString();
                        tesis.PrecedenteSql = reader["precedentes"].ToString();
                        tesis.NotaPublicaSql = reader["NotaPublica"].ToString().Trim();
                        tesis.BaseAccess = reader["BD"].ToString();

                        listaPorChecar.Add(tesis);
                        //this.GetTesisAccess(tesis,listaTesisNoConcuerdan);
                    }

                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Obtiene la versión de access de la tesis que se esta verificando y la compara con su simil que se obtuvo
        /// de la base en SQL Server e indica en que campos existen diferencias en caso de encontrarlas
        /// </summary>
        /// <param name="tesisComparar">Tesis que se obtuvo en el Servidor y que se va a comparar</param>
        /// <param name="listaTesisNoConcuerdan">Lista de Tesis que presentan diferencias entre bases</param>
        public void GetTesisAccess(Tesis tesisComparar, ObservableCollection<Tesis> listaTesisNoConcuerdan)
        {
            OleDbConnection connection = null;

            OleDbCommand cmd;
            OleDbDataReader reader = null;

            try
            {
                string bd = tesisComparar.BaseAccess;

                connection = new OleDbConnection(@"Data Source=\\ct9iis2\Mant.BD\" + bd + "; Provider=Microsoft.Jet.OLEDB.4.0; Mode=ReadWrite|Share Deny None");
                connection.Open();

                string sqlCadena = "SELECT * FROM Tesis WHERE ius4 = " + tesisComparar.RegIus;

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    tesisComparar.RubroAccess = reader["Rubro"].ToString();
                    tesisComparar.TextoAccess = reader["Texto"].ToString();
                    tesisComparar.PrecedenteAccess = reader["precedentes"].ToString();

                    int epoca = Convert.ToInt32(reader["Epoca"]);

                    if ((epoca == 5 || epoca == 100) && (tesisComparar.RegIus < 1000000 && tesisComparar.RegIus > 1999999) )
                        tesisComparar.NotaPublicaAccess = reader["NotaPublica"].ToString().Trim();

                    
                    if (!tesisComparar.RubroSql.Equals(tesisComparar.RubroAccess))
                        tesisComparar.QDifiere += "R";
                    if (!tesisComparar.TextoSql.Equals(tesisComparar.TextoAccess))
                        tesisComparar.QDifiere += "T";
                    if (!tesisComparar.PrecedenteSql.Equals(tesisComparar.PrecedenteAccess))
                        tesisComparar.QDifiere += "P";

                    if ((epoca == 5 || epoca == 100) && (tesisComparar.RegIus < 1000000 && tesisComparar.RegIus > 1999999))
                        if (!tesisComparar.NotaPublicaSql.Equals(tesisComparar.NotaPublicaAccess))
                            tesisComparar.QDifiere += "N";

                    if (!tesisComparar.QDifiere.Equals(String.Empty))
                        listaTesisNoConcuerdan.Add(tesisComparar);
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }


        /// <summary>
        /// Obtienen el número total de tesis de la época seleccionada que se encuentran almacenadas en la base
        /// de datos de Access
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public int GetTesisCountAccess(string dataBaseName)
        {
            OleDbConnection connection = null;// = new SqlConnection(ConfigurationManager.ConnectionStrings["MantesisSQL"].ConnectionString);

            OleDbCommand cmd;
            OleDbDataReader reader = null;

            int numTotal = 0;

            try
            {
                //connectionMantesisSql = new OleDbConnection(@"Data Source=\\ct9iis2\Mant.BD\Decima.mdb; Provider=Microsoft.Jet.OLEDB.4.0; Mode=ReadWrite|Share Deny None");
                connection = new OleDbConnection(@"Data Source=\\ct9iis2\Mant.BD\" + dataBaseName + "; Provider=Microsoft.Jet.OLEDB.4.0; Mode=ReadWrite|Share Deny None");
                connection.Open();

                string sqlCadena = "SELECT COUNT(IUS4) as Total FROM Tesis ";

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    numTotal = reader["total"] as int? ?? -1;
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            finally
            {
                connection.Close();
            }

            return numTotal;
        }

        /// <summary>
        /// Obtiene el número total de tesis de la época seleccionada que se encuentran almacenadas en la base de
        /// datos del Servidor SQL
        /// </summary>
        /// <param name="epoca"></param>
        /// <returns></returns>
        public int GetTesisCountSql(int epoca)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MantesisSql"].ConnectionString);

            SqlCommand cmd;
            SqlDataReader reader = null;

            int numTotal = 0;

            try
            {
                connection.Open();

                //Solo Checa decima
                //string sqlCadena = "SELECT * FROM Tesis WHERE Epoca = 100 and Parte <> 99 ORDER BY BD";
                string sqlCadena = "SELECT COUNT(IUS) Total  FROM Tesis WHERE Parte < 99 and epoca = @epoca";

                cmd = new SqlCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@epoca", epoca);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        numTotal = reader["Total"] as int? ?? -1;
                    }
                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            finally
            {
                connection.Close();
            }

            return numTotal;
        }


        /// <summary>
        /// Obtiene el registro digital, rubro, texto y precedente de todas las tesis de la epoca seleccionada
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public ObservableCollection<Tesis> GetTesisAccess(string dataBaseName)
        {
            ObservableCollection<Tesis> listadoRegresa = new ObservableCollection<Tesis>();

            OleDbConnection connection = null;// = new SqlConnection(ConfigurationManager.ConnectionStrings["MantesisSQL"].ConnectionString);

            OleDbCommand cmd;
            OleDbDataReader reader = null;

            try
            {
                connection = new OleDbConnection(@"Data Source=\\ct9iis2\Mant.BD\" + dataBaseName + "; Provider=Microsoft.Jet.OLEDB.4.0; Mode=ReadWrite|Share Deny None");
                connection.Open();

                string sqlCadena = "SELECT * FROM Tesis";

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tesis tesis = new Tesis();
                        tesis.RegIus = reader["IUS4"] as int? ?? -1;
                        tesis.RubroAccess = reader["Rubro"].ToString();
                        tesis.TextoAccess = reader["Texto"].ToString();
                        tesis.PrecedenteAccess = reader["precedentes"].ToString();


                        listadoRegresa.Add(tesis);
                    }
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            finally
            {
                connection.Close();
            }
            return listadoRegresa;
        }

        /// <summary>
        /// Obtiene el registro digital, rubro, texto y precedente de todas las tesis de la epoca seleccionada, 
        /// descartando las eliminadas y las del apéndice
        /// </summary>
        /// <param name="idEpocaServer"></param>
        /// <returns></returns>
        public ObservableCollection<Tesis> GetTesisServer(int idEpocaServer)
        {
            ObservableCollection<Tesis> listadoRegresa = new ObservableCollection<Tesis>();

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MantesisSQL"].ConnectionString);

            SqlCommand cmd;
            SqlDataReader reader = null;

            try
            {
                connection.Open();

                string sqlCadena = "SELECT * FROM Tesis WHERE Parte < 99 AND epoca = @epoca";

                cmd = new SqlCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@epoca", idEpocaServer);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tesis tesis = new Tesis();
                        tesis.RegIus = reader["IUS"] as int? ?? -1;
                        tesis.RubroAccess = reader["Rubro"].ToString();
                        tesis.TextoAccess = reader["Texto"].ToString();
                        tesis.PrecedenteAccess = reader["precedentes"].ToString();


                        listadoRegresa.Add(tesis);
                    }
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ChecaPrecedentes");
            }
            finally
            {
                connection.Close();
            }
            return listadoRegresa;
        }

    }
}
