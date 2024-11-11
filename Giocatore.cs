using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello {
    using System;

    public class Giocatore : IGiocatore {
        public string Nome { get; private set; }
        public char Colore { get; private set; }

        public Giocatore(string nome, char colore) {
            Nome = nome;
            Colore = colore;
        }

        public (int, int) EffettuaMossa(Scacchiera scacchiera) {
            bool errore = true;
            int riga = 0;
            int colonna = 0;

            do {
                try {
                    Console.Write($"{Nome} ({Colore}) - Inserisci mossa (es: E3): ");
                    string mossa = Console.ReadLine();

                    if (mossa.Length != 2 || !char.IsLetter(mossa[0]) || !char.IsDigit(mossa[1])) {
                        throw new Exception("Formato mossa non valido");
                    }

                    riga = int.Parse(mossa.Substring(1)) - 1;
                    colonna = char.ToUpper(mossa[0]) - 'A';

                    if (riga < 0 || riga >= 8 || colonna < 0 || colonna >= 8) {
                        throw new Exception("Mossa fuori dai limiti");
                    }

                    errore = false;
                }
                catch (Exception ex) {
                    Console.WriteLine($"Errore: {ex.Message}");
                    errore = true;
                }
            }
            while (errore);

            return (riga, colonna);
        }
    }



}
