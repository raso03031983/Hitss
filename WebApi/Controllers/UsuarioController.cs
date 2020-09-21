using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Service;
using WebApi.Data;
using WebApi.Models;


[Route("v1/usuarios")]
public class UsuarioController : Controller
{

  [HttpGet]
  public async Task<ActionResult<List<Usuario>>> Get([FromServices] DataContext context)
  {
    try
    {
      var item = await context.Usuarios.AsNoTracking().ToListAsync();
      return Ok(item);
    }
    catch (Exception error)
    {
      return BadRequest(error);
    }
  }


  [HttpPost]
  [Route("")]
  public async Task<ActionResult<Usuario>> Post([FromBody] Usuario model, [FromServices] DataContext context)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    else
    {
      try
      {
        context.Usuarios.Add(model);
        await context.SaveChangesAsync();
        return Ok(model);
      }
      catch (Exception error)
      {
        return BadRequest(new { message = error.Message });
      }
    }
  }

  [HttpPost]
  [Route("login")]
  public async Task<ActionResult<dynamic>> Authenticate([FromBody] Usuario model, [FromServices] DataContext context)
  {
    try
    {
      var user = await context.Usuarios.AsNoTracking().Where(x => x.Login == model.Login && x.Senha == model.Senha).FirstOrDefaultAsync();

      if (user == null)
      {
        return NotFound(new { message = "usuário ou senha incorretos" });
      }
      else
      {
        var tokken = TokkenService.GenerateTokken(user);
        return Ok(new
        {
          //user = user,
          tokken = tokken
        });
      }

    }
    catch (Exception error)
    {
      return BadRequest(new { message = error.Message });
    }
  }
}







