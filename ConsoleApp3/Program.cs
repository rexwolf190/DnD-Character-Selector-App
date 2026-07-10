using Spectre.Console;
using System.Reflection.Metadata;
using System.Text.Json;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

public class Program
{
    private static string? name;
    private static Race? race;
    private static GameClass? gameClass;
    private static readonly Race[] validRaces = [
        new Race("Aasimar", 0, 0, 0, 0, 1, 2, "Aasimar are mortals who carry a spark of the Upper Plane within their souls."),
        new Race("Dragonborn", 2, 0, 1, 0, 0, 0, "The ancestors of dragonborn hatched from the eggs of chromatic and metallic dragons."),
        new Race("Dwarf", 2, 0, 2, 0, 0, 0, "Dwarves were raised from the earth in the elder days by a deity of the forge."),
        new Race("Elf", 2, 0, 0, 0, 1, 0, "The elves' curiosity led many of them to explore the other planes of existence."),
        new Race("Gnome", 0, 1, 0, 2, 0, 0, "Gnomes are magical folk created by gods of invention, illusions and life underground."),
        new Race("Goliath", 2, 0, 1, 0, 0, 0, "Goliaths are distant descendants of giants and seek heights above those reached by their ancestors."),
        new Race("Halfling", 0, 2, 1, 0, 0, 0, "Halflings possess a brave and adventurous spirit that leads them on journeys of discovery."),
        new Race("Human", 1, 1, 1, 1, 1, 1, "Found throughout the multiverse, humans are as varied as they are numerous."),
        new Race("Orc", 0, 0, 2, 0, 0, 0, "Orcs are equipped with gifts to help them wander great plains, vast caverns and churning seas."),
        new Race("Tiefling", 0, 0, 0, 1, 0, 2, "Tieflings are either born in the Lower Plans or have Fiendish Ancestors who originated there.")
    ];

    private static readonly GameClass[] validClasses = [
        new GameClass ("Barbarian", "A Fierce Warrior Of Primal Rage", "Barbarians are mighty warriors who are powered by primal forces of the multiverse that manifest as a Rage. "),
        new GameClass ("Bard",  "An Inspiring Performer Of Music, Dance And Magic", "Invoking magic through music, dance, and verse, Bards are expert at inspiring others, soothing hurts, disheartening foes, and creating illusions. "),
        new GameClass ("Cleric",  "A Miraculous Priest Of Divine Power", "Blessed by a deity, a pantheon, or another immortal entity, a Cleric can reach out to the divine magic of the Outer Planes—where gods dwell—and channel it to bolster people and battle foes.. "),
        new GameClass ("Druid",  "A Nature Priest Of Primal Power", "Druids belong to ancient orders that call on the forces of nature. Harnessing the magic of animals, plants, and the four elements, Druids heal, transform into animals, and wield elemental destruction. "),
        new GameClass ("Fighter",  "A Master Of All Arms And Armor", "Fighters rule many battlefields. Questing knights, royal champions, elite soldiers, and hardened mercenaries—as Fighters, they all share an unparalleled prowess with weapons and armor. "),
        new GameClass ("Monk",  "A Martial Master Of Supernatural Focus", "Monks use rigorous combat training and mental discipline to align themselves with the multiverse and focus their internal reservoirs of power. Different Monks conceptualize this power in various ways: as breath, energy, life force, essence, or force. "),
        new GameClass ("Paladin",  "A Devout Warrior Of Sacred Oaths", "Paladins are united by their oaths to stand against the forces of annihilation and corruption. Whether sworn before a god’s altar, in a sacred glade before nature spirits, or in a moment of desperation and grief with the dead as the only witnesses, a Paladin’s oath is a powerful bond."),
        new GameClass ("Ranger",  "A Wandering Warrier Imbued With Primal Magic", "Far from bustling cities, amid the trees of trackless forests and across wide plains, Rangers keep their unending watch in the wilderness. Rangers learn to track their quarry as a predator does, moving stealthily through the wilds and hiding themselves in brush and rubble. "),
        new GameClass ("Rogue",  "A Dexterous Expert In Stealth And Subterfuge", "Rogues rely on cunning, stealth, and their foes’ vulnerabilities to get the upper hand in any situation. They have a knack for finding the solution to just about any problem. In combat, Rogues prioritize subtle strikes over brute strength. They would rather make one precise strike than wear an opponent down with a barrage of blows. "),
        new GameClass ("Sorcerer",  "A Dazzling Mage Filled With Innate Magic", "Sorcerers wield innate magic that is stamped into their being. Some Sorcerers can’t name the origin of their power, while others trace it to strange events in their personal or family history. The blessing of a dragon or a dryad at a baby’s birth or the strike of lightning from a clear sky might spark a Sorcerer’s gift.  "),
        new GameClass ("Warlock",  "An Occultist Empowered By Otherworldly Pacts", "Warlocks quest for knowledge that lies hidden in the fabric of the multiverse. They often begin their search for magical power by delving into tomes of forbidden lore, dabbling in invocations meant to attract the power of extraplanar beings, or seeking places of power where the influence of these beings can be felt.  "),
        new GameClass ("Wizard",  "A Scholarly Magic-User Of Arcane Power", "Wizards are defined by their exhaustive study of magic’s inner workings. They cast spells of explosive fire, arcing lightning, subtle deception, and spectacular transformations. Their magic conjures monsters from other planes of existence, glimpses the future, or forms protective barriers. Their mightiest spells change one substance into another, call meteors from the sky, or open portals to other worlds. ")
    ];

    private static List<Character> characters = new List<Character>();

    private static Character character;



    private static void Main()
    {
        MainMenu(characters);
    }

    public static void GetCharacterIntoFile()
    {
        name = GetName();
        race = GetRace();
        gameClass = GetClass();
        Character character = new Character(race, gameClass, name);
        ChangeAttributes(character);
        characters.Add(character);
        Console.WriteLine($"You are {name}. Your role is {gameClass.Name}. You belong to the {race.Name} race. Good luck on your adventure, fellow traveller.");
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadLine();
        MainMenu(characters);
    }


    public static void MainMenu(List<Character> Characters1)
    {

        while (true)
        {
            Console.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Main Menu")
                .AddChoices(
                   "Create a new character",
                   "Save all characters",
                   "Load all characters"
                    ));

            if (choice == "Create a new character")
            {
                GetCharacterIntoFile();
            }

            if (choice == "Save all characters")
            {
                SaveToNewFile(characters);
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();

            }


            if (choice == "Load all characters")
            {
                GetCharacter(characters);
            }

        }



    }

    public static Character GetCharacter(List<Character> c)
    {
        c = ReadFromFile();
        var choosenCharacter = AnsiConsole.Prompt(
            new SelectionPrompt<Character>()
            .Title("What [green]character[/] would you like?")
            .UseConverter(choosenCharacter => $"{choosenCharacter.Name} -- {choosenCharacter.Race.Name} -- {choosenCharacter.GameClass.Name}")
            .PageSize(3)
            .AddChoices(c));
        var question = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Are you [green]sure[/]?")
            .PageSize(4)
            .AddChoices("Yes", "No")
            );
        return choosenCharacter;
    }

    public static Race GetRace()
    {
        while (true)
        {
            var race = AnsiConsole.Prompt(
                new SelectionPrompt<Race>()
                .Title("What [green]race[/] would you like?")
                .UseConverter(race => $"{race.Name} -- {race.Description}")
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

    public static GameClass GetClass()
    {
        while (true)
        {
            var gameClass = AnsiConsole.Prompt(
                new SelectionPrompt<GameClass>()
                .Title("What [green]class[/] would you like?")
                .UseConverter(gameClass => $"{gameClass.Name} -- {gameClass.CoolDescription}. {gameClass.Description}")
                .PageSize(3)
                .AddChoices(validClasses));

            var question = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("Are you [green]sure[/]?")
               .PageSize(4)
               .AddChoices("Yes", "No")
               );

            if (question.ToLower() == "yes") { Console.WriteLine($"You have selected {gameClass.Name}."); return gameClass; }
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





    public static void ChangeAttributes(Character newCharacter)
    {
        BuildTable(newCharacter);
        while (newCharacter.TotalSkillPoints > 0)
        {



            var keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.F1:
                    {
                        Console.Clear();
                        if (newCharacter.PointBuyStrength < 7 && (newCharacter.TotalSkillPoints - PointBuyCost(newCharacter.PointBuyStrength)) >= 0)
                        {
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyStrength);
                            newCharacter.PointBuyStrength++;
                        }
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        AnsiConsole.WriteLine($"Strength increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        BuildTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F2:
                    {
                        Console.Clear();

                        if (newCharacter.PointBuyDexterity < 7 && (newCharacter.TotalSkillPoints - PointBuyCost(newCharacter.PointBuyDexterity)) >= 0)
                        {
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyDexterity);
                            newCharacter.PointBuyDexterity++;

                        }

                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Dexterity increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        BuildTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F3:
                    {
                        Console.Clear();
                        if (newCharacter.PointBuyConstitution < 7 && (newCharacter.TotalSkillPoints - PointBuyCost(newCharacter.PointBuyConstitution)) >= 0)
                        {
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyConstitution);
                            newCharacter.PointBuyConstitution++;
                        }
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Constitution increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        BuildTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F4:
                    {
                        Console.Clear();
                        if (newCharacter.PointBuyIntelligence < 7 && (newCharacter.TotalSkillPoints - PointBuyCost(newCharacter.PointBuyIntelligence)) >= 0)
                        {
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyIntelligence);
                            newCharacter.PointBuyIntelligence++;
                        }
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Intelligence increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        BuildTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F5:
                    {
                        Console.Clear();
                        if (newCharacter.PointBuyWisdom < 7 && (newCharacter.TotalSkillPoints - PointBuyCost(newCharacter.PointBuyWisdom)) >= 0)
                        {
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyWisdom);
                            newCharacter.PointBuyWisdom++;
                        }
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Wisdom increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        BuildTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F6:
                    {
                        Console.Clear();
                        if (newCharacter.PointBuyCharisma < 7 && (newCharacter.TotalSkillPoints - PointBuyCost(newCharacter.PointBuyCharisma)) >= 0)
                        {
                            newCharacter.PointBuyCharisma++;
                            newCharacter.TotalSkillPoints -= PointBuyCost(newCharacter.PointBuyCharisma);
                        }
                        else { Console.WriteLine("Cannot be increased. Maximum value reached"); }
                        Console.WriteLine($"Charisma increased. Skillpoints left are {newCharacter.TotalSkillPoints}");
                        BuildTable(newCharacter);
                        break;
                    }

                case ConsoleKey.F7:
                    {
                        Console.Clear();
                        Console.WriteLine("Points have been reset.");
                        newCharacter.Reset();
                        BuildTable(newCharacter);
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

    public static int GetModifier(int number) => ((number - 10) / 2);


    public static int PointBuyCost(int attribute)
        => attribute switch
        {
            > 5 => 2,
            _ => 1
        };




    public static void BuildTable(Character c)
    {
        Table table = new Table() { Expand = true };

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

        AnsiConsole.Write(table);
    }


    public static void SaveToNewFile(List<Character> c)
    {
        if (!File.Exists("Characters.json"))
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(c, options);
            File.WriteAllText("Characters.json", jsonString);
        }

        else {
            var existingCharacters = ReadFromFile();
            existingCharacters.AddRange(c);
            c = existingCharacters;
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(c, options);
            File.WriteAllText("Characters.json", jsonString);
        }
    }


    public static List<Character> ReadFromFile()
    {


        if (File.Exists("Characters.json"))
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = File.ReadAllText("Characters.json");
            List<Character> newCharacters = JsonSerializer.Deserialize<List<Character>>(jsonString, options);
            return newCharacters;
        }

        else { Console.WriteLine("Does not exist."); MainMenu(characters); return new List<Character>(); }

    }
}






public class Character
{
    public string Name { get; private set; }
    public Race Race { get; private set; }
    public GameClass GameClass { get; private set; }
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


    public Character(Race race, GameClass gameClass, string name)
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

public record Race(string Name, int Strength, int Dexterity, int Constitution, int Intelligence, int Wisdom, int Charisma, string Description)
{

}

public record GameClass(string Name, string CoolDescription, string Description) { }
