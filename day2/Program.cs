using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace day2 {
    class RGB {
        public int r = 0, g = 0, b = 0;
        public RGB (string data) {
            List<string> input = Regex.Split(data, " |, ").ToList();
            for (int i = 0; i < input.Count; i+=2)
                switch (input[i+1]) {
                    case "red": r = Convert.ToInt32(input[i]); break;
                    case "green": g = Convert.ToInt32(input[i]); break;
                    case "blue": b = Convert.ToInt32(input[i]); break;
                }
        }
    }
    class Game {
        public int index;
        public List<RGB> rgb;
        public Game(string sor) {
            List<string> input = Regex.Split(sor, "Game |: |; ").Skip(1).ToList();
            index = Convert.ToInt32(input[0]);
            rgb = Array.ConvertAll(input.Skip(1).ToArray(), x => new RGB(x)).ToList();
        }

        public bool getIsPossible => rgb.FindAll(x => x.r <= 12 && x.g <= 13 && x.b <= 14).Count == rgb.Count;

        public int getMinRGB {
            get {
                int r = 0, g = 0, b = 0;
                foreach (var item in rgb) {
                    r = Math.Max(item.r, r);
                    g = Math.Max(item.g, g);
                    b = Math.Max(item.b, b);
                }
                return r * g * b;
            }
        }
    }
    public class Program {
        static void Main(string[] args) {
            List<Game> input = Array.ConvertAll(
                File.ReadAllLines("../../input.txt"), x=>new Game(x)
            ).ToList();
            Console.WriteLine(input.FindAll(x=>x.getIsPossible).Select(x=>x.index).Sum());
            Console.WriteLine(input.Select(x=>x.getMinRGB).Sum());  
            Console.ReadKey();
        }
    }
}
