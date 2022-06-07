using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ShipCell : MonoBehaviour
{
    private ShipShop shipShop;
    private ShipData shipData;

    public ShipCell Init(Transform parent, ShipData data)
    {
        this.shipData = data;

        this.gameObject.GetComponent<RectTransform>().SetParent(parent);
        this.gameObject.GetComponent<Image>().sprite = data.Icon;
        this.gameObject.GetComponent<Button>().onClick.AddListener(BuyShip);

        return this;
    }

    private void BuyShip()
    {
        Debug.Log("Changing ship...");
        Managers.Player.Controller.State.SetShip(this.shipData.Kind);
    }
}
