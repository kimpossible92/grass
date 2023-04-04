using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarC : MonoBehaviour
{
    // We can later change it in the inspector because it's serialized

    [SerializeField] int toolbarSize = 8;
    public int selectedTool;                               // Holds the id of the selected tool
   

    internal void Set(int id)
    {
        selectedTool = id;
    }

    public Item GetItem
    {
        get
        {
            return GameM.instance.inventoryContainer.slots[selectedTool].item;
        }
    }

    public int GetCount
    {
        get
        {
            return GameM.instance.inventoryContainer.slots[selectedTool].count;
        }
    }


}
