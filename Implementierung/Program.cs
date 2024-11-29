using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    // Datenstruktur
    public class Nutzer
    {
        public string Name { get; set; }
        public int Punkte { get; set; }
        public int Besuche { get; set; }  // Anzahl der Besuche
    }

    static string dataFile = "data.json";
    static Dictionary<string, Nutzer> nutzerDaten = new Dictionary<string, Nutzer>();

    // Daten speichern
    static void SaveData()
    {
        var jsonData = JsonConvert.SerializeObject(nutzerDaten, Formatting.Indented);
        File.WriteAllText(dataFile, jsonData);
    }

    // Daten laden
    static void LoadData()
    {
        if (File.Exists(dataFile))
        {
            var jsonData = File.ReadAllText(dataFile);
            nutzerDaten = JsonConvert.DeserializeObject<Dictionary<string, Nutzer>>(jsonData) ?? new Dictionary<string, Nutzer>();
        }
    }

    // Nutzer registrieren oder einloggen
    static void RegisterOrLoginUser()
    {
        Console.Write("E-Mail: ");
        string email = Console.ReadLine();

        if (nutzerDaten.ContainsKey(email))
        {
            // Nutzer einloggen und einen Punkt für den Besuch hinzufügen
            nutzerDaten[email].Punkte += 1;
            nutzerDaten[email].Besuche += 1;
            Console.WriteLine("Erfolgreich eingeloggt! Du hast 1 Punkt für deinen Besuch erhalten.");
        }
        else
        {
            // Nutzer registrieren und 1 Punkt für den ersten Besuch vergeben
            Console.Write("Name: ");
            string name = Console.ReadLine();

            nutzerDaten[email] = new Nutzer { Name = name, Punkte = 1, Besuche = 1 };  // 1 Punkt für Registrierung
            Console.WriteLine("Registrierung erfolgreich! Du hast 1 Punkt für deinen ersten Besuch erhalten.");
        }

        SaveData();
    }

    // Punkte anzeigen
    static void ShowPoints()
    {
        Console.Write("E-Mail: ");
        string email = Console.ReadLine();

        if (!nutzerDaten.ContainsKey(email))
        {
            Console.WriteLine("Nutzer nicht gefunden!");
            return;
        }

        Console.WriteLine($"Punkte: {nutzerDaten[email].Punkte}, Besuche: {nutzerDaten[email].Besuche}");
    }

    // Punkte einlösen
    static void RedeemPoints()
    {
        Console.Write("E-Mail: ");
        string email = Console.ReadLine();

        if (!nutzerDaten.ContainsKey(email))
        {
            Console.WriteLine("Nutzer nicht gefunden!");
            return;
        }

        // Abfrage, ob Rabatt oder Freikarte gewünscht wird
        Console.WriteLine("Möchtest du einen Rabatt oder eine Freikarte einlösen?");
        Console.WriteLine("1. Rabatt (5 Punkte)");
        Console.WriteLine("2. Freikarte (10 Punkte)");
        Console.Write("Deine Wahl: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            // Rabatt einlösen
            if (nutzerDaten[email].Punkte >= 5)
            {
                nutzerDaten[email].Punkte -= 5;
                SaveData();
                Console.WriteLine("Rabatt erfolgreich eingelöst!");
            }
            else
            {
                Console.WriteLine("Nicht genügend Punkte für den Rabatt!");
            }
        }
        else if (choice == "2")
        {
            // Freikarte einlösen
            if (nutzerDaten[email].Punkte >= 10)
            {
                nutzerDaten[email].Punkte -= 10;
                SaveData();
                Console.WriteLine("Freikarte erfolgreich eingelöst!");
            }
            else
            {
                Console.WriteLine("Nicht genügend Punkte für eine Freikarte!");
            }
        }
        else
        {
            Console.WriteLine("Ungültige Auswahl!");
        }
    }

    // Admin-Funktion: Punkte von anderen Nutzern anpassen
    static void AdjustPoints()
    {
        Console.Write("Admin E-Mail: ");
        string email = Console.ReadLine();

        // Nur Admin "melih@gmail.com" kann diese Funktion nutzen
        if (email != "melih@gmail.com")
        {
            Console.WriteLine("Unberechtigter Zugriff.");
            return;
        }

        Console.Write("Admin Passwort: ");
        string password = Console.ReadLine();

        // Überprüfen, ob das Passwort korrekt ist
        if (password == "melih@gmail.com")
        {
            // Admin kann die Punkte von anderen Nutzern ändern
            Console.Write("E-Mail des Nutzers, dessen Punkte angepasst werden sollen: ");
            string targetEmail = Console.ReadLine();

            if (!nutzerDaten.ContainsKey(targetEmail))
            {
                Console.WriteLine("Nutzer nicht gefunden!");
                return;
            }

            Console.Write("Neuer Punktestand: ");
            if (int.TryParse(Console.ReadLine(), out int points))
            {
                nutzerDaten[targetEmail].Punkte = points;
                SaveData();
                Console.WriteLine("Punktestand des Nutzers erfolgreich angepasst!");
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe!");
            }
        }
        else
        {
            Console.WriteLine("Falsches Passwort.");
        }
    }

    // Hauptmenü
    static void Main(string[] args)
    {
        LoadData();
        while (true)
        {
            Console.WriteLine("Prämien und Treueprogramm");
            Console.WriteLine("\n1. Registrieren/Einloggen\n2. Punkte anzeigen\n3. Punkte einlösen\n4. Punkte anpassen (Admin)\n5. Beenden");
            Console.Write("Auswahl: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterOrLoginUser();
                    break;
                case "2":
                    ShowPoints();
                    break;
                case "3":
                    RedeemPoints();
                    break;
                case "4":
                    AdjustPoints();
                    break;
                case "5":
                    Console.WriteLine("Programm beendet.");
                    return;
                default:
                    Console.WriteLine("Ungültige Auswahl!");
                    break;
            }
        }
    }
}
