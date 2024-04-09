

namespace RealStateAppProg3.Core.Application.Helpers
{
    public static class CodeGenerator
    {
        public static int Unique9DigitsGenerator()
        {
            // Obtener la fecha y hora actual
            DateTime now = DateTime.Now;

            // Convertir la fecha y hora a un número entero representando la marca de tiempo en segundos
            long timestamp = ((DateTimeOffset)now).ToUnixTimeSeconds();

            // Obtener una instancia de Random para generar un número aleatorio
            Random random = new Random();

            // Generar un número aleatorio de 3 dígitos (entre 0 y 999)
            int randomNumber = random.Next(100000, 1000000); // Generar un número de 6 dígitos (entre 100000 y 999999)

            // Combina el timestamp y el número aleatorio para formar el código único
            long uniqueCode = (timestamp * 1000000) + randomNumber; // Multiplicar el timestamp por 1000000 para garantizar un código de 6 dígitos

            // Asegura de que el número generado sea siempre positivo
            int positiveUniqueCode = Math.Abs((int)uniqueCode);

            // Devolver el número
            return positiveUniqueCode;
        }
    }
}
