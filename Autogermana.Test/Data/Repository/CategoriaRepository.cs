using Autogermana.Test.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autogermana.Test.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<SelectListItem>> GetListaSeleccionCategoriasAsync()
        {
            var categorias = await GetAllAsync();
            return categorias.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Idcategoria.ToString(),
            });
        }

        public void Update(Categoria categoria)
        {
            var categoriaDB = _db.Categorias.FirstOrDefault(s => s.Idcategoria == categoria.Idcategoria);

            if (categoriaDB != null)
            {
                var categoriaJson = JsonConvert.SerializeObject(categoria);
                JsonConvert.PopulateObject(categoriaJson, categoriaDB);
            }


        }

    }
}
