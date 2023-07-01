using HtmlAgilityPack;
using PermBuses.Entity;

namespace PermBuses
{
    internal class Program
    {
        public static string site = "https://www.m.gortransperm.ru";

        private static List<Route> poolRoutes = new List<Route>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Начинаю парсинг данных...");

            StartParsing();
        }

        public static void StartParsing()
        {
            var web = new HtmlWeb();
            var document = web.Load(site + "/routes-list/0/");

            var routesDoc = document.DocumentNode.SelectNodes("//ul[@data-role='listview']/li/a");
            foreach (var routeDoc in routesDoc)
            {
                string[] parts1 = routeDoc.InnerText.Trim().Split(", ");

                string number = parts1[0].Trim();
                string[] parts2 = parts1[1].Replace("–", "-").Split("-");

                string start = parts2[0].Trim();
                string end = parts2[1].Trim();

                string address = site + routeDoc.GetAttributeValue("href", "");

                Route route = new Route(number, start, end, address);

                poolRoutes.Add(route);

                Console.WriteLine("Маршрут №" + number + " загружен.");
            }
            // Тестируем работу программы
            //Test();
        }

        public static void Test()
        {
            // Перебираем все маршруты
            foreach (Route route in poolRoutes)
            {
                Console.WriteLine("Маршрут №" + route.number);
                // Перебираем все направления указанного маршрута
                foreach (Side side in route.sidesList)
                {
                    Console.WriteLine("Направление: " + side.name);
                    // Перебираем все остановки в указанном направлении
                    foreach (Stop stop in side.stopsList)
                    {
                        Console.WriteLine("Остановка: " + stop.name);
                        // Перебираем времена прибытия на указанную остановку
                        foreach (TimeSpan time in stop.arrivalTimesList)
                        {
                            Console.WriteLine("Время прибытия: " + time.ToString());
                        }
                    }
                }
            }
        }
    }
}