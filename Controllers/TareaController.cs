using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//***** AGREGO USING
using Microsoft.EntityFrameworkCore;
using ProyectoAngularApiCORS.Models;
using System.Threading;
//..

namespace ProyectoAngularApiCORS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {

        private readonly DBAngularContext _baseDatos;

        public TareaController(DBAngularContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var listaTareas = await _baseDatos.Tareas.ToListAsync();
            return Ok(listaTareas);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar( [FromBody] Tarea request)
        {
            await _baseDatos.Tareas.AddAsync(request);
            await _baseDatos.SaveChangesAsync();
            return Ok(request);
        }

        [HttpDelete]
        [Route("Elimnar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var vTareaEliminar = await _baseDatos.Tareas.FindAsync(id);
            if(vTareaEliminar == null)
                return BadRequest("No existe Tarea a Eliminar");

            _baseDatos.Tareas.Remove(vTareaEliminar);
            await _baseDatos.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("Modificar/{id:int}")]
        public async Task<IActionResult> Modificar(int id, [FromBody] Tarea request)
        {
            var vTareaModicicar = await _baseDatos.Tareas.FindAsync(id);
            if (vTareaModicicar == null)
                return BadRequest("No existe Tarea a Modificar");

            vTareaModicicar.IdTarea = request.IdTarea;
            vTareaModicicar.Nombre = request.Nombre;
            await _baseDatos.SaveChangesAsync();
            return Ok(request);
        }
    }
}
