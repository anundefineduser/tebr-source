using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager current;
    private void Awake()
    {
        current = this;
    }

    public ItemAsset[] inventory = new ItemAsset[3];
    public int selected;

    public void UseItem()
    {
        if (inventory[selected] != null && inventory[selected].info.itemScript != null)
        {
            GameObject itemOBJ = new GameObject(inventory[selected].name);
            itemOBJ.transform.parent = transform;
            Component itemScript = itemOBJ.AddComponent(Type.GetType(inventory[selected].info.itemScript.name));
            itemScript.SendMessage("Use");
            Destroy(itemOBJ);
        }
    }

    public void ClearItem()
    {
        inventory[selected] = null;
    }
    public void ClearItem(int slot)
    {
        inventory[slot] = null;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
            UseItem();
    }
}
