using Jovana.DAO;
using Jovana.DAO.Impl;
using Jovana.DTO;
using Jovana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.Services
{
    class Service
    {
        private static readonly IObjekatDAO objekatDAO = new ObjekatDAO();
        private static readonly ILiceDAO<Lice> liceDAO = new LiceDAO();
        private static readonly IVrstaObjektaDAO vrstaObjektaDAO = new VrstaObjektaDAO();
        private static readonly IBilansStanjaDAO bilansStanjaDAO = new BilansStanjaDAO();

        public void Izvestaj1(string idl)
        {
            // Ovde izvlacimo sve objekte za odgovarajuci ID lica
            ObjekatIDL lica = objekatDAO.All(idl);

            //Ispisujemo vrednosti koje smo dobili
            Console.WriteLine("For: {0}", idl);
            Console.WriteLine("IDO   IDVO   POVRSINA   ADRESA     VREDNOST");

            foreach (Objekat obj in lica.Objekti)
            {
                Console.WriteLine($"{obj.Ido}     {obj.Idvo}      {obj.Povrsina}        {obj.Adresa}  {obj.Vrednost}");
            }

            //Dobijamo ukupnu cenu objekata za odredjeni idl
            int cenaObjekata = objekatDAO.CenaObjekata(idl);

            //Ovo je prakticno visak nasao usled testiranja to string metode, testirala si preko debugger-a...
            // Ako branis sa svog ekrana obrisi zajedno sa komentarima
            foreach (var obj in lica.Objekti)
            {
                obj.ToString();
            }

            Console.WriteLine($"Price : {cenaObjekata}");

        }

        public void Izvestaj2()
        {
            //Izvlacis sve objekte odredjenog tipa, u ovom slucaju tipa PRAVNO
            List<Objekat> objektiPravni = liceDAO.ObjByType("PRAVNO");
            //Izvlacis sva lica koja su tipa PRAVNO
            List<Lice> pravna = liceDAO.ByType("PRAVNO");

            //Izvlacis sve objekte odredjenog tipa, u ovom slucaju tipa FIZICKO
            List<Objekat> objektiFizicki = liceDAO.ObjByType("FIZICKO");
            //Izvlacis sva lica koja su tipa FIZICKO
            List<Lice> fizicka = liceDAO.ByType("FIZICKO");


            Console.WriteLine("Fizicka: ");
            Console.WriteLine("IDO   VRSTA OBJEKTA   VREDNOST    IDL   IMEL     PRZL");
            //Ispis svih fizickih lica, i za svako lice sve objekte koje poseduju
            foreach (var lice in fizicka)
            {
                foreach (var obj in objektiFizicki)
                {
                    if (lice.Idl.Equals(obj.Idl))
                    {
                        string vrstaobj = vrstaObjektaDAO.GetVrstaByID(obj.Idvo);
                        Console.WriteLine($"{obj.Ido}     {vrstaobj}                  {obj.Vrednost}   {obj.Idl}    {lice.Imel}       {lice.Przl}");
                    }
                }
            }


            Console.WriteLine($"Ukupan broj objekata: {objektiFizicki.Count()}; Ukupan Dug: {liceDAO.SumDug("FIZICKO")} dinara.");

            Console.WriteLine("Objekti pravnih lica: ");

            Console.WriteLine("IDO   VRSTA OBJEKTA   VREDNOST(€)    IDL   IMEL     PRZL");
            //Isto za pravna lica
            foreach (var lice in pravna)
            {
                foreach (var obj in objektiPravni)
                {
                    if (lice.Idl.Equals(obj.Idl))
                    {
                        string vrstaobj = vrstaObjektaDAO.GetVrstaByID(obj.Idvo);
                        Console.WriteLine($"{obj.Ido}     {vrstaobj}                  {obj.Vrednost}   {obj.Idl}    {lice.Imel}       {lice.Przl}");
                    }
                }
            }
            //Svaka lista ima ugradjenu count i ona broji koliko elemenata ta lista ima
            Console.WriteLine($"Sum obj: {objektiPravni.Count()}.");
            //Suma dugova za pravna lica
            Console.WriteLine($"Sum Dug: {liceDAO.SumDug("PRAVNO")}.");
        }

        internal void Izvestaj3(Objekat objekat)
        {
            if (!liceDAO.ExistsById(objekat.Idl))
            {
                Console.WriteLine("\nDato lice ne postoji.\n");
                return;
            }
            if (objekatDAO.ExistsById(objekat.Ido))
            {
                Console.WriteLine("\nObjekat.Ido vec postoji\n");
                return;
            }

            objekatDAO.Save(objekat);
            var bilans = bilansStanjaDAO.GetByIdl(objekat.Idl);
            bilans.Dug += bilans.Saldo / 120;
            bilans.Kamata += (decimal)0.1 * objekat.Vrednost;
            bilans.Saldo -= (bilans.Dug + bilans.Kamata);
            bilansStanjaDAO.Save(bilans);
        }

        public void Izvestaj4(string objectType)
        {
            int idvo = vrstaObjektaDAO.GetByName(objectType);
            if (idvo == -1)
            {
                Console.WriteLine("\nNije nadjen\n");
            }

            List<Objekat> objekats = objekatDAO.GetByType(idvo);

            int counter = 0;
            double sum = 0;
            foreach (var objekat in objekats)
            {
                Console.WriteLine(objekat);
                sum += objekat.Vrednost;
                counter++;
            }

            Console.WriteLine("\nProsecna cena objekata je " + sum / counter);

        }
    }

}
