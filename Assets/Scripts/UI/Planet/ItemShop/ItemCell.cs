using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ItemCell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject buttonInfo;
    [SerializeField] private GameObject miniInfoWindow;
    [SerializeField] private GameObject textCount;

    private ItemShopView shopView;
    private ItemState itemState;
    private bool forBuy = false;

    public ItemState State => itemState;

    public ItemCell Init(ItemShopView shopView,ItemState item,bool forBuy)
    {
        this.shopView = shopView;
        this.itemState = item;
        this.forBuy = forBuy;

        this.icon.GetComponent<Image>().sprite = itemState.Data.Icon;
        this.buttonInfo.GetComponent<Button>().onClick.AddListener(OpenMiniInfo);

        return this;
    }

    public void OnPointerDown(PointerEventData data)
    {
        TryInteract();
    }
    protected virtual void DropItem()
    {
        Debug.Log("Выкинуть предмет из магазина нельзя.");
    }
    private void TryInteract()
    {
        if (itemState.Data.ItemKind != ItemKind.EmptyDevice && itemState.Data.ItemKind != ItemKind.EmptyGun)
        {
            if (forBuy)
            {
                CreateTradeWindow(true);
            }
            else if (forBuy == false)
            {
                CreateTradeWindow(false);
            }
        }
    }

    private void CreateTradeWindow(bool forBuy = true)
    {
        GameObject prefabTradeWindow;
        if(forBuy)
        {
             prefabTradeWindow = Managers.Resources.DownloadData(ObjectType.ConfirmBuying);
        }
        else
        {
            prefabTradeWindow = Managers.Resources.DownloadData(ObjectType.ConfirmSelling);
        }
            
        var tradeWindow = Instantiate(prefabTradeWindow, this.transform);
        var rect = tradeWindow.GetComponent<RectTransform>();
        rect.transform.SetParent(this.shopView.Shop.gameObject.transform);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.offsetMin = new Vector2(-96, -120);
        rect.offsetMax = new Vector2(96, 70);
        if(forBuy)
        {
            tradeWindow.GetComponent<BuyWindow>().Init(shopView, tradeWindow.transform, this);
        }
        else
        {
            tradeWindow.GetComponent<SellWindow>().Init(shopView, tradeWindow.transform, this);
        }
        
    }

    private void OpenMiniInfo()
    {
        Debug.Log("Open mini info");
        this.miniInfoWindow.SetActive(true);
    }

/*    protected virtual void TryInteract()
    {
        if (state.Data.ItemKind != ItemKind.EmptyDevice && state.Data.ItemKind != ItemKind.EmptyGun)
        {
            Managers.Player.Controller.PlayerState.Ship.TryInteractWithItem(this.state);
        }
    }*/
}
