using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Trabajo.Models; // Asegúrate de que el modelo RegisterViewModel esté en el espacio de nombres correcto

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // Muestra la vista de registro
    public IActionResult Register()
    {
        return View();
    }

    // Maneja el registro de un nuevo usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Asignar el rol por defecto
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                await _userManager.AddToRoleAsync(user, "User");

                // Redirige al usuario después del registro exitoso
                return RedirectToAction("Index", "Home");
            }

            // Agregar errores al modelo si el registro falla
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // Vuelve a mostrar el formulario de registro con errores
        return View(model);
    }
}
