using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day1 {
    public class Program {
        public static int findDigits(string s, bool b=false) {
            List<string> numbers = new List<string>() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            List<int> digits = new List<int>();
            for (int i = 0; i < s.Length; i++)
                if (s[i] >= '0' && s[i] <= '9')
                    digits .Add(s[i] - 48);
                else if (b)
                    for (int j = 0; j < numbers.Count; j++)
                        if (i + numbers[j].Length - 1 < s.Length &&
                            s.Substring(i, numbers[j].Length) == numbers[j])
                            digits.Add(j + 1);
            return digits.First()*10+digits.Last();
        }
        
        static void Main(string[] args) {
            List<string> input = File.ReadAllLines("../../input.txt").ToList();
            Console.WriteLine(input.Aggregate(0, (acc, x) => acc + findDigits(x))); 
            Console.WriteLine(input.Aggregate(0, (acc, x) => acc + findDigits(x,true))); 
            Console.ReadKey();
        }
    }
}
