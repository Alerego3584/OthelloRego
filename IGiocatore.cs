using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello {
    public interface IGiocatore {
        string Nome { get; }
        char Colore { get; }
        (int, int) EffettuaMossa(Scacchiera scacchiera);
    }

}
