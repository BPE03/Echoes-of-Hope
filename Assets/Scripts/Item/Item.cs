using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : MonoBehaviour
{
    public string itemName;           // Name of the item
    public Sprite icon;               // Icon to display in UI
    public bool isStackable;          // Can multiple items stack?
    public int maxStack = 99;         // Max stack size for the item
    public string description;        // Item description
}
