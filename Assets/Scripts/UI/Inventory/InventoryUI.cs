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
    private GameObject rightInventoryList;      // Правая часть инвентаря, отображение оборудования корабля.

    private GameObject rightMiddleInventory;        // Центр правой части инвентаря
    private GameObject rightMiddleInventoryList;    // Содержимое инвентаря правой части по центру.

    private GameObject rightDownInventory;      // Инвентарь, справа внизу.
    private GameObject rightDownInventoryList;  // Содержимое инвентаря, справа внизу.

    private List<GameObject> itemSlots = new List<GameObject>();
    private List<GameObject> gunSlots = new List<GameObject>();
    private List<GameObject> deviceSlots = new List<GameObject>();

    private Dictionary<ItemKind, BaseItemState> itemStates = new Dictionary<ItemKind, BaseItemState>();
    private List<GunState> gunStates = new List<GunState>();
    private List<DeviceState> deviceStates = new List<DeviceState>();

    private bool isEnabled = true;
    public bool IsEnabled => isEnabled;

    //          Ширина и высота UI элемента(в данном случае инвентаря).
    [SerializeField]
    private int width = 300;
    [SerializeField]
    private int height = 200;

    public ManagerStatus Status { get; private set; }
    public UIModuleKind Kind { get; private set; }

    public void Startup(ICanvas canvas)
    {
        Kind = UIModuleKind.Inventory;
        Status = ManagerStatus.Initializing;
        CreateInventory(canvas);
        CreateLeftInventory();
        CreateLeftInventoryList();
        CreateRightInventory();
        CreateRightInventoryList();
        OnValidate();
        Status = ManagerStatus.Started;
        Debug.Log("InventoryUI started.");
    }

    public void ShowInventory(IInventory inventory, Dictionary<ItemKind, BaseItemState> items)
    {
        foreach (var item in itemSlots)
        {
            if (item != null)
            {
                Destroy(item.gameObject);
            }
        }
        this.itemStates.Clear();
        itemSlots.Clear();


        foreach (var item in items)
        {
            this.itemStates[item.Key] = item.Value;
        }

        foreach (var item in this.itemStates)
        {
            CreateItemSlot(inventory, item.Value);
        }
    }

    public void ShowInventory(IShipInventory inventory, List<GunState> guns)
    {
        foreach (var gun in gunSlots)
        {
            if (gun != null)
            {
                Destroy(gun.gameObject);
            }
        }

        this.gunStates.Clear();
        gunSlots.Clear();


        foreach (var gun in guns)
        {
            this.gunStates.Add(gun);
        }

        foreach (var gun in this.gunStates)
        {
            CreateGunSlot(inventory, gun);
        }
    }

    public void ShowInventory(IShipInventory inventory, List<DeviceState> devices)
    {
        foreach (var device in devices)
        {
            if (device != null)
            {
                Destroy(device.gameObject);
            }
        }

        this.deviceStates.Clear();
        deviceSlots.Clear();

        foreach (var device in devices)
        {
            this.deviceStates.Add(device);
        }

        foreach (var device in devices)
        {
            CreateDeviceSlot(inventory, device);
        }

    }

    [ContextMenu("Enable")]
    public void Enable()
    {
        inventory.SetActive(true);
        isEnabled = true;
    }
    [ContextMenu("Disable")]
    public void Disable()
    {
        inventory.SetActive(false);
        isEnabled = false;
    }


    private void CreateInventory(ICanvas canvas)
    {
        inventory = new GameObject("Inventory", typeof(RectTransform));
        canvas.AddModule(inventory);
        inventory.AddComponent<Image>();
        var image = inventory.GetComponent<Image>();
        image.color = new UnityEngine.Color(134, 183, 219, 90) / 256f;
        image.raycastTarget = false;

        var rect = inventory.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(1, 1);

        rect.offsetMin = new Vector2(-width, -height);
        rect.offsetMax = new Vector2(width, height);

        //inventory.AddComponent<VerticalLayoutGroup>();
        Status = ManagerStatus.Started;
        Debug.Log("InventoryUI started.");
    }

    private void CreateLeftInventory()
    {
        leftInventory = new GameObject("LeftPanel", typeof(Image), typeof(LayoutElement), typeof(ScrollRect), typeof(Mask));
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
        rightInventory = new GameObject("RightPanel", typeof(Image), typeof(LayoutElement), typeof(ScrollRect), typeof(Mask));
        rightInventory.transform.parent = inventory.transform;

        rightInventory.GetComponent<LayoutElement>().ignoreLayout = true;
        var rect = rightInventory.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(1, 0.5f);
        rect.anchorMax = new Vector2(1, 0.5f);
        rect.pivot = new Vector2(1, 0.5f);
        rect.position = Vector2.zero;

        var scroll = rightInventory.GetComponent<ScrollRect>();
        scroll.inertia = false;
        scroll.scrollSensitivity = 10;
        scroll.horizontal = false;

        rightInventory.GetComponent<Image>().color = new UnityEngine.Color(9, 65, 219, 90) / 256f;
        Debug.Log("RightInventory created.");


        CreateRightDownInventory(rightInventory.transform);
        CreateRightMiddleInventory(rightInventory.transform);

    }

    private void CreateRightMiddleInventory(Transform parent)
    {
        rightMiddleInventory = new GameObject("RightMiddlePanel",typeof(Image),typeof(LayoutElement),typeof(ScrollRect), typeof(Mask));
        rightMiddleInventory.transform.parent = parent.transform;

        rightMiddleInventory.GetComponent<LayoutElement>().ignoreLayout = true;
        var rect = rightMiddleInventory.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 0);
        rect.pivot = new Vector2(1, 0.5f);
        rect.position = Vector2.zero;

        rect.offsetMin = new Vector2(10, 10 + 0.66f * height);
        rect.offsetMax = new Vector2(-10, 10 + 2 * 0.66f * height);

        var scroll = rightMiddleInventory.GetComponent<ScrollRect>();
        scroll.inertia = false;
        scroll.scrollSensitivity = 10;
        scroll.horizontal = false;

        rightMiddleInventory.GetComponent<Image>().color = new UnityEngine.Color(9, 65, 219, 90) / 256f;
        Debug.Log("RightMiddleInventory created.");

        CreateRightMiddleInventoryList(rightMiddleInventory.transform);
    }

    private void CreateRightDownInventory(Transform parent)
    {
        rightDownInventory = new GameObject("RightDownPanel", typeof(Image), typeof(LayoutElement), typeof(ScrollRect), typeof(Mask));
        rightDownInventory.transform.parent = parent.transform;

        rightDownInventory.GetComponent<LayoutElement>().ignoreLayout = true;
        var rect = rightDownInventory.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 0);
        rect.pivot = new Vector2(1, 0.5f);
        rect.position = Vector2.zero;

        rect.offsetMin = new Vector2(10, 10);
        rect.offsetMax = new Vector2(-10, 0.66f * height);

        var scroll = rightDownInventory.GetComponent<ScrollRect>();
        scroll.inertia = false;
        scroll.scrollSensitivity = 10;
        scroll.horizontal = false;

        rightDownInventory.GetComponent<Image>().color = new UnityEngine.Color(9, 65, 219, 90) / 256f;
        Debug.Log("RightDownInventory created.");

        CreateRightDownInventoryList(rightDownInventory.transform);
    }

    private void CreateRightMiddleInventoryList(Transform rightMiddle)
    {
        rightMiddleInventoryList = new GameObject("Devices",typeof(RectTransform),typeof(GridLayoutGroup), typeof(ContentSizeFitter));

        var rect = rightMiddleInventoryList.GetComponent<RectTransform>();
        rect.SetParent(rightMiddle.transform);
        rect.anchorMin = new Vector2(0, 0.5f);
        rect.anchorMax = new Vector2(1, 0.5f);
        rect.pivot = new Vector2(1, 1);
         rect.position = Vector2.zero;
         rect.offsetMin = new Vector2(0, 0);
         rect.offsetMax = new Vector2(1, 1);

         var layout = rightMiddleInventoryList.GetComponent<GridLayoutGroup>();
         layout.cellSize = new Vector2(64, 64);
         layout.spacing = new Vector2(15, 15);
         layout.padding.top = 15;
         layout.padding.left = 15;

         var fitter = rightMiddleInventoryList.GetComponent<ContentSizeFitter>();
         fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

         rightMiddleInventory.GetComponent<ScrollRect>().content = rightMiddleInventoryList.GetComponent<RectTransform>();
    }


    private void CreateRightDownInventoryList(Transform rightDown)
    {
        rightDownInventoryList = new GameObject("Items",typeof(ScrollRect), typeof(GridLayoutGroup), typeof(ContentSizeFitter));
        rightDownInventoryList.transform.parent = rightDown.transform;

        var rect = rightDownInventoryList.GetComponent<RectTransform>();
        rect.SetParent(rightDown.transform);
        rect.anchorMin = new Vector2(0, 0.5f);
        rect.anchorMax = new Vector2(1, 0.5f);
        rect.pivot = new Vector2(1, 1);
        rect.position = Vector2.zero;
        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(1, 1);

        var layout = rightDownInventoryList.GetComponent<GridLayoutGroup>();
        layout.cellSize = new Vector2(64, 64);
        layout.spacing = new Vector2(15, 15);
        layout.padding.top = 15;
        layout.padding.left = 15;

        var fitter = rightDownInventoryList.GetComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;


        rightDownInventory.GetComponent<ScrollRect>().content = rightDownInventoryList.GetComponent<RectTransform>();
    }


    private void CreateLeftInventoryList()
    {
        leftInventoryList = new GameObject("Items", typeof(GridLayoutGroup), typeof(ContentSizeFitter));
        leftInventoryList.transform.parent = leftInventory.transform;

        var rect = leftInventoryList.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(1, 1);
        rect.pivot = new Vector2(1, 1);

        var layout = leftInventoryList.GetComponent<GridLayoutGroup>();
        layout.cellSize = new Vector2(64, 64);
        layout.spacing = new Vector2(15, 15);
        layout.padding.top = 15;
        layout.padding.left = 15;

        var fitter = leftInventoryList.GetComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;


        leftInventory.GetComponent<ScrollRect>().content = leftInventoryList.GetComponent<RectTransform>();
    }

    private void CreateRightInventoryList()
    {
        rightInventoryList = new GameObject("Items", typeof(GridLayoutGroup), typeof(ContentSizeFitter));
        rightInventoryList.transform.parent = rightInventory.transform;

        var rect = rightInventoryList.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(1, 1);
        rect.pivot = new Vector2(1, 1);
        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);

        var layout = rightInventoryList.GetComponent<GridLayoutGroup>();
        layout.cellSize = new Vector2(64, 64);
        layout.spacing = new Vector2(15, 15);
        layout.padding.top = 15;
        layout.padding.left = 15;

        var fitter = rightInventoryList.GetComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;


        rightInventory.GetComponent<ScrollRect>().content = rightInventoryList.GetComponent<RectTransform>();
    }


    [ContextMenu("CreateItemSlot")]
    private void CreateItemSlot(IInventory inventory, BaseItemState state)
    {
        var itemSlot = new GameObject("Item" + state.Data.Title, typeof(Image), typeof(Selectable), typeof(ItemSlot));
        itemSlot.GetComponent<ItemSlot>().Init(rightDownInventoryList.transform, inventory, state);
        itemSlots.Add(itemSlot);
    }

    private void CreateGunSlot(IShipInventory inventory, BaseItemState state)
    {
        var gunSlot = new GameObject("Gun" + state.Data.Title, typeof(Image), typeof(Selectable), typeof(GunSlot));
        gunSlot.GetComponent<GunSlot>().Init(rightInventoryList.transform, inventory, state);
        gunSlots.Add(gunSlot);
    }

    private void CreateDeviceSlot(IShipInventory inventory, BaseItemState state)
    {
        var deviceSlot = new GameObject("Device" + state.Data.Title, typeof(Image), typeof(Selectable), typeof(DeviceSlot));
        deviceSlot.GetComponent<DeviceSlot>().Init(rightMiddleInventoryList.transform, inventory, state);
        deviceSlots.Add(deviceSlot);
    }

    private void OnValidate()
    {
        if (inventory != null)
        {
            inventory.GetComponent<RectTransform>().offsetMin = new Vector2(-width, -height);
            inventory.GetComponent<RectTransform>().offsetMax = new Vector2(width, height);
        }

        if (leftInventory != null)
        {
            leftInventory.GetComponent<RectTransform>().offsetMin = new Vector2(10, -height + 10);
            leftInventory.GetComponent<RectTransform>().offsetMax = new Vector2((int)(0.64f * width), height - 10);
        }

        if (rightInventory != null)
        {
            rightInventory.GetComponent<RectTransform>().offsetMin = new Vector2(-(int)(1.30f * width), -height + 10);
            rightInventory.GetComponent<RectTransform>().offsetMax = new Vector2(-10, height - 10);
        }

        if (leftInventoryList != null)
        {
            leftInventoryList.GetComponent<RectTransform>().offsetMin = Vector2.up;
            leftInventoryList.GetComponent<RectTransform>().offsetMax = Vector2.one;
        }
    }

    public void AddItemSlot(GameObject slot)
    {
        itemSlots.Add(slot);
    }

    public void AddGunSlot(GameObject slot)
    {
        gunSlots.Add(slot);
    }

    public void AddDeviceSlot(GameObject slot)
    {
        deviceSlots.Add(slot);
    }
}
