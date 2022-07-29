using Autogermana.Test.Data;
using Autogermana.Test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autogermana.Test.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Categoria.Insert(categoria);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Categoria categoria = await _unitOfWork.Categoria.GetByIdAsync(id);
            if (categoria is null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Categoria.Update(categoria);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Categoria categoria = await _unitOfWork.Categoria.GetByIdAsync(id);
            if (categoria == null)
                return Json(new { success = false, message = "Error borrando categoria" });

            _unitOfWork.Categoria.DeleteByIdAsync(id);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Categoría borrada correctamente" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Categoria> categorias = await _unitOfWork.Categoria.GetAllAsync();
            return Json(new { data = categorias });
        }
    }
}
