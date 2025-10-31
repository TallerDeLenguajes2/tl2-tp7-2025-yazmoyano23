using Microsoft.Data.Sqlite;
using espacioPresupuesto;

namespace repoPresupuesto
{
    public class PresupuestoRepository
    {
        string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "nuevo.db");

        string cadenaConexion;

        public PresupuestoRepository()
        {
            // La conexión debe usar la ruta absoluta
            cadenaConexion = $"Data Source={dbPath};";
        }

        //Crear un nuevo Presupuesto. (recibe un objeto Presupuesto)

        public void InsertarPresupuesto(Presupuestos nuevoPresupuesto)
        {
            using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@Destinatario,@Fecha)"; //consulta
                using var insertCmd = new SqliteCommand(consulta, conexion);

                insertCmd.Parameters.Add(new SqliteParameter("@Destinatario", nuevoPresupuesto.NombreDestinatario));
                insertCmd.Parameters.Add(new SqliteParameter("@Fecha", nuevoPresupuesto.FechaCreacion));
                insertCmd.ExecuteNonQuery();

            }
        }


        public List<Presupuestos> GetAllPresupuestos()
        {
            //Consultar a la bd con el select construir la lista en el bucle y retornar
            var presupuestos = new List<Presupuestos>();

            using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = "SELECT * FROM Presupuestos";
                using (SqliteCommand selectCmd = new SqliteCommand(consulta, conexion))
                using (SqliteDataReader reader = selectCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var id = Convert.ToInt32(reader["idPresupuesto"]);
                        var destinatario = reader["NombreDestinatario"].ToString();
                        var fecha = Convert.ToDateTime(reader["FechaCreacion"]);

                        var nuevoPresupuesto = new Presupuestos(id, destinatario, fecha);
                        presupuestos.Add(nuevoPresupuesto);
                    }
                    conexion.Close();

                }

                return presupuestos;
            }
        }
        
        //Obtener detalles de un Presupuesto por su ID. (recibe un Id y devuelve un
        //Presupuesto con sus productos y cantidades)

        public Presupuesto GetById(int idPresupuesto)
        {
            Presupuesto presupuesto = new Presupuesto();
            int presupuestoEncontrado = 0;
            string query = @"SELECT 
                        P.idPresupuesto,
                        P.NombreDestinatario,
                        P.FechaCreacion,
                        PR.idProducto,
                        PR.Descripcion AS Producto,
                        PR.Precio,
                        PD.Cantidad,
                        (PR.Precio * PD.Cantidad) AS Subtotal
                    FROM 
                        Presupuestos P
                    LEFT JOIN 
                        PresupuestosDetalle PD ON P.idPresupuesto = PD.idPresupuesto
                    LEFT JOIN 
                        Productos PR ON PD.idProducto = PR.idProducto
                    WHERE 
                        P.idPresupuesto = @idPresupuesto;";

            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Si encuentro un presupuesto
                        if (presupuestoEncontrado == 0)
                        {
                            presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                            presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                            presupuesto.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
                            presupuesto.Detalle = [];
                            presupuestoEncontrado++;
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("idProducto")))
                        {
                            var producto = new Producto();
                            producto.Idproducto = Convert.ToInt32(reader["idProducto"]);
                            producto.Descripcion = reader["Producto"].ToString();
                            producto.Precio = Convert.ToInt32(reader["Precio"]);
                            PresupuestoDetalle detalle = new PresupuestoDetalle(producto, Convert.ToInt32(reader["Cantidad"]));
                            presupuesto.Detalle.Add(detalle);
                        }
                    }
                }
                connection.Close();
            }
            return presupuesto;
        }
    }


}
