 using Microsoft.AspNetCore.Mvc;
 using espacioProdRepo;

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
//A partir de aquí van todos los Action Methods (Get, Post,etc.)

    [HttpGet("GetProductos")]
    public ActionResult<List<Productos>> GetProductos(){

        List<Producto> listProductos;
        listProductos=productoRepository.GetAllProductos();
        return Ok(listProductos);

    }
}


