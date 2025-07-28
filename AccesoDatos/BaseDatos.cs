using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class BaseDatos
    {
        private readonly string _conexionString;
        private SqlConnection _conexion;
        private SqlTransaction _transaccion;

        public BaseDatos()
        {
            this._conexionString = "Data Source=172.24.159.150,1433;Initial Catalog=ApiNet_4_7;Integrated Security=False;User ID=SA;Password=am12345;MultipleActiveResultSets=True;";
        }

        public SqlConnection Conectar()
        {
            if (_conexion == null || _conexion.State == ConnectionState.Closed || _conexion.State == ConnectionState.Broken)
            {
                _conexion = new SqlConnection(_conexionString);
                _conexion.Open();
            }
            return _conexion;
        }

        public SqlTransaction IniciarTransaccion()
        {
            if (_transaccion == null || _conexion.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("La conexión debe estar abierta para iniciar transaccion");
            }
            _transaccion = _conexion.BeginTransaction();
            return _transaccion;
        }

        public void ConfirmarTransaccion()
        {
            _transaccion.Commit();
            _transaccion = null;
        }

        public void RevertirTransaccion()
        {
            _transaccion.Rollback();
            _transaccion = null;
        }

        public SqlCommand CrearComando(string comandoSql, CommandType tipoComando = CommandType.Text, params SqlParameter[] parametros)
        {
            var comando = new SqlCommand(comandoSql, _conexion, _transaccion);
            comando.CommandType = tipoComando;
            if (parametros != null)
            {
                comando.Parameters.AddRange(parametros);
            }
            return comando;
        }

        /// <summary>
        /// Ejecuta un comando SQL que no retorna un resultado (filas)
        /// </summary>
        /// <param name="comandoSQL">Texto del comando SQL</param>
        /// <param name="tipoComando">Tipos de comando</param>
        /// <param name="parametros">Parámtros</param>
        /// <returns></returns>
        public async Task<int> EjecutarComandoAsync(string comandoSQL, CommandType tipoComando = CommandType.Text, params SqlParameter[] parametros)
        {
            var comando = CrearComando(comandoSQL, tipoComando, parametros);
            using (comando)
            {
                return await comando.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL que devuelve un único valor escalar
        /// </summary>
        /// <typeparam name="T">Tipo del valor escalar esperado</typeparam>
        /// <param name="comandoSQL">Texto del comando SQL</param>
        /// <param name="tipoComando">Tipo de comando</param>
        /// <param name="parametros">Parámetros</param>
        /// <returns></returns
        public async Task<T> EjecutarEscalarAsync<T>(string comandoSQL, CommandType tipoComando = CommandType.Text, params SqlParameter[] parametros)
        {
            var comando = CrearComando(comandoSQL, tipoComando, parametros);
            using (comando)
            {
                var result = await comando.ExecuteScalarAsync();
                if (result == DBNull.Value || result == null)
                {
                    return default(T);
                }
                return (T)Convert.ChangeType(result, typeof(T));
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL y devuelve un SqlDataReader.
        /// Es responsabilidad del llamador cerrar el SqlDataReader.
        /// </summary>
        /// <param name="commandText">El texto del comando SQL.</param>
        /// <param name="commandType">Tipo de comando.</param>
        /// <param name="parameters">Parámetros.</param>
        /// <returns>Un SqlDataReader.</returns>
        public async Task<SqlDataReader> EjecutarLectorAsync(string comandoSQL, CommandType tipoComando = CommandType.Text, params SqlParameter[] parametros)
        {
            using (var comando = CrearComando(comandoSQL, tipoComando, parametros))
            {
                return await comando.ExecuteReaderAsync();
            }
        }

        /// <summary>
        /// Implementación de IDisposable para asegurar que la conexión se cierre.
        /// </summary>
        public void Dispose()
        {
            if(_transaccion != null)
            {
                try
                {
                    _transaccion.Rollback();
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error al revertir la transacción en Dispose: {e.Message}");
                }
                finally
                {
                    _transaccion = null;
                }
            }

            if(_conexion == null && _conexion.State != ConnectionState.Closed)
            {
                _conexion.Close();
                _conexion.Dispose();
            }
        }
    }
}
