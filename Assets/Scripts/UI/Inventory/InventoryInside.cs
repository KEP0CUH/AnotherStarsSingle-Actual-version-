using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInside : MonoBehaviour
{
    [SerializeField] private Image shipIcon;
    [SerializeField] private Text shipTitle;
    [SerializeField] private Text health;
    [SerializeField] private Transform gunsList;
    [SerializeField] private Transform devicesList;
    [SerializeField] private Transform itemsList;
    [SerializeField] private Button buttonClose;

    private List<GameObject> gunCells = new List<GameObject>();
    private List<GameObject> deviceCells = new List<GameObject>();
    private List<GameObject> itemCells = new List<GameObject>();
    private Dictionary<int, GunState> guns = new Dictionary<int, GunState>();
    private Dictionary<int, DeviceState> devices = new Dictionary<int, DeviceState>();
    private Dictionary<int, ItemState> items = new Dictionary<int, ItemState>();

    private PlayerController playerController;

    public PlayerController Player => playerController;

    public InventoryInside Init(PlayerController controller)
    {
        this.playerController = controller;
        gunCells = new List<GameObject>();
        deviceCells = new List<GameObject>();
        itemCells = new List<GameObject>();

        this.shipIcon.sprite = playerController.ShipController.State.Data.Icon;
        this.shipTitle.text = playerController.ShipController.State.Data.Title;
        this.buttonClose.onClick.AddListener(CloseInventory);
        this.playerController.ShipController.State.Inventory.OnInteractWithEquipment += ShowInventory;

        ShowInventory();
        return this;
    }

    public void ShowInventory()
    {
        ShowGuns();
        ShowDevices();
        ShowItems();
    }

    public void Reswitch()
    {
        if (this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
            ShowInventory();
        }
        else this.gameObject.SetActive(true);
    }

    private void ShowGuns()
    {
        foreach(var gun in gunCells)
        {
            if (gun != null) Object.Destroy(gun.gameObject);
        }
        this.gunCells.Clear();
        this.guns.Clear();

        foreach(var gun in this.playerController.ShipController.State.Inventory.GetGuns())
        {
            this.guns.Add(gun.Id, gun);
        }

        foreach(var gun in this.guns)
        {
            CreateCell(gun.Value);
        }
    }

    private void ShowDevices()
    {
        foreach(var device in deviceCells)
        {
            if (device != null) Object.Destroy(device.gameObject);
        }
        this.deviceCells.Clear();
        this.devices.Clear();

        foreach(var device in this.playerController.ShipController.State.Inventory.GetDevices())
        {
            this.devices.Add(device.Id, device);
        }

        foreach(var device in devices)
        {
            CreateCell(device.Value);
        }
    }

    private void ShowItems()
    {
        foreach(var item in itemCells)
        {
            if(item != null) Object.Destroy(item.gameObject);
        }
        this.itemCells.Clear();
        this.items.Clear();

        foreach(var item in this.playerController.Inventory.GetItems())
        {
            this.items.Add(item.Key, item.Value);
        }

        foreach(var item in items)
        {
            CreateCell(item.Value);
        }
    }

    private void CreateCell(ItemState state)
    {
        var itemUI = Instantiate(Managers.Resources.DownloadData(ObjectType.ItemUI));
        if (state.IsItem || state.IsSet == false)
        {
            itemUI.GetComponent<ItemUI>().Init(itemsList.transform, state);
            itemCells.Add(itemUI);
        }
        else if(state.IsWeapon && state.IsSet)
        {
            itemUI.GetComponent<ItemUI>().Init(gunsList.transform, state);
            gunCells.Add(itemUI);
        }
        else if(state.IsDevice && state.IsSet)
        {
            itemUI.GetComponent<ItemUI>().Init(devicesList.transform, state);
            deviceCells.Add(itemUI);
        }
    }

    private void CloseInventory()
    {
        this.gameObject.SetActive(false);
    }
}
