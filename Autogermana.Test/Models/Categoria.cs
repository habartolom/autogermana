using System;
using System.Collections.Generic;

#nullable disable

namespace Autogermana.Test.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int Idcategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
