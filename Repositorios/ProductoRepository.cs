using Microsoft.Data.Sqlite;
using espacioProductos;
namespace espacioProdRepo
{
    public class ProductoRepository
    {
        string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "nuevo.db");
    
    // Asigna la cadena de conexión
        string connectionString;
        public ProductoRepository()
        {
            // La conexión debe usar la ruta absoluta
            connectionString = $"Data Source={dbPath};"; 
        }
        public List<Productos> GetAllProductos(){
            //Consultar a la bd con el select construir la lista en el bucle y retornar
            var productos = new List<Productos>();

            using (SqliteConnection connection = new SqliteConnection(connectionString)){
                    connection.Open();
                string selectQuery = "SELECT * FROM Productos";
                    using (SqliteCommand selectCmd = new SqliteCommand(selectQuery, connection))
                    using (SqliteDataReader reader = selectCmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            var id = Convert.ToInt32(reader["idProducto"]);
                            var descripcion = reader["Descripcion"].ToString();
                            var precio= Convert.ToInt32(reader["Precio"]);

                            var nuevoProducto = new Productos(id,descripcion,precio);
                            productos.Add(nuevoProducto);
                        }

                        return productos;
                    }

                    connection.Close();
            }
        }
    }
}