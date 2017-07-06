using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace SolitaireGames
{
    public class State
    {
        State parent = null;
        public int cost = 0;
        public List<List<PlayingCard>> tabla; //tableus
        public String potez;
        public bool nemaVisePoteza;

        public State()
        {
            tabla = new List<List<PlayingCard>>();
            nemaVisePoteza = true;
        }
       
        public State sledeceStanje()
        {
            State rez = new State();
            rez.parent = this;
            rez.cost = this.cost + 1;
            //for(int i=0; i<this.tabla.Count; i++)
            //{
            //    rez.tabla.Add(this.tabla[i]);
            //}

            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu
            //TODO2: Prosiriti metodu tako da se ne moze prolaziti kroz sive kutije
            List<State> rez = new List<State>();

            //proveravamo za svaki od 10 setova karata
            for(int i=0; i<10; i++)
            {
                if (tabla[i].Count > 0)
                {
                    PlayingCard temp = tabla[i][tabla[i].Count - 1];
                    List<PlayingCard> listaKarata = new List<PlayingCard>();
                    listaKarata.Add(temp);
                    int brojac = 2;
                    while(!tabla[i][tabla[i].Count - brojac].IsFaceDown && tabla[i][tabla[i].Count - brojac].Suit == temp.Suit
                        && tabla[i][tabla[i].Count - brojac].Value == temp.Value + 1)
                    {
                        temp = tabla[i][tabla[i].Count - brojac];
                        listaKarata.Add(temp);
                        brojac++;
                    }

                    for(int j=0; j<10; j++)
                    {
                        if (j != i) {

                            if (tabla[j].Count == 0)
                            {
                                State sledece = this.sledeceStanje();
                                sledece.tabla[i] = sledece.tabla[i].Except(listaKarata).ToList();
                                sledece.tabla[j].AddRange(tabla[i]);
                                sledece.potez = "Pomeri sa pozicije " + (i+1) + " na poziciju " + (j+1);
                                nemaVisePoteza = false;
                                rez.Add(sledece);
                            }
                            else if(!tabla[j][tabla[j].Count - 1].IsFaceDown && temp.Suit == tabla[j][tabla[j].Count-1].Suit && temp.Value == tabla[j][tabla[j].Count - 1].Value - 1)
                            {
                                State sledece = this.sledeceStanje();
                                sledece.tabla = this.tabla;
                                sledece.tabla[i] = sledece.tabla[i].Except(listaKarata).ToList();
                                sledece.tabla[j].AddRange(tabla[i]);
                                sledece.potez = "Pomeri sa pozicije " + (i+1) + " na poziciju " + (j+1);
                                nemaVisePoteza = false;
                                rez.Add(sledece);
                            }
                        }
                    }
                }
            }

            return rez;
        }

        public override int GetHashCode()
        {
            //prepraviti
            return cost*cost + 73;
        }

        public bool isKrajnjeStanje() { 

            if(cost > 2){
                return true;
            }
            return false;
        //{
        //    if(nemaVisePoteza)
        //    {
        //        return true;
        //    }
        //    return false;
            //return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && kutija;
        }

        public void ispisiPotez()
        {
            Console.Write(potez + "\n");
        }

        public List<State> path()
        {
            List<State> putanja = new List<State>();
            State tt = this;
            while (tt != null)
            {
                putanja.Insert(0, tt);
                tt = tt.parent;
            }
            return putanja;
        }

        
    }
}
