using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace ConsoleApp1
{
    public class RestoU
    {
//        public static List<string> GetResto()
//        {
//            
//        }
        
        
        public static List<List<List<string>>> GetMenus(string nom)
        {
            List<List<List<string>>> res = new List<List<List<string>>>();
            var client = new RestClient("https://www.crous-grenoble.fr/restaurant/"+nom);
            var request = new RestRequest("", Method.GET);
            
            IRestResponse response = client.Execute(request);
            
            foreach (string i in response.Content.Split("Menu du ").Skip(1).ToArray())
            {
                List<List<string>> tmp = new List<List<string>> {new List<string> {i.Split("</h3>")[0]}};

                string repas = i.Split("Déjeuner")[1].Split("Dîner")[0];

                foreach (var h1 in repas.Split("<span class=\"name\">").Skip(1).ToArray())
                {
                    List<string> tmp2 = new List<string> {h1.Split("</span>")[0]};

                    foreach (var h2 in h1.Split("<li>").Skip(1).ToArray())
                    {
                        string tmp3 = h2.Split("</li>")[0];
                        if (tmp3 != "")
                            tmp2.Add(tmp3);
                    }
                    tmp.Add(tmp2);
                }
                res.Add(tmp);
            }
            
            return res;
        }
    }
}