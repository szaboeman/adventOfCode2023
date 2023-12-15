using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace day15 {
    internal class Program {
        static int hash(string mit) {
            return mit.Aggregate(0,(acc,val)=>(acc=(acc+(int)val)*17)%256);
        }
        static void Main(string[] args) {
            List<string> input = File.ReadAllText("../../input.txt").Split(',').ToList();
            Console.WriteLine(input.Select(x=>hash(x)).Sum());
            Dictionary<int, List<(string, int)>> boxes = new Dictionary<int, List<(string, int)>>();
            foreach (var item in input) {
                if (char.IsDigit(item.Last())) {
                    string[] data = item.Split('=');
                    string label = data[0];
                    int boxIndex = hash(label);
                    int focusLength = Convert.ToInt32(data[1]);
                    if (!boxes.ContainsKey(boxIndex)) {
                        boxes.Add(boxIndex, new List<(string, int)>() { (label, focusLength) });
                    } else {
                        int index = boxes[boxIndex].FindIndex(x => x.Item1 == label);
                        if (index!=-1) {
                            boxes[boxIndex][index] = (label, focusLength);
                        } else {
                            boxes[boxIndex].Add((label, focusLength));
                        }
                    }
                } else if (item.Last()=='-') {
                    string label = item.Substring(0, item.Length - 1);
                    int boxIndex = hash(label);
                    if (boxes.ContainsKey(boxIndex)) {
                        int index = boxes[boxIndex].FindIndex(x => x.Item1 == label);
                        if (index != -1) {
                            boxes[boxIndex].RemoveAt(index);
                        }
                    }
                }
            }

            int s = 0;
            foreach (var item in boxes) {
                for (int i = 0; i < item.Value.Count; i++) {
                    s += (item.Key + 1) * (i + 1) * item.Value[i].Item2;
                }
            }
            Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}
