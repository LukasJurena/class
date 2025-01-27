
namespace classtridy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kolik studentů chcete zadat?");
            int pocetStudentu = int.Parse(Console.ReadLine()); //zadani poctu uzivatelu

            Student[] studenti = new Student[pocetStudentu]; //vytvoreni pole podle poctu uzivatelu

            for (int i = 0; i < pocetStudentu; i++) //foreach na kazdeho studenta
            {
                Console.WriteLine($"Zadejte jméno studenta {i + 1}:"); //zadani jmeno studenta {cislo studenta}
                string jmeno = Console.ReadLine();                     //

                Console.WriteLine($"Kolik známek má student {jmeno}?"); //pocet znamek na jmeno studenta
                int pocetZnamek = int.Parse(Console.ReadLine()); //zadani
                int[] znamky = new int[pocetZnamek]; // zapsani kolik znamek ma

                for (int j = 0; j < pocetZnamek; j++) //zadavani znamek podle poctu znamek
                {
                    Console.WriteLine($"Zadejte známku {j + 1}:"); 
                    znamky[j] = int.Parse(Console.ReadLine());
                }

                studenti[i] = new Student(jmeno, znamky); //zapsani studenta a znamek
            }

            Console.WriteLine("\n--- Informace o studentech ---");
            foreach (var student in studenti)

            {
                student.ZobrazInfo();
                student.KontrolaPrumeru();
                Console.WriteLine();
            }
        }

    }

    public class Student
    {
        //atributy
        public string Jmeno;
        public double PrumernaZnamka;
        public int[] Znamky;
        //konstruktor
        public Student(string jmeno, int[] znamky)
        {
            Jmeno = jmeno;
            Znamky = znamky;
            PrumernaZnamka = VypocitejPrumer();
        }
        //metody
        // zobraz inforamce; kontrola prumeru
        public void ZobrazInfo()
        {
            Console.WriteLine("Jméno studenta: " + Jmeno);
            Console.WriteLine("Známky: " + string.Join(", ", Znamky));
            Console.WriteLine("Průměrná známka: " + PrumernaZnamka.ToString("F2"));
            Console.WriteLine("--------------------------------------------------");
        }
        private double VypocitejPrumer()
        {
            int soucet = 0;
            foreach (int znamka in Znamky)
            {
                soucet += znamka;
            }
            return (double) soucet / Znamky.Length;
        }
        public void KontrolaPrumeru()
        {
            if (PrumernaZnamka >= 2)
            {
                Console.WriteLine("Průměr je vyšší než 2.");
            }
            else
            {
                Console.WriteLine("Průměr je nižší než 2.");
            }
        }
    }
}