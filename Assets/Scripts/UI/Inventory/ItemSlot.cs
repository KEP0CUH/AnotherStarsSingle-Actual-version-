using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Selectable))]
public class ItemSlot : MonoBehaviour, IPointerDownHandler
{
    protected GameObject slot;
    protected Transform parent;
    protected ItemState state;

    private IPlayerInventory playerInventory;

    public void Init(Transform parent, IPlayerInventory inventory, ItemState state)
    {
        this.playerInventory = inventory;
        this.parent = parent;
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
    protected virtual void DropItem()
    {
        Managers.Player.Controller.PlayerState.Ship.Inventory.TryDropItemFromShip(this.state);
    }
    public void OnPointerDown(PointerEventData data)
    {
        TryInteract();
    }

    protected virtual void TryInteract()
    {
        if (state.Data.ItemKind != ItemKind.EmptyDevice && state.Data.ItemKind != ItemKind.EmptyGun)
        {
            Managers.Player.Controller.PlayerState.Ship.TryInteractWithItem(this.state);
        }
    }
}
