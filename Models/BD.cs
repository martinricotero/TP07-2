using Dapper;
using System.Data.SqlClient;

namespace TP07.Models
{
    public class BD
    {
        private static string _connectionString = @"Server=DESKTOP-72CATTS\SQLEXPRESS; Database=Preguntados; Trusted_Connection=True";

        public static List<Categoria> ObtenerCategorias()
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string sql = "SELECT [IdCategoria], [Nombre], [Foto] FROM [dbo].[Categoria];";
                return conexion.Query<Categoria>(sql).ToList();
            }
        }

        public static List<Dificultades> ObtenerDificultades()
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string sql = "SELECT [IdDificultad], [nombre] FROM [dbo].[Dificultades];";
                return conexion.Query<Dificultades>(sql).ToList();
            }
        }

        public static List<Preguntas> ObtenerPreguntas(int idDificultad, int idCategoria)
        {
            List<Preguntas> info = new List<Preguntas> { };
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                if (idDificultad == -1 && idCategoria == -1)
                {
                    string sql = "SELECT [IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto] FROM [dbo].[Preguntas];";
                    info = conexion.Query<Preguntas>(sql).ToList();
                }
                else if (idDificultad == -1)
                {
                    string sql = "SELECT [IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto] FROM [dbo].[Preguntas] WHERE IdCategoria = @idCategoria;";
                    info = conexion.Query<Preguntas>(sql, new { IdCategoria = idCategoria }).ToList();
                }
                else if (idCategoria == -1)
                {
                    string sql = "SELECT [IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto] FROM [dbo].[Preguntas] WHERE IdDificultad = @idDificultad;";
                    info = conexion.Query<Preguntas>(sql, new { idDificultad = idDificultad }).ToList();
                }
                else
                {
                    string sql = "SELECT [IdPregunta], [IdCategoria], [IdDificultad], [Enunciado], [Foto] FROM [dbo].[Preguntas] WHERE IdCategoria = @idCategoria AND IdDificultad = @idDificultad";
                    info = conexion.Query<Preguntas>(sql, new { IdCategoria = idCategoria, idDificultad = idDificultad }).ToList();
                }
            }
            return info;
        }

        public static List<Respuestas> ObtenerRespuestas(List<Preguntas> preguntas)
        {
            List<Respuestas> info = new List<Respuestas> { };
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string sql = "SELECT [IdRespuesta], [IdPregunta], [Opcion], [Contenido], [Correcta], [Foto] FROM [dbo].[Respuestas]";
                info = conexion.Query<Respuestas>(sql).ToList();
            }
            return info;
        }
    }
}
