using Jovana.Model;
using Jovana.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G6_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            /*service.Izvestaj1("L1");

            service.Izvestaj2();
*/

            int izbor;
            while (1 == 1)
            {
                Console.Clear();
                Console.WriteLine("Izaberite opciju:");
                Console.WriteLine("1 - Izvestaj1");
                Console.WriteLine("2 - Izvestaj2");
                Console.WriteLine("3 - Izvestaj3");
                Console.WriteLine("4 - Izvestaj4");
                Console.WriteLine("5 - Exit");

                izbor = Int32.Parse(Console.ReadLine());

                switch (izbor)
                {
                    case 1:
                        Console.WriteLine("Izaberite lice: ");
                        string str = Console.ReadLine();
                        service.Izvestaj1(str);
                        break;
                    case 2:
                        service.Izvestaj2();
                        break;
                    case 3:
                        do
                        {
                            Objekat objekat = new Objekat();
                            try
                            {
                                Console.WriteLine("Unesite ido novog objekta: ");
                                objekat.Ido = int.Parse(Console.ReadLine());

                                Console.WriteLine("Unesite idl novog objekta: ");
                                objekat.Idl = Console.ReadLine();

                                Console.WriteLine("Unesite idvo novog objekta: ");
                                objekat.Idvo = int.Parse(Console.ReadLine());

                                Console.WriteLine("Unesite povrsinu novog objekta: ");
                                objekat.Povrsina = int.Parse(Console.ReadLine());

                                Console.WriteLine("Unesite adresu novog objekta: ");
                                objekat.Adresa = Console.ReadLine();

                                Console.WriteLine("Unesite vrednost novog objekta: ");
                                objekat.Vrednost = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                Console.WriteLine("Probajte opet...");
                                continue;
                            }

                            if (objekat.Ido < 1 || objekat.Idvo < 0 || objekat.Povrsina < 0 || objekat.Vrednost < 0)
                            {
                                Console.WriteLine("Probajte opet...");
                                continue;
                            }

                            service.Izvestaj3(objekat);
                            break;
                        } while (true);
                        break;
                    case 4:
                        Console.WriteLine("Odaberite tip: ");
                        string str2 = Console.ReadLine();
                        service.Izvestaj4(str2);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Pogresna opcija, probajte opet.");
                        break;
                }

                Console.WriteLine("Pritisnite bilo koji taster kada ste zavrsili sa citanjem izvestaja.");
                Console.ReadKey();
            }
        }
    }
}
