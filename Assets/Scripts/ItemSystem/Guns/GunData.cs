using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Guns",fileName ="NewGun",order =53)]
public class GunData : ItemData
{
    [SerializeField] private AmmoKind ammoKind;


    public AmmoKind AmmoKind => ammoKind;

    public override bool IsItem()
    {
        return false;
    }
    public override bool IsWeapon()
    {
        return true;
    }
}
