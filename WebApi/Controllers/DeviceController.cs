using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

[Route("v1/devices")]
public class DeviceController : Controller
{

  // [Authorize]
  [HttpGet]
  public async Task<ActionResult<List<Device>>> Get([FromServices] DataContext context)
  {
    try
    {
      var item = await context.Devices.AsNoTracking().ToListAsync().OrderByDescending();
      return Ok(item);
    }
    catch (Exception error)
    {
      return BadRequest(error);
    }
  }

  [HttpGet]

  [Route("{id:int}")]
  public async Task<ActionResult<Device>> GetById(int id, [FromServices] DataContext context)
  {
    try
    {
      var item = await context.Devices.AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
      return Ok(item);
    }
    catch (Exception error)
    {

      return BadRequest(new { message = error.Message });
    }

  }

  [HttpPost]
  [Route("")]
  public async Task<ActionResult<Device>> Post([FromBody] Device model, [FromServices] DataContext context)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    else
    {
      try
      {
        context.Devices.Add(model);
        await context.SaveChangesAsync();
        return Ok(model);
      }
      catch (Exception error)
      {
        return BadRequest(new { message = error.Message });
      }
    }
  }

  [HttpPut]
  [Route("{id:int}")]
  public async Task<ActionResult<Device>> Put(int id, [FromBody] Device model, [FromServices] DataContext context)
  {
    try
    {
      if (model.id == id)
      {
        context.Entry<Device>(model).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return Ok(model);
      }
      else
      {
        return NotFound(new { message = "Device não encontrado" });
      }
    }
    catch (DbUpdateConcurrencyException)
    {
      return BadRequest(new { message = "Item esta sendo atualizado neste momento, tente mais tarde" });
    }
    catch (Exception error)
    {
      return BadRequest(new { message = error.Message });
    }
  }

  [HttpDelete]
  [Route("{id:int}")]
  public async Task<ActionResult<Device>> Delete(int id, [FromServices] DataContext context)
  {

    var item = await context.Devices.FirstOrDefaultAsync(x => x.id == id);
    if (item == null)
    {
      return NotFound("Device não encontrado");
    }

    try
    {
      context.Devices.Remove(item);
      await context.SaveChangesAsync();
      return Ok(new { message = "Transação Realizada" });
    }
    catch (Exception error)
    {
      return BadRequest(new { message = error.Message });
    }
  }
}