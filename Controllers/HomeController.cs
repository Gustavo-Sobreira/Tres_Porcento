using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TresPorcento.Models;

namespace TresPorcento.Controllers;

public class HomeController : Controller
{
    private static readonly System.Random random = new System.Random();
    private static readonly decimal _numeroCorreto = random.Next(0,100);
    private static decimal _numUsuario = 0;
    private static decimal _vidasTotais = 3;

    public static  decimal LimiteMenorNumero { get; private set; }
    public static decimal LimiteMaiorNumero { get; private set; }
    public static decimal MostrarChancesDeAcerto { get; private set; }

    [HttpGet]
    public IActionResult Index()
    {
        MostrarChancesDeAcerto = 3;
        LimiteMenorNumero = 0;
        LimiteMaiorNumero = 100;
        _vidasTotais = 3;
        return View();
    }

    [HttpPost]
    public IActionResult Index(decimal numeroEscolhidoPeloUsuario)
    {
        if (_vidasTotais > 0)
        {
            _numUsuario = numeroEscolhidoPeloUsuario;
            CalcularChanceDeAcerto();
            return View();
        }
        return View();
    }

    private void CalcularChanceDeAcerto()
    {
        if (_numUsuario == _numeroCorreto)
        {
            MostrarChancesDeAcerto = 100;
        }
        else
        {
            _vidasTotais--;
            AtualizarLimites();
            MostrarChancesDeAcerto = _vidasTotais / (LimiteMaiorNumero - LimiteMenorNumero) * 100;
        }
    }

    private void AtualizarLimites()
    {
        _numUsuario = _numUsuario < _numeroCorreto ? LimiteMenorNumero = _numUsuario : LimiteMaiorNumero = _numUsuario;
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
