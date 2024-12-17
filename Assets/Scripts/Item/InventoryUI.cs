using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform slotsParent;  // Parent object of inventory slots
    public GameObject slotPrefab;  // Prefab for inventory slot UI
    private Inventory inventory;

    private List<InventorySlotUI> slotUIs = new List<InventorySlotUI>();

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();

        // Generate slots dynamically
        foreach (var slot in inventory.slots)
        {
            AddSlotUI(slot);
        }
    }

    void AddSlotUI(InventorySlot slot)
    {
        GameObject newSlot = Instantiate(slotPrefab, slotsParent);
        InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();
        slotUI.UpdateSlot(slot.item, slot.amount);
        slotUIs.Add(slotUI);
    }

    public void RefreshUI()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            slotUIs[i].UpdateSlot(inventory.slots[i].item, inventory.slots[i].amount);
        }
    }
}
