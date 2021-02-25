using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_39_1_3_Tobi_Turnier {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            var data = readFile(System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\spielstaerken4.txt");
            double numstart = 10;
            double num = numstart;
            Random rnd = new Random();
            for (int modus = 0; modus < 3; modus++) {
                Console.WriteLine("\n\nModus: " + (modus == 0 ? "liga" : (modus == 1 ? "kO" : "kOx5")));

                if (modus != 0) {
                    for (int kombimodus = 0; kombimodus < 2; kombimodus++) {
                        Console.WriteLine("\nKombimodus: " + kombimodus);
                        for (int i = 0; i < 5; i++) {
                            Console.WriteLine("{0:P2}", Math.Round(runMult(data, (float)num, modus, rnd, kombimodus),4));
                            num = (int)Math.Pow(num, 1.5);
                        }
                        num = numstart;
                    }
                }
                else {
                    for (int i = 0; i < 5; i++) {
                        Console.WriteLine("{0:P2}",Math.Round(runMult(data, (float)num, modus, rnd, 0),4));
                        num = (int)Math.Pow(num, 1.5);
                    }
                    num = numstart;
                }

            }

            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spielstärken"></param>
        /// <param name="maxNum">number of runs</param>
        /// <param name="modus">0: liga; 1: kO; 2: kO x5</param>
        /// <returns>percentage of victories of best player</returns>
        public static float runMult(int[] spielstärken, float maxNum, int modus, Random rnd, int kombimodus) {
            float victories = 0;
            int bestManInd = getHighest(spielstärken);

            for (int i = 0; i < maxNum; i++) {
                int erg = modus == 0 ? liga(spielstärken, rnd) : kO(spielstärken, (modus == 2), rnd, kombimodus);
                if (erg == bestManInd) {
                    victories++;
                }
            }
            Console.Write("{0} von {1} : ", victories, maxNum);
            return ((victories) / maxNum);
        }


        #region liga

        //returns index of victory
        public static int liga(int[] spielstärken, Random rnd) {

            int[] gewinne = new int[spielstärken.Length];
            for (int i = 0; i < spielstärken.Length; i++) {
                for (int j = 0; j < spielstärken.Length; j++) {
                    if (i != j && j < i) {
                        if (makeGame(spielstärken, new Tuple<int, int>(i, j), rnd)) {
                            gewinne[i]++;
                        }
                        else {
                            gewinne[j]++;
                        }
                    }
                }
            }
            //Console.WriteLine(getHighest(gewinne));
            return getHighest(gewinne);
        }

        #endregion


        #region ko-spiel

        public static int kO(int[] spielstärken, bool times5, Random rnd, int kombimodus) {
            List<Tuple<int, int>> ausstehendeSpiele = new List<Tuple<int, int>>();
            if (kombimodus == 0) {
                for (int i = 0; i < spielstärken.Length; i += 2) {
                    ausstehendeSpiele.Add(new Tuple<int, int>(i, i + 1));
                }
            }
            else {
                for (int i = 0; i < spielstärken.Length / 2; i++) {
                    ausstehendeSpiele.Add(new Tuple<int, int>(i, spielstärken.Length - i - 1));
                }
            }

            //alle vorfinalspiele: 
            List<Tuple<int, int>> ausstehendeSpieleNeu = new List<Tuple<int, int>>();
            for (int i = 0; i < Math.Log(spielstärken.Length, 2) - 1; i++) {
                for (int j = 0; j < ausstehendeSpiele.Count; j += 2) {
                    int spieler1neu; int spieler2neu;
                    if (times5) {
                        spieler1neu = makeGamex5(spielstärken, ausstehendeSpiele[j], rnd) ? ausstehendeSpiele[j].Item1 : ausstehendeSpiele[j].Item2;
                        spieler2neu = makeGamex5(spielstärken, ausstehendeSpiele[j + 1], rnd) ? ausstehendeSpiele[j + 1].Item1 : ausstehendeSpiele[j + 1].Item2;
                    }
                    else {
                        spieler1neu = makeGame(spielstärken, ausstehendeSpiele[j], rnd) ? ausstehendeSpiele[j].Item1 : ausstehendeSpiele[j].Item2;
                        spieler2neu = makeGame(spielstärken, ausstehendeSpiele[j + 1], rnd) ? ausstehendeSpiele[j + 1].Item1 : ausstehendeSpiele[j + 1].Item2;
                    }
                    ausstehendeSpieleNeu.Add(new Tuple<int, int>(spieler1neu, spieler2neu));
                }
                //printAusstehende(ausstehendeSpiele);
                ausstehendeSpiele.Clear();
                ausstehendeSpiele = new List<Tuple<int, int>>(ausstehendeSpieleNeu);
                ausstehendeSpieleNeu.Clear();
            }

            //finale:
            //printAusstehende(ausstehendeSpiele);
            if (times5) {
                return makeGamex5(spielstärken, ausstehendeSpiele[0], rnd) ? ausstehendeSpiele[0].Item1 : ausstehendeSpiele[0].Item2;
            }
            else {
                return makeGame(spielstärken, ausstehendeSpiele[0], rnd) ? ausstehendeSpiele[0].Item1 : ausstehendeSpiele[0].Item2;
            }
        }

        public static void printAusstehende(List<Tuple<int, int>> ausstehendeSpiele) {
            Console.WriteLine();
            for (int i = 0; i < ausstehendeSpiele.Count; i++) {
                Console.Write("{0} vs {1}   ", ausstehendeSpiele[i].Item1, ausstehendeSpiele[i].Item2);
            }
            Console.WriteLine();
        }

        #endregion


        #region allgemeine Funktionen

        public static bool makeGame(int[] spielstärken, Tuple<int, int> spieler, Random rnd) {
            int rn = rnd.Next(0, spielstärken[spieler.Item1] + spielstärken[spieler.Item2]);
            bool ret = rn < spielstärken[spieler.Item1];
            return ret;
        }

        public static bool makeGamex5(int[] spielstärken, Tuple<int, int> spieler, Random rnd) {
            int gewinneSp1 = 0;
            int gewinneSp2 = 0;
            for (int i = 0; i < 5; i++) {
                int rn = rnd.Next(0, spielstärken[spieler.Item1] + spielstärken[spieler.Item2]);
                if (rn < spielstärken[spieler.Item1]) { gewinneSp1++; }
                else { gewinneSp2++; }
            }
            return gewinneSp1 > gewinneSp2;
        }

        public static int getHighest(int[] arr) {
            int ind = -1;
            int high = -1;
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] > high) {
                    high = arr[i];
                    ind = i;
                }
            }
            return ind;
        }


        public static int[] readFile(string pfad = @"C:\Users\Jakov\Desktop\git\BWInf 20\BwInf 39.1.3 Tobi Turnier\spielstaerken4.txt") {
            List<string> lines = new List<string>();
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(pfad);
            for (int i = 0; (line = file.ReadLine()) != null; i++) {
                lines.Add(line);
            }
            file.Close();

            int spieler = int.Parse(lines[0].Trim());
            int[] spielstärken = new int[spieler];
            for (int i = 1; i < lines.Count; i++) {
                spielstärken[i - 1] = int.Parse(lines[i].Trim());
                Console.WriteLine(spielstärken[i - 1]);
            }
            return spielstärken;
        }

        #endregion
    }
}
