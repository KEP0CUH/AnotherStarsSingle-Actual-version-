using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ships/NewShip", fileName = "NewShip", order = 54)]
public class ShipData : ScriptableObject
{
    [SerializeField]
    private ShipKind kind;
    private Sprite icon;
    private string title;
    private int cost, size, maxGuns, maxDevices, armor, shields, structure, speed, energy, cpu, radar;

    public Sprite Icon => icon;
    public string Title => title;
    public ShipKind Kind => kind;

    public int Cost => cost;
    public int Size => size;
    public int MaxGuns => maxGuns;
    public int MaxDevices => maxDevices;
    public int Armor => armor;
    public int Shields => shields;
    public int Structure => structure;
    public int Speed => speed;
    public int Energy => energy;
    public int Cpu => cpu;
    public int Radar => radar;

    private void OnValidate()
    {
        var iconsShipsPath = "Icons/Ships/";
        icon = Resources.Load<Sprite>(iconsShipsPath + kind);

        switch (kind)
        {
            case ShipKind.GreenKorvet:
                name = "Korvet";
                title = "Корвет";
                cost = 21250;
                size = 670;
                maxGuns = 1;
                maxDevices = 3;
                armor = 2;
                shields = 18;
                structure = 670;
                speed = 92;
                energy = 230;
                cpu = 700;
                radar = 2000;
                break;
            case ShipKind.GreenFrigate:
                name = "Frigate";
                title = "Фрегат";
                cost = 120417;
                size = 1100;
                maxGuns = 3;
                maxDevices = 3;
                armor = 5;
                shields = 25;
                structure = 1400;
                speed = 75;
                energy = 300;
                cpu = 2000;
                radar = 2000;
                break;
            case ShipKind.GreenLinkor:
                name = "Linkor";
                title = "Линкор";
                cost = 312651;
                size = 1400;
                maxGuns = 4;
                maxDevices = 5;
                armor = 8;
                shields = 32;
                structure = 1750;
                speed = 65;
                energy = 410;
                cpu = 3100;
                radar = 2100;
                break;

            case ShipKind.PirateIndus:
                name = "Indus";
                title = "Индустриальный";
                maxGuns = 1;
                maxDevices = 2;
                break;
            case ShipKind.PirateIstrebitel:
                name = "Istrebitel";
                title = "Истребитель";
                maxGuns = 2;
                maxDevices = 1;
                break;
            case ShipKind.PirateFrigate:
                name = "Frigate";
                title = "Фрегат";
                maxGuns = 3;
                maxDevices = 2;
                break;
        }
    }

}
