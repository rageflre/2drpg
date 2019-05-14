using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands
{
    static bool CommandExists(string command)
    {
        //Loops though the commands dictionary
        foreach (KeyValuePair<string, string[]> entry in commands)
        {
            //Checks if any of the key's matches the command
            if (entry.Key.Equals(command))
            {
                return true;
            }
        }
        return false;
    }

    public static void HandleCommands(GameObject playerObject, string[] args)
    {
        string command = args[0];
        //Checks if the commands exists, if not add some text to the console
        if (!CommandExists(command))
        {
            Console.AddTextToConsole("Unkown command: " + command);
            return;
        }

        //Grabs all possible options
        string[] options = commands[command];

        switch (command)
        {
            case "help":
                Console.AddTextToConsole(options[0]);
                break;
            case "add":
                int id = int.Parse(args[1]);
                int amount = 1;
                Item item = null;
                int slot = -1;
                if (args.Length == 2) item = new Item(id, 1);
                else if (args.Length == 3)
                {
                    amount = int.Parse(args[2]);
                    item = new Item(id, amount);
                }
                else if (args.Length == 4)
                {
                    amount = int.Parse(args[2]);
                    slot = int.Parse(args[3]);
                    item = new Item(id, amount);
                }

                if (item != null)
                {
                    GameManager.GetInventory().Add(item, slot);
                    Console.AddTextToConsole(options[0].Replace("{0}", amount.ToString()).Replace("{1}", item.getDefinition().itemName));
                }
                else Console.AddTextToConsole(options[1]);

                break;
            case "remove":
                id = int.Parse(args[1]);
                amount = 1;
                item = null;
                slot = -1;
                if (args.Length == 2) item = new Item(id, 1);
                else if (args.Length == 3)
                {
                    amount = int.Parse(args[2]);
                    item = new Item(id, amount);
                }
                else if (args.Length == 4)
                {
                    amount = int.Parse(args[2]);
                    slot = int.Parse(args[3]);
                    item = new Item(id, amount);
                }
                if (item != null)
                {
                    GameManager.GetInventory().Remove(item);
                    Console.AddTextToConsole(options[0].Replace("{0}", amount.ToString()).Replace("{1}", item.getDefinition().itemName));
                }
                else Console.AddTextToConsole(options[1]);
                break;
        }
    }

    static Dictionary<string, string[]> commands = new Dictionary<string, string[]>()
    {
        { "help", new string[] { "You are able to use the following commands:\nAdd [Usage: add id amount(Optional) slot(Optional)] - Adds items to your inventory\nRemove [Usage: remove id amount(Optional) slot(Optional) - Removes items from your inventory", "N.V.T" } },
        { "add", new string[] { "Succesfully added {0}x {1}'s to your inventory", "Please use the command correctly example: add itemid amount" }},
        { "remove", new string[] { "Succesfully removed {0}x {1}'s to your inventory", "Please use the command correctly example: remove itemid amount" }}
    };
}
