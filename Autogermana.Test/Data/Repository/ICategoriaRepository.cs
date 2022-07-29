using Autogermana.Test.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autogermana.Test.Data.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<SelectListItem>> GetListaSeleccionCategoriasAsync();
        void Update(Categoria categoria);
    }
}
