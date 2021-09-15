using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace crudProdutosBA
{
    public class DBConexion
    {
        
        string cadenaConexion = "data source = DESKTOP-FGSS2T3; initial catalog = db_inventario; Integrated security = True";    

     public SqlConnection Conectarbd = new SqlConnection();

     public DBConexion()
     {
       Conectarbd.ConnectionString=cadenaConexion;
     }

      public void abrir()
     {
        try
         {
           Conectarbd.Open();
          } catch(Exception ex)
         {
         Console.WriteLine("Error al iniciar la conexión con DB ", ex.Message); 
          }
     }

      public void cerrar()
     {
       Conectarbd.Close();
      }
  
 
    }
}
