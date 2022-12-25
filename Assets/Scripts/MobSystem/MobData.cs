///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newMob",menuName="Mob",order =51)]
public class MobData : ScriptableObject
{
    [SerializeField]
    private              string                 title;
    [SerializeField] 
    private              MobKind                kind;
    [SerializeField] 
    private              ShipKind               ship;
    [SerializeField] 
    private              List<ItemKind>         guns;
    [SerializeField]
    private              List<ItemKind>         devices;

    public               string                 Title => title;
    public               ShipKind               Ship => ship;
    public               MobKind                MobKind => kind;

    private              void                   OnValidate()
    {
        guns = new List<ItemKind>();
        devices = new List<ItemKind>();
        switch(kind)
        {
            case MobKind.PirateIndus1:
                title = "PirateIndus1";
                ship = ShipKind.PirateIndus;
                guns.Add(ItemKind.PulsarGun);
                devices.Add(ItemKind.TourbineDevice);
                break;
            case MobKind.PirateIstrebitel1:
                title = "PirateIndus1";
                ship = ShipKind.PirateIstrebitel;
                break;
            case MobKind.PirateFrigate1:
                title = "PirateFrigate1";
                guns.Add(ItemKind.RezakGun);
                devices.Add(ItemKind.PhotonGun);
                ship = ShipKind.PirateFrigate;
                break;
        }
    }
}
