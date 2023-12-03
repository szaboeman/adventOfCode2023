using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;

namespace day3 {
    class Program {

        static List<string> input;
        static bool isNumber(int i, int j) {
            return input[i][j] <= '9' && input[i][j] >= '0';
        }

        static bool isEmpty(int i, int j) {
            return input[i][j] =='.';
        }

        static bool isSymbol(int i, int j) {
            return !(isEmpty(i, j) || isNumber(i, j));
        }

        public static (int, int) isSymbolAround(int x, int y) {
            for (int i = -1; i < 2; i++) { 
                for (int j = -1; j < 2; j++) {
                    if (x+i>=0 && x + i <input.Count && y+j>=0 && y + j < input[x + i].Length && isSymbol(x+i,y+j)) {
                        return (x+i,y+j);
                    }
                }
            }
            return (-1,-1);
        }


        public static (int,int) isNumberAround(int sor, int eleje, int vege) {
            for (int i = eleje; i < vege; i++) {
                if (isSymbolAround(sor, i).Item1!=-1) {
                    return isSymbolAround(sor, i);
                }
            }
            return (-1,-1);
        }


        static void Main(string[] args) {
            input = File.ReadAllLines("../../input.txt").ToList();
            List<int> numbers = new List<int>();
            Dictionary<(int, int), int> dict = new Dictionary<(int, int), int>();
            int taskB = 0;
            for (int i = 0; i < input.Count; i++) {
                int j = 0;
                while (j < input[i].Length) {
                    while (j < input[i].Length && (isEmpty(i,j) || !isNumber(i, j))) {
                        j++;
                    }
                    if (j < input[i].Length) {
                        int start = j;
                        while (j < input[i].Length && isNumber(i,j)) {
                            j++;
                        }
                        int end = j;
                        int szam = 0;
                        szam = Convert.ToInt32(input[i].Substring(start, end - start));
                        int sx, sy;
                        (sx, sy) = isNumberAround(i, start, end);
                        if (sx!=-1) {
                            if (dict.ContainsKey((sx,sy))) {
                                taskB += dict[(sx, sy)] * szam;
                            } else {
                                if (input[sx][sy]=='*') {
                                    dict.Add((sx,sy),szam);
                                }
                            }
                            numbers.Add(szam);
                        }
                    }
                }
            }
            Console.WriteLine(numbers.Sum());
            Console.WriteLine(taskB);
            Console.ReadKey();
        }
    }
}
