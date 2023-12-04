using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day4 {
    class Game {
        List<int> numbersOfWin;
        List<int> numbersOfOwn;
        public Game(string sor) {
            List<string> data = Regex.Split(sor, ":  |: | \\|  | \\| ").ToList();
            numbersOfWin= Array.ConvertAll(Regex.Split(data[1], "  | "), Convert.ToInt32).ToList();
            numbersOfOwn= Array.ConvertAll(Regex.Split(data[2], "  | "), Convert.ToInt32).ToList();
        }
        public int countOfIntersect() {
            return numbersOfOwn.Intersect(numbersOfWin).Count();
        }
    }
    internal class Program {
        static int winCard(List<Game> input) {
            int[] dbs = new int[input.Count];
            for (int i = 0; i < input.Count; i++) {
                dbs[i]++;
                int db = input[i].countOfIntersect();
                for (int j = 1; j <= db; j++) {
                    dbs[i + j] += dbs[i];
                }
            }
            return dbs.ToList().Sum();
        }
        static void Main(string[] args) {
            List<Game> input = Array.ConvertAll(
                File.ReadAllLines("../../input.txt"), 
                x => new Game(x)
            ).ToList();
            Console.WriteLine(input.Select(x=>(int)Math.Pow(2, x.countOfIntersect() - 1)).Sum());
            Console.WriteLine(winCard(input));
            Console.ReadKey();
        }
    }
}
