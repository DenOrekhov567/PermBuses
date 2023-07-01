using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermBuses.Entity
{
    internal class Route
    {
        //string? what?
        public string number;

        public string start;
        public string end;

        public string address;

        public List<Side> sidesList = new List<Side>();

        public Route(string number, string start, string end, string address)
        {
            this.number = number;

            this.start = start;
            this.end = end;

            this.address = address;

            Init();
        }

        public void Init()
        {
            var web = new HtmlWeb();
            var document = web.Load(address);

            var sidesDoc = document.DocumentNode.SelectNodes("//div[@data-role='collapsible']/h3");

            foreach (var sideDoc in sidesDoc)
            {
                var stopsDoc = sideDoc.SelectNodes("//ul[@data-role='listview']/li/a");
                List<Stop> stopsList = new List<Stop>();
                foreach (var stopDoc in stopsDoc)
                {
                    string name = stopDoc.InnerText.Trim();
                    string address = Program.site + stopDoc.GetAttributeValue("href", "");

                    Stop stop = new Stop(name, address);

                    stopsList.Add(stop);
                }
                sidesList.Add(new Side(sideDoc.InnerText.Trim(), stopsList));
            }
        }
    }
}
