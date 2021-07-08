using ControlIngresosGasto.Data;
using ControlIngresosGasto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIngresosGasto.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly ApplicationDbContext _context; // Llama  alos metodos para guardar
        public EspecialidadController(ApplicationDbContext context)
        {
            _context = context; //context es invocado
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Especialidads.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var especialidad = await _context.Especialidads.FindAsync(id);

            if (especialidad == null)
            {
                return NotFound();
            }
            return View(especialidad);
        }

        [HttpPost] // Esto diferencia el metodo Edit que graba el que solo previsualiza
        public async Task<IActionResult> Edit(int id, [Bind("idEspecialidad, Descripcion")] Especialidad especialidad)
        {
            if (id != especialidad.idEspecialidad)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Update(especialidad); // actualizar los datos
                await _context.SaveChangesAsync(); // Salva los datoss
                return RedirectToAction(nameof(Index)); // redireccina al index
            }
            return View(especialidad);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var especialidad = await _context.Especialidads.FirstOrDefaultAsync(e => e.idEspecialidad == id);

            if (especialidad == null)
            {
                return NotFound();
            }
            return View(especialidad);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var especialidad = await _context.Especialidads.FindAsync(id);
            _context.Especialidads.Remove(especialidad); // Eliminar la especialidad
            await _context.SaveChangesAsync(); // Guardar definitivamente la eliminacion
            return RedirectToAction(nameof(Index));
        }

        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idEspecialidad, Descripcion")] Especialidad especialidad)
        { 
            if(ModelState.IsValid)
            {
                _context.Add(especialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
