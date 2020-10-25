using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMannschaftsverwaltung
{
    public class RandomUtils
    {
        public static string fromCollection(string[] collection)
        {
            Random random = new Random();
            int rand = random.Next(0, collection.Length);
            string result = collection[rand];

            return collection[rand];
        }

        public static bool asBoolean()
        {
            Random random = new Random();
            int rand = random.Next(0, 2);

            if (rand == 0) return false;
            return true;
        }

        public static int asInteger(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static SportArt asSportArt()
        { 
            Random random = new Random();
            int rand = random.Next(1, 3);

            SportArt sa = (SportArt)rand;

            return sa;
        }
    }
}