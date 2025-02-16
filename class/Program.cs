using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tridy_procvicovani
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> studenti = new List<Student>();  // Seznam studentů

            // Přidání studentů pomocí while cyklu
            while (true)
            {
                Console.WriteLine("Zadejte jméno studenta (nebo stiskněte Enter pro ukončení):");
                string jmeno = Console.ReadLine();
                if (string.IsNullOrEmpty(jmeno)) break;  // Ukončení cyklu, pokud je vstup prázdný

                Student novyStudent = new Student(jmeno);
                studenti.Add(novyStudent);  // Přidání studenta do seznamu
            }

            // Pro každý student kontrolujeme průměr a přidáváme známky
            foreach (var student in studenti)
            {
                Console.WriteLine($"\nZadejte 5 známek pro studenta {student.Jmeno}:");
                try
                {
                    for (int i = 0; i < 5; i++)
                    {
                        student.Znamky.Add(Convert.ToInt32(Console.ReadLine()));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Něco se pokazilo při zadávání známek.");
                }

                // Kontrola průměru pro každého studenta
                student.KontrolaPrumeru();
            }

            Console.WriteLine("\nPočet studentů: " + studenti.Count);

            // Zobrazíme seznam všech studentů
            Console.WriteLine("\nSeznam všech studentů:");
            foreach (var student in studenti)
            {
                student.ZobrazInfo();
            }

            // Umožníme upravit známky pro konkrétního studenta
            Console.WriteLine("\nZadejte jméno studenta, pro kterého chcete upravit známky:");
            string upravitJmeno = Console.ReadLine();

            var studentUpravit = studenti.FirstOrDefault(s => s.Jmeno.Equals(upravitJmeno, StringComparison.OrdinalIgnoreCase));

            if (studentUpravit != null)
            {
                Console.WriteLine($"\nUpravíme známky studenta {studentUpravit.Jmeno}:");
                studentUpravit.UpravZnamku();

                Console.WriteLine("\nAktualizované známky:");
                studentUpravit.ZobrazInfo();
            }
            else
            {
                Console.WriteLine("Student s tímto jménem nebyl nalezen.");
            }

            Console.ReadKey();
        }

        /// TODO:
        // - (studenti se bodou pridavat ve while cykl v mainu)
        // - opravte konrolu průměru pro situaci, kdy nejsou zadány žádné známky
        // - kontrola počtu studentů bude vlastní metoda
        // - přidání známky přepište na vlastní metodu (v rámci třídy)
        // - přidejte metodu, která zobrazuje jména všech studentů
        // - přidejte metodu, která bude známky upravovat (mazat, editovat, ... )
        // - zobrazení průměru všech studentů
    }
    public class Student
    {
        public string Jmeno;
        public List<int> Znamky;
        public static int seznamStudentu = 0;
        private static List<Student> studenti = new List<Student>();
        public Student(string jmeno)
        {
            Jmeno = jmeno;
            Znamky = new List<int>();
            seznamStudentu++;
            studenti.Add(this);
        }
        public void PridejZnamku(int znamka)
        {
            if (znamka >= 1 && znamka <= 5)
            {
                Znamky.Add(znamka);
                Console.WriteLine("Známka přidána.");
            }
            else
            {
                Console.WriteLine("Neplatná známka, zadejte hodnotu mezi 1 a 5.");
            }
        }
        public void ZobrazInfo()
        {
            Console.WriteLine($"Jméno: {Jmeno}\nZnámky: {string.Join(", ", Znamky)}\nPrůměrná známka: {PrumernaZnamka():F2}");
        }
        public double PrumernaZnamka()
        {
            return Znamky.Count > 0 ? Znamky.Average() : 0;
        }
        public void KontrolaPrumeru()
        {
            try
            {
                double prumer = PrumernaZnamka();
                if (prumer < 1.5)
                {
                    Console.WriteLine("Student má nárok na stipendium.");
                }
                else
                {
                    Console.WriteLine("Student nemá nárok na stipendium.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("neco se pokazilo");
            }
        }
        public void UpravZnamku()
        {
            Console.WriteLine($"Aktuální známky studenta {Jmeno}: {string.Join(", ", Znamky)}");
            Console.WriteLine("Co chcete udělat?");
            Console.WriteLine("1 - Smazat známku");
            Console.WriteLine("2 - Upravit známku");
            Console.WriteLine("3 - Přidat novou známku");
            Console.Write("Vyberte možnost: ");

            int volba;
            try
            {
                volba = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Neplatná volba.");
                return;
            }

            switch (volba)
            {
                case 1:
                    SmazZnamku();
                    break;
                case 2:
                    EditujZnamku();
                    break;
                case 3:
                    Console.Write("Zadejte novou známku: ");
                    int novaZnamka;
                    if (int.TryParse(Console.ReadLine(), out novaZnamka))
                    {
                        PridejZnamku(novaZnamka);
                    }
                    else
                    {
                        Console.WriteLine("Neplatná hodnota.");
                    }
                    break;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
        private void SmazZnamku()
        {
            Console.Write("Zadejte číslo známky k odstranění (1 až " + Znamky.Count + "): ");
            int index;
            if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= Znamky.Count)
            {
                Znamky.RemoveAt(index - 1);
                Console.WriteLine("Známka odstraněna.");
            }
            else
            {
                Console.WriteLine("Neplatná volba.");
            }
        }

        private void EditujZnamku()
        {
            Console.Write("Zadejte číslo známky k úpravě (1 až " + Znamky.Count + "): ");
            int index;
            if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= Znamky.Count) 
                /*Vysvětlení:
                int.TryParse(Console.ReadLine(), out index)
                    Pokusí se převést vstup (Console.ReadLine()) na celé číslo (index).
                    Pokud je vstup číslo, uloží jej do index a vrátí true.
                    Pokud je vstup neplatný (např. text), vrátí false a index se nemění.

                index > 0 && index <= Znamky.Count
                    Ověřuje, zda je číslo v platném rozsahu seznamu Znamky:
                        index > 0 – Musí být větší než 0 (indexace začíná od 1, nikoliv od 0).
                        index <= Znamky.Count – Nesmí být větší než počet známek v seznamu.*/
            {
                Console.Write("Zadejte novou hodnotu známky: ");
                int novaHodnota;
                if (int.TryParse(Console.ReadLine(), out novaHodnota) && novaHodnota >= 1 && novaHodnota <= 5)
                {
                    Znamky[index - 1] = novaHodnota;
                    Console.WriteLine("Známka upravena.");
                }
                else
                {
                    Console.WriteLine("Neplatná hodnota.");
                }
            }
            else
            {
                Console.WriteLine("Neplatná volba.");
            }
        }
    }
}