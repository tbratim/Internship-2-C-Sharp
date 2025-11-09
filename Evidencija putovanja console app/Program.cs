namespace Evidencija_putovanja_console_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA");
            
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
                                default:
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
