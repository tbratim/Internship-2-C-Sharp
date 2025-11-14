using System.Globalization;
using System.Numerics;

namespace Evidencija_putovanja_console_app
{
    internal class Program
    {
        static Dictionary<int, Tuple<string, string, DateTime, List<object>>> users = new Dictionary<int, Tuple<string, string, DateTime, List<object>>>();
        static Dictionary<int, Tuple<DateTime, double, double, double, double>> trips = new Dictionary<int, Tuple<DateTime, double, double, double, double>>();
        static void Main(string[] args)
        {
            Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA");

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
            static void InputUser()
            {
                Console.WriteLine("Unesite id korisnika: ");
                int idKorisnik = CheckIntInput(Console.ReadLine());
                while (users.ContainsKey(idKorisnik))
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
                
                var listOfTrips = InputTrip(idKorisnik); //dodat za unos više putovanja
                var userAttributes = new Tuple<string, string, DateTime, List<object>>(name, surname, dateOfBirth, listOfTrips);
                users[idKorisnik] = userAttributes;
            }

            static List<object> InputTrip(int id)
            {
                //odaberi korisnika
                while (users.ContainsKey(id))
                {
                    Console.WriteLine("Id već postoji!\nUnesite id putovanja: ");
                    id = CheckIntInput(Console.ReadLine());
                }
                Console.WriteLine("Unesite id putovanja: ");
                int idTrip = CheckIntInput(Console.ReadLine());
                while (trips.ContainsKey(idTrip))
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
                double spent = fuelUsed * priceOfFuel;

                var tripAttributes = new Tuple<DateTime, double, double, double, double>(dateOfTrip, kilometers, fuelUsed, priceOfFuel, spent);
                trips[idTrip] = tripAttributes;
                var listOfTrips = new List<object>();
                listOfTrips.Add(trips[idTrip]);
                Console.WriteLine(listOfTrips);
                Console.WriteLine("Putovanje uspješno dodano!");
                return listOfTrips;
                //dodat putovanje određenom korisniku
            }

            static void DeleteUserById(int id)
            {
                if (users.Count==0)
                {
                    Console.WriteLine("Nema korisnika za brisanje.");
                    return;
                }
                while (!users.ContainsKey(id))
                {
                    Console.WriteLine("Id ne postoji!\nUnesite id korisnika kojeg želite izbrisati: ");
                    id = CheckIntInput(Console.ReadLine());
                }
                var user = users[id];
                Console.WriteLine($"Jeste li sigurni da želite obrisati {user.Item1} {user.Item2} (da/ne)? ");
                var confirmation = Console.ReadLine().ToLower().Trim();
                while (confirmation!="da" && confirmation!="ne")
                {
                    Console.WriteLine("Neispravan unos!\nJeste li sigurni da želite obrisati korisnika (da/ne)? ");
                    confirmation=Console.ReadLine().ToLower().Trim();
                }
                if (confirmation=="da")
                {
                    users.Remove(id);
                    Console.WriteLine($"Korisnik {user.Item1} {user.Item2} uspješno izbrisan!");
                }
                else Console.WriteLine("Brisanje korisnika obustavljeno.");
            }

            static void DeleteUserByName(string name, string surname)
            {
                if (users.Count == 0)
                {
                    Console.WriteLine("Nema korisnika za brisanje.");
                    return;
                }
                int userId = -1;
                name = name.ToLower().Trim();
                surname = surname.ToLower().Trim();
                bool exists = false;
                foreach (var user in users)
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
                    var user = users[userId];
                    Console.WriteLine($"Jeste li sigurni da želite obrisati {user.Item1} {user.Item2} (da/ne)? ");
                    var confirmation = Console.ReadLine().ToLower().Trim();
                    while (confirmation != "da" && confirmation != "ne")
                    {
                        Console.WriteLine("Neispravan unos!\nJeste li sigurni da želite obrisati korisnika (da/ne)? ");
                        confirmation = Console.ReadLine().ToLower().Trim();
                    }
                    if (confirmation == "da")
                    {
                        users.Remove(userId);
                        Console.WriteLine($"Korisnik {user.Item1} {user.Item2} uspješno izbrisan!");
                    }
                    else Console.WriteLine("Brisanje korisnika obustavljeno.");
                }
                else
                {
                    Console.WriteLine($"Korisnik {name} {surname} ne postoji!");
                }
            }
            //EditUser to be fixed
            static void EditUser(int id)
            {
                if (users.Count == 0)
                {
                    Console.WriteLine("Nema korisnika za urediti.");
                    return;
                }
                while (!users.ContainsKey(id))
                {
                    Console.WriteLine("Id ne postoji!\nUnesite id korisnika kojeg želite urediti: ");
                    id = CheckIntInput(Console.ReadLine());
                }
                var user = users[id];
                var editedName = user.Item1;
                var editedSurname = user.Item2;
                var editedDateOfBirth = user.Item3;
                //list of trips dodat?
                Console.WriteLine($"Uređujete korisnika: {user.Item1} {user.Item2} ({user.Item3:yyyy-MM-dd})");
                Console.WriteLine("Upišite novo ime (ostavi prazno za bez promjene): ");
                if (Console.ReadLine().Trim() != "")
                {
                    editedName = Console.ReadLine().Trim();
                }
                Console.WriteLine("Upišite novo prezime (ostavi prazno za bez promjene): ");
                if (Console.ReadLine().Trim() != "")
                {
                    editedSurname = Console.ReadLine().Trim();
                }
                Console.WriteLine("Upišite novi datum rođenja (ostavi prazno za bez promjene): ");
                if (Console.ReadLine().Trim() != "")
                {
                    editedDateOfBirth=CheckDateInput(Console.ReadLine());
                }
                Console.WriteLine($"Jeste li sigurni da želite urediti korisnika {user.Item1} {user.Item2} (da/ne)? ");
                var confirmation = Console.ReadLine().ToLower().Trim();
                while (confirmation != "da" && confirmation != "ne")
                {
                    Console.WriteLine("Neispravan unos!\nJeste li sigurni da želite urediti korisnika (da/ne)? ");
                    confirmation = Console.ReadLine().ToLower().Trim();
                }
                if (confirmation == "da")
                {
                    var editedUser = new Tuple<string, string, DateTime, List<object>>(editedName, editedSurname, editedDateOfBirth, user.Item4);
                    users[id] = editedUser;
                    Console.WriteLine($"Korisnik uspješno uređen: {editedUser.Item1} {editedUser.Item2} ({editedUser.Item3:yyyy-MM-dd})");
                }
                else Console.WriteLine("Uređivanje korisnika obustavljeno.");
            }

            static void PrintUsers()
            {
                var printChoice = "";
                var sortedBySurname = users.OrderBy(x => x.Value.Item2).ToDictionary(x => x.Key, x => x.Value);
                do
                {
                    Console.WriteLine("1 - Ispis korisnika abecedno po prezimenu\n2 - Ispis korisnika starijih od 20g\n3 - Ispis korisnika s barem 2 putovanja\n0 - Povratak na prethodni izbornik");
                    printChoice = Console.ReadLine();
                    switch (printChoice)
                    {
                        case "1":
                            Console.WriteLine("Korisnici abecedno po prezimenu:");
                            foreach (var user in sortedBySurname)
                            {
                                Console.WriteLine($"{user.Key} - {user.Value.Item1} - {user.Value.Item2} - {user.Value.Item3:yyyy-MM-dd}");
                            }
                            break;
                        case "2":
                            Console.WriteLine("Korisnici stariji od 20:");
                            foreach (var user in sortedBySurname)
                            {
                                if ((DateTime.Now - user.Value.Item3).TotalDays >= 365.25*20)
                                    Console.WriteLine($"{user.Key} - {user.Value.Item1} - {user.Value.Item2} - {user.Value.Item3:yyyy-MM-dd}");
                            }
                            break;
                        case "3":
                            Console.WriteLine("Korisnici s barem 2 putovanja:");
                            foreach (var user in sortedBySurname)
                            {
                                if (user.Value.Item4.Count>=2)
                                    Console.WriteLine($"{user.Key} - {user.Value.Item1} - {user.Value.Item2} - {user.Value.Item3:yyyy-MM-dd}");
                            }
                            break;
                        default:
                            if (printChoice != "0")
                                Console.WriteLine("Neispravan unos!");
                            break;
                    }
                }
                while (printChoice != "0");
            }

            var menuChoice = "";
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
                            switch (usersMenuChoice)
                            {
                                case "1":
                                    Console.WriteLine("Unos novog korisnika");
                                    InputUser();
                                    break;
                                case "2":
                                    var deleteChoice = "";
                                    do
                                    {
                                        Console.WriteLine("Brisanje korisnika:\n1 - Brisanje po id-u\n2 - Brisanje po imenu i prezimenu\n0 - Povratak na prethodni izbornik");
                                        deleteChoice = Console.ReadLine();
                                        switch (deleteChoice)
                                        {
                                            case "1":
                                                Console.WriteLine("Unesite id korisnika kojeg želite izbrisati: ");
                                                int userIdDelete = CheckIntInput(Console.ReadLine());
                                                DeleteUserById(userIdDelete);
                                                break;
                                            case "2":
                                                Console.WriteLine("Unesite ime korisnika kojeg želite izbrisati: ");
                                                string nameChoiceDelete = CheckStringInput(Console.ReadLine());
                                                Console.WriteLine("Unesite prezime korisnika kojeg želite izbrisati: ");
                                                string surnameChoiceDelete = CheckStringInput(Console.ReadLine());
                                                DeleteUserByName(nameChoiceDelete, surnameChoiceDelete);
                                                break;
                                            default:
                                                if (deleteChoice != "0")
                                                    Console.WriteLine("Neispravan unos!");
                                                break;
                                        }
                                    }
                                    while (deleteChoice != "0");
                                    break;
                                case "3":
                                    Console.WriteLine("Uređivanje korisnika");
                                    Console.WriteLine("Unesi id korisnika koje želiš urediti: ");
                                    int userIdEdit = CheckIntInput(Console.ReadLine());
                                    EditUser(userIdEdit);
                                    break;
                                case "4":
                                    Console.WriteLine("Pregled svih korisnika");
                                    PrintUsers();
                                    break;
                                default:
                                    if (usersMenuChoice != "0")
                                        Console.WriteLine("Neispravan unos!");
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
                                    if (travelsMenuChoice != "0")
                                        Console.WriteLine("Neispravan unos!");
                                    break;
                            }
                        }
                        while (travelsMenuChoice != "0");
                        Console.WriteLine("Povratak na glavni izbornik");
                        break;
                    default:
                        if (menuChoice != "0")
                            Console.WriteLine("Neispravan unos!");
                        break;

                }
            }
            while (menuChoice != "0");
            Console.WriteLine("Izlaz iz aplikacije");
        }
    }
}
