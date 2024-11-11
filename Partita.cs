using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello {
    public class Partita {
        private Scacchiera scacchiera;
        private IGiocatore giocatore1;
        private IGiocatore giocatore2;
        private IGiocatore giocatoreCorrente;

        public Partita() {
            scacchiera = new Scacchiera();
            ConfiguraGiocatori();
        }

        private void ConfiguraGiocatori() {
            Console.WriteLine("Scegli modalità di gioco:");
            Console.WriteLine("1. Giocatore vs Giocatore");
            Console.WriteLine("2. Giocatore vs Bot");
            Console.Write("Inserisci scelta (1 o 2): ");
            string scelta = Console.ReadLine();

            if (scelta == "1") {
                giocatore1 = new Giocatore("Giocatore 1", 'N');
                giocatore2 = new Giocatore("Giocatore 2", 'B');
            }
            else {
                giocatore1 = new Giocatore("Giocatore", 'N');
                giocatore2 = new Bot("Bot", 'B');
            }

            giocatoreCorrente = giocatore1;
        }

        public void Inizia() {
            while (true) {
                scacchiera.Visualizza();
                (int riga, int colonna) mossa = giocatoreCorrente.EffettuaMossa(scacchiera);

                if (scacchiera.MossaValida(mossa.Item1, mossa.Item2, giocatoreCorrente.Colore)) {
                    scacchiera.EffettuaMossa(mossa.Item1, mossa.Item2, giocatoreCorrente.Colore);
                    giocatoreCorrente = (giocatoreCorrente == giocatore1) ? giocatore2 : giocatore1;
                }
                else {
                    Console.WriteLine("Mossa non valida, riprova.");
                }

                scacchiera.Visualizza();

                if (!scacchiera.NessunaMossaDisponibile()) {
                    Console.WriteLine("Nessuna mossa disponibile per entrambi i giocatori. Partita terminata!");
                    break;
                }
                CalcolaVincitore();
            }
        }



        private void CalcolaVincitore() {
            int pedineNere = scacchiera.ContaPedine('N');
            int pedineBianche = scacchiera.ContaPedine('B');

            Console.WriteLine($"Punteggio finale: Nero {pedineNere} - Bianco {pedineBianche}");
            if (pedineNere > pedineBianche) {
                Console.WriteLine("Nero vince!");
            }
            else if (pedineBianche > pedineNere) {
                Console.WriteLine("Bianco vince!");
            }
            else {
                Console.WriteLine("È un pareggio!");
            }
        }
    }





}

