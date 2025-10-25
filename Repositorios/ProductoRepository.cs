using Microsoft.Data.Sqlite;
using espacioProductos;
namespace espacioProdRepo
{
    public class ProductoRepository
    {
        string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "nuevo.db");

        // Asigna la cadena de conexión
        string cadenaConexion;
        public ProductoRepository()
        {
            // La conexión debe usar la ruta absoluta
            cadenaConexion = $"Data Source={dbPath};";
        }
        public List<Productos> GetAllProductos()
        {
            //Consultar a la bd con el select construir la lista en el bucle y retornar
            var productos = new List<Productos>();

            using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = "SELECT * FROM Productos";
                using (SqliteCommand selectCmd = new SqliteCommand(consulta, conexion))
                using (SqliteDataReader reader = selectCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var id = Convert.ToInt32(reader["idProducto"]);
                        var descripcion = reader["Descripcion"].ToString();
                        var precio = Convert.ToInt32(reader["Precio"]);

                        var nuevoProducto = new Productos(id, descripcion, precio);
                        productos.Add(nuevoProducto);
                    }

                    return productos;
                }

                conexion.Close();
            }
        }

        public void InsertarProducto(Productos nuevoProducto)
        {
            using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = "INSERT INTO Productos(Descripcion,Precio) VALUES (@Descripcion,@Precio)"; //consulta
                using var insertCmd = new SqliteCommand(consulta, conexion); 

                insertCmd.Parameters.Add(new SqliteParameter("@Descripcion", nuevoProducto.descripcion));
                insertCmd.Parameters.Add(new SqliteParameter("@Precio", nuevoProducto.precio));
                insertCmd.ExecuteNonQuery();
                    
            }
        }
    }
}