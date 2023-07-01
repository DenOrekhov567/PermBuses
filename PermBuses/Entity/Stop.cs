using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PermBuses.Entity
{
    internal class Stop
    {
        public string name;
        public string address;

        public List<TimeSpan> arrivalTimesList = new List<TimeSpan>();

        public Stop(string name, string address)
        {
            this.name = name;
            this.address = address;

            Init();
        }

        public void Init()
        {
            var web = new HtmlWeb();
            var document = web.Load(address);

            var timesDoc = document.DocumentNode.SelectNodes("//ul[@class='time-table']/li");

            foreach (var timeDoc in timesDoc)
            {
                int hour = Convert.ToInt16(timeDoc.SelectNodes(".//div[@class='hour']")[0].InnerText.Trim());

                var minutesListDoc = timeDoc.SelectNodes(".//div[@class='minute trip-with-note']");
                if (minutesListDoc == null) continue;

                foreach (var minutesDoc in minutesListDoc)
                {
                    int minutes = Convert.ToInt16(minutesDoc.InnerText.Trim().Replace("*", ""));
                    arrivalTimesList.Add(new TimeSpan(hour, minutes, 0));
                }
            }
        }
    }
}