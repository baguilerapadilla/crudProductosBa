using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace crudProdutosBA.Controllers
{   // Endpoints para mostrar todos los productos.
    [ApiController]
    [Route("[controller]")]
    public class ProductoController: ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<Models.Producto> ProductosList = new List<Models.Producto>();
            try
            {
                DBConexion DBConn = new DBConexion();
                DBConn.abrir();
                String Query = "select * from dbo.producto p " +
                                "inner join dbo.marca m on m.id_marca = p.id_marca " +
                                "inner join dbo.presentacion p2 on p.id_presentacion = p2.id_presentacion " +
                                "inner join dbo.proveedor p3 on p.id_proveedor = p3.id_proveedor " +
                                "inner join dbo.zona z on p.id_zona = z.id_zona";

                using (SqlCommand Comando = new SqlCommand(Query, DBConn.Conectarbd))
                {
                    using (SqlDataReader reader = Comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Producto ProductoTemp = new Models.Producto();
                            ProductoTemp.IdProducto = reader.GetInt32(0);

                            Models.Marca MarcaTemp = new Models.Marca();
                            MarcaTemp.IdMarca = reader.GetInt32(11);
                            MarcaTemp.Descripcion = reader.GetString(12);
                            ProductoTemp.Marca = MarcaTemp;

                            Models.Presentacion PresentacionTemp = new Models.Presentacion();
                            PresentacionTemp.IdPresentacion = reader.GetInt32(13);
                            PresentacionTemp.Descripcion = reader.GetString(14);
                            ProductoTemp.Presentacion = PresentacionTemp;

                            Models.Proveedor ProveedorTemp = new Models.Proveedor();
                            ProveedorTemp.IdProveedor = reader.GetInt32(15);
                            ProveedorTemp.Descripcion = reader.GetString(16);
                            ProductoTemp.Proveedor = ProveedorTemp;

                            Models.Zona ZonaTemp = new Models.Zona();
                            ZonaTemp.IdZona = reader.GetInt32(17);
                            ZonaTemp.Descripcion = reader.GetString(18);
                            ProductoTemp.Zona = ZonaTemp;

                            ProductoTemp.Codigo = reader.GetInt32(5);
                            ProductoTemp.Descripcion = reader.GetString(6);
                            ProductoTemp.Precio = reader.GetDouble(7);
                            ProductoTemp.Stock = reader.GetInt32(8);
                            ProductoTemp.Iva = reader.GetInt32(9);
                            ProductoTemp.Peso = reader.GetDouble(10);

                            ProductosList.Add(ProductoTemp);
                        }
                    }
                    
                }
            } catch(SqlException e)
            {
                Console.WriteLine("Error al obtener los datos ", e.Message);
            }

            return Ok(ProductosList);
        }

        // Endpoints para mostrar un producto.
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Models.Producto ProductoTemp = new Models.Producto();
            try
            {
                DBConexion DBConn = new DBConexion();
                DBConn.abrir();
                String Query = "select * from dbo.producto p " +
                                "inner join dbo.marca m on m.id_marca = p.id_marca " +
                                "inner join dbo.presentacion p2 on p.id_presentacion = p2.id_presentacion " +
                                "inner join dbo.proveedor p3 on p.id_proveedor = p3.id_proveedor " +
                                "inner join dbo.zona z on p.id_zona = z.id_zona " +
                                "where p.id_producto = " + Id;

                using (SqlCommand Comando = new SqlCommand(Query, DBConn.Conectarbd))
                {
                    using (SqlDataReader reader = Comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            ProductoTemp.IdProducto = reader.GetInt32(0);

                            Models.Marca MarcaTemp = new Models.Marca();
                            MarcaTemp.IdMarca = reader.GetInt32(11);
                            MarcaTemp.Descripcion = reader.GetString(12);
                            ProductoTemp.Marca = MarcaTemp;

                            Models.Presentacion PresentacionTemp = new Models.Presentacion();
                            PresentacionTemp.IdPresentacion = reader.GetInt32(13);
                            PresentacionTemp.Descripcion = reader.GetString(14);
                            ProductoTemp.Presentacion = PresentacionTemp;

                            Models.Proveedor ProveedorTemp = new Models.Proveedor();
                            ProveedorTemp.IdProveedor = reader.GetInt32(15);
                            ProveedorTemp.Descripcion = reader.GetString(16);
                            ProductoTemp.Proveedor = ProveedorTemp;

                            Models.Zona ZonaTemp = new Models.Zona();
                            ZonaTemp.IdZona = reader.GetInt32(17);
                            ZonaTemp.Descripcion = reader.GetString(18);
                            ProductoTemp.Zona = ZonaTemp;

                            ProductoTemp.Codigo = reader.GetInt32(5);
                            ProductoTemp.Descripcion = reader.GetString(6);
                            ProductoTemp.Precio = reader.GetDouble(7);
                            ProductoTemp.Stock = reader.GetInt32(8);
                            ProductoTemp.Iva = reader.GetInt32(9);
                            ProductoTemp.Peso = reader.GetDouble(10);

                        }
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error al obtener los datos ", e.Message);
            }

            return Ok(ProductoTemp);
        }

        // Endpoints para insertar un nuevo productos.
        [HttpPost("Insertar")]
        public IActionResult InsertarProducto(Models.Producto Producto)
        {
            String Respuesta = "";
            Boolean Error = false;
            try
            {
                DBConexion DBConn = new DBConexion();
                DBConn.abrir();
                String Query = "INSERT INTO dbo.producto " +
                                "(id_marca, id_presentacion, id_proveedor, id_zona, codigo, descripcion_producto, precio, stock, iva, peso) " +
                                "VALUES(@IdMarca, @IdPresentacion, @IdProveedor, @IdZona, @Codigo, @DescripcionProducto, @Precio, @Stock, @Iva, @Peso)";


                using (SqlCommand Comando = new SqlCommand(Query, DBConn.Conectarbd))
                {
                    Comando.Parameters.Add("@IdMarca", SqlDbType.Int).Value = Producto.Marca.IdMarca;
                    Comando.Parameters.Add("@IdPresentacion", SqlDbType.Int).Value = Producto.Presentacion.IdPresentacion;
                    Comando.Parameters.Add("@IdProveedor", SqlDbType.Int).Value = Producto.Proveedor.IdProveedor;
                    Comando.Parameters.Add("@IdZona", SqlDbType.Int).Value = Producto.Zona.IdZona;
                    Comando.Parameters.Add("@Codigo", SqlDbType.Int).Value = Producto.Codigo;
                    Comando.Parameters.Add("@DescripcionProducto", SqlDbType.VarChar, 1000).Value = Producto.Codigo;
                    Comando.Parameters.Add("@Precio", SqlDbType.Float).Value = Producto.Precio;
                    Comando.Parameters.Add("@Stock", SqlDbType.Int).Value = Producto.Stock;
                    Comando.Parameters.Add("@Iva", SqlDbType.Int).Value = Producto.Iva;
                    Comando.Parameters.Add("@Peso", SqlDbType.Float).Value = Producto.Peso;
                    Comando.CommandType = CommandType.Text;
                    Comando.ExecuteNonQuery();

                    Respuesta = "Producto Creado Correctamente";
                }
            }
            catch (SqlException e)
            {
                Respuesta = "Error de SQL: " + e.Message;
                Error = true;
            }

            if (Error)
                return BadRequest(Respuesta);
            else
                return CreatedAtAction(nameof(InsertarProducto), Respuesta);

           
        }



        // Endpoints para actualizar productos.
        [HttpPut("Actualizar")]
        public IActionResult ActualizarProducto(Models.Producto Producto)
        {
            String Respuesta = "";
            Boolean Error = false;
            try
            {
                DBConexion DBConn = new DBConexion();
                DBConn.abrir();
                String Query = "UPDATE dbo.producto " +
                "SET id_marca = @IdMarca, id_presentacion = @IdPresentacion, id_proveedor = @IdProveedor, id_zona = @IdZona, " +
                "codigo = @Codigo, descripcion_producto = @DescripcionProducto, precio = @Precio, stock = @Stock, iva = @Iva, peso = @Peso " +
                "WHERE id_producto = @IdProducto ";


                using (SqlCommand Comando = new SqlCommand(Query, DBConn.Conectarbd))
                {
                    Comando.Parameters.Add("@IdMarca", SqlDbType.Int).Value = Producto.Marca.IdMarca;
                    Comando.Parameters.Add("@IdPresentacion", SqlDbType.Int).Value = Producto.Presentacion.IdPresentacion;
                    Comando.Parameters.Add("@IdProveedor", SqlDbType.Int).Value = Producto.Proveedor.IdProveedor;
                    Comando.Parameters.Add("@IdZona", SqlDbType.Int).Value = Producto.Zona.IdZona;
                    Comando.Parameters.Add("@Codigo", SqlDbType.Int).Value = Producto.Codigo;
                    Comando.Parameters.Add("@DescripcionProducto", SqlDbType.VarChar, 1000).Value = Producto.Codigo;
                    Comando.Parameters.Add("@Precio", SqlDbType.Float).Value = Producto.Precio;
                    Comando.Parameters.Add("@Stock", SqlDbType.Int).Value = Producto.Stock;
                    Comando.Parameters.Add("@Iva", SqlDbType.Int).Value = Producto.Iva;
                    Comando.Parameters.Add("@Peso", SqlDbType.Float).Value = Producto.Peso;
                    Comando.Parameters.Add("@IdProducto", SqlDbType.Int).Value = Producto.IdProducto;
                    Comando.CommandType = CommandType.Text;
                    Comando.ExecuteNonQuery();

                    Respuesta = "Producto Actualizado Correctamente";
                }
            }
            catch (SqlException e)
            {
               Respuesta = "Error de SQL: " + e.Message;
               Error = true;
            }

            if (Error)
                return BadRequest(Respuesta);
            else
                return CreatedAtAction(nameof(ActualizarProducto), Respuesta);
        }



        // Endpoints para eliminar productos.
        [HttpDelete("Eliminar")]
        public IActionResult EliminarProducto(int IdProducto)
        {
            String Respuesta = "";
            Boolean Error = false;
            try
            {
                DBConexion DBConn = new DBConexion();
                DBConn.abrir();
                String Query = "DELETE from dbo.producto WHERE id_producto = @IdProducto";


                using (SqlCommand Comando = new SqlCommand(Query, DBConn.Conectarbd))
                {
                    Comando.Parameters.Add("@IdProducto", SqlDbType.Int).Value = IdProducto;
                    Comando.CommandType = CommandType.Text;
                    Comando.ExecuteNonQuery();

                    Respuesta = "Producto Eliminado Correctamente";
                }
            }
            catch (SqlException e)
            {
                Respuesta = "Error de SQL: " + e.Message;
                Error = true;
            }

            if (Error)
                return BadRequest(Respuesta);
            else
                return CreatedAtAction(nameof(EliminarProducto), Respuesta);
            
        }

    }

}
