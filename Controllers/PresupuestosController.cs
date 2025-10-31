using Microsoft.AspNetCore.Mvc;
using repoPresupuesto;
using espacioPresupuesto;
namespace tl2_tp7_2025_yazmoyano23.Controllers;

[ApiController]
[Route("[controller]")]

public class PresupuestosController : ControllerBase
{
    private PresupuestoRepository repoPresupuesto;

    public PresupuestosController()
    {
        repoPresupuesto = new PresupuestoRepository();
    }

    [HttpPost("AltaPresupuesto")]
    public ActionResult<string> AltaPresupuesto(Presupuestos nuevoPresupuesto)
    {
        repoPresupuesto.InsertarPresupuesto(nuevoPresupuesto);
        return Ok("Presupuesto dado de alta exitosamente");
    }

    [HttpGet("GetPresupuestos")]
    public ActionResult<List<Presupuestos>> ObtenerPresupuestos()
    {

        List<Presupuestos> lista;
        lista = repoPresupuesto.GetAllPresupuestos();
        return Ok(lista);

    }
}