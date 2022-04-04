using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ShipShop : MonoBehaviour
{
    private List<ShipData> shipData;
    private GameObject shipList = null;

    public void Init()
    {
        var scroll = this.gameObject.GetComponent<ScrollRect>();
        scroll.horizontal = true;
        scroll.vertical = false;
        scroll.scrollSensitivity = 15;

        CreateUpPart();


        shipData = new List<ShipData>();
        ShowShips();

    }

    private void CreateUpPart()
    {
        shipList = new GameObject("ShipList", typeof(Image),typeof(GridLayoutGroup),typeof(ContentSizeFitter));
        var rect = shipList.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform, false);
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0, 1);
        rect.offsetMin = new Vector2(5, -55);
        rect.offsetMax = new Vector2(490, -5);

        var image = shipList.GetComponent<Image>();
        image.color = new UnityEngine.Color(134, 183, 219, 90) / 256f;
        image.raycastTarget = false;

        var layout = shipList.GetComponent<GridLayoutGroup>();
        layout.cellSize = new Vector2(64, 64);
        layout.spacing = new Vector2(15, 15);
        layout.padding.top = 5;
        layout.padding.left = 5;
        layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        layout.constraintCount = 1;

        var fitter = shipList.GetComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

        
        this.gameObject.GetComponent<ScrollRect>().content = shipList.GetComponent<RectTransform>();
    }

    private void ShowShips()
    {
        shipData.Add(Managers.Resources.DownloadData(ShipKind.GreenLinkor));
        shipData.Add(Managers.Resources.DownloadData(ShipKind.GreenFrigate));

        foreach(var ship in shipData)
        {
            var newObj = new GameObject("Ship", typeof(ShipSlot));
            newObj.GetComponent<ShipSlot>().Init(shipList.transform,ship);
        }
    }

}
