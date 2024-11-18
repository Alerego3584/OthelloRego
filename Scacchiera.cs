using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Othello {
    public class Scacchiera {
        public const int Dimensione = 8;
        public char[,] Griglia { get; private set; }

        public Scacchiera() {
            Griglia = new char[Dimensione, Dimensione];
            Inizializza();
        }

        public void Inizializza() {
            for (int i = 0; i < Dimensione; i++) {
                for (int j = 0; j < Dimensione; j++) {
                    Griglia[i, j] = '-';
                }
            }

            Griglia[3, 3] = 'B';
            Griglia[3, 4] = 'N';
            Griglia[4, 3] = 'N';
            Griglia[4, 4] = 'B';
        }

        //public void Visualizza() {
        //    Console.WriteLine("  A B C D E F G H");
        //    for (int i = 0; i < Dimensione; i++) {
        //        Console.Write($"{i + 1} ");
        //        for (int j = 0; j < Dimensione; j++) {
        //            Console.Write($"{Griglia[i, j]} ");
        //        }
        //        Console.WriteLine();
        //    }
        //}
        public void Visualizza(char coloreGiocatore = 'N') {
            Console.WriteLine("  A B C D E F G H");
            for (int riga = 0; riga < Dimensione; riga++) {
                Console.Write($"{riga + 1} ");
                for (int colonna = 0; colonna < Dimensione; colonna++) {
                    // Verifica se la mossa è valida
                    if (Griglia[riga, colonna] == '-' && MossaValida(riga, colonna, coloreGiocatore)) {
                        Console.Write("* ");
                    }
                    else {
                        Console.Write($"{Griglia[riga, colonna]} ");
                    }
                }
                Console.WriteLine();
            }
        }

        public bool MossaValida(int riga, int colonna, char coloreGiocatore) {
            if (Griglia[riga, colonna] != '-') return false;
            char coloreAvversario = (coloreGiocatore == 'N') ? 'B' : 'N';

            int[][] direzioni = {
                new[] {-1, 0}, new[] {1, 0}, new[] {0, -1}, new[] {0, 1},
                new[] {-1, -1}, new[] {-1, 1}, new[] {1, -1}, new[] {1, 1}
        };

            foreach (int[] direzione in direzioni) {
                int x = riga + direzione[0];
                int y = colonna + direzione[1];
                bool haAvversarioInMezzo = false;

                while (x >= 0 && x < Dimensione && y >= 0 && y < Dimensione) {
                    if (Griglia[x, y] == coloreAvversario) {
                        haAvversarioInMezzo = true;
                    }
                    else if (Griglia[x, y] == coloreGiocatore) {
                        if (haAvversarioInMezzo) return true;
                        break;
                    }
                    else break;

                    x += direzione[0];
                    y += direzione[1];
                }
            }
            
            return false;
        }

        public void EffettuaMossa(int riga, int colonna, char coloreGiocatore) {
            if (!MossaValida(riga, colonna, coloreGiocatore)) return;

            Griglia[riga, colonna] = coloreGiocatore;
            char coloreAvversario = (coloreGiocatore == 'N') ? 'B' : 'N';

            int[][] direzioni = {
            new[] {-1, 0}, new[] {1, 0}, new[] {0, -1}, new[] {0, 1},
            new[] {-1, -1}, new[] {-1, 1}, new[] {1, -1}, new[] {1, 1}
        };

            foreach (int[] direzione in direzioni) {
                List<(int, int)> celleDaGirare = new();
                int x = riga + direzione[0];
                int y = colonna + direzione[1];

                while (x >= 0 && x < Dimensione && y >= 0 && y < Dimensione) {
                    if (Griglia[x, y] == coloreAvversario) {
                        celleDaGirare.Add((x, y));
                    }
                    else if (Griglia[x, y] == coloreGiocatore) {
                        foreach ((int, int) cella in celleDaGirare) {
                            Griglia[cella.Item1, cella.Item2] = coloreGiocatore;
                        }
                        break;
                    }
                    else break;

                    x += direzione[0];
                    y += direzione[1];
                }
            }
        }

        public int ContaPedine(char colore) {
            int conteggio = 0;
            for (int riga = 0; riga < Dimensione; riga++) {
                for (int colonna = 0; colonna < Dimensione; colonna++) {
                    if (Griglia[riga, colonna] == colore) {
                        conteggio++;
                    }
                }
            }
            return conteggio;
        }

        public bool CiSonoMosseValide(char colore) {
            for (int riga = 0; riga < Dimensione; riga++) {
                for (int colonna = 0; colonna < Dimensione; colonna++) {
                    if (MossaValida(riga, colonna, colore)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool NessunaMossaDisponibile() {
            return !CiSonoMosseValide('N') && !CiSonoMosseValide('B');
        }
    }
}
