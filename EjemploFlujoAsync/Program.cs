using EjemploFlujoAsync;
using System.Diagnostics; // podemos hacer analisis de tiempos

// iniciamos un contador de tiempo - SINCRONA
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

Console.WriteLine("\nBienvenido a la calculadora de Hipotecas sincrona");

var aniosVidaLabora = CalculadoraHipotecaSync.ObtenerAniosVidaLaboral();
Console.WriteLine($"\nAños de vida laboral: {aniosVidaLabora}");

var esTipoContratoIndefinido = CalculadoraHipotecaSync.EsTipoContratoIndefinido();
Console.WriteLine($"\nTipo de contrato indefinido: {esTipoContratoIndefinido}");

var sueldoNeto = CalculadoraHipotecaSync.ObtenerSueldoNeto();
Console.WriteLine($"\nSueldo neto: {sueldoNeto}");

var obtenerGastos = CalculadoraHipotecaSync.ObtenerGastosMensuales();
Console.WriteLine($"\nGastos mensuales: {obtenerGastos}");

var hipotecaConcedida = CalculadoraHipotecaSync.AnalisarInformacionParaConcederHipoteca(
    aniosVidaLabora,
    esTipoContratoIndefinido,
    sueldoNeto,
    obtenerGastos,
    cantidadSolicitada: 100000, // cantidad solicitada
    aniosPagar: 20 // años a pagar
);
var resultado = hipotecaConcedida ? "APROBADA" : "DENEGADA";
Console.WriteLine($"\nAnalisis Finalizado. Su solicitud de hipoteca ha sido: {resultado}");

stopwatch.Stop();

Console.WriteLine($"\nLa operacion ha durado: {stopwatch.Elapsed}");

// REINIIAMOS un contador de tiempo - ASINCRONA
stopwatch.Restart();
Console.WriteLine("\n*****************************************************************");
Console.WriteLine("\nBienvenido a la calculadora de Hipoteca Asincrona");


Task<int> aniosVidaLaboraTask = CalculadoraHipotecaAsync.ObtenerAniosVidaLaboral();
Task<bool> esTipoContratoIndefinidoTask = CalculadoraHipotecaAsync.EsTipoContratoIndefinido();
Task<int> sueldoNetoTask = CalculadoraHipotecaAsync.ObtenerSueldoNeto();
Task<int> obtenerGastosTask = CalculadoraHipotecaAsync.ObtenerGastosMensuales();

var analisisHipotecaTask = new List<Task>
{
    aniosVidaLaboraTask,
    esTipoContratoIndefinidoTask,
    sueldoNetoTask,
    obtenerGastosTask
};

while(analisisHipotecaTask.Any())
{
    Task tareaFinalizada = await Task.WhenAny(analisisHipotecaTask);
    if (tareaFinalizada == aniosVidaLaboraTask) Console.WriteLine($"\nAños de vida laboral: {aniosVidaLaboraTask.Result}");
    if(tareaFinalizada == esTipoContratoIndefinidoTask) Console.WriteLine($"\nTipo de contrato indefinido: {esTipoContratoIndefinidoTask.Result}");
    if (tareaFinalizada == sueldoNetoTask) Console.WriteLine($"\nSueldo neto: {sueldoNetoTask.Result}");
    if (tareaFinalizada == obtenerGastosTask) Console.WriteLine($"\nGastos mensuales: {obtenerGastosTask.Result}");

    analisisHipotecaTask.Remove(tareaFinalizada); // eliminamos de la lista de tareas
}

var hipotecaAsyncConcedida = CalculadoraHipotecaAsync.AnalisarInformacionParaConcederHipoteca(
    aniosVidaLaboraTask.Result,
    esTipoContratoIndefinidoTask.Result,
    sueldoNetoTask.Result,
    obtenerGastosTask.Result,
    cantidadSolicitada: 100000, // cantidad solicitada
    aniosPagar: 20 // años a pagar
);
var resultadoAsync = hipotecaAsyncConcedida ? "APROBADA" : "DENEGADA";
Console.WriteLine($"\nAnalisis Finalizado. Su solicitud de hipoteca ha sido: {resultadoAsync}");

stopwatch.Stop();
Console.WriteLine($"\nLa operacion sincrona ha durado: {stopwatch.Elapsed}");
Console.Read(); // permite que la consola no se cierre
