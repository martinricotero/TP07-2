using TP07.Models;
public class Juego{
    private static string? _username{get;set;}
    private static int _puntajeActual{get;set;}
    private static int _cantidadPreguntasCorrectas{get;set;}
    private static List<Preguntas> _preguntas{get;set;}
    private static List<Respuestas> _respuestas{get;set;}
    public static void InicializarJuego(){
        _username = null;
        _puntajeActual = 0;
        _cantidadPreguntasCorrectas = 0;
    }
    public static string ObtenerNombre()
    {
        return _username;
    }
    public static int ObtenerPuntaje()
    {
        return _puntajeActual;
    }
    public static List<Categoria> ObtenerCategorias(){
        return BD.ObtenerCategorias();
    }
    public static List<Dificultades> ObtenerDificultades(){
        return BD.ObtenerDificultades();
    }
   public static void CargarPartida(string username, int dificultad, int categoria)
{
    _username = username;
    _preguntas = BD.ObtenerPreguntas(dificultad, categoria);
    _respuestas = BD.ObtenerRespuestas(_preguntas); // Aquí pasamos la lista de preguntas
}

    public static List<Preguntas> ObtenerPreguntas(){
        return _preguntas;
    }
    public static Preguntas ObtenerProximaPregunta()
{
    Preguntas preguntar;
    if (_preguntas != null && _preguntas.Count > 0)
    {
        return preguntar = _preguntas[0];
    }
    else
    {
        return null;
    }
}

    public static List<Respuestas> ObtenerProximasRespuestas(int idPregunta){
        List<Respuestas> respuestas = new List<Respuestas>{};
        foreach (var item in _respuestas)
        {
            if (item.IdPregunta == idPregunta)
            {
                respuestas.Add(item);
            }
        }
        return respuestas;
    }
    public static bool VerificarRespuesta(bool respuesta){
        bool t;
        if(respuesta){
            _puntajeActual+=5;
            _preguntas.RemoveAt(0);
            t = true;
        }
        else
        {
            _preguntas.RemoveAt(0);
            t = false;
        }
        return t;
    }
}

