using InfoCard.Di;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace InfoCard.Dal
{
    class Program
    {
        public List<InfoCardBase> LoadJson()
        {
            List<InfoCardBase> a = new List<InfoCardBase>();
            using (StreamReader r = new StreamReader("cards.json"))
            {
                string json = r.ReadToEnd();
               //  a  = JsonSerializer.Serialize(json);
            }
            return a;
        }
    }
}
