using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Selectable))]
public class ItemSlot : MonoBehaviour, IPointerDownHandler
{
    protected GameObject slot;
    protected Transform parent;
    protected ItemState state;

    private IPlayerInventory playerInventory;

    public void Init(Transform transform, IPlayerInventory inventory, ItemState state)
    {
        this.playerInventory = inventory;
        this.parent = transform;
        this.state = state;

        CreateItemSlot();
        CanvasUI.Inventory.AddItemSlot(slot);
    }

    protected virtual void CreateItemSlot()
    {
        if (state != null)
        {
            slot = this.gameObject;

            slot.GetComponent<RectTransform>().SetParent(parent, false);
            var colors = slot.GetComponent<Selectable>().colors;
            colors.pressedColor = new UnityEngine.Color(23, 229, 225, 255) / 256f;
            slot.GetComponent<Selectable>().colors = colors;

            slot.GetComponent<Image>().sprite = state.Data.Icon;


            //      Добавление текстовой информации об итеме.
            //
            var itemSlotText = new GameObject("ItemText", typeof(Text));
            itemSlotText.GetComponent<RectTransform>().SetParent(slot.transform);
            var text = itemSlotText.GetComponent<Text>();
            Font font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            text.font = font;
            text.text = $"{state.Data.Title}: {state.Count}";


            //      Добавления кнопки выбросить итем и ее настройка.
            var itemSlotDrop = new GameObject("ItemDrop", typeof(Image), typeof(Button));
            var rect = itemSlotDrop.GetComponent<RectTransform>();
            rect.SetParent(this.gameObject.transform, false);
            rect.anchorMin = new Vector2(1, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.offsetMin = new Vector2(-8, -8);
            rect.offsetMax = new Vector2(8, 8);
            itemSlotDrop.GetComponent<Image>().color = new UnityEngine.Color(255, 0, 0, 140) / 256f;
            var buttonDrop = itemSlotDrop.GetComponent<Button>();
            buttonDrop.onClick.AddListener(DropItem);

            rect = itemSlotText.GetComponent<RectTransform>();
            rect.pivot = Vector2.up;
            rect.anchorMax = new Vector2(1, 0);
            rect.anchorMin = new Vector2(0, 0);
            rect.localPosition = Vector2.zero;
            rect.offsetMin = new Vector2(0, -30);
            rect.offsetMax = new Vector2(100, 0);
        }
    }



    [ContextMenu("DropItem")]
    private void DropItem()
    {
        playerInventory.RemoveItem(state);
        var item = new GameObject("Item" + state.Data.Title, typeof(ItemViewGame));
        if (state.IsWeapon)
        {
            item.GetComponent<ItemViewGame>().Init(((GunState)state).Data.ItemKind, 1);
        }
        else if (state.IsDevice)
        {
            item.GetComponent<ItemViewGame>().Init(((DeviceState)state).Data.ItemKind, 1);
        }
        else
        {
            item.GetComponent<ItemViewGame>().Init(state.Data.ItemKind, 1);
        }

        //Destroy(this.gameObject);

    }
    public void OnPointerDown(PointerEventData data)
    {
        TryInteract();
    }

    protected void TryInteract()
    {
        Managers.Player.Controller.PlayerState.Ship.TryInteractWithItem(this.state);
    }
}
