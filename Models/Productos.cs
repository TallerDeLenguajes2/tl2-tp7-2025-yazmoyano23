namespace espacioProductos
{
    public class Productos
    {
        public int idProducto {get;set;}
        public string descripcion {get;set;}
        public int precio {get;set;}

        public Productos();
        
        public void Productos(int id,string descripcion, int precio){
            this.idProducto = id;
            this.descripcion = descripcion;
            this.precio = precio;
        }
    }

}
