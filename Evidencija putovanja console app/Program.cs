using System.Globalization;
using System.Numerics;
using System.Xml;

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
                Console.WriteLine("Unesite potrošeno gorivo (L): ");
                double fuelUsed = CheckDoubleInput(Console.ReadLine());
                Console.WriteLine("Unesite cijenu po litri (EUR): ");
                double priceOfFuel = CheckDoubleInput(Console.ReadLine());
                double totalSpent = fuelUsed * priceOfFuel;

                var tripAttributes = new Tuple<DateTime, double, double, double, double>(dateOfTrip, kilometers, fuelUsed, priceOfFuel, totalSpent);
                trips[idTrip] = tripAttributes;
                var listOfTrips = new List<object>();
                listOfTrips.Add(trips[idTrip]);
                Console.WriteLine(listOfTrips);
                Console.WriteLine("Putovanje uspješno dodano!");
                return listOfTrips;
                //dodat putovanje određenom korisniku
                //ispise system ...
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
            //EditUser fixed
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
                string nameInput = Console.ReadLine().Trim();
                if (nameInput != "")
                {
                    editedName = nameInput;
                }
                Console.WriteLine("Upišite novo prezime (ostavi prazno za bez promjene): ");
                string surnameInput = Console.ReadLine().Trim();
                if (surnameInput != "")
                {
                    editedSurname = surnameInput;
                }
                Console.WriteLine("Upišite novi datum rođenja (ostavi prazno za bez promjene): ");
                string dateInput = Console.ReadLine().Trim();
                if (dateInput != "")
                {
                    editedDateOfBirth=CheckDateInput(dateInput);
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
                if (users.Count == 0)
                {
                    Console.WriteLine("Ne postoje korisnici za ispisati.");
                    return;
                }
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
                                Console.WriteLine($"{user.Key} - {user.Value.Item1} - {user.Value.Item2} - {user.Value.Item3:yyyy-MM-dd}");
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

            static void DeleteTripById(int id)
            {
                if (trips.Count==0)
                {
                    Console.WriteLine("Ne postoje putovanja za izbrisati.");
                    return;
                }
                while (!trips.ContainsKey(id))
                {
                    Console.WriteLine("Id putovanja ne postoji!");
                    id = CheckIntInput(Console.ReadLine());
                }
                Console.WriteLine($"Jeste li sigurni da želite obrisati {trips[id]} (da/ne)? ");
                var confirmation = Console.ReadLine().ToLower().Trim();
                while (confirmation != "da" && confirmation != "ne")
                {
                    Console.WriteLine("Neispravan unos!\nJeste li sigurni da želite obrisati putovanje (da/ne)? ");
                    confirmation = Console.ReadLine().ToLower().Trim();
                }
                if (confirmation == "da")
                {
                    foreach (var trip in trips.Keys)
                    {
                        if (trip == id)
                            trips.Remove(trip);
                    }
                    Console.WriteLine("Putovanje uspješno izbrisano!");
                }
                else Console.WriteLine("Brisanje putovanja obustavljeno.");
            }

            static void DeleteTripExspensive(double maxPrice)
            {
                if (trips.Count == 0)
                {
                    Console.WriteLine("Ne postoje putovanja za izbrisati.");
                    return;
                }
                int tripCounter = 0;
                foreach (var trip in trips.Values)
                {
                    if (trip.Item5 > maxPrice)
                        tripCounter++;
                }
                Console.WriteLine($"Jeste li sigurni da želite obrisati {tripCounter} putovanja skupljih od {maxPrice} eura (da/ne)? ");
                var confirmation = Console.ReadLine().ToLower().Trim();
                while (confirmation != "da" && confirmation != "ne")
                {
                    Console.WriteLine("Neispravan unos!\nJeste li sigurni da želite obrisati putovanja (da/ne)? ");
                    confirmation = Console.ReadLine().ToLower().Trim();
                }
                int tempTripId = -1;
                if (confirmation == "da")
                {
                    foreach (var trip in trips)
                    {
                        if (trip.Value.Item5 > maxPrice)
                            tempTripId = trip.Key;
                            trips.Remove(tempTripId);
                    }
                    Console.WriteLine("Putovanja uspješno izbrisana!");
                }
                else Console.WriteLine("Brisanje putovanja obustavljeno.");
            }

            static void DeleteTripCheap(double minPrice)
            {
                if (trips.Count == 0)
                {
                    Console.WriteLine("Ne postoje putovanja za izbrisati.");
                    return;
                }
                int tripCounter = 0;
                foreach (var trip in trips.Values)
                {
                    if (trip.Item5 > minPrice)
                        tripCounter++;
                }
                Console.WriteLine($"Jeste li sigurni da želite obrisati {tripCounter} putovanja jeftinijih od {minPrice} eura (da/ne)? ");
                var confirmation = Console.ReadLine().ToLower().Trim();
                while (confirmation != "da" && confirmation != "ne")
                {
                    Console.WriteLine("Neispravan unos!\nJeste li sigurni da želite obrisati putovanja (da/ne)? ");
                    confirmation = Console.ReadLine().ToLower().Trim();
                }
                int tempTripId = -1;
                if (confirmation == "da")
                {
                    foreach (var trip in trips)
                    {
                        if (trip.Value.Item5 < minPrice)
                            tempTripId = trip.Key;
                        trips.Remove(tempTripId);
                    }
                    Console.WriteLine("Putovanja uspješno izbrisana!");
                }
                else Console.WriteLine("Brisanje putovanja obustavljeno.");
            }

            static void EditTrip(int id)
            {
                if (trips.Count == 0)
                {
                    Console.WriteLine("Ne postoje putovanja za izbrisati.");
                    return;
                }
                while (!trips.ContainsKey(id))
                {
                    Console.WriteLine("Id putovanja ne postoji!");
                    id = CheckIntInput(Console.ReadLine());
                }
                var trip = trips[id];
                var editedTripDate = trip.Item1;
                var editedKilometers = trip.Item2;
                var editedFuelUsed = trip.Item3;
                var editedPriceOfFuel = trip.Item4;
                Console.WriteLine($"Uređujete putovanje: ({trip.Item1:yyyy-MM-dd}) {trip.Item2} {trip.Item3} {trip.Item4} {trip.Item5}");
                Console.WriteLine("Upišite novi datum (ostavi prazno za bez promjene): ");
                string tripDateInput = Console.ReadLine().Trim();
                if (tripDateInput != "")
                {
                    editedTripDate = CheckDateInput(tripDateInput);
                }
                Console.WriteLine("Upišite prijeđenu kilometražu (ostavi prazno za bez promjene): ");
                string kilometersInput = Console.ReadLine().Trim();
                if (kilometersInput != "")
                {
                    editedKilometers = CheckDoubleInput(kilometersInput);
                }
                Console.WriteLine("Upišite potrošeno gorivo (ostavi prazno za bez promjene): ");
                string fuelUsedInput = Console.ReadLine().Trim();
                if (fuelUsedInput != "")
                {
                    editedFuelUsed = CheckDoubleInput(fuelUsedInput);
                }
                Console.WriteLine("Upišite potrošeno gorivo (ostavi prazno za bez promjene): ");
                string priceOfFuelInput = Console.ReadLine().Trim();
                if (priceOfFuelInput != "")
                {
                    editedPriceOfFuel = CheckDoubleInput(priceOfFuelInput);
                }
                var editedTotalSpent = editedPriceOfFuel * editedFuelUsed;
                Console.WriteLine($"Jeste li sigurni da želite urediti putovanje {trip.Item1:yyyy-MM-dd}) {trip.Item2} Km {trip.Item3} L {trip.Item4} EUR (da/ne)? ");
                var confirmation = Console.ReadLine().ToLower().Trim();
                while (confirmation != "da" && confirmation != "ne")
                {
                    Console.WriteLine("Neispravan unos!\nJeste li sigurni da želite urediti putovanje (da/ne)? ");
                    confirmation = Console.ReadLine().ToLower().Trim();
                }
                if (confirmation == "da")
                {
                    var editedTrip = new Tuple<DateTime, double, double, double, double>(editedTripDate, editedKilometers, editedFuelUsed, editedPriceOfFuel, editedTotalSpent);
                    trips[id] = editedTrip;
                    Console.WriteLine($"Putovanje uspješno uređeno: ({editedTrip.Item1:yyyy-MM-dd}) {editedTrip.Item2} {editedTrip.Item3} {editedTrip.Item4} {editedTrip.Item5}");
                }
                else Console.WriteLine("Uređivanje putovanja obustavljeno.");
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
                                    var deleteUserChoice = "";
                                    do
                                    {
                                        Console.WriteLine("Brisanje korisnika:\n1 - Brisanje po id-u\n2 - Brisanje po imenu i prezimenu\n0 - Povratak na prethodni izbornik");
                                        deleteUserChoice = Console.ReadLine();
                                        switch (deleteUserChoice)
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
                                                if (deleteUserChoice != "0")
                                                    Console.WriteLine("Neispravan unos!");
                                                break;
                                        }
                                    }
                                    while (deleteUserChoice != "0");
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
                            Console.WriteLine("Putovanja:\n1 - Unos novog putovanja\n2 - Brisanje putovanja\n3 - Uređivanje postojećeg putovanja");
                            Console.WriteLine("4 - Pregled svih putovanja\n5 - Izvještaji i analize\n0 - Povratak na glavni izbornik");
                            travelsMenuChoice = Console.ReadLine();
                            Console.WriteLine("Odabir: {0}", travelsMenuChoice);
                            switch (travelsMenuChoice)
                            {
                                case "1":
                                    Console.WriteLine("Unos novog putovanja");
                                    //var listOfTrips = InputTrip(id);
                                    break;
                                case "2":
                                    Console.WriteLine("Brisanje putovanja");
                                    var deleteTripChoice = "";
                                    do
                                    {
                                        Console.WriteLine("1 - Brisanje putovanja po id-u\n2 - Brisanje putovanja skupljih od xx\n3 - Brisanje putovanja jeftinijih od xx");
                                        deleteTripChoice = Console.ReadLine();
                                        switch (deleteTripChoice)
                                        {
                                            case "1":
                                                Console.WriteLine("Upišite id putovanja kojeg želite izbrisati: ");
                                                int idDeleteTrip = CheckIntInput(Console.ReadLine());
                                                DeleteTripById(idDeleteTrip);
                                                break;
                                            case "2":
                                                Console.WriteLine("Unesi maksimalni iznos: ");
                                                double maxPrice = CheckDoubleInput(Console.ReadLine());
                                                DeleteTripExspensive(maxPrice);
                                                break;
                                            case "3":
                                                Console.WriteLine("Unesi minimalni iznos: ");
                                                double minPrice = CheckDoubleInput(Console.ReadLine());
                                                DeleteTripCheap(minPrice);
                                                break;
                                            default:
                                                if (deleteTripChoice != "0")
                                                    Console.WriteLine("Neispravan unos!");
                                                break;
                                        }
                                    }
                                    while (deleteTripChoice != "0");
                                    break;
                                case "3":
                                    Console.WriteLine("Uređivanje postojećeg putovanja");
                                    Console.WriteLine("Unesite id putovanja kojeg želite urediti: ");
                                    int editTripId = CheckIntInput(Console.ReadLine());
                                    EditTrip(editTripId);
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
