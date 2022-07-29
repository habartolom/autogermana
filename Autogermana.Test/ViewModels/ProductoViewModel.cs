using Autogermana.Test.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Autogermana.Test.ViewModels
{
    public class ProductoViewModel
    {
        public Producto Producto { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
