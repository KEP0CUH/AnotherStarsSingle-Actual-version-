using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    private PlayerController controller;

    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("Player manager starting...".SetColor(Color.Yellow));



        Status = ManagerStatus.Started;
        Debug.Log("Player manager started.".SetColor(Color.Green));
    }

    public void Init(PlayerController controller)
    {
        this.controller = controller;
    }


    public void ChangeGun(GunState gun)
    {
        controller.SetGun(gun);
    }
    public void ChangeGun(GunState gun,IInventory inventory)
    {
        controller.SetGun(gun,inventory);
    }

    public void AddItemInventory(GunState state)
    {
        controller.Inventory.AddItem(state.Data.ItemKind, state);
    }

    public void AddItemInventory(ItemKind kind,BaseItemState state)
    {
        controller.Inventory.AddItem(kind, state);
    }
    public void RemoveItemInventory(GunState state)
    {
        controller.Inventory.RemoveItem(state.Data.ItemKind);
    }
    public void RemoveItemInventory(ItemKind kind)
    {
        controller.Inventory.RemoveItem(kind);
    }


}
