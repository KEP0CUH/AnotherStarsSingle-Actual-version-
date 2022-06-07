using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Ships/NewShip",fileName="NewShip",order = 54)]
public class ShipData : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string title, description;
    [SerializeField]
    private ShipKind kind;

    [SerializeField] private int maxGuns, maxDevices;

    public Sprite Icon => icon;
    public string Title => title;
    public string Description => description;
    public ShipKind Kind => kind;

    public int MaxGuns => maxGuns;
    public int MaxDevices => maxDevices;

    private void OnValidate()
    {
        var iconsShipsPath = "Icons/Ships/";
        icon = Resources.Load<Sprite>(iconsShipsPath + kind);
        description = "";

        switch (kind)
        {
            case ShipKind.GreenLinkor:
                name = "Linkor";
                title = "Линкор";
                description = "Корабль, предназначенный для штурма укрепленных баз противника.";
                maxGuns = 5;
                maxDevices = 4;
                break;
            case ShipKind.GreenFrigate:
                name = "Frigate";
                title = "Фрегат";
                description = "";
                maxGuns = 3;
                maxDevices = 2;
                break;
            case ShipKind.GreenKorvet:
                name = "Korvet";
                title = "Корвет";
                description = "";
                maxGuns = 2;
                maxDevices = 2;
                break;
            case ShipKind.PirateIndus:
                name = "Indus";
                title = "Индустриальный";
                description = "";
                maxGuns = 1;
                maxDevices = 2;
                break;
            case ShipKind.PirateIstrebitel:
                name = "Istrebitel";
                title = "Истребитель";
                description = "";
                maxGuns = 2;
                maxDevices = 1;
                break;
            case ShipKind.PirateFrigate:
                name = "Frigate";
                title = "Фрегат";
                description = "";
                maxGuns = 3;
                maxDevices = 2;
                break;
        }
    }

}
