using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SellWindow : MonoBehaviour
{
    ItemShop itemShop;
    private ItemShopView itemShopView; 
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
            ConfirmSelling();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            CancelSelling();
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
        buttonYes.GetComponent<Button>().onClick.AddListener(ConfirmSelling);

        buttonNo.GetComponent<Image>().color = new UnityEngine.Color(230, 59, 24, 255) / 256;
        buttonNo.GetComponent<Button>().onClick.AddListener(CancelSelling);

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
        catch (System.FormatException ex)
        {
            return;
        }
    }

    private void ConfirmSelling()
    {
        //itemShop.AddItem(this.itemSlot.ItemState);
        itemShopView.AddItem(this.itemSlot.ItemState,(int)sliderComponent.value);
        Managers.Player.Controller.Inventory.RemoveItem(this.itemSlot.ItemState,(int)sliderComponent.value);
        //itemShop.ShowItems();

        //itemShop.AddItem()
        Object.Destroy(this.gameObject);
        itemShopView.ShowListItemShop();
    }

    private void CancelSelling()
    {
        Object.Destroy(this.gameObject);
    }
}
