using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers;

[ApiController]
[Route("api/autores")]
public class AutoresController:ControllerBase
{

    private readonly AplicationDbContext context;
    public AutoresController(AplicationDbContext context)
    {
        this.context = context;
    }

    //Ver un autor
    [HttpGet]
    public async Task< ActionResult<List<Autor>>> Get()
    {
        return await context.Autores.ToListAsync();
        
    }
    //Crear un autor
    [HttpPost]
    public async Task<ActionResult> Insertar(Autor autor)
    {

        context.Add(autor);
        await context.SaveChangesAsync();

        return Ok(autor);
    }
    //Actualizar un autor
    [HttpPut("{id:int}")] //api/autores/2
    public async Task<ActionResult> Actualizar (Autor autor, int id)
    {
        if (autor.id != id)
        {
            //BadRequest es el error 400
            return BadRequest("El id autor no es igual ");
        }
        var existe = await context.Autores.AnyAsync(x => x.id == id);
        if (!existe)
        {
            return NotFound();
        }
        context.Update(autor);
        await context.SaveChangesAsync();
        return Ok();

    }
    //Eliminar un autor
    [HttpDelete("{id:int}")]//api/autores/2
    public async Task<ActionResult> Eliminar( int id)
    {
        var existe = await context.Autores.AnyAsync(x => x.id == id);
        if (!existe)
        {
            return NotFound();
        }
        context.Remove(new Autor() { id = id });
        await context.SaveChangesAsync();

        return Ok();
    }
}
