using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaVacheEstDansLePre
{
    class CalculPre
    {
        private List<Piquet> listePiquets;

        public CalculPre(List<Piquet> listePiquets)
        {
            this.listePiquets = listePiquets;
        }

        public List<Piquet> ListePiquet
        {
            get { return listePiquets; }
            set { listePiquets = value; }
        }

        public double CalculAire()
        {
            double abscisse1;
            double ordonne1;

            double abscisse2;
            double ordonne2;

            double m_aire = 0;

            double liaisonSegment = ListePiquet.ElementAt(ListePiquet.Count - 1).Abscisse * ListePiquet.ElementAt(0).Ordonnee
                                    - ListePiquet.ElementAt(0).Abscisse * ListePiquet.ElementAt(ListePiquet.Count - 1).Ordonnee;

            for (int segment = 0; segment < listePiquets.Count - 1; segment++)
            {
                abscisse1 = ListePiquet.ElementAt(segment).Abscisse;
                ordonne1 = ListePiquet.ElementAt(segment).Ordonnee;
                abscisse2 = ListePiquet.ElementAt(segment + 1).Abscisse;
                ordonne2 = ListePiquet.ElementAt(segment + 1).Ordonnee;
                m_aire += (abscisse1 * ordonne2) - (abscisse2 * ordonne1);
            }
            m_aire += liaisonSegment;

            return m_aire / 2;
        }


    }
}
