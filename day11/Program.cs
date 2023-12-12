using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day11 {

    class Table {
        List<List<char>> table;
        List<List<int>> coords = new List<List<int>>();
        List<int> emptyColIndexes = new List<int>();
        List<int> emptyRowIndexes = new List<int>();
        long task(int mul) {
            long tav = 0;
            for (int i = 0; i < coords.Count - 1; i++) {
                for (int j = i + 1; j < coords.Count; j++) {
                    tav += Math.Abs(coords[i][0] - coords[j][0]) + Math.Abs(coords[i][1] - coords[j][1]);
                    for (int k = Math.Min(coords[i][0], coords[j][0]); k < Math.Max(coords[i][0], coords[j][0]); k++) {
                        if (emptyRowIndexes.Any(x => x == k)) {
                            tav += (mul-1); 
                        }
                    }
                    for (int k = Math.Min(coords[i][1], coords[j][1]); k < Math.Max(coords[i][1], coords[j][1]); k++) {
                        if (emptyColIndexes.Any(x => x == k)) {
                            tav += (mul - 1);
                        }
                    }

                }
            }
            return tav;
        }
        public Table(string fileName) {
            table = Array.ConvertAll(File.ReadAllLines("../../" + fileName),x => x.ToCharArray().ToList()).ToList();

            for (int i = table.Count - 1; i >= 0; i--) {
                if (!table[i].Any(x=>x=='#')) {
                    emptyRowIndexes.Add(i);
                }
            }

            
            for (int i = table[0].Count - 1; i >= 0; i--) {
                if (!table.Select(x => x[i]).Any(x => x == '#')) {
                    emptyColIndexes.Add(i);
                }
            }

            for (int i = 0; i < table.Count; i++) {
                for (int j = 0; j < table[i].Count; j++) {
                    if (table[i][j]=='#') {
                        coords.Add(new List<int>() { i, j });
                    }
                }
            }
            Console.WriteLine(task(2));
            Console.WriteLine(task(1000000));
        }
    }
    class Program {
        static void Main(string[] args) {
            Table table = new Table("input.txt");
            Console.ReadKey();
        }
    }
}
