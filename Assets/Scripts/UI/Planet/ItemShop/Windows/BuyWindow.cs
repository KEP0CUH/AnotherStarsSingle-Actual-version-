using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class BuyWindow : MonoBehaviour
{
    private ItemShopView itemShopView;
    ItemShop itemShop;
    ItemSlotShop itemSlot;

    [SerializeField] private GameObject itemIcon;
    [SerializeField] private GameObject inputField;
    [SerializeField] private GameObject inputFieldComponentText;
    [SerializeField] private GameObject slider;
    private Slider sliderComponent;
    [SerializeField] private GameObject buttonYes;
    [SerializeField] private GameObject buttonNo;
    public void Init(ItemShopView itemShopView,ItemShop shop, Transform parent, ItemSlotShop itemSlot)
    {
        this.itemShopView = itemShopView;
        this.itemShop = shop;
        this.GetComponent<RectTransform>().SetParent(parent);
        this.itemSlot = itemSlot;
        SettingElements();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            ConfirmBuying();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            CancelBuying();
        }
    }

    private void SettingElements()
    {
        itemIcon.GetComponent<Image>().sprite = this.itemSlot.ItemState.Data.Icon;

        sliderComponent = slider.GetComponent<Slider>();
        sliderComponent.minValue = 1;
        sliderComponent.maxValue = this.itemSlot.ItemState.Count;
        sliderComponent.wholeNumbers = true;
        sliderComponent.value = 1;
        sliderComponent.onValueChanged.AddListener((content) => UpdateTextField(content));

        inputFieldComponentText.GetComponent<Text>().text = sliderComponent.value.ToString();
        inputField.GetComponent<InputField>().onValueChanged.AddListener((content) => UpdateSlider(content));

        buttonYes.GetComponent<Image>().color = new UnityEngine.Color(24, 171, 64, 255) / 256;
        buttonYes.GetComponent<Button>().onClick.AddListener(ConfirmBuying);

        buttonNo.GetComponent<Image>().color = new UnityEngine.Color(230, 59, 24, 255) / 256;
        buttonNo.GetComponent<Button>().onClick.AddListener(CancelBuying);

    }

    private void UpdateTextField(float a)
    {
        Debug.Log("UpdateTextField worked");
        inputField.GetComponent<InputField>().text = a.ToString();
    }

    private void UpdateSlider(string content)
    {
        Debug.Log("UpdateTextField");
        try
        {
            slider.GetComponent<Slider>().value = int.Parse(content);
        }
        catch(System.FormatException ex)
        {
            return;
        }
    }

    private void ConfirmBuying()
    {
/*        itemShop.RemoveItemState(this.itemSlot.ItemState);
        Managers.Player.Controller.Inventory.AddItem(this.itemSlot.ItemState);
        itemShop.ShowItems();
        Object.Destroy(this.gameObject);*/


        Managers.Player.Controller.Inventory.AddItem(this.itemSlot.ItemState,(int)sliderComponent.value ,false);
        itemShopView.RemoveItem(this.itemSlot.ItemState, itemSlot.ItemState.Count,false);
        Object.Destroy(this.gameObject);
        itemShopView.ShowListItemShop();
    }

    private void CancelBuying()
    {
        Object.Destroy(this.gameObject);
    }
}
