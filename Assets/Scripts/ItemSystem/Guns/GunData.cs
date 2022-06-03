using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Items/Guns/newGun",fileName ="NewGun",order =54)]
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
                ammoKind = AmmoKind.DesintegratorAmmo;
                soundKind = SoundKind.ShotEnergetic2;
                break;
            case ItemKind.MultiblasterGun:
                ammoKind = AmmoKind.MultiblasterAmmo;
                soundKind = SoundKind.ShotKinetic2;
                break;
        }
    }
}
