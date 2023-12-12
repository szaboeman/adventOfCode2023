using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace day9 {
    class Dataset {
        public List<long> input = new List<long>();
        public List<List<long>> sequences=new List<List<long>>();

        public Dataset(string data) { 
            input=Array.ConvertAll(data.Split(' '),Convert.ToInt64).ToList();
            List<long> actList=new List<long>(input);
            while (actList.Any(x=>x>0 || x<0)) {
                sequences.Add(actList);
                List<long> uj = new List<long>();
                for (int i = 0; i < actList.Count-1; i++) {
                    uj.Add(actList[i + 1] - actList[i]);
                }
                actList = new List<long>(uj);
            }
            sequences.Add(actList);
        }

        public long taskA() {
            sequences.Last().Add(0);
            for (int i = sequences.Count - 2; i >= 0; i--) {
                sequences[i].Add(sequences[i].Last() + sequences[i + 1].Last());
            }
            return sequences.First().Last();
        }
        public long taskB() {
            sequences.Last().Insert(0,0);
            for (int i = sequences.Count - 2; i >= 0; i--) {
                sequences[i].Insert(0,sequences[i].First() - sequences[i + 1].First());
            }
            return sequences.First().First();
        }
    }
    internal class Program {
        static void Main(string[] args) {
            List<Dataset> iput= Array.ConvertAll(File.ReadAllLines("../../input.txt"),x => new Dataset(x)).ToList();
            Console.WriteLine(iput.Select(x=>x.taskA()).Sum());
            Console.WriteLine(iput.Select(x=>x.taskB()).Sum());
            Console.ReadKey();
        }
    }
}
