using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaVacheEstDansLePre
{
    public class CalculPre
    {
        private List<Piquet> listePiquets;
        private double Gx;
        private double Gy;

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

        public string CalculCentreGravite()
        {
            double abscisseGx = 0;
            double ordonneGy = 0;

            double abscisse1;
            double ordonne1;
            double abscisse2;
            double ordonne2;

            double liaisonX;
            double liaisonY;

            string centreGravite;

            double m_aire = CalculAire();

            for (int segment = 0; segment < ListePiquet.Count - 1; segment++)
            {
                abscisse1 = ListePiquet.ElementAt(segment).Abscisse;
                ordonne1 = ListePiquet.ElementAt(segment).Ordonnee;
                abscisse2 = ListePiquet.ElementAt(segment + 1).Abscisse;
                ordonne2 = ListePiquet.ElementAt(segment + 1).Ordonnee;
                abscisseGx += (abscisse1 + abscisse2) * (abscisse1 * ordonne2 - abscisse2 * ordonne1);
                ordonneGy += (ordonne1 + ordonne2) * (abscisse1 * ordonne2 - abscisse2 * ordonne1);
            }

            liaisonX = (ListePiquet.ElementAt(ListePiquet.Count - 1).Abscisse + ListePiquet.ElementAt(0).Abscisse)
                        * ((ListePiquet.ElementAt(ListePiquet.Count - 1).Abscisse * ListePiquet.ElementAt(0).Ordonnee)
                        - ListePiquet.ElementAt(0).Abscisse * ListePiquet.ElementAt(ListePiquet.Count - 1).Ordonnee);

            liaisonY = (ListePiquet.ElementAt(ListePiquet.Count - 1).Ordonnee + ListePiquet.ElementAt(0).Ordonnee)
                        * ((ListePiquet.ElementAt(ListePiquet.Count - 1).Abscisse * ListePiquet.ElementAt(0).Ordonnee)
                        - ListePiquet.ElementAt(0).Abscisse * ListePiquet.ElementAt(ListePiquet.Count - 1).Ordonnee);

            abscisseGx = Math.Round((1 / (6 * m_aire)) * (abscisseGx + liaisonX), 6);
            ordonneGy = Math.Round((1 / (6 * m_aire)) * (ordonneGy + liaisonY), 6);

            this.Gx = abscisseGx;
            this.Gy = ordonneGy;

            centreGravite = abscisseGx + ", " + ordonneGy;

            return centreGravite;
        }

        public string CalculAppartenance()
        {
            double abscisse1;
            double ordonne1;
            double abscisse2;
            double ordonne2;

            double coordsVectX1;
            double coordsVectY1;
            double coordsVectX2;
            double coordsVectY2;

            double centreGx = this.Gx;
            double centreGy = this.Gy;

            double scalaire;
            double produit;

            double normCoordsVectX2;
            double normCoordsVectY2;

            double determinant;

            double somme = 0.0;
            double thetaI;

            string appartenance;

            for (int segment = 0; segment < ListePiquet.Count - 1; segment++)
            {
                abscisse1 = ListePiquet.ElementAt(segment).Abscisse;
                ordonne1 = ListePiquet.ElementAt(segment).Ordonnee;
                abscisse2 = ListePiquet.ElementAt(segment + 1).Abscisse;
                ordonne2 = ListePiquet.ElementAt(segment + 1).Ordonnee;

                //Calcul des coordonnées des vecteurs
                coordsVectX1 = abscisse1 - Gx;
                coordsVectY1 = ordonne1 - Gy;
                coordsVectX2 = abscisse2 - Gx;
                coordsVectY2 = ordonne2 - Gy;

                //Calcul du produit scalaire
                scalaire = (coordsVectX1 * coordsVectX2) + (coordsVectY1 * coordsVectY2);

                //Calcul norme vecteur
                normCoordsVectX2 = (double)Math.Sqrt((coordsVectX1 * coordsVectX1) + (coordsVectY1 * coordsVectY1));
                normCoordsVectY2 = (double)Math.Sqrt((coordsVectX2 * coordsVectX2) + (coordsVectY2 * coordsVectY2));

                produit = normCoordsVectX2 * normCoordsVectY2;
                
                //Calcul Determinant
                determinant = (coordsVectX1 * coordsVectY2) - (coordsVectY1 * coordsVectX2);


                // Recherche du signe de l'angle
                if (determinant>=0)
                {
                    //Calcul thetaI : l’angle signé entre les segments => signe positif
                    thetaI = (double)Math.Acos(scalaire / produit);
                }
                else
                {
                    //Calcul thetaI : l’angle signé entre les segments => signe négatif
                    thetaI = -(double)Math.Acos(scalaire / produit);
                }

                somme += thetaI;
            }

            coordsVectX1 = ListePiquet.ElementAt(ListePiquet.Count - 1).Abscisse - centreGx;
            coordsVectY1 = ListePiquet.ElementAt(ListePiquet.Count - 1).Ordonnee - centreGy;
            coordsVectX2 = ListePiquet.ElementAt(0).Abscisse - centreGx;
            coordsVectY2 = ListePiquet.ElementAt(0).Ordonnee - centreGy;
            normCoordsVectX2 = (double)Math.Sqrt((coordsVectX1 * coordsVectX1) + (coordsVectY1 * coordsVectY1));
            normCoordsVectY2 = (double)Math.Sqrt((coordsVectX2 * coordsVectX2) + (coordsVectY2 * coordsVectY2));
            produit = normCoordsVectX2 * normCoordsVectY2;
            scalaire = (coordsVectX1 * coordsVectX2) + (coordsVectY1 * coordsVectY2);
            thetaI = (double)Math.Acos(scalaire / produit);
            determinant = (coordsVectX1 * coordsVectX2) - (coordsVectY1 * coordsVectY2);
            somme += thetaI;

            if (somme == 0)
            {
                appartenance = "Attention, la vache est hors du pré";
                Console.Out.WriteLine("Somme des angles theta: {0}", somme);
            }
            else
            {
                Console.Out.WriteLine("Somme des angles theta: {0}", somme);
                appartenance = "La vache est bien dans le pré";
            }

            return appartenance;
        }
    }
}
