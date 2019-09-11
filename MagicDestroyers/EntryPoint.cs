using MagicDestroyers.Characters;
using MagicDestroyers.Characters.Melee;
using MagicDestroyers.Characters.Spellcasters;
using System.Collections.Generic;
using MagicDestroyers.InputOutputUser;
using System.IO;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MagicDestroyers
{
    class EntryPoint
    {
        static List<Character> characters;
        static List<string> options = new List<string>()
        {
            "Test send message",
            "Test error message",
            "Test load file",
            "Show Characters",
            "Create Warrior",
            "Exit"
        };
        static void Main()
        {
            // Check for previous data
            if (!LoadData())
            {
                IOUser.ConsoleOutput("No previous data");
                characters = new List<Character>();
            }
            else
            {
                IOUser.ConsoleOutput("Characters loaded...");
            }
       
            int option = -1;

            while (option != options.Count - 1)
            {
                IOUser.ConsoleListOutput("Please select one option", options);
                option = IOUser.ConsoleReadInput();
                switch (option)
                {
                    case 0:
                        IOUser.ConsoleOutput("Testing send message...");
                        break;
                    case 1:
                        IOUser.ConsoleError("Testing error message...");
                        break;
                    case 2:
                        IOUser.ConsoleOutput("Loading file...");
                        string s = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../Files/Characters.txt");
                        StreamReader reader = new StreamReader(s);

                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string [] characterParams = line.Split(',');
                            switch (characterParams[0])
                            {
                                case "Warrior":
                                    characters.Add(new Warrior(characterParams[1], int.Parse(characterParams[2])));
                                    break;
                                case "Knight":
                                    characters.Add(new Knight(characterParams[1], int.Parse(characterParams[2])));
                                    break;
                                case "Assassin":
                                    characters.Add(new Assassin(characterParams[1], int.Parse(characterParams[2])));
                                    break;
                                case "Mage":
                                    characters.Add(new Mage(characterParams[1], int.Parse(characterParams[2])));
                                    break;
                                case "Druid":
                                    characters.Add(new Druid(characterParams[1], int.Parse(characterParams[2])));
                                    break;
                                case "Necromancer":
                                    characters.Add(new Necromancer(characterParams[1], int.Parse(characterParams[2])));
                                    break;
                                default:
                                    break;
                            }
                        }
                        foreach (Character character in characters)
                        {
                            System.Console.WriteLine($"Character class: {character.GetType().Name}, Character Name: {character.Name}");
                        }
                        break;
                    case 3:
                        foreach (Character character in characters)
                        {
                            System.Console.WriteLine($"Character class: {character.GetType().Name}, Character Name: {character.Name}");
                        }
                        break;
                    case 4:
                        characters.Add(new Warrior());
                        break;
                    default:
                        SaveData();
                        break;
                }
            }
        }

        static void changeWarriorName(Warrior warrior)
        {
            //Al ser por referencia, el parametro cambiara el valor del guerrero que se encuentra en el main.
            warrior.Name = "Phil";
        }

        static int PlusOne(int number)
        {
            return number+1;
        }

        private static void SaveData()
        {
            // Creamos el Stream donde guardaremos nuestros personajes
            String fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../Characters.txt");
            FileStream fs = new FileStream(fileName, FileMode.Create);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, characters);
            fs.Close();
        }

        private static Boolean LoadData()
        {
            String fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../Characters.txt");
            if (!File.Exists(fileName))
            {
                return false;
            }
            FileStream fs = new FileStream(fileName, FileMode.Open);
            IFormatter formatter = new BinaryFormatter();
            characters = formatter.Deserialize(fs) as List<Character>;
            fs.Close();
            return true;
        }
    }
}
