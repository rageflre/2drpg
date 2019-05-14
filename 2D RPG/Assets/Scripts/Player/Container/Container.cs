using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container
{
    //The amount of slots the container has
    private int capacity;
    //An array of the items in this container
    private Item[] items;

    public Container(int capacity)
    {
        //Sets the amount of slots in the container
        this.capacity = capacity;
        //Sets the array with the size of the container
        this.items = new Item[capacity];
    }

    public void set(int index, Item item)
    {
        //Sets the item on a certain slot
        items[index] = item;
    }

    public Item get(int slot)
    {
        //Checks if the slot isnt -1
        if (slot == -1) return null;
        //Return the item in this slot
        return items[slot];
    }

    public bool add(Item item)
    {
        return add(item, -1);
    }

    public bool add(Item item, int slot)
    {
        //Checks if the item isnt null
        if (item == null) return false;
        //Checks if the slot is higher then -1, else look for the next free slot
        int newSlot = slot > -1 ? slot : freeSlot();
        //Checks if the item is stackable and the player has the item
        if (item.getDefinition().stackable && contains(item)) newSlot = getSlot(item.GetId());
        //Make sure the slot is actually free once again
        if (get(newSlot) != null) newSlot = freeSlot();
        //Checks if new slot is -1
        if (newSlot == -1) return false;
        //Checks if the item is stackable
        if(item.getDefinition().stackable)
        {
            //Loops though all the items
            for (int index = 0; index < items.Length; index++)
            {
                //Checks if the item isnt null and the id's match
                if (items[index] != null && items[index].GetId() == item.GetId())
                {
                    //Grabs the new total count of the items
                    int totalCount = item.GetAmount() + items[index].GetAmount();
                    //Checks if the total count is higher then the max int or lower then 1
                    if (totalCount >= int.MaxValue || totalCount < 1) return false;
                    //Sets the new item amount on the correct slot
                    set(index, new Item(items[index].GetId(), items[index].GetAmount() + item.GetAmount()));
                    return true;
                }
            }
            //If item doesnt exist in the container already
            set(newSlot, item);
            return true;
        } else
        {
            //Checks how many free slots there are left in the container
            int openSlots = freeSlots();
            //Checks if the amount of the items is higher then the free amount of slots the container has
            if (item.GetAmount() > openSlots)
                item.setAmount(openSlots);

            if (openSlots >= item.GetAmount())
            {
                //Loops though how many items it should add to the inventory
                for (int i = 0; i < item.GetAmount(); i++)
                    //Sets the item to the new slot if its free else it takes the next available slot
                    set(get(newSlot) == null ? newSlot : freeSlot(), new Item(item.GetId()));
                return true;
            }
        }
        return false;
    }

    public int remove(Item item)
    {
        return remove(item, -1);
    }

    public int remove(Item item, int preferredSlot)
    {
        //Checks if the item your trying to remove is null
        if (item == null) return -1;
        int removed = 0;
        //Checks if the item is stackable
        if(item.getDefinition().stackable)
        {
            //Grabs the slot your trying to remove
            int slot = getSlot(item.GetId());
            //Grabs the item stack your removing
            Item stack = get(slot);
            //Checks if there is a stack available or not
            if (stack == null) return -1;
            //Checks if the stack has more items then its removing
            if (stack.GetAmount() > item.GetAmount())
            {
                //Sets how many items were removed
                removed = item.GetAmount();
                //Sets the new item with the correct amount
                set(slot, new Item(stack.GetId(), stack.GetAmount() - item.GetAmount()));
            } else
            {
                //Sets how many items were removed
                removed = stack.GetAmount();
                //Sets the item to null
                set(slot, null);
            }
        } else
        {
            //Loops though how many items are being removed
            for (int index = 0; index < item.GetAmount(); index++)
            {
                //Grabs what slot it has to be removed
                int slot = getSlot(item.GetId());
                //Checks if the index is 0 and the preferred slot isnt -1
                if (index == 0 && preferredSlot != -1)
                {
                    //Grabs the item in the preferred slot
                    Item inSlot = get(preferredSlot);
                    //Checks if the item in slot match the item your trying to remove
                    if (inSlot.GetId() == item.GetId()) slot = preferredSlot;
                }
                //Checks if slot isnt -1
                if (slot != -1) {
                    //Increase the removed
                    removed++;
                    //Sets the slot to null
                    set(slot, null);
                }
                else break;
            }
        }
        //Returns how many items were removed
        return removed;
    }

    public bool contains(Item contains)
    {
        //Loops though all items in this container
        foreach(Item item in items)
        {
            //Checks if the item is null
            if (item == null) continue;
            //Checks if the item ids match
            if (item.GetId() == contains.GetId()) return true;
        }
        return false;
    }

    public int freeSlot()
    {
        //Loops though all the items as a for loop as we need a number to return for the slot
        for (int index = 0; index < items.Length; index++)
            if (items[index] == null || items[index].GetId() == -1) return index;
        return -1;
    }

    public int size()
    {
        int size = 0;
        //Loops though all the items in the container
        foreach(Item item in items)
        {
            //Checks if the item isnt null
            if (item != null)
            {
                //Increase the total size thats currently in the container
                size++;
            }
        }
        return size;
    }

    public int freeSlots()
    {
        return capacity - size();
    }

    public int getCapacity()
    {
        //Returns how many slots the container has
        return capacity;
    }

    public int getSlot(int id)
    {
        //Loops though all the items in a for loop as we need a number to return for the slot
        for (int index = 0; index < items.Length; index++)
        {
            //Checks if the item is null
            if (items[index] == null) continue;
            //If the id's match return the right slot
            if (items[index].GetId() == id) return index;
        }
        return -1;
    }

    public Item[] toArray()
    {
        //Returns an item array
        return items;
    }
}
