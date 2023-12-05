using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day5 {

    class Game {
        public List<List<List<long>>> map=new List<List<List<long>>>();
        public List<List<long>> reed(string data) {
            List<List<long>> input = new List<List<long>>();
            List<string> sts = Regex.Split(data, "\\r\\n").Skip(1).ToList();
            foreach (var row in sts) {
                List<long> info = Array.ConvertAll(row.Split(' '), Convert.ToInt64).ToList();
                input.Add(info);
            }
            return input;
        }

        public Game(string input) {
            List<string> data = Regex.Split(input, "\\r\\n\\r\\n").Skip(1).ToList();
            foreach (var item in data) {
                map.Add(reed(item));
            }
        }

        public long meghataroz(List<List<long>> map, long seed) {
            foreach (var row in map) {
                if (row[1]<=seed && row[1] + row[2]>seed) {
                    return seed + row[0] - row[1];
                }
            }

            return seed;
        }

        public long taskA(List<long> seeds) {
            long min = (int)Math.Pow(2,63)-1;
            foreach (var seed in seeds) {
                long v = seed;
                foreach (var data in map) {
                    v = meghataroz(data,v);
                }
                if (v<min) {
                    min = v;
                }
            }
            return min;
        }


        //pffff, very slow 
        public long taskB(List<long> seeds) {
            long min = (long)Math.Pow(2, 63) - 1;
            for (int i = 0; i < seeds.Count; i+=2) {
                for (long j = seeds[i]; j < seeds[i]+seeds[i+1]; j++) {
                    long v = j;
                    foreach (var data in map) {
                        v = meghataroz(data, v);
                    }
                    if (v < min) {
                        min = v;
                        Console.WriteLine(min);
                    }
                }
            }
            return min;
        }
    }
    internal class Program {
        static List<long> seeds;
        
        static void Main(string[] args) {
            seeds= Array.ConvertAll(Regex.Split(File.ReadAllLines("../../input.txt").First(), ": ").Skip(1).First().Split(' '), Convert.ToInt64).ToList();
            Game g = new Game(File.ReadAllText("../../input.txt"));
            Console.WriteLine(g.taskA(seeds));
            Console.WriteLine(g.taskB(seeds));
            Console.ReadKey();
        }
    }
}
