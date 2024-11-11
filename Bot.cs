using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello {
    using System;
    using System.Collections.Generic;

    public class Bot : IGiocatore {
        public string Nome { get; private set; }
        public char Colore { get; private set; }
        private Random random;

        public Bot(string nome, char colore) {
            Nome = nome;
            Colore = colore;
            random = new Random();
        }

        public (int, int) EffettuaMossa(Scacchiera scacchiera) {
            Console.WriteLine($"{Nome} sta pensando...");
            List<(int, int)> mosseValide = TrovaMosseValide(scacchiera);

            if (mosseValide.Count > 0) {
                int indiceScelto = random.Next(mosseValide.Count);
                var mossa = mosseValide[indiceScelto];
                Console.WriteLine($"{Nome} ({Colore}) sceglie la mossa: {(char)('A' + mossa.Item2)}{mossa.Item1 + 1}");
                return mossa;
            }

            return (-1, -1); // Nessuna mossa valida
        }

        private List<(int, int)> TrovaMosseValide(Scacchiera scacchiera) {
            List<(int, int)> mosseValide = new List<(int, int)>();
            for (int riga = 0; riga < Scacchiera.Dimensione; riga++) {
                for (int colonna = 0; colonna < Scacchiera.Dimensione; colonna++) {
                    if (scacchiera.MossaValida(riga, colonna, Colore)) {
                        mosseValide.Add((riga, colonna));
                    }
                }
            }
            return mosseValide;
        }
    }

}
