using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day14 {
    
    class Map {
        List<List<char>> map;
        public Map(string fileName) {
            map = Array.ConvertAll(File.ReadAllLines("../../" + fileName),x => x.ToCharArray().ToList()).ToList();
        }
        public void moveNorth() {
            for (int i = 0; i < map.Count; i++) {
                for (int j = 0; j < map[i].Count; j++) {
                    if (map[i][j]=='O') {
                        map[i][j] = '.';
                        int k = i;
                        while (k-1 >= 0 && map[k-1][j] == '.') {
                            k--;
                        }
                        map[k][j] = 'O';
                    }
                }
            }
        }
        public void moveWest() {
            for (int j = 0; j < map[0].Count; j++) {
                for (int i = 0; i < map.Count; i++) {
                    if (map[i][j] == 'O') {
                        map[i][j] = '.';
                        int k = j;
                        while (k - 1 >= 0 && map[i][k-1] == '.') {
                            k--;
                        }
                        map[i][k] = 'O';
                    }
                }
            }
        }
        public void moveSouth() {
            for (int i = map.Count- 1; i >= 0; i--) {
                for (int j = 0; j < map[i].Count; j++) {
                    if (map[i][j] == 'O') {
                        map[i][j] = '.';
                        int k = i;
                        while (k + 1 < map.Count && map[k + 1][j] == '.') {
                            k++;
                        }
                        map[k][j] = 'O';
                    }
                }
            }
        }
        public void moveEast() {
            for (int j = map[0].Count- 1; j >= 0; j--) {
                for (int i = 0; i < map.Count; i++) {
                    if (map[i][j] == 'O') {
                        map[i][j] = '.';
                        int k = j;
                        while (k + 1< map[i].Count && map[i][k + 1] == '.') {
                            k++;
                        }
                        map[i][k] = 'O';
                    }
                }
            }
        }
        public long count(string map) {
            int value = 0;
            for (int i = 0; i < map.Length; i++) {
                int sorIndex = i / this.map[0].Count;
                if (map[i] == 'O') {
                    value += (this.map.Count - sorIndex);
                }
            }
            return value;
        }
        public long taskA() {
            moveNorth();
            return count(string.Join("",map.Select(x=>string.Join("",x))));
        }

        public long taskB(int cycyles) {
            List<string> check = new List<string>();
            for (int i = 0; i < cycyles; i++) {
                moveNorth();  
                moveWest();
                moveSouth();
                moveEast();
                string newMap = string.Join("", map.SelectMany(x => x));
                if (check.Any(x=>x==newMap)) {
                    int start = check.FindIndex(x =>x==newMap);
                    int intervallum = i - start;
                    int modulo = (cycyles-start) % (intervallum);
                    return count(check[start+modulo-1]);
                } else {
                    check.Add(newMap);
                }
            }
            return 0;
        }
    }
    internal class Program {
        static void Main(string[] args) {
            Map m = new Map("input.txt");
            Console.WriteLine(m.taskA());
            Console.WriteLine(m.taskB(1000000000));
            Console.ReadKey();
        }
    }
}
