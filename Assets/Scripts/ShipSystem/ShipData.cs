using UnityEngine;


[CreateAssetMenu(menuName="Ship",fileName="NewShip",order = 54)]
public class ShipData : ScriptableObject
{

    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string title;
    [SerializeField]
    private string description;
    [SerializeField]
    private ShipKind kind;

    public Sprite Icon => icon;
    public string Title => title;
    public string Description => description;
    public ShipKind Kind => kind;

    private void OnValidate()
    {
        var greenPath = "Icons/Ships/Green";
        switch(kind)
        {
            case ShipKind.GreenLinkor:
                name = "Linkor";
                icon = Resources.Load<Sprite>(greenPath + "Linkor");
                title = "Линкор";
                description = "Корабль, предназначенный для штурма укрепленных баз противника.";
                break;
            case ShipKind.GreenFrigate:
                name = "Frigate";
                icon = Resources.Load<Sprite>(greenPath + "Frigate");
                title = "Фрегат";
                description = "";
                break;
            case ShipKind.GreenKorvet:
                name = "Korvet";
                icon = Resources.Load<Sprite>(greenPath + "Korvet");
                title = "Корвет";
                description = "";
                break;
        }
    }

}
