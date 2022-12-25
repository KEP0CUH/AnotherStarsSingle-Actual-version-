///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ShipCell : MonoBehaviour
{
    private             ShipShop            shipShop;
    private             ShipData            shipData;

    public              ShipCell            Init(ShipShop       shipShop,
                                                 Transform      parent,
                                                 ShipData       data)
    {
        this.shipShop = shipShop;
        this.shipData = data;

        this.gameObject.GetComponent<RectTransform>().SetParent(parent);
        this.gameObject.GetComponent<Image>().sprite = data.Icon;
        this.gameObject.GetComponent<Button>().onClick.AddListener(SelectShip);
        this.SelectShip();

        return this;
    }

    private             void                SelectShip()
    {
        shipShop.UpdateSelectedShip(shipData);
    }
}
