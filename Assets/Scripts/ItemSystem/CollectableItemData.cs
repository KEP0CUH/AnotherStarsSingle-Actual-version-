using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newItem", menuName = "Data/Items/CollectableItems", order = 51)]
public class CollectableItemData : BaseItemData
{
    private int maxCountStack;
    public int MaxCountStack => maxCountStack;
}
