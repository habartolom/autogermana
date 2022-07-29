using Autogermana.Test.Data;
using Autogermana.Test.Models;
using Autogermana.Test.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autogermana.Test.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public ProductosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Producto> productos = await _unitOfWork.Producto.GetAllAsync();
            return Json(new { data = productos });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductoViewModel productoVM = new ProductoViewModel()
            {
                Producto = new Producto(),
                ListaCategorias = await _unitOfWork.Categoria.GetListaSeleccionCategoriasAsync()
            };
            return View(productoVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoViewModel productoVM)
        {
            //Nuevo Artículo
            if (productoVM.Producto.Idproducto == 0)
            {
                _unitOfWork.Producto.Insert(productoVM.Producto);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            productoVM.ListaCategorias = await _unitOfWork.Categoria.GetListaSeleccionCategoriasAsync();
            return View(productoVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Producto producto = await _unitOfWork.Producto.GetByIdAsync(id);

            if (producto == null)
                return NotFound();

            ProductoViewModel productoVM = new ProductoViewModel()
            {
                Producto = producto,
                ListaCategorias = await _unitOfWork.Categoria.GetListaSeleccionCategoriasAsync()
            };
            return View(productoVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductoViewModel productoVM)
        {
            if (ModelState.IsValid)
            {
                var productoDB = await _unitOfWork.Producto.GetByIdAsync(productoVM.Producto.Idproducto);
                if (productoDB == null)
                    return NotFound();

                _unitOfWork.Producto.Update(productoVM.Producto);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            productoVM.ListaCategorias = await _unitOfWork.Categoria.GetListaSeleccionCategoriasAsync();
            return View(productoVM);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Producto producto = await _unitOfWork.Producto.GetByIdAsync(id);
            if (producto == null)
                return Json(new { success = false, message = "Error borrando producto" });

            _unitOfWork.Producto.DeleteByIdAsync(id);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Producto borrado correctamente" });
        }
    }
}
