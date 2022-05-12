using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ItemShop",fileName ="newItemShop",order =51)]
public class ItemShopData : ScriptableObject
{
    [SerializeField] private ItemShopType itemShopType;
    [SerializeField] private List<ItemData> itemsForBuyingData;

    public ItemShopType ItemShopType => itemShopType;
    public List<ItemData> ItemsForBuyingData => itemsForBuyingData;


    private void OnValidate()
    {
        if (Managers.Resources != null)
        {
            itemsForBuyingData = new List<ItemData>();
            switch (itemShopType)
            {
                case ItemShopType.GreenShop1:
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.GoldOre));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.FerrumOre));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.MultiblasterGun));
                    itemsForBuyingData.Add(Managers.Resources.DownloadData(ItemKind.DesintegratorGun));
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
