using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAC.Data;
using SistemaAC.Models;
using SistemaAC.ModelsClass;

namespace SistemaAC.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CategoriaModels categoriModels;
        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
            categoriModels = new CategoriaModels(_context);
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public List<object[]> filtrarDatos(int numPagina, string valor, string order)
        {
            return categoriModels.filtarDatos(numPagina, valor, order);
        }

        public List<IdentityError> editarCategoria(int id, string nombre, string descripcion, Boolean estado, int funcion)
        {
            return categoriModels.editarCategoria(id, nombre, descripcion, estado, funcion);
        }

        public List<Categoria> getCategorias(int id)
        {
            return categoriModels.getCategorias(id);
        }
        public List<IdentityError> guardarCategoria(string nombre, string descripcion, string estado)
        {
            return categoriModels.guardarCategoria(nombre, descripcion, estado);
        }

    }
}
