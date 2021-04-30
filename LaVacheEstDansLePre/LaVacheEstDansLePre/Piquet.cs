using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaVacheEstDansLePre
{
    public class Piquet
    {
        private double m_abscisse;
        private double m_ordonnee;

        public double Abscisse
        {
            get { return m_abscisse; }
            set { m_abscisse = value; }
        }

        public double Ordonnee
        {
            get { return m_ordonnee; }
            set { m_ordonnee = value; }
        }

        /// Constructeur de la classe Piquet
        public Piquet(double p_abscisse, double p_ordonnee)
        {
            this.m_abscisse = p_abscisse;
            this.m_ordonnee = p_ordonnee;
        }
    }
}
