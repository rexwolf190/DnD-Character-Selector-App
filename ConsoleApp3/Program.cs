using Spectre.Console;
using System.Reflection.Metadata;
using System.Text.Json;
using System;
using System.Reflection.Metadata.Ecma335;

public class Program
{
    private static string name;
    private static Race race;
    private static string gameClass;
    private static readonly Race[] validRaces = [
        new Race("Human", 1, 1, 1, 1, 1, 1),
        new Race("Orc", 2, 0, 1, 0, 0, 0),
        new Race("Dwarf", 0, 0, 2, 0, 0,0)
    ];

    private static readonly string[] validClasses = ["Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard"];



    private static void Main()
    {
        MainMenu();
    }

    public static void GetCharacterIntoFile()
    {
        name = GetName();
        race = GetRace();
        gameClass = GetClass();
        Character Char1 = new Character(race, gameClass, name);
        ChangeAttributes(Char1);
        CreateCharacterFile(Char1);
        Console.WriteLine($"You are {name}. Your role is {gameClass}. You belong to the {race.Name} race. Good luck on your adventure, fellow traveller.");
    }

    public static void MainMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Main Menu")
            .AddChoices(
                "Create a new character"
            ));

        if ( choice.ToLower() == "create a new character")
        {
            GetCharacterIntoFile();
        }
    }
    public static Race GetRace()
    {
        while (true)
        {
            var race = AnsiConsole.Prompt(
                new SelectionPrompt<Race>()
                .Title("What [green]race[/] would you like?")
                .UseConverter(race => $"{race.Name}")
                .PageSize(4)
                .AddChoices(validRaces));
            var question = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Are you [green]sure[/]?")
                .PageSize(4)
                .AddChoices("Yes", "No")
                );

            if (question.ToLower() == "yes") { Console.WriteLine($"You have selected {race.Name}."); return race; }
            else { continue; }
            
           
        }
    }

    public static string GetClass()
    {
        while (true)
        {
            var gameClass = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What [green]class[/] would you like?")
                .PageSize(4)
                .AddChoices(validClasses));

            var question = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("Are you [green]sure[/]?")
               .PageSize(4)
               .AddChoices("Yes", "No")
               );

            if (question.ToLower() == "yes") { Console.WriteLine($"You have selected {gameClass}."); return gameClass; }
            else { continue; }
        }
    }


public static string GetName()
{
    while (true)
    {
        var name = AnsiConsole.Ask<string>("What is your [green]name?[/]");

        if (name.Length < 40 && name.Length > 0)
        {
                AnsiConsole.MarkupLine($"You are [green]{name}[/].");
            return name;
        }

        Console.WriteLine("Invalid Input.");
        continue;
    }
}

public static void CreateCharacterFile(Character newCharacter)
{
        string fileName = $"{newCharacter.Name}.json";
        string jsonString = JsonSerializer.Serialize(newCharacter);
        File.WriteAllText(fileName, jsonString);
}

    public static void ChangeAttributes(Character newCharacter)
    {
        GetTable(newCharacter);
        while (newCharacter.TotalSkillPoints > 0)
        {
           
            

            var keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.F1:
                    {
                        Console.Clear();
                        if (newCharacter.PointBuyStrength < 7)
                        {
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyStrength);
                            newCharacter.PointBuyStrength++;
                        }
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Strength increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        GetTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F2:
                    {
                        Console.Clear();
                        
                        if (newCharacter.PointBuyDexterity < 7)
                        {
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyDexterity);
                            newCharacter.PointBuyDexterity++;
                           
                        }
                        
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Dexterity increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        GetTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F3:
                    {
                        Console.Clear();
                        if (newCharacter.PointBuyConstitution < 7)
                        {
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyConstitution);
                            newCharacter.PointBuyConstitution++;
                        }
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Constitution increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        GetTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F4:
                    {
                        Console.Clear();
                        if (newCharacter.PointBuyIntelligence < 7)
                        {
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyIntelligence);
                            newCharacter.PointBuyIntelligence++;
                        }
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Intelligence increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        GetTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F5:
                    {
                        Console.Clear();
                        if (newCharacter.PointBuyWisdom < 7)
                        {                          
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyWisdom);
                            newCharacter.PointBuyWisdom++;
                        }
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Wisdom increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        GetTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F6:
                    {
                        Console.Clear();
                        if (newCharacter.PointBuyCharisma < 7) { 
                        newCharacter.PointBuyCharisma++; 
                        newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyCharisma); 
                        }
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Charisma increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        GetTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F7:
                    {
                        Console.Clear();
                        Console.WriteLine("Points have been reset."); 
                        newCharacter.Reset();
                        GetTable(newCharacter);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
            }
        }
    }

    public static void PrintCurrentStats(Character newCharacter)
    {
        Console.WriteLine($"F1 -- Strength: {newCharacter.TotalStrength}({GetModifier(newCharacter.TotalStrength)})");
        Console.WriteLine($"F2 -- Dexterity: {newCharacter.TotalDexterity}({GetModifier(newCharacter.TotalDexterity)})");
        Console.WriteLine($"F3 -- Constitution: {newCharacter.TotalConstitution}({GetModifier(newCharacter.TotalConstitution)})");
        Console.WriteLine($"F4 -- Intelligence: {newCharacter.TotalIntelligence}({GetModifier(newCharacter.TotalIntelligence)})");
        Console.WriteLine($"F5 -- Wisdom: {newCharacter.TotalWisdom}({GetModifier(newCharacter.TotalWisdom)})");
        Console.WriteLine($"F6 -- Charisma: {newCharacter.TotalCharisma}({GetModifier(newCharacter.TotalCharisma)})");
        Console.WriteLine("F7 -- Reset Stats");

    }

    public static int GetModifier(int number) => ((number - 10) / 2);


    public static int PointBuyCost(int attribute)
        => attribute switch
        {
            > 5 => 2,
            _ => 1
        };

    public static void GetTable(Character c)
    {
        

       AnsiConsole.Write(BuildTable(c));

    }


    public static Table BuildTable(Character c)
    {
        var table = new Table() { Expand = true };

        table.AddColumn("Attributes");
        table.AddColumn("Buttons");
        table.AddColumn("Points");
        table.AddColumn("Modifier");

        table.AddRow(
                    "Strength",
                    "F1",
                    c.TotalStrength.ToString(),
                    GetModifier(c.TotalStrength).ToString()
                );

        table.AddRow(
           "Dexterity",
           "F2",
           c.TotalDexterity.ToString(),
           GetModifier(c.TotalDexterity).ToString()
       );

        table.AddRow(
           "Constitution",
           "F3",
           c.TotalConstitution.ToString(),
           GetModifier(c.TotalConstitution).ToString()
       );

        table.AddRow(
           "Intelligence",
           "F4",
           c.TotalIntelligence.ToString(),
           GetModifier(c.TotalIntelligence).ToString()
       );

        table.AddRow(
           "Wisdom",
           "F5",
           c.TotalWisdom.ToString(),
           GetModifier(c.TotalWisdom).ToString()
       );

        table.AddRow(
           "Charisma",
           "F6",
           c.TotalCharisma.ToString(),
           GetModifier(c.TotalCharisma).ToString()
       );

        table.AddRow(
           "Reset Points",
           "F7",
           "",
           ""
       );

        return table;
    }
   
}
    




public class Character
{
    public string Name { get; private set; }
    public Race Race { get; private set; }
    public string GameClass { get; private set; }
    public int BaseStrength { get; set; } = 8;
    public int BaseDexerity { get; set; } = 8;
    public int BaseConstitution { get; set; } = 8;
    public int BaseIntelligence { get; set; } = 8;
    public int BaseWisdom { get; set; } = 8;
    public int BaseCharisma { get; set; } = 8;
    public int TotalSkillPoints { get; set; } = 27;

    public int PointBuyStrength { get; set; }
    public int PointBuyDexterity { get; set; }
    public int PointBuyConstitution { get; set; } 
    public int PointBuyIntelligence { get; set; } 
    public int PointBuyWisdom { get; set; }
    public int PointBuyCharisma { get; set; }

    
    public Character(Race race, string gameClass, string name)
    {

        Race = race;
        GameClass = gameClass;
        Name = name;
        Reset();
    }

    public void Reset()
    {
        BaseStrength = 8 + Race.Strength;
        BaseDexerity = 8 + Race.Dexterity;
        BaseConstitution = 8 + Race.Constitution;
        BaseIntelligence = 8 + Race.Intelligence;
        BaseWisdom = 8 + Race.Wisdom;
        BaseCharisma = 8 + Race.Charisma;
        TotalSkillPoints = 27;
        PointBuyStrength = 0;
        PointBuyDexterity = 0;
        PointBuyConstitution = 0;
        PointBuyIntelligence = 0;
        PointBuyWisdom = 0;
        PointBuyCharisma = 0;
    }

    public int TotalStrength => BaseStrength + PointBuyStrength;
    public int TotalDexterity => BaseDexerity + PointBuyDexterity;
    public int TotalConstitution => BaseConstitution + PointBuyConstitution;
    public int TotalIntelligence => BaseIntelligence + PointBuyIntelligence;
    public int TotalWisdom => BaseWisdom + PointBuyWisdom;
    public int TotalCharisma => BaseCharisma + PointBuyCharisma;

}

public record Race( string Name, int Strength, int Dexterity, int Constitution, int Intelligence, int Wisdom, int Charisma)
{
    
}

