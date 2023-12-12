using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day10 {
    class Character {
        public char c;
        public int value;
        public bool isWay;
        public bool isChecked;
        public Character(char c, int value) {
            this.c = c;
            this.value = value;
            isWay = false;
            isChecked = false;
        }
    }

    class Table {
        List<List<Character>> table=new List<List<Character>>();
        
        int[,] dirs = { { -2, 0 }, { 0, 2 }, { 2, 0 }, { 0, -2 } };
        string[] hovaJo = { "|7FS", "-J7S", "|LJS", "-FLS" };
        string[] honnanJo = { "|LJS", "-LFS", "|F7S", "-J7S" };
        
        (int, int) getCoord(int index) {
            return (index / table[0].Count, index % table[0].Count);
        }
        
        (int, int) getStartCoord() {
            return getCoord(table.SelectMany(x => x).Select(x => x.c).ToList().FindIndex(x => x == 'S'));
        }

        public void insertColumns() {
            foreach (var row in table) {
                int i = 0;
                while (i < row.Count) {
                    row.Insert(i, new Character('_', 0));
                    i += 2;
                }
                row.Add(new Character('_', 0));
            }
        }

        public List<Character> emptyRow() {
            List<Character> row = new List<Character>();
            for (int i = 0; i < table[0].Count; i++) {
                row.Add(new Character('_', 0));
            }
            return row;
        }

        public void insertRows() {
            int j = 0;
            while (j < table.Count) {
                table.Insert(j, emptyRow());
                j += 2;
            }
            table.Insert(j, emptyRow());
        }

        public void strechTable() {
            insertColumns();
            insertRows();
        }

        public Table(string fileName) {
            table = Array.ConvertAll(File.ReadAllLines("../../" + fileName), x => Array.ConvertAll(x.ToCharArray(),y=>new Character(y,1)).ToList()).ToList();
            strechTable();
            int startX, startY, actX, actY, lastX = -1, lastY = -1;
            (actX, actY) = getStartCoord();
            table[actX][actY].isWay = true;
            (startX, startY) = (actX, actY);
            int c = 0;
            while (!(actX == startX && actY == startY) || c == 0) {
                for (int i = 0; i < 4; i++) {
                    int newX = actX + dirs[i, 0];
                    int newY = actY + dirs[i, 1];
                    if (newX >= 0 && newY >= 0 && newX < table.Count && newY < table[0].Count) {
                        if (hovaJo[i].Any(x => x == table[newX][newY].c) &&
                            honnanJo[i].Any(x => x == table[actX][actY].c)) {
                            if (!(lastX == newX && lastY == newY)) {
                                lastX = actX;
                                lastY = actY;
                                actX = newX;
                                actY = newY;
                                table[actX][actY].isWay = true;
                                table[(actX+lastX)/2][(actY+lastY)/2].isWay = true;
                                break;
                            }
                        }
                    }
                }
                c++;
            }
            Console.WriteLine(c / 2);
            visitArea(0, 0);
            Console.WriteLine(table.SelectMany(x=>x)
                                   .Where(x=>!x.isChecked && !x.isWay)
                                   .Select(x=>x.value).Sum());
        }

        List<(int, int)> openPoint = new List<(int, int)>();
        public void visitArea(int x, int y) {
            openPoint.Add((x, y));
            int actx , acty;
            while (openPoint.Count>0) {
                (actx, acty) = openPoint.First();
                openPoint.RemoveAt(0);
                table[actx][acty].isChecked = true;
                for (int i = 0; i < 4; i++) {
                    int newX = actx + dirs[i, 0] / 2;
                    int newY = acty + dirs[i, 1] / 2;
                    if (newX >= 0 && newY >= 0 && newX < table.Count && newY < table[0].Count &&
                        !table[newX][newY].isChecked && !table[newX][newY].isWay &&
                        !openPoint.Any(z=>z.Item1== newX && z.Item2== newY)) {
                        openPoint.Add( (newX, newY) );
                    }
                }
            }
        }
    }
    internal class Program {
        static void Main(string[] args) {
            Table table = new Table("input.txt");
            Console.ReadKey();
        }
    }
}
