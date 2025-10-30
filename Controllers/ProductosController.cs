 using Microsoft.AspNetCore.Mvc;
 using espacioProdRepo;
using espacioProductos;

 namespace tl2_tp7_2025_yazmoyano23.Controllers;

[ApiController]
[Route("[controller]")]

public class ProductosController : ControllerBase
{
    private ProductoRepository productoRepository;
    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }

    [HttpGet("GetProductos")]
    public ActionResult<List<Productos>> ObtenerProductos()
    {

        List<Productos> listaProductos;
        listaProductos = productoRepository.GetAllProductos();
        return Ok(listaProductos);

    }

    [HttpPost("AltaProducto")]
    public ActionResult<string> AltaProducto(Productos nuevoProducto)
    {
        productoRepository.InsertarProducto(nuevoProducto);
        return Ok("Producto dado de alta exitosamente");
    }

    //PUT /api/Producto/{Id}: Permite modificar un nombre de un Producto.
    [HttpPut("ModificarNombre")]
    public ActionResult ModificarNombre([FromForm] int idProducto, [FromForm] string nuevoNombre)
    {
        productoRepository.ModificarNombreProducto(idProducto, nuevoNombre);
        return Ok("Nombre actualizado correctamente");
    }

    [HttpGet("{idProducto}")]
    public ActionResult<string> ObtenerDetalle(int idProducto)
    {
        var descripcion = productoRepository.GetDescripcion(idProducto);
        if (descripcion == null)
        {
            return NotFound($"No se encontro un producto con ID {idProducto}");
        }

        return Ok(descripcion);
    }

    [HttpDelete("{idProducto}")]
    public ActionResult EliminarProducto(int idProducto)
    {
        bool eliminado = productoRepository.Eliminar(idProducto);
        if (eliminado)
        {
            return NoContent(); //HTTP 204 (Eliminacion exitosa)
        }else
        {
            return NotFound($"No se encontró el producto con ID {idProducto} para eliminar");
        }
    }



}

