using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Selectable))]
public class ItemSlot : MonoBehaviour
{
    private GameObject slot;
    private Transform parent;
    private BaseItemData data;
    private int count;
    private IInventoryUI inventory;

    [ContextMenu("RemoveItem")]
    private void RemoveItem()
    {
        Managers.Inventory.RemoveItem(data);
    }

    public void Init(Transform transform,IInventoryUI inventory, BaseItemData itemData,int count)
    {
        this.inventory = inventory;
        this.parent = transform;
        this.data = itemData;
        this.count = count;
        CreateItemSlot(itemData, count);
        this.inventory.AddItemSlot(slot);
    }

    private void CreateItemSlot(BaseItemData itemData, int count)
    {
        slot = this.gameObject;

        slot.GetComponent<RectTransform>().SetParent(parent, false);
        var colors = slot.GetComponent<Selectable>().colors;
        colors.pressedColor = new UnityEngine.Color(23, 229, 225, 255) / 256f;
        slot.GetComponent<Selectable>().colors = colors;

        slot.GetComponent<Image>().sprite = itemData.Icon;


        //      Добавление текстовой информации об итеме.
        //
        var itemSlotText = new GameObject("ItemText", typeof(Text));
        itemSlotText.GetComponent<RectTransform>().SetParent(slot.transform);
        var text = itemSlotText.GetComponent<Text>();
        Font font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = font;
        text.text = $"{itemData.Title}: {count}";


        //      Добавления кнопки выбросить итем и ее настройка.
        var itemSlotDrop = new GameObject("ItemDrop", typeof(Image),typeof(Button));
        var rect = itemSlotDrop.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform, false);
        rect.anchorMin = new Vector2(1, 1);
        rect.anchorMax = new Vector2(1, 1);
        rect.offsetMin = new Vector2(-8, -8);
        rect.offsetMax = new Vector2(8, 8);
        itemSlotDrop.GetComponent<Image>().color = new UnityEngine.Color(255,0,0,140) / 256f;
        var buttonDrop = itemSlotDrop.GetComponent<Button>();
        buttonDrop.onClick.AddListener(RemoveItem);

        rect = itemSlotText.GetComponent<RectTransform>();
        rect.pivot = Vector2.up;
        rect.anchorMax = new Vector2(1, 0);
        rect.anchorMin = new Vector2(0, 0);
        rect.localPosition = Vector2.zero;
        rect.offsetMin = new Vector2(0, -30);
        rect.offsetMax = new Vector2(100, 0);
    }



}
