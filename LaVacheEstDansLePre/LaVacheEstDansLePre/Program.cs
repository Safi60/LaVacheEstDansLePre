using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaVacheEstDansLePre
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculPre calculPre = new CalculPre(new List<Piquet>());

            int nombrePiquet;
            double abscisse;
            double ordonne;
            double aire;

            Console.Out.WriteLine("Saisir le nombre de piquets");
            nombrePiquet = int.Parse(Console.In.ReadLine());

            for (int compteurPiquet = 0; compteurPiquet < nombrePiquet; compteurPiquet++)
            {
                //Saisie des coordonnée {abscisse, ordonne} de chaque piquet
                Console.Out.WriteLine("Saisir le piquet {0}", compteurPiquet);
                abscisse = double.Parse(Console.In.ReadLine());
                ordonne = double.Parse(Console.In.ReadLine());

                Piquet newPiquet = new Piquet(abscisse, ordonne);
                newPiquet.Abscisse = abscisse;
                newPiquet.Ordonnee = ordonne;
                calculPre.ListePiquet.Add(newPiquet);
            }

            //Renvoie l'aire du polygone
            aire = Math.Abs(calculPre.CalculAire());
            Console.Out.WriteLine("Aire = {0}", aire);

        }
    }
}
