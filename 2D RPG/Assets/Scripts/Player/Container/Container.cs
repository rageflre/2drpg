using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container
{
    //The amount of slots the container has
    private int capacity;
    //An array of the items in this container
    private Item[] items;
    //The container it has to refesh
    GameObject containerObject;

    public Container(int capacity, GameObject container)
    {
        //Sets the amount of slots in the container
        this.capacity = capacity;
        //Sets the array with the size of the container
        this.items = new Item[capacity];
        //Sets the container
        this.containerObject = container;
    }

    public void Set(int index, Item item)
    {
        //Sets the item on a certain slot
        items[index] = item;
        //Refreshes the container
        Refresh();
    }

    public Item Get(int slot)
    {
        //Checks if the slot isnt -1
        if (slot == -1) return null;
        //Return the item in this slot
        return items[slot];
    }

    public bool Add(Item item)
    {
        return Add(item, -1);
    }

    public bool Add(Item item, int slot)
    {
        //Checks if the item isnt null
        if (item == null) return false;
        //Checks if the slot is higher then -1, else look for the next free slot
        int newSlot = slot > -1 ? slot : FreeSlot();
        //Checks if the item is stackable and the player has the item
        if (item.getDefinition().stackable && Contains(item)) newSlot = GetSlot(item.GetId());
        //Make sure the slot is actually free once again
        if (Get(newSlot) != null) newSlot = FreeSlot();
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
                    Set(index, new Item(items[index].GetId(), items[index].GetAmount() + item.GetAmount()));
                    return true;
                }
            }
            //If item doesnt exist in the container already
            Set(newSlot, item);
            return true;
        } else
        {
            //Checks how many free slots there are left in the container
            int openSlots = FreeSlots();
            //Checks if the amount of the items is higher then the free amount of slots the container has
            if (item.GetAmount() > openSlots)
                item.setAmount(openSlots);

            if (openSlots >= item.GetAmount())
            {
                //Loops though how many items it should add to the inventory
                for (int i = 0; i < item.GetAmount(); i++)
                    //Sets the item to the new slot if its free else it takes the next available slot
                    Set(Get(newSlot) == null ? newSlot : FreeSlot(), new Item(item.GetId()));
                return true;
            }
        }
        return false;
    }

    public int Remove(Item item)
    {
        return Remove(item, -1);
    }

    public int Remove(Item item, int preferredSlot)
    {
        //Checks if the item your trying to remove is null
        if (item == null) return -1;
        int removed = 0;
        //Checks if the item is stackable
        if(item.getDefinition().stackable)
        {
            //Grabs the slot your trying to remove
            int slot = GetSlot(item.GetId());
            //Grabs the item stack your removing
            Item stack = Get(slot);
            //Checks if there is a stack available or not
            if (stack == null) return -1;
            //Checks if the stack has more items then its removing
            if (stack.GetAmount() > item.GetAmount())
            {
                //Sets how many items were removed
                removed = item.GetAmount();
                //Sets the new item with the correct amount
                Set(slot, new Item(stack.GetId(), stack.GetAmount() - item.GetAmount()));
            } else
            {
                //Sets how many items were removed
                removed = stack.GetAmount();
                //Sets the item to null
                Set(slot, null);
            }
        } else
        {
            //Loops though how many items are being removed
            for (int index = 0; index < item.GetAmount(); index++)
            {
                //Grabs what slot it has to be removed
                int slot = GetSlot(item.GetId());
                //Checks if the index is 0 and the preferred slot isnt -1
                if (index == 0 && preferredSlot != -1)
                {
                    //Grabs the item in the preferred slot
                    Item inSlot = Get(preferredSlot);
                    //Checks if the item in slot match the item your trying to remove
                    if (inSlot.GetId() == item.GetId()) slot = preferredSlot;
                }
                //Checks if slot isnt -1
                if (slot != -1) {
                    //Increase the removed
                    removed++;
                    //Sets the slot to null
                    Set(slot, null);
                }
                else break;
            }
        }
        //Returns how many items were removed
        return removed;
    }

    void Refresh()
    {
        for (int index = 0; index < items.Length; index++)
        {
            Item item = items[index];
            //Grabs the slot object
            GameObject slotObject = containerObject.transform.GetChild(index).gameObject;
            //Grabs the slot image object
            GameObject slotImage = slotObject.transform.GetChild(0).gameObject;
            //Grabs the slot text object
            GameObject slotStack = slotObject.transform.GetChild(1).gameObject;
            //Grabs the image component
            Image image = slotImage.GetComponent<Image>();
            //Grabs the text amount component
            Text stackAmount = slotStack.GetComponent<Text>();
            //Checks if the item isnt null
            if (item == null || slotObject == null || image == null || stackAmount == null)
            {
                //Disables the image
                slotImage.SetActive(false);
                //Disables the text
                slotStack.SetActive(false);
                continue;
            }
            int id = item.GetId();
            //Grabs the path of the item sprite
            string spritePath = "Items/Sprites/sprite_" + id;
            //Grabs the item sprite
            Sprite sprite = Resources.Load<Sprite>(spritePath);
            //Sets the image in the item image
            image.sprite = sprite;
            //Sets the image parent active
            slotImage.SetActive(true);
            //Checks if the item is stackable
            if (item.getDefinition().stackable)
            {
                //Sets the amount of items the player has
                stackAmount.text = item.GetAmount().ToString();
                //Actives the text
                slotStack.SetActive(true);
            }
        }
    }

    public bool Contains(Item contains)
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

    public int FreeSlot()
    {
        //Loops though all the items as a for loop as we need a number to return for the slot
        for (int index = 0; index < items.Length; index++)
            if (items[index] == null || items[index].GetId() == -1) return index;
        return -1;
    }

    public int Size()
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

    public int FreeSlots()
    {
        return capacity - Size();
    }

    public int GetCapacity()
    {
        //Returns how many slots the container has
        return capacity;
    }

    public int GetSlot(int id)
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

    public Item[] ToArray()
    {
        //Returns an item array
        return items;
    }
}
