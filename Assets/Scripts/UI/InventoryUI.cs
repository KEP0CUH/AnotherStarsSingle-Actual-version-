using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IUIModule, IInventoryUI
{
    private GameObject inventory;               // Окно инвентаря.
    private GameObject leftInventory;           // Левая часть инвентаря
    private GameObject leftInventoryList;       // Левая часть инвентаря, отображение списка предметов.
    private GameObject rightInventory;          // Правая часть инвентаря

    private List<GameObject> itemSlots = new List<GameObject>();

    private Dictionary<BaseItemData, int> itemData = new Dictionary<BaseItemData, int>();


    //          Ширина и высота UI элемента(в данном случае инвентаря).
    [SerializeField]
    private int width = 300;
    [SerializeField]
    private int height = 200;

    public ManagerStatus Status { get;private set; }
    public UIModuleKind Kind { get;private set; }

    public void Startup(ICanvas canvas)
    {
        Kind = UIModuleKind.Inventory;
        Status = ManagerStatus.Initializing;
        Debug.Log("InventoryUI initializing...");
        CreateInventory(canvas);
        CreateLeftInventory();
        CreateLeftInventoryList();
        CreateRightInventory();
        ShowInventory(Managers.Inventory.GetItemList());
        OnValidate();
    }

    public void ShowInventory(Dictionary<BaseItemData, int> items)
    {
        foreach(var item in itemSlots)
        {
            if(item != null)
            {
                Destroy(item.gameObject);
            }
        }
        this.itemData.Clear();
        itemSlots.Clear();


        foreach(var item in items)
        {
            if(this.itemData.ContainsKey(item.Key))
            {
                this.itemData[item.Key] += 1;
                continue;
            }
            this.itemData.Add(item.Key, item.Value);
        }

        foreach(var item in this.itemData)
        {
            CreateItemSlot(item.Key, item.Value);
        }
    }

    [ContextMenu("Enable")]
    public void Enable()
    {
        inventory.SetActive(true);
    }
    [ContextMenu("Disable")]
    public void Disable()
    {
        inventory.SetActive(false);
    }


    private void CreateInventory(ICanvas canvas)
    {
        inventory = new GameObject("Inventory",typeof(RectTransform));
        canvas.AddModule(inventory);
        inventory.AddComponent<Image>();
        inventory.GetComponent<Image>().color = new UnityEngine.Color(134,183,219,90) / 256f;
        inventory.GetComponent<Image>().Rebuild(CanvasUpdate.PreRender);
        inventory.GetComponent<Image>().raycastTarget = false;

        inventory.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        inventory.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        inventory.GetComponent<RectTransform>().pivot = new Vector2(1, 1);

        inventory.GetComponent<RectTransform>().offsetMin = new Vector2(-50, -50);
        inventory.GetComponent<RectTransform>().offsetMax = new Vector2(40, 32);

        //inventory.AddComponent<VerticalLayoutGroup>();
        Status = ManagerStatus.Started;
        Debug.Log("InventoryUI started.");
    }

    private void CreateLeftInventory()
    {
        leftInventory = new GameObject("LeftPanel", typeof(Image), typeof(LayoutElement),typeof(ScrollRect),typeof(Mask));
        leftInventory.transform.parent = inventory.transform;

        leftInventory.GetComponent<LayoutElement>().ignoreLayout = true;
        var rect = leftInventory.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0.5f);
        rect.anchorMax = new Vector2(0, 0.5f);
        rect.pivot = new Vector2(0, 0.5f);
        rect.position = Vector2.zero;

        var scroll = leftInventory.GetComponent<ScrollRect>();
        scroll.inertia = false;
        scroll.scrollSensitivity = 10;
        scroll.horizontal = false;

        leftInventory.GetComponent<Image>().color = new UnityEngine.Color(9, 65, 219, 90) / 256f;
        Debug.Log("LeftInventory created.");
    }

    private void CreateRightInventory()
    {
        rightInventory = new GameObject("RightPanel", typeof(Image), typeof(LayoutElement));
        rightInventory.transform.parent = inventory.transform;

        rightInventory.GetComponent<LayoutElement>().ignoreLayout = true;
        var rect = rightInventory.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(1, 0.5f);
        rect.anchorMax = new Vector2(1, 0.5f);
        rect.pivot = new Vector2(1, 0.5f);
        rect.position = Vector2.zero;

        rightInventory.GetComponent<Image>().color = new UnityEngine.Color(9, 65, 219, 90) / 256f;
        Debug.Log("RightInventory created.");
    }


    private void CreateLeftInventoryList()
    {
        leftInventoryList = new GameObject("Items", typeof(GridLayoutGroup),typeof(ContentSizeFitter));
        leftInventoryList.transform.parent = leftInventory.transform;

        var rect = leftInventoryList.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(1, 1);
        rect.pivot = Vector2.one;
        //rect.offsetMin = (

        var layout = leftInventoryList.GetComponent<GridLayoutGroup>();
        layout.cellSize = new Vector2(64, 64);
        layout.spacing = new Vector2(15, 15);
        layout.padding.top = 15;
        layout.padding.left = 15;
        
        var fitter = leftInventoryList.GetComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;


        leftInventory.GetComponent<ScrollRect>().content = leftInventoryList.GetComponent<RectTransform>();
    }


    [ContextMenu("CreateItemSlot")]
    private void CreateItemSlot(BaseItemData itemData, int count)
    {
        var itemSlot = new GameObject("Item" + itemData.Title, typeof(Image), typeof(Selectable),typeof(ItemSlot));
        itemSlot.GetComponent<ItemSlot>().Init(leftInventoryList.transform,this, itemData, count);
    }

    private void OnValidate()
    {
        if(inventory != null)
        {
            inventory.GetComponent<RectTransform>().offsetMin = new Vector2(-width, -height);
            inventory.GetComponent<RectTransform>().offsetMax = new Vector2(width, height);
        }

        if(leftInventory != null)
        {
            leftInventory.GetComponent<RectTransform>().offsetMin = new Vector2(10, -height + 10);
            leftInventory.GetComponent<RectTransform>().offsetMax = new Vector2((int)(0.64f * width), height - 10);
        }
        
        if(rightInventory != null)
        {
            rightInventory.GetComponent<RectTransform>().offsetMin = new Vector2(-(int)(1.30f * width), -height + 10);
            rightInventory.GetComponent<RectTransform>().offsetMax = new Vector2(-10, height - 10);
        }

        if(leftInventoryList != null)
        {
            leftInventoryList.GetComponent<RectTransform>().offsetMin = Vector2.up;
            leftInventoryList.GetComponent<RectTransform>().offsetMax = Vector2.one;
        }
    }

    public void AddItemSlot(GameObject slot)
    {
        itemSlots.Add(slot);
    }
}
