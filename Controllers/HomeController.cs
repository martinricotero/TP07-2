using Microsoft.AspNetCore.Mvc;
using TP07.Models;

namespace TP07.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult ConfigurarJuego()
        {
            ViewBag.Dificultades = Juego.ObtenerDificultades();
            ViewBag.Categorias = Juego.ObtenerCategorias();

            if (Juego.ObtenerPreguntas() == null || Juego.ObtenerPreguntas().Count == 0)
            {
                Juego.InicializarJuego();
                return RedirectToAction("ConfigurarJuego");
            }
            else
            {
                return RedirectToAction("Jugar");
            }
        }

        public IActionResult Comenzar(string username, int dificultad, int categoria)
        {
            Juego.CargarPartida(username, dificultad, categoria);

            if (Juego.ObtenerPreguntas() != null && Juego.ObtenerPreguntas().Count > 0)
            {
                return RedirectToAction("Jugar");
            }
            else
            {
                return RedirectToAction("ConfigurarJuego");
            }
        }

        public IActionResult Jugar()
        {
            ViewBag.Pregunta = Juego.ObtenerProximaPregunta();
            ViewBag.Username = Juego.ObtenerNombre();
            ViewBag.Puntaje = Juego.ObtenerPuntaje();

            if (ViewBag.Pregunta != null && Juego.ObtenerPreguntas() != null && Juego.ObtenerPreguntas().Count() > 0)
            {
                ViewBag.Respuestas = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);
                return View("Juego");
            }
            else
            {
                return View("Fin");
            }
        }

        public IActionResult VerificarRespuesta(bool respuesta)
        {
            ViewBag.Puntaje = Juego.ObtenerPuntaje();
            ViewBag.Correcto = Juego.VerificarRespuesta(respuesta);
            return View("Respuesta");
        }
    }
}
