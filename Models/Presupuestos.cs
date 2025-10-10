using espacioDetalleP;
namespace espacioPresupuesto
{
    public class Presupuestos
    {
        int idPresupuesto {get;set;}
        string nombreDestinatario {get;set;}
        //date fechaCreacion {get;set;}
        List<PresupuestosDetalle> detalle {get;set;}

        //Agregar metodos
    }
}