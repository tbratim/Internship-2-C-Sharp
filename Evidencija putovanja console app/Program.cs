using System.Globalization;
using System.Numerics;

namespace Evidencija_putovanja_console_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA");
            var users = new Dictionary<int, Tuple<string, string, DateTime, List<Dictionary<int, Tuple<DateTime, double, double, double, double>>>>>();
            var trips = new Dictionary<int, Tuple<DateTime, double, double, double, double>>();
            var listOfTrips = new List<Dictionary<int, Tuple<DateTime, double, double, double, double>>>();

            static DateTime CheckDateInput(string input)
            {
                while (true)
                {
                    if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                    {
                        if (date.Date > DateTime.Today)
                            Console.WriteLine("Neispravan unos, datum ne smije biti u budućnosti!\nUnesite datum: ");
                        else return date;
                    }
                    else Console.WriteLine("Neispravan format!\nUnesite datum: ");

                    input = Console.ReadLine();
                }
            }

            static int CheckIntInput(string input)
            {
                int number;
                while (true)
                {
                    if (int.TryParse(input, out number))
                    {
                        if (number > 0)
                            return number;
                        else Console.WriteLine("Neispravan unos!\nUnesite pozitivan broj: ");
                    }
                    else Console.WriteLine("Neispravan unos!\nUnesite cijeli broj: ");

                    input = Console.ReadLine();
                }
            }

            static double CheckDoubleInput(string input)
            {
                double number;
                while (true)
                {
                    if (double.TryParse(input, out number))
                    {
                        if (number > 0)
                            return number;
                        else Console.WriteLine("Neispravan unos!\nUnesite pozitivan broj: ");
                    }
                    else Console.WriteLine("Neispravan unos!\nUnesite broj: ");

                    input = Console.ReadLine();
                }

            }

            static string CheckStringInput(string input)
            {
                while (true)
                {
                    if (input=="")
                        Console.WriteLine("Neispravan unos!\nPokušajte ponovno: ");
                    else return input;

                    input = Console.ReadLine();
                }
            }
            //InputUser and InputTrip incomplete!
            static void InputUser(Dictionary<int, Tuple<string, string, DateTime, List<Dictionary<int, Tuple<DateTime, double, double, double, double>>>>> userDictionary)
            {
                Console.WriteLine("Unesite id korisnika: ");
                int idKorisnik = CheckIntInput(Console.ReadLine());
                while (userDictionary.ContainsKey(idKorisnik))
                {
                    Console.WriteLine("Id već postoji!\nUnesite id korisnika: ");
                    idKorisnik = CheckIntInput(Console.ReadLine());
                }
                Console.WriteLine("Unesite ime: ");
                string name = CheckStringInput(Console.ReadLine());
                Console.WriteLine("Unesite prezime: ");
                string surname = CheckStringInput(Console.ReadLine());
                Console.WriteLine("Unesite datum rođenja (yyyy-MM-dd): ");
                DateTime dateOfBirth = CheckDateInput(Console.ReadLine());
                //missing list of travels
                var userAttributes = (Name: name, Surname: surname, DateOfBirth: dateOfBirth);
                //Console.WriteLine(userAttributes.ToString());
                //InputTrip();
            }

            static void InputTrip(Dictionary<int, Tuple<DateTime, double, double, double, double>> tripDictionary)
            {
                //odaberi korisnika
                Console.WriteLine("Unesite id putovanja: ");
                int idTrip = CheckIntInput(Console.ReadLine());
                while (tripDictionary.ContainsKey(idTrip))
                {
                    Console.WriteLine("Id već postoji!\nUnesite id putovanja: ");
                    idTrip = CheckIntInput(Console.ReadLine());
                }
                Console.WriteLine("Unesite datum putovanja (yyyy-MM-dd): ");
                DateTime dateOfTrip = CheckDateInput(Console.ReadLine());
                Console.WriteLine("Unesite kilometražu: ");
                double kilometers = CheckDoubleInput(Console.ReadLine());
                Console.WriteLine("Unesite potrošeno gorivo: ");
                double fuelUsed = CheckDoubleInput(Console.ReadLine());
                Console.WriteLine("Unesite cijenu po litri: ");
                double priceOfFuel = CheckDoubleInput(Console.ReadLine());

                var tripAttributes = (Date: dateOfTrip, Km: kilometers, Fuel: fuelUsed, Price: priceOfFuel);

                Console.WriteLine("Putovanje uspješno dodano!");
            }

            static void DeleteUserById(int id, Dictionary<int, Tuple<string, string, DateTime, List<Dictionary<int, Tuple<DateTime, double, double, double, double>>>>> userDictionary)
            {
                if (userDictionary.Count==0)
                {
                    Console.WriteLine("Nema korisnika za brisanje.");
                    return;
                }
                while (!userDictionary.ContainsKey(id))
                {
                    Console.WriteLine("Id ne postoji!\nUnesite id korisnika kojeg želite izbrisati: ");
                    id = CheckIntInput(Console.ReadLine());
                }
                var user = userDictionary[id];
                Console.WriteLine($"Jeste li sigurni da želite obrisati {user.Item1} {user.Item2} (da/ne)? ");
                var confirmation = Console.ReadLine().ToLower().Trim();
                while (confirmation!="da" && confirmation!="ne")
                {
                    Console.WriteLine("Neispravan unos!\nJeste li sigurni da želite obrisati korisnika (da/ne)? ");
                    confirmation=Console.ReadLine().ToLower().Trim();
                }
                if (confirmation=="da")
                {
                    userDictionary.Remove(id);
                    Console.WriteLine($"Korisnik {user.Item1} {user.Item2} uspješno izbrisan!");
                }
                else Console.WriteLine("Brisanje korisnika obustavljeno.");
            }

            static void DeleteUserByName(string name, string surname, Dictionary<int, Tuple<string, string, DateTime, List<Dictionary<int, Tuple<DateTime, double, double, double, double>>>>> userDictionary)
            {
                if (userDictionary.Count == 0)
                {
                    Console.WriteLine("Nema korisnika za brisanje.");
                    return;
                }
                int userId = -1;
                name = name.ToLower().Trim();
                surname = surname.ToLower().Trim();
                bool exists = false;
                foreach (var user in userDictionary)
                {
                    if (user.Value.Item1.ToLower().Trim()==name && user.Value.Item2.ToLower().Trim() == surname)
                    {
                        userId = user.Key;
                        exists = true;
                        break;
                    }
                }
                if (exists)
                {
                    var user = userDictionary[userId];
                    Console.WriteLine($"Jeste li sigurni da želite obrisati {user.Item1} {user.Item2} (da/ne)? ");
                    var confirmation = Console.ReadLine().ToLower().Trim();
                    while (confirmation != "da" && confirmation != "ne")
                    {
                        Console.WriteLine("Neispravan unos!\nJeste li sigurni da želite obrisati korisnika (da/ne)? ");
                        confirmation = Console.ReadLine().ToLower().Trim();
                    }
                    if (confirmation == "da")
                    {
                        userDictionary.Remove(userId);
                        Console.WriteLine($"Korisnik {user.Item1} {user.Item2} uspješno izbrisan!");
                    }
                    else Console.WriteLine("Brisanje korisnika obustavljeno.");
                }
                else
                {
                    Console.WriteLine($"Korisnik {name} {surname} ne postoji!");
                }
            }

            var menuChoice = "";
            //string? menuChoice = null;
            do
            {
                Console.WriteLine("Izbornik:\n1 - Korisnici\n2 - Putovanja\n0 - Izlaz iz aplikacije");
                menuChoice = Console.ReadLine();
                Console.WriteLine("Odabir: {0}", menuChoice);
                switch (menuChoice)
                {
                    case "1":
                        var usersMenuChoice = "";
                        do
                        {
                            Console.WriteLine("Korisnici:\n1 - Unos novog korisnika\n2 - Brisanje korisnika\n3 - Uređivanje korisnika\n4 - Pregled svih korisnika\n0 - Povratak na glavni izbornik\n");
                            usersMenuChoice = Console.ReadLine();
                            Console.WriteLine("Odabir: {0}", usersMenuChoice);
                            switch (usersMenuChoice) //might change
                            {
                                case "1":
                                    Console.WriteLine("Unos novog korisnika");
                                    InputUser(users);
                                    break;
                                case "2":
                                    Console.WriteLine("Brisanje korisnika");
                                    break;
                                case "3":
                                    Console.WriteLine("Uređivanje korisnika");
                                    break;
                                case "4":
                                    Console.WriteLine("Pregled svih korisnika");
                                    break;
                                default: //odradi i kad upise 0
                                    Console.WriteLine("Pogrešan unos!");
                                    break;
                            }
                        }
                        while (usersMenuChoice != "0");
                        Console.WriteLine("Povratak na glavni izbornik");
                        break;
                    case "2":
                        var travelsMenuChoice = "";
                        do
                        {
                            Console.WriteLine("Putovanja:\n1 - Unos novog putovanja\n2 - Brisanje putovanja\n3 - Uređivanje postojećeg putovanja\n4 - Pregled svih putovanja\n5 - Izvještaji i analize\n0 - Povratak na glavni izbornik\n");
                            travelsMenuChoice = Console.ReadLine();
                            Console.WriteLine("Odabir: {0}", travelsMenuChoice);
                            switch (travelsMenuChoice)
                            {
                                case "1":
                                    Console.WriteLine("Unos novog putovanja");
                                    break;
                                case "2":
                                    Console.WriteLine("Brisanje putovanja");
                                    break;
                                case "3":
                                    Console.WriteLine("Uređivanje postojećeg putovanja");
                                    break;
                                case "4":
                                    Console.WriteLine("Pregled svih putovanja");
                                    break;
                                case "5":
                                    Console.WriteLine("Izvještaji i analize");
                                    break;
                                default:
                                    Console.WriteLine("Pogrešan unos!");
                                    break;
                            }
                        }
                        while (travelsMenuChoice != "0");
                        Console.WriteLine("Povratak na glavni izbornik");
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos!");
                        break;

                }
            }
            while (menuChoice != "0");
            Console.WriteLine("Izlaz iz aplikacije");
        }
    }
}
