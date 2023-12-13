using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day13 {
    class Map {
        List<string> map;
        public Map(string input) {
            map = Regex.Split(input,"\r\n").ToList();
        }
        public int taksA()=>verticalReflection() + 100 * horizontalReflection();
        
        string replaceValue(string row, int i) {
            char[] newRow = row.ToCharArray();
            newRow[i] = (newRow[i] == '.') ? '#' : '.';
            return string.Join("", newRow);
        }

        public int taksB() {
            int oldVerticalValue = verticalReflection();
            int oldHorizontalValue = horizontalReflection();
            for (int i = 0; i < map.Count; i++) {
                for (int j = 0; j < map[i].Length; j++) {
                    map[i] = replaceValue(map[i],j);
                    int newVerticalValue = verticalReflection(oldVerticalValue);
                    int newHorizontalValue = horizontalReflection(oldHorizontalValue);
                    map[i] = replaceValue(map[i], j);
                    if (oldVerticalValue!=0 && oldVerticalValue!=newVerticalValue && newVerticalValue!=0 || 
                        oldVerticalValue == 0 && newVerticalValue !=0) {
                        return newVerticalValue;
                    } else if (oldVerticalValue != 0 && newHorizontalValue!=0) {
                        return 100 * newHorizontalValue;
                    } else if (oldHorizontalValue!=0 && newHorizontalValue!=oldHorizontalValue && newHorizontalValue!=0 || 
                               oldHorizontalValue==0 && newHorizontalValue!=0) {
                        return 100 * newHorizontalValue;
                    } else if (oldHorizontalValue != 0 && newVerticalValue!=0) {
                        return newVerticalValue;
                    }
                }
            }
            return -1;
        }

        public int verticalReflection(int regi=-1) {
            int width = map[0].Length;
            for (int i = 0; i < width-1; i++) {
                List<string> partOfMap;
                if (i + 1 <= width /2) {
                    partOfMap = map.Select(x => string.Join("",x.Take((i + 1) * 2))).ToList();
                } else {
                    partOfMap = map.Select(x => string.Join("", x.Skip(2* (i + 1) - width))).ToList();
                }
                bool jo = string.Join("",partOfMap.SelectMany(x=>x.Take(x.Length/2)))==
                          string.Join("",partOfMap.SelectMany(x => x.Skip(x.Length / 2).Reverse()));
                if (regi==-1 && jo || regi!=-1 && jo && regi!= (i + 1)) {
                    return i + 1;
                }
            }
            return 0;
        }


        public int horizontalReflection(int regi=-1) {
            int width = map.Count;
            for (int i = 0; i < width - 1; i++) {
                List<string> partOfMap;
                if (i + 1 <= width / 2) {
                    partOfMap = map.Take((i + 1) * 2).ToList();
                } else {
                    partOfMap = map.Skip(2 * (i + 1) - width).ToList();
                }
                bool jo = string.Join("", partOfMap.Take(partOfMap.Count / 2)) ==
                          string.Join("", partOfMap.Skip(partOfMap.Count / 2).Reverse());
                if (regi == -1 && jo || regi != -1 && jo && regi != i + 1) {
                    return i + 1;
                }
            }
            return 0;
        }
    }
    class Program {
        static void Main(string[] args) {
            List<Map> maps = Array.ConvertAll(Regex.Split(File.ReadAllText("../../input.txt"), "\r\n\r\n"),x => new Map(x)).ToList();
            Console.WriteLine(maps.Select(x =>x.taksA()).Sum()); 
            Console.WriteLine(maps.Select(x =>x.taksB()).Sum()); 
            Console.ReadKey();
        }
    }
}
