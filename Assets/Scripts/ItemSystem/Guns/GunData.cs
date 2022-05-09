using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Guns",fileName ="NewGun",order =53)]
public class GunData : ItemData
{
    [SerializeField] private AmmoKind ammoKind;
    [SerializeField] private SoundKind soundKind;


    public AmmoKind AmmoKind => ammoKind;
    public SoundKind SoundKind => soundKind;

    public override bool IsItem()
    {
        return false;
    }
    public override bool IsWeapon()
    {
        return true;
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        switch(ItemKind)
        {
            case ItemKind.DesintegratorGun:
                soundKind = SoundKind.ShotEnergetic2;
                break;
            case ItemKind.MultiblasterGun:
                soundKind = SoundKind.ShotKinetic2;
                break;
        }
    }
}
