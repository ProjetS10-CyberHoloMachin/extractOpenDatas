using System;
using RestSharp;

namespace ConsoleApp1
{
    public static class CoursAde
    {
        public static DateTime RawToDate(String raw) {
            return new DateTime(
                Convert.ToInt32(raw.Substring(0,4)),
                Convert.ToInt32(raw.Substring(4,2)),
                Convert.ToInt32(raw.Substring(6,2)),
                Convert.ToInt32(raw.Substring(9,2)),
                Convert.ToInt32(raw.Substring(11,2)),
                Convert.ToInt32(raw.Substring(13,2))
            );
        }

        public static dynamic[] GetNext() {
            var client = new RestClient("http://ade6-ujf-ro.grenet.fr/jsp/custom/modules/plannings/anonymous_cal.jsp");

            var request = new RestRequest("", Method.GET);
            request.AddParameter("resources", 11491);
            request.AddParameter("projectId", 2);
            request.AddParameter("calType", "ical");
            request.AddParameter("firstDate", "2018-01-29");
            request.AddParameter("lastDate", "2018-02-04");
            
            // execute the request
            IRestResponse response = client.Execute(request);
            
            var cours = response.Content.Split("BEGIN:VEVENT");

            // set the result as an empty room (nom, debut, fin, salle)
            var nextRoom = new dynamic[] {"",new DateTime(),new DateTime(),""};
            
            // get current date and time
            DateTime currentDate = DateTime.UtcNow;

            
            for (int i = 1; i < cours.Length; i++)
            {
                string rawDate = cours[i].Split("DTEND:")[1].Split("\n")[0];
                DateTime date = RawToDate(rawDate);
                
                
                if ((date - currentDate).TotalSeconds > 0)
                {
                    if ((date - currentDate).TotalSeconds < (nextRoom[2] - currentDate).TotalSeconds | nextRoom[0] == "")
                    {
                        nextRoom[0] = cours[i].Split("SUMMARY:")[1].Split("\n")[0];
                        nextRoom[1] = RawToDate(cours[i].Split("DTSTART:")[1].Split("\n")[0]);
                        nextRoom[2] = RawToDate(cours[i].Split("DTEND:")[1].Split("\n")[0]);
                        nextRoom[3] = cours[i].Split("LOCATION:")[1].Split("DESCRIPTION")[0].Replace(Environment.NewLine+" ", "");
                    }
                }
            }
//            client.ExecuteAsync(request, response =>
//            {
//                Console.Write($"{request.Resource}");
//            });
            
            return nextRoom;
        }
    }
}