using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Extensions
{
    public static class GeneratedName
    {
       public static string GetName(int length)
        {
            const string vowels = "aeiou";
            const string consonants = "bcdfghjklmnpqrstvwxyz";

            var rnd = new Random();

            length = length % 2 == 0 ? length : length + 1;

            var name = new char[length];

            for (var i = 0; i < length; i += 2)
            {
                name[i] = vowels[rnd.Next(vowels.Length)];
                name[i + 1] = consonants[rnd.Next(consonants.Length)];
            }

            return new string(name);
        }
    }
}
