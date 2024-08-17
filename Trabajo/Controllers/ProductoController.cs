using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabajo.Models;
using System.Linq;
using System.Threading.Tasks;
using Trabajo.Data;

public class ProductoController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductoController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();
        return View(productos);
    }

    public IActionResult Create(Producto producto)
    {
        if (ModelState.IsValid)
        {
            // Aquí se agregarían los datos a la base de datos
            return RedirectToAction(nameof(Index));
        }
        return View(producto);
    }

    // Otras acciones para Crear, Editar, Eliminar, etc.
}
