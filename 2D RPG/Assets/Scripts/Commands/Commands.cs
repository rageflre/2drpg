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

    public static void HandleCommands(string[] args)
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
            case "inventory":
            case "bank":
                Debug.Log("length: " + args.Length);
                if (args.Length > 2)
                {
                    bool adding = args[1].ToLower().Equals("add");
                    bool removing = args[1].ToLower().Equals("remove");
                    Item item = null;
                    int amount = 1;
                    int slot = -1;
                    int id = int.Parse(args[2]);

                    if (args.Length == 3) item = new Item(id, 1);
                    else if (args.Length == 4)
                    {
                        amount = int.Parse(args[3]);
                        item = new Item(id, amount);

                    }
                    else if (args.Length == 5)
                    {
                        amount = int.Parse(args[3]);
                        slot = int.Parse(args[4]);
                        item = new Item(id, amount);

                    }

                    if (command.Equals("inventory"))
                    {
                        if (adding) Inventory.instance.GetInventory().Add(item, slot);
                        else Inventory.instance.GetInventory().Remove(item, slot);
                    } else if (command.Equals("bank"))
                    {
                        if (adding) Bank.instance.GetBank().Add(item, slot);
                        else Bank.instance.GetBank().Remove(item, slot);
                    }
                    Console.AddTextToConsole(options[0].Replace("{0}", adding ? "Succesfully added " + amount + "x " + item.getDefinition().itemName + "'s to your bank" : "Succesfully removed " + amount + "x " + item.getDefinition().itemName + "'s to your bank"));
                } else Console.AddTextToConsole(options[1]);
                break;
        }
    }

    static Dictionary<string, string[]> commands = new Dictionary<string, string[]>()
    {
        { "help", new string[] { "You are able to use the following commands:\nAdd [Usage: add id amount(Optional) slot(Optional)] - Adds items to your inventory\nRemove [Usage: remove id amount(Optional) slot(Optional) - Removes items from your inventory", "N.V.T" } },
        { "inventory", new string[] { "{0}", "Please use the command correctly example inventory add/remove itemid amount" } },
        { "bank", new string[] { "{0}", "Please use the command correctly example bank add/remove itemid amount" } }
    };
}
