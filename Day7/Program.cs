using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day7 {

    public class Game {
        public List<int> numbersOfcard;
        public string card;
        public string sequence;
        public int bet;

        public Dictionary<int, int> countOfNumbers = new Dictionary<int, int>();
        public long rang;
        public int charToNumber(char c) {
            return sequence.Length - sequence.IndexOf(c);
        }

        public long value() {
            int sum = 0;
            for (int i = 0; i < numbersOfcard.Count; i++) {
                sum += (int)Math.Pow(sequence.Length, 4 - i) * numbersOfcard[i];
            }
            return sum;
        }

        public void countOfNumber() {
            countOfNumbers = numbersOfcard.GroupBy(x => x)
                                .Select(x => new { key = x.Key, value = x.Count() })
                                .OrderByDescending(x => x.value)
                                .ThenByDescending(x => x.key)
                                .ToDictionary(x=>x.key, x=>x.value);
        }

        public Game(string sor, string sequence, bool partB) {
            this.sequence = sequence;
            List<string> adatok = sor.Split(' ').ToList();

            numbersOfcard= adatok[0].ToCharArray().Select(x => charToNumber(x)).ToList(); ;
            bet= Convert.ToInt32(adatok[1]);
            countOfNumber();


            int increase = 0;
            if (partB) {
                if (countOfNumbers.ContainsKey(1)) {
                    increase = countOfNumbers[1];
                    countOfNumbers.Remove(1);
                }
                if (countOfNumbers.Count==0) {
                    countOfNumbers.Add(14, 0);
                }
                countOfNumbers[countOfNumbers.First().Key] += increase;
            }
            
        }
    }
    internal class Program {

        public static int megszamol(List<Game> betsA) {
            List<Game> orderdCards= new List<Game>();
            orderdCards.AddRange(betsA.Where(x => x.countOfNumbers.Count == 1).OrderByDescending(x => x.value()));
            orderdCards.AddRange(betsA.Where(x => x.countOfNumbers.First().Value == 4).OrderByDescending(x => x.value()));
            orderdCards.AddRange(betsA.Where(x => x.countOfNumbers.First().Value == 3 && x.countOfNumbers.Last().Value == 2).OrderByDescending(x => x.value()));
            orderdCards.AddRange(betsA.Where(x => x.countOfNumbers.First().Value == 3 && x.countOfNumbers.Last().Value == 1).OrderByDescending(x => x.value()));
            orderdCards.AddRange(betsA.Where(x => x.countOfNumbers.Where(y => y.Value == 2).Count() == 2).OrderByDescending(x => x.value()));
            orderdCards.AddRange(betsA.Where(x => x.countOfNumbers.Count == 4).OrderByDescending(x => x.value()));
            orderdCards.AddRange(betsA.Where(x => x.countOfNumbers.Count == 5).OrderByDescending(x => x.value()));
            return Enumerable.Range(0, orderdCards.Count).Aggregate(0, (acc, ind) => acc + (orderdCards.Count - ind) * orderdCards[ind].bet);
        }
        static void Main(string[] args) {
            string sequenceA = "AKQJT98765432";
            List<Game> betsA = Array.ConvertAll(File.ReadAllLines("../../input.txt"),x=>new Game(x,sequenceA,false)).ToList();
            Console.WriteLine(megszamol(betsA));

            string sequenceB = "AKQT98765432J";
            List<Game> betsB = Array.ConvertAll(File.ReadAllLines("../../input.txt"),x => new Game(x, sequenceB, true)).ToList();
            Console.WriteLine(megszamol(betsB));

            Console.ReadKey();

        }
    }
}
