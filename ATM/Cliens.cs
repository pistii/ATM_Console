
namespace ATM
{
    public class Cliens
    {

        private static string name;
        public static string kartyaId;
        public static int pin;
        private static int balance;
        public static int amount;


        public static int Balance
        {
            get { return balance; }
            set { balance += amount; }
        }


        public static string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static string KartyaId
        {
            get { return kartyaId; }
            set { kartyaId = value; }
        }

        public static int Pin
        {
            get { return pin; }
            set { pin = value; }
        }

        public Cliens(string _name, string _id, int pin, int _balance)
        {
            Name = _name;
            KartyaId = _id;
            Pin = pin;
            Balance = _balance;
        }
    }
}
