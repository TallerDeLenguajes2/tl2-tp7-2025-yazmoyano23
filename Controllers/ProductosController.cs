 using Microsoft.AspNetCore.Mvc;
 using espacioProdRepo;
using espacioProductos;

 namespace tl2_tp7_2025_yazmoyano23.Controllers;

[ApiController]
[Route("[controller]")]

public class ProductosController: ControllerBase
{
    private ProductoRepository productoRepository;
    public ProductosController()
    {
        productoRepository= new ProductoRepository();
    }

    [HttpGet("GetProductos")]
    public ActionResult<List<Productos>> GetProductos(){

        List<Productos> listaProductos;
        listaProductos=productoRepository.GetAllProductos();
        return Ok(listaProductos);

    }
}


