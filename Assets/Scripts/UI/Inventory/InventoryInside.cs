using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInside : MonoBehaviour
{
    [SerializeField] private Image shipIcon;
    [SerializeField] private Text health;
    [SerializeField] private Transform gunsList;
    [SerializeField] private Transform devicesList;
    [SerializeField] private Transform itemsList;

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

        return this;
    }

    public void ShowInventory(IShipInventory inventory)
    {
        ShowGuns();
        ShowDevices();
        ShowItems();
    }

    private void ShowGuns()
    {
        //this.guns = playerController.State.Ship.Inventory.GetGuns();

        foreach(var gun in gunCells)
        {
            if (gun != null) Object.Destroy(gun.gameObject);
        }
        this.gunCells.Clear();
    }

    private void ShowDevices()
    {

    }

    private void ShowItems()
    {

    }
}
