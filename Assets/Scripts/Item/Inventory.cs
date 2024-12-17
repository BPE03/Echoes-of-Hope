using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>(); // Inventory slots

    public void AddItem(Item newItem, int amount)
    {
        // Check if the item already exists in inventory
        foreach (var slot in slots)
        {
            if (slot.item == newItem && newItem.isStackable)
            {
                slot.AddAmount(amount);
                return;
            }
        }

        // Add a new item if not stackable or no existing slot
        slots.Add(new InventorySlot(newItem, amount));
    }

    public void RemoveItem(Item itemToRemove, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == itemToRemove)
            {
                slots[i].RemoveAmount(amount);
                if (slots[i].amount <= 0)
                    slots.RemoveAt(i); // Remove slot if amount is zero
                return;
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;

    public InventorySlot(Item newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void RemoveAmount(int value)
    {
        amount -= value;
    }
}
