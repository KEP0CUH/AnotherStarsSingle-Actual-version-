///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] 
    private                 Image                   icon;
    [SerializeField] 
    private                 GameObject              infoPanel;
    [SerializeField] 
    private                 Text                    textTitle;
    [SerializeField] 
    private                 Text                    textSize;
    [SerializeField] 
    private                 Button                  buttonInfo;
    [SerializeField] 
    private                 Button                  buttonDrop;

    private                 ItemState               state;
    public                  ItemUI                  Init(Transform      parent,
                                                     ItemState      state)
    {
        this.state = state;

        this.gameObject.GetComponent<RectTransform>().SetParent(parent);

        this.icon.sprite = state.Data.Icon;
        this.textTitle.text = $"<color=white>Название</color> <color=orange>{state.Data.Title}</color>";
        this.textSize.text = $"<color=white>Размер</color>     <color=orange>{state.Data.ItemKind}</color>";
        buttonInfo.onClick.AddListener(ShowInfo);
        buttonDrop.onClick.AddListener(DropItem);

        return this;
    }

    protected virtual       void                    ShowInfo()
    {
        infoPanel.SetActive(!infoPanel.activeInHierarchy);
    }

    protected virtual       void                    DropItem()
    {
        Managers.Player.Controller.ShipController.State.Inventory.TryDropItemFromShip(this.state);
    }

    public                  void                    OnPointerDown(PointerEventData data)
    {
        this.TryInteract();
    }

    protected virtual       void                    TryInteract()
    {
        if(state.IsEmpty() == false)
        {
            Managers.Player.TryInteractWithEquipment(this.state);
        }
    }
}
