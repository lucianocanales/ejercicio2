using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ejercicio2
{
    internal class Program
    {
        static  void Main(string[] args)
        {
            var currencies = GetCurrencies().Result;

            Console.WriteLine("Obteniendo datos de Mercadolibre Aguarde Por favor");
            Console.WriteLine("Obteniendo datos de monedas ....");
            List<Currencies> currenciesArray = JsonConvert.DeserializeObject<List<Currencies>>(currencies);
            Console.WriteLine("Obteniendo datos de Conversiones ....");
            List<CurrencyConversions> csvArray = new List<CurrencyConversions>();
            foreach (var currencie in currenciesArray)
            {
                Console.Write("*");
                var Comversion = GetCurrencyComversions(currencie.id).Result;
                CurrencyConversions toDolar = JsonConvert.DeserializeObject<CurrencyConversions>(Comversion);
                csvArray.Add(toDolar); 
                currencie.todolar = toDolar;
            }
            Console.WriteLine("*");
            Console.WriteLine("Datos Obtenido escriba la ubicacion donde exportar los datos");
            Console.WriteLine(@"Ejemplo: D:\Usuarios\Lucho\Desktop\.net\test");

            var route = Console.ReadLine();
            try
            {
                File.WriteAllText
                (
                    route + "\\currencies.json",
                    JsonConvert.SerializeObject(currenciesArray)
                );
                Console.WriteLine("Se ha creado el archivo currencies.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                using (var writer = new StreamWriter(route + "\\toDolar.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(csvArray);
                }
                Console.WriteLine("Se ha creado el archivo toDolar.csv");
                Console.WriteLine($"Archivo exportados con exito en: {route}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task<String> GetCurrencies()
        {
            var URL = "https://api.mercadolibre.com/currencies/";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(URL);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<String> GetCurrencyComversions(String currency)
        {
            var URL = $"https://api.mercadolibre.com/currency_conversions/search?from={currency}&to=USD";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(URL);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
