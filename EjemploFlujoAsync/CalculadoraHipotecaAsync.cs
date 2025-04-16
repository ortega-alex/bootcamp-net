using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploFlujoAsync
{
    internal class CalculadoraHipotecaAsync
    {
        public static async Task<int> ObtenerAniosVidaLaboral()
        {
            Console.WriteLine("\nObteniendo años de vida laboral....");
            await Task.Delay(5000); // esperamos 5 segundos
            return new Random().Next(1, 35); // devolvemos un número aleatorio entre 1 y 35
        }

        public static async Task<bool> EsTipoContratoIndefinido()
        {
            Console.WriteLine("\nVerificando si el tipo de contrato es indefinido");
            await Task.Delay(5000);
            return new Random().Next(1, 10) % 2 == 0; //devolvemos un booleano aleatorio
        }

        public static async Task<int> ObtenerSueldoNeto()
        {
            Console.WriteLine("\nObteniendo sueldo neto");
            await Task.Delay(5000);
            return new Random().Next(800, 6000); //devolvemos un número aleatorio entre 800 y 6000
        }

        public static async Task<int> ObtenerGastosMensuales()
        {
            Console.WriteLine("\nObteniendo gastos mensuales");
            await Task.Delay(10000);
            return new Random().Next(200, 2000); //devolvemos un número aleatorio entre 200 y 2000
        }

        public static bool AnalisarInformacionParaConcederHipoteca(
         int aniosVidaLaboral,
         bool tipoContratoEsIndefinido,
         int sueldoNeto,
         int gastosMensuales,
         int cantidadSolicitada,
         int aniosPagar
         )
        {
            Console.WriteLine("\nAnalizando informacion para conceder hipoteca");
            if (aniosVidaLaboral < 2) return false;

            // obtener la cuota
            var cuota = (cantidadSolicitada / aniosPagar) / 12; // cuota mensual
            if (cuota > (sueldoNeto / 2)) return false;

            // obtener porcentaje de gastos sobre el sueldo neto del usuario
            var porcentajeGastosSobreSueldo = ((gastosMensuales * 100) / sueldoNeto);
            if (porcentajeGastosSobreSueldo <= 30) return false;

            if ((cuota + gastosMensuales) >= sueldoNeto) return false;

            if (!tipoContratoEsIndefinido && ((cuota + gastosMensuales) > (sueldoNeto / 3))) return false;

            return true;
        }
    }
}
