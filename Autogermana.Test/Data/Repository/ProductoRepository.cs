using Autogermana.Test.Models;
using Newtonsoft.Json;
using System.Linq;

namespace Autogermana.Test.Data.Repository
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Producto producto)
        {
            var productoDB = _db.Productos.FirstOrDefault(s => s.Idproducto == producto.Idproducto);

            var productoJson = JsonConvert.SerializeObject(producto);
            JsonConvert.PopulateObject(productoJson, productoDB);
        }
    }
}
