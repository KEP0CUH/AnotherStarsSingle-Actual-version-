///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Items/Guns/newGun",fileName ="NewGun",order =54)]
public class GunData : ItemData
{
    [SerializeField]
    private             AmmoKind            ammoKind;
    [SerializeField]
    private             SoundKind           soundKind;
    [SerializeField]
    private             int                 maxCartriges;
    [SerializeField]
    private             int                 attackRange;
    [SerializeField]
    private             float               cooldown;
    [SerializeField]
    private             int                 energyCost;
    [SerializeField] 
    private             int                 damage;
    private             float               deviation       = 0.2f;
    [SerializeField] 
    private             GunType             type;
    [SerializeField] 
    private             int                 cpu;

    public              AmmoKind            AmmoKind => ammoKind;
    public              SoundKind           SoundKind => soundKind;
    public              int                 MaxCartriges => maxCartriges;
    public              int                 AttackRange => attackRange;
    public              float               Cooldown => cooldown;
    public              int                 EnergyCost => energyCost;
    public              int                 Damage => damage;
    public              float               Deviation => deviation;
    public              int                 CPU => cpu;

    public              int                 CalculateDamage()
    {
        return this.damage + (int)Random.Range(-deviation * damage, deviation * damage);
    }

    public override     bool                IsItem()
    {
        return false;
    }

    public override     bool                IsWeapon()
    {
        return true;
    }

    protected override  void                OnValidate()
    {
        deviation = 0.2f;
        base.OnValidate();
        switch(ItemKind)
        {
            case ItemKind.DesintegratorGun:
                ammoKind = AmmoKind.DesintegratorAmmo;
                soundKind = SoundKind.ShotEnergetic2;
                maxCartriges = 3;
                attackRange = 500;
                cooldown = 2.5f;
                energyCost = 3;
                damage = 50;
                type = GunType.Energetic;
                cpu = 1200;
                break;
            case ItemKind.MultiblasterGun:
                ammoKind = AmmoKind.MultiblasterAmmo;
                soundKind = SoundKind.ShotKinetic2;
                maxCartriges = 5;
                attackRange = 350;
                cooldown = 2.0f;
                energyCost = 4;
                damage = 25;
                type = GunType.Kinetic;
                cpu = 1000;
                break;
        }
    }
}
