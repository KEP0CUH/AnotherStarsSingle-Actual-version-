using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ItemShop",fileName ="newItemShop",order =51)]
public class ItemShopData : ScriptableObject
{
    [SerializeField] private ItemShopType itemShopType;
    [SerializeField] private List<ItemData> itemsForBuyingData;
    [SerializeField] private List<ItemState> itemsForBuyingStates;

    public ItemShopType ItemShopType => itemShopType;
    public List<ItemData> ItemsForBuyingData => itemsForBuyingData;
    public List<ItemState> ItemsForBuyingState => itemsForBuyingStates;

    private void OnValidate()
    {
        if (Managers.Resources != null)
        {
            itemsForBuyingData = new List<ItemData>();
            itemsForBuyingStates = new List<ItemState>();
            switch (itemShopType)
            {
                case ItemShopType.GreenShop1:
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.GoldOre));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.FerrumOre));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.MultiblasterGun));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.DesintegratorGun));

                    var newItem = new GameObject("shopItem",typeof(ItemState));
                    newItem.GetComponent<ItemState>().Init(ItemKind.GoldOre,50);
                    break;
                case ItemShopType.GreenShop2:
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.OsmiumOre));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.MineralOre));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.TitanOre));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.FerrumOre));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.GoldOre));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.OrganicOre));
                    break;

            }
        }

    }
}
