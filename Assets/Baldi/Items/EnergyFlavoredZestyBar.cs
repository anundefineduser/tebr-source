using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyFlavoredZestyBar : ItemBase
{
    public override void Pickup()
    {
        base.Pickup();
    }

    public override void Use()
    {
        GameManager.current.player.stamina = GameManager.current.player.maxStamina * 2f;
        base.Use();
    }
}