using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crudProdutosBA.Models
{
    public class Producto
    {
        public int IdProducto{ get; set; }
        public Marca Marca { get; set; }
        public Proveedor Proveedor { get; set; }
        public Zona Zona     { get; set; }
        public Presentacion Presentacion { get; set; }
        public int Codigo { get; set; }
        public String Descripcion { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public int Iva { get; set; }
        public double Peso { get; set; }
    }
}


namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}