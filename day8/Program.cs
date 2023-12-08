using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day8 {
    class Node {
        public string value;
        public string left, right;
        public Node(string row) {
            List<string> data = Regex.Split(row, " = \\(|, |\\)").ToList();
            value = data[0];
            left = data[1];
            right = data[2];
        }
    }
    
    internal class Program {
        static bool isGood(string value, bool taskB = false) {
            return (taskB) ? value.Last() != 'Z' : value != "ZZZ";
        }
        static int solution(string inputString, List<Node> nodes, string start, bool taskB=false) {
            Node act = nodes.Find(x => x.value == start);
            int i = 0;
            while (isGood(act.value,taskB)) { 
                if (inputString[i % inputString.Length] == 'R') {
                    act = nodes.Find(x => x.value == act.right);
                } else {
                    act = nodes.Find(x => x.value == act.left);
                }
                i++;
            }
            return i;
        }

        static long GCD(long a, long b) {
            int i = 1;
            while ((Math.Max(a,b) * i) % Math.Min(a, b) != 0) {
                i++;
            }
            return Math.Max(a, b) * i;
        }
        static void Main(string[] args) {
            string inputString = File.ReadAllLines("../../input.txt")[0];

            List<Node> nodes = Array.ConvertAll(
                    File.ReadAllLines("../../input.txt").Skip(2).ToArray(),
                    x=>new Node(x)
                ).ToList();

            Console.WriteLine(solution(inputString, nodes, "AAA"));

            List<string> actNodes = nodes.FindAll(x => x.value.Last() == 'A').Select(x=>x.value).ToList();
            List<int> values = new List<int>();
            for (int i = 0; i < actNodes.Count; i++) {
                values.Add(solution(inputString,nodes, actNodes[i],true));
            }
            long partB = 1;
            foreach (var v in values) {
                partB = GCD(v, partB);
            }
            Console.WriteLine(partB);
            Console.ReadKey();

        }
    }
}
