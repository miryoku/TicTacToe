using System;
using System.Collections.Generic;

namespace TicTacToe
{
    internal class Program
    {
        public static void PeupleMAp(ref char[,] map, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = '-';
                }
            }
        }

        public static void AfficheMap(char[,] map, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(map[i, j]+" ");
                }
                Console.WriteLine();
            }
        }

        public static int CheckPos(int size)
        {
            int x;
            while (!int.TryParse(Console.ReadLine(), out x) || (x <= 0 || x > size))
            {
                Console.WriteLine("erreur");
            }

            return x;
        }

        public static int EntreVal(char c, int size)
        {
            Console.WriteLine($"Rentre l'axe {c}");
            return CheckPos(size) - 1;
        }

        public static bool SearchList(List<int> list, int valeur)
        {
            bool flag = false;
            for (int j = 0; j < list.Count && !flag; j++)
            {
                if (list[j] == valeur)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static bool CheckWin(int x, int y, int size, char[,] Map, char logo)
        {
            int win = 0;
            for (int j = 0; j < size; j++)
            {
                if (Map[x, j] == logo)
                {
                    win++;
                }
            }
            if (win == 3)
            {
                return true;
            }
            win = 0;
            for (int j = 0; j < size; j++)
            {
                if (Map[j, y] == logo)
                {
                    win++;
                }
            }
            if (win == 3)
            {
                return true;
            }
            win = 0;
            for (int j = 0; j < size; j++)
            {
                if (Map[j, j] == logo)
                {
                    win++;
                }
            }
            if (win == 3)
            {
                return true;
            }
            win = 0;
            for (int j = 0; j < size; j++)
            {
                if (Map[j, size-j] == logo)
                {
                    win++;
                }
            }
            if (win == 3)
            {
                return true;
            }

            return false;
        }

        public static bool Replay()
        {
            bool b = false;
            bool exit = false;
            do
            {
                Console.WriteLine("Voulez-vous recommence ? (O/N)");
                string mot = Console.ReadLine();
                if (mot.ToUpper() == "O") { b = true; exit = true; }
                else if (mot.ToUpper() == "N") { b = false; exit = true; }
                else Console.WriteLine("error");
            } while (!exit);
            return b;
        }

        static void Main(string[] args)
        {
            int size = 3, x, y, val;
            char[,] Map = new char[size, size];
            char[] logo = { 'X', 'O' };
            do
            {
                List<int> cible = new List<int>();
                bool flag = false;
                PeupleMAp(ref Map, size);
                AfficheMap(Map, size);
                int tour = 0;
                for (int i = 0; i < 9 && !flag; i++)
                {

                    do
                    {
                        flag = false;
                        Console.WriteLine($"Le joueur {(tour % 2) + 1}");
                        x = EntreVal('X', size);
                        y = EntreVal('Y', size);
                        val = ((int)((char)(x + 48)) + (int)((char)(y + 48))) * (x + 1);
                        if (cible.Count != 0)
                        {
                            flag = SearchList(cible, val);
                        }
                        else
                        {
                            cible.Add(val);
                        }
                    } while (flag);
                    cible.Add(val);
                    Map[x, y] = logo[(tour % 2)];
                    Console.Clear();
                    AfficheMap(Map, size);
                    flag = false;

                    flag = CheckWin(x, y, size, Map, logo[tour % 2]);
                    if (flag)
                    {
                        Console.WriteLine($"c'est gagne pour le joueur {(tour % 2) + 1} ");
                    }
                    tour++;

                }
                flag = false;
            } while (Replay());

        }
    }
}