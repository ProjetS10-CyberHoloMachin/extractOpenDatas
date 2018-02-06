using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace ConsoleApp1
{
    public class Barnave
    {
        public static List<String> GetMenus()
        {
            List<String> res = new List<String>();
            var client = new RestClient("https://www.crous-grenoble.fr/restaurant/ru-barnave-etudiant");
            var request = new RestRequest("", Method.GET);
            
            IRestResponse response = client.Execute(request);
            
            foreach (string i in response.Content.Split("Menu du ").Skip(1).ToArray())
            {
                String tmp = i.Split("</h3>")[0];

                string repas = i.Split("Déjeuner")[1].Split("Dîner")[0];

                foreach (var repa in repas.Split("<li>").Skip(1).ToArray())
                {
                    tmp = tmp + "," + repa.Split("</li>")[0];
                }
                res.Add(tmp);
            }
            
            return res;
        }
    }
}