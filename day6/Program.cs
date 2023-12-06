using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day6 {
    internal class Program {
        static long numberOfOption(long time, long distance) {
            return (long)Math.Floor((-time + Math.Sqrt(time * time - 4 * distance)) / (-2));
        }
        static void Main(string[] args) {
            List<long> times = Array.ConvertAll(Regex.Split(File.ReadAllLines("../../input.txt")[0], ": | ").Skip(1).ToArray(),Convert.ToInt64).ToList();
            List<long> distances = Array.ConvertAll(Regex.Split(File.ReadAllLines("../../input.txt")[1], ": | ").Skip(1).ToArray(), Convert.ToInt64).ToList();

            Console.WriteLine(  
                times.Select((t, index) => t - 2 * numberOfOption(t, distances[index])-1)
                     .Aggregate((long)1,(a, b) => a * b)
            );
            

            long time = Convert.ToInt64(string.Join("", times));
            long distance= Convert.ToInt64(string.Join("", distances));
            Console.WriteLine(time - 2*numberOfOption(time,distance) - 1);
            Console.ReadKey();
        }
    }
}
