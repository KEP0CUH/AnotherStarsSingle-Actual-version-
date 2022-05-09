using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newMob",menuName="Mob",order =51)]
public class MobData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private MobKind kind;
    [SerializeField] private ShipKind ship;
    [SerializeField] private List<ItemKind> guns;
    [SerializeField] private List<ItemKind> devices;

    public string Title => title;
    public ShipKind Ship => ship;

    private void OnValidate()
    {
        guns = new List<ItemKind>();
        devices = new List<ItemKind>();
        switch(kind)
        {
            case MobKind.PirateIndus1:
                title = "PirateIndus1";
                ship = ShipKind.GreenKorvet;
                guns.Add(ItemKind.PulsarGun);
                devices.Add(ItemKind.TourbineDevice);
                break;
        }
    }
}
