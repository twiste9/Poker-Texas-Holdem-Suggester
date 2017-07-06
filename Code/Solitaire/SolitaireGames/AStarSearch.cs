using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SolitaireGames
{
    class AStarSearch
    {
        public State search(State pocetnoStanje)
        {
            List<State> stanjaZaObradu = new List<State>();
            Hashtable predjeniPut = new Hashtable();
            stanjaZaObradu.Add(pocetnoStanje);

            while (stanjaZaObradu.Count > 0)
            {
                State naObradi = getBest(stanjaZaObradu);
                // Console.Write(naObradi.isKrajnjeStanje() + "\n");
                naObradi.ispisiPotez();

                List<State> sledecaStanja = naObradi.mogucaSledecaStanja();
                //Main.allSearchStates.Add(naObradi);
                if (naObradi.isKrajnjeStanje())
                {
                    //Console.Write("Nema vise mogucih poteza.\n");
                    return naObradi;
                }
                predjeniPut.Add(naObradi.GetHashCode(),null);
    
                foreach (State s in sledecaStanja)
                {
                    stanjaZaObradu.Add(s);
                }
                
                stanjaZaObradu.Remove(naObradi);
            }
            return null;
        }

        //funkcija odredjuje verovatnocu za pobedu
        public double heuristicFunction(State s)
        {
            double rez = s.mogucaSledecaStanja().Count;

            return rez;
        }
       
        public State getBest(List<State> stanja)
        {
            State rez = stanja[0];

            //stanje sa najvise mogucih sledecih stanja
            for(int i=1; i<stanja.Count; i++)
            {
                if(stanja[i].mogucaSledecaStanja().Count > rez.mogucaSledecaStanja().Count)
                {
                    rez = stanja[i];
                }
                
            }

            return rez;
        }
        
    }
}
