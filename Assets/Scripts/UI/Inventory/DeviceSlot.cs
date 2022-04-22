using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Selectable))]
public class DeviceSlot : MonoBehaviour, IPointerDownHandler
{
    private GameObject slot;
    private Transform parent;
    private BaseItemState state;

    private IShipInventory inventory;

    public void Init(Transform transform, IShipInventory shipInventory, BaseItemState state)
    {
        this.parent = transform;
        this.inventory = shipInventory;
        this.state = state;

        CreateItemSlot();
        CanvasUI.Inventory.AddDeviceSlot(slot);
    }

    private void CreateItemSlot()
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
            //text.text = $"{state.Data.Title}: {state.Count}";
            text.text = "Устройство";

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

    [ContextMenu("Set Device")]
    private void SetDevice()
    {
        Debug.Log($"{this.state.IsWeapon}");
        if (this.state.IsWeapon)
        {
            var deviceState = (DeviceState)this.state;
            Managers.Player.Controller.PlayerState.Ship.SetDevice(deviceState);
        }

    }

    [ContextMenu("DropItem")]
    private void DropItem()
    {
        inventory.TryUnsetDevice((DeviceState)state);
        var item = new GameObject("Item" + state.Data.Title, typeof(ItemViewGame));
        item.GetComponent<ItemViewGame>().Init(((DeviceState)state).Data.ItemKind, 1);

        Destroy(this.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.inventory.TryUnsetDevice((DeviceState)this.state);
        if (this.state.Data.ItemKind != ItemKind.deviceEmpty)
        {
            Managers.Player.Controller.Inventory.AddItem(this.state.Data.ItemKind, this.state);
        }

    }
}