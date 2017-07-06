using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SolitaireGames
{
    class DepthFirstSearch
    {
        public State search(State pocetnoStanje)
        {
            List<State> stanjaNaObradi = new List<State>();
            Hashtable predjeniPut = new Hashtable();
            stanjaNaObradi.Add(pocetnoStanje);
            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = stanjaNaObradi[stanjaNaObradi.Count - 1];
                naObradi.ispisiPotez();

                if (naObradi.isKrajnjeStanje())
                {
                    return naObradi;
                }
                List<State> mogucaSledecaStanja = naObradi.mogucaSledecaStanja();
                foreach (State sledeceStanje in mogucaSledecaStanja)
                {
                    stanjaNaObradi.Add(sledeceStanje);
                }
                
                stanjaNaObradi.Remove(naObradi);
            }
            return null;
        }
    }
}
