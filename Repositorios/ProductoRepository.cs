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

                insertCmd.Parameters.Add(new SqliteParameter("@Descripcion", nuevoProducto.Descripcion));
                insertCmd.Parameters.Add(new SqliteParameter("@Precio", nuevoProducto.Precio));
                insertCmd.ExecuteNonQuery();

            }
        }

        public void ModificarNombreProducto(int idProducto, string nuevoNombre)
        {
            using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = @"UPDATE Productos 
                                    SET Descripcion = @Descripcion 
                                    WHERE idProducto = @idProducto";
                using var updateCmd = new SqliteCommand(consulta, conexion);

                updateCmd.Parameters.Add(new SqliteParameter("@Descripcion", nuevoNombre));
                updateCmd.Parameters.Add(new SqliteParameter("@idProducto", idProducto));
                updateCmd.ExecuteNonQuery();
            }
        }

        public string GetDescripcion(int idProducto)
        {
            using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = @"SELECT Descripcion 
                                    FROM Productos
                                    WHERE idProducto = @idProducto";
                using var selectCmd = new SqliteCommand(consulta, conexion);

                selectCmd.Parameters.Add(new SqliteParameter("@idProducto", idProducto));

                object? resultado = selectCmd.ExecuteScalar(); //Devuelve resultado de la consulta o null

                return resultado.ToString(); //Paso de object a string 


            }
        }

        public bool Eliminar(int idProducto)
        {
            using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = "DELETE FROM Productos WHERE idProducto = @idProducto";

                using var deleteCmd = new SqliteCommand(consulta, conexion);
                deleteCmd.Parameters.Add(new SqliteParameter("@idProducto", idProducto));
                int filasAfectadas = deleteCmd.ExecuteNonQuery();

                //Devuelve true si al menos una fila fue eliminada
                return filasAfectadas > 0;
            }
        }
    }
}