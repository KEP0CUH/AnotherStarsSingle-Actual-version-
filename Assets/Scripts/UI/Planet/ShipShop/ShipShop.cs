using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipShop : MonoBehaviour
{
    private List<ShipData> shipData;

    [SerializeField]
    private GameObject shipList;

    public void Init()
    {
        shipData = new List<ShipData>();
        ShowShips();
    }

    private void ShowShips()
    {
        shipData.Add(Managers.Resources.DownloadData(ShipKind.GreenKorvet));
        shipData.Add(Managers.Resources.DownloadData(ShipKind.GreenFrigate));
        shipData.Add(Managers.Resources.DownloadData(ShipKind.GreenLinkor));

        foreach (var ship in shipData)
        {
            var shipCell = Instantiate(Managers.Resources.DownloadData(ObjectType.ShipCell));
            shipCell.GetComponent<ShipCell>().Init(shipList.transform, ship);

            //var newObj = new GameObject("Ship", typeof(ShipSlot));
            //newObj.GetComponent<ShipSlot>().Init(shipList.transform,ship);
        }
    }

}
