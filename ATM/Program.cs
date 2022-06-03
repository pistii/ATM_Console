using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ATM
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("Üdvözlet az ATM-ben");
            Console.WriteLine("1. Regisztrálás\n2. Bejelentkezés\n3. Kilépés");
            string menu = Console.ReadLine();

            switch (menu)
            {
                case "1":
                    Registration();
                    Login();
                    break;
                case "2":
                    Login();
                    p.MenuChose();
                    break;
                case "3":
                    Console.WriteLine("Kilépés...");
                    break;
                default:
                    Console.WriteLine("Nincs ilyen menüpont.");
                    
                    break;
            }
        }


        public static void Registration()
        {
            Program p = new Program();
            Random random = new Random();
            string fullname = "";
            int id; //Későbbi kártyaid
            int pin = 0;
            string kartyaId = "";
            int cardL = 0;
            bool egyedi = false;

            Console.WriteLine("==========Regisztrálás==========");
            if (!File.Exists(@"ATM.txt"))
            {
                StreamWriter sw = new StreamWriter(@"ATM.txt");
            }
            else
            {
                while (cardL < 3)
                {
                    id = random.Next(1000, 9999);
                    kartyaId += String.Concat(id + "-");
                    cardL++;
                    if (cardL == 3)
                    {
                        id = random.Next(1000, 9999);
                        kartyaId += String.Concat(id);
                    }
                }
                pin = random.Next(100, 999);


                while (!egyedi)
                {
                    //A regisztrációhoz elég csak a nevet megadni, a többi generálódik
                    Console.WriteLine("Teljes név: ");
                    fullname = Console.ReadLine();

                    foreach (string line in File.ReadLines(@"ATM.txt"))
                    {
                        string[] words = line.Split(',');

                        if (fullname == words[0])
                        {
                            Console.WriteLine("Ez a személy már regisztrált.");
                            break;
                        }
                        else {
                            Cliens cliens = new Cliens(fullname, kartyaId, pin, 0);
                            
                            break;
                        }
                    }
                    Console.WriteLine("Kártyaszám: " + kartyaId + "\nPinkód: " + pin + "\nRegisztrálás befejezve.");
                    break;
                }
                using (StreamWriter sw = new StreamWriter(@"ATM.txt", append: true))
                {
                    sw.WriteLine(fullname + "," + pin + "," + kartyaId + "," + "0");
                }
            }
        }



        public static void Login()
        {
            Program p = new();
            Console.WriteLine("==========Belépés==========");
            p.LoginKiir(); //input bekérése
            bool belep = false;


            //fullname, pin, kartyaId, balance

            using (StreamReader sr = new StreamReader(@"ATM.txt"))
            {
                foreach (string line in File.ReadLines(@"ATM.txt"))
                {
                    string[] sor = line.Split(',');
                    if (sor[0] == Cliens.Name && sor[1] == Cliens.Pin.ToString() && sor[2] == Cliens.KartyaId) {
                        belep = true;
                        Console.WriteLine(belep);
                        break;
                    }
                }
            }
            if (belep)
            {
                p.MenuChose();
            }
            else
            {
                Console.WriteLine("Érvénytelen belépési adatok");
            }
        }
        

        public void LoginKiir()
        {
            Console.WriteLine("Teljes név: ");
            Cliens.Name = Console.ReadLine();

            Console.WriteLine("Kártyaszám: ");
            Cliens.KartyaId = Console.ReadLine();

            Console.WriteLine("Pinkód: ");
            Cliens.Pin = Int32.Parse(Console.ReadLine());
        }

        public void MenuChose()
        {
            String[] options = { "Withdraw", "Deposit", "Send", "Exit"};
            Program p = new();
            for (int i = 0; i < options.Length; i++)
            {
                Console.Write(i + 1 + ". " + options[i] + "\n");
            }
            int chosen = Int32.Parse(Console.ReadLine());

            while (chosen != 4)
            {
                if (chosen == 1)
                {
                    p.Withdraw();
                }
                else if (chosen == 2)
                {
                    p.Deposit();
                }
                else if (chosen == 3)
                {
                    p.Send();
                }
                else
                {
                    Console.WriteLine("Érvénytelen menüpont.");
                    chosen = Int32.Parse(Console.ReadLine());
                }
            }
        }


        public void Withdraw()
        {
            Console.WriteLine("mennyi pénzt szeretnél kivenni?: ");
            int amount = Int32.Parse(Console.ReadLine()); //amount = the user required to withdraw the amount of money

            if (Cliens.Balance < amount)
            {
                Console.WriteLine("Nincs elég fedezet a kártyán.");
            }
            if ((amount - Cliens.Balance == 0) || (amount < Cliens.Balance))
            {
                Cliens.Balance = Cliens.Balance - amount;
            }
        }

        public void Deposit()
        {
            Console.WriteLine("írd be mennyit szeretnél betenni: ");
            int amount = Int32.Parse(Console.ReadLine()); //amount = the user required to withdraw the amount of money

            if (Cliens.Balance < amount)
            {
                Console.WriteLine("Nincs elég fedezet.");
            }
            if ((amount - Cliens.Balance == 0) || (amount < Cliens.Balance))
            {
                Cliens.Balance = Cliens.Balance - amount;
            }
        }

        public void Send()
        {
            Console.WriteLine("Mennyi pénzt szeretnél küldeni? ");
            int send = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Cél ID: ");
            int target = Int32.Parse(Console.ReadLine());
            string[] bank = new string[10];
            List<string> ls = new List<string>();


            using (StreamReader sr = new StreamReader("ATM.txt"))
            {
                foreach (string line in File.ReadLines("ATM.txt"))
                {
                    string[] words = line.Split(',');

                    for (int i = 0; i < words.Length; i++)
                    {
                        ls.Add(words[i]);
                    }


                    Console.WriteLine();
                }
            }
        }
    }
}

