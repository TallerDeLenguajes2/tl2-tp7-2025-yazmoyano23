using Microsoft.Data.Sqlite;
using espacioProductos;
namespace espacioProdRepo
{
    public class ProductoRepository : IRepository
    {

        string connectionString = "Data Source=Tienda.db;";

        public List<Productos> GetAllProductos(){
            //Consultar a la bd con el select construir la lista en el bucle y retornar
            var productos = new List<Productos>();

            using (SqliteConnection connection = new SqliteConnection(connectionString)){
                    connection.Open();
                    string selectQuery = "SELECT * FROM productos";
                    using (SqliteCommand selectCmd = new SqliteCommand(selectQuery, connection))
                    using (SqliteDataReader reader = selectCmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            id= Convert.ToInt32(reader["id"]);
                            descripcion = reader["nombre"].ToString();
                            precio= Convert.ToInt32(reader["precio"]);

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