using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RestSharp;

namespace ConsoleApp1
{
    public static class CoursAde
    {
        private static DateTime RawToDate(String raw) {
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

        

        public static List<String> GetSallesLibres()
        {
            List<String> res = new List<String>();
            
            var client =
                new RestClient("http://ade6-ujf-ro.grenet.fr/jsp/custom/modules/plannings/anonymous_cal.jsp");
            var request = new RestRequest("", Method.GET);
            request.AddParameter("resources", "13902,11084,11081,12036,12035,12034,11444,11429,11426,11351,11951,11949,11486,11598,11468,11467,11466,10552,11183,11169,10579,11493,11171,10545,10544,10527,10641,10536,10535,10534,10533,10532,12214,10543,10539,10538,10537,10507,10494,10493,3544,10531,10530,10529,10528,10526,10525,10524,10523,10522,10521,10518,10546,10496,10495,10551,10550,10548,10547,10506,10488,10487,10517");
            request.AddParameter("projectId", 2);
            request.AddParameter("calType", "ical");
            request.AddParameter("firstDate", "2018-01-29");
            request.AddParameter("lastDate", "2018-02-04");
            
            IRestResponse response = client.Execute(request);
            
            var cours = response.Content.Split("BEGIN:VEVENT");

            List<String> listeSalles = new List<String>()
            {
                "PG AMPHI 001 (122 pl.)",
                "PG AMPHI 005 (58 pl.)",
                "PG AMPHI 007 (61 pl.)",
                "PG AMPHI 101 (123 pl.)",
                "PG SALLE 114",
                "PG SALLE 117",
                "PG SALLE 118",
                "PG SALLE 122",
                "PG SALLE 009 (50 pl.)",
                "PG SALLE 011 (45 pl.)",
                "PG SALLE 013 (32 pl.)",
                "PG SALLE 035 (32 pl.)",
                "PG SALLE 037 (40 pl.)",
                "PG SALLE 039 (60 pl.)",
                "PG SALLE 043 (33 pl.)",
                "PG SALLE 052 (60 pl.)",
                "PG SALLE 105 (100 pl.)",
                "PG SALLE 113 (15 pl.)",
                "PG SALLE 123 (42 pl.)",
                "PG SALLE 125 (46 pl.)",
                "PG SALLE 127 (26 pl.)",
                "PG SALLE 129 (40 pl.)",
                "PG SALLE 131 (42 pl.)",
                "PG SALLE 133 (50 pl.)",
                "PG SALLE 135 (34 pl.)",
                "PG SALLE 144 (66 pl.)",
                "PG SALLE 241 (35 pl.)"
                
            };
            
            foreach (var c in cours)
            {
                if (c.Split("LOCATION:").Length > 1)
                {
                    string rawDateStart = c.Split("DTSTART:")[1].Split("\n")[0];
                    DateTime dateStart = RawToDate(rawDateStart);
                    string rawDateEnd = c.Split("DTEND:")[1].Split("\n")[0];
                    DateTime dateEnd = RawToDate(rawDateEnd);
                                    
                    DateTime currentDate = DateTime.UtcNow;
                                    
                    if ((dateStart - currentDate).TotalSeconds < 0 & (dateEnd - currentDate).TotalSeconds > 0)
                    {
                        foreach (var s in c.Split("LOCATION:")[1].Split("DESCRIPTION")[0].Replace(Environment.NewLine + " ","").Replace(Environment.NewLine,"").Replace("  "," ").Split("\\,"))
                        {
                            listeSalles.Remove(s);
                        }
                    }
                }  
            }
            return listeSalles;
        }
    }

}