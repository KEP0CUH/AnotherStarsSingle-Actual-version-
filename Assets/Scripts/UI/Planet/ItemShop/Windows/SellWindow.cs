///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SellWindow : MonoBehaviour
{
    private             ItemShop                itemShop;
    private             ItemShopView            itemShopView; 
    private             ItemCell                itemCell;

    [SerializeField] 
    private             GameObject              itemIcon;
    [SerializeField] 
    private             GameObject              inputField;
    [SerializeField] 
    private             GameObject              inputFieldComponentText;
    [SerializeField] 
    private             GameObject              slider;
    private             Slider                  sliderComponent;
    [SerializeField] 
    private             GameObject              buttonYes;
    [SerializeField] 
    private             GameObject              buttonNo;

    public              void                    Init(ItemShopView           itemShopView,
                                                     Transform              parent,
                                                     ItemCell               itemCell)
    {
        this.itemShopView = itemShopView;
        this.GetComponent<RectTransform>().SetParent(parent);
        this.itemCell = itemCell;
        SettingElements(itemCell.State);
    }

    private             void                    Update()
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

    private             void                    SettingElements(ItemState   state)
    {
        itemIcon.GetComponent<Image>().sprite = state.Data.Icon;

        sliderComponent = slider.GetComponent<Slider>();
        sliderComponent.minValue = 1;
        sliderComponent.maxValue = state.Count;
        sliderComponent.wholeNumbers = true;
        sliderComponent.value = 1;
        sliderComponent.onValueChanged.AddListener((content) => UpdateTextField(content));

        inputFieldComponentText.GetComponent<Text>().text = sliderComponent.value.ToString();
        inputField.GetComponent<InputField>().onValueChanged.AddListener((content) => UpdateSlider(content));

        buttonYes.GetComponent<Button>().onClick.AddListener(ConfirmSelling);
        buttonNo.GetComponent<Button>().onClick.AddListener(CancelSelling);
    }
    private             void                    UpdateTextField(float       a)
    {
        inputField.GetComponent<InputField>().text = a.ToString();
    }

    private             void                    UpdateSlider(string         content)
    {
        try
        {
            slider.GetComponent<Slider>().value = int.Parse(content);
        }
        catch (System.FormatException ex)
        {
            Debug.Log($"{ex.ToString().SetColor(Color.Red)}");
        }
    }

    private             void                    ConfirmSelling()
    {
        itemShopView.AddItem(this.itemCell.State, (int)sliderComponent.value);
        Managers.Player.Controller.Inventory.RemoveItem(this.itemCell.State, (int)sliderComponent.value);
        Managers.Player.Controller.ShowInventory();
        Object.Destroy(this.gameObject);
        itemShopView.ShowListItemShop();
    }

    private             void                    CancelSelling()
    {
        Object.Destroy(this.gameObject);
    }
}
