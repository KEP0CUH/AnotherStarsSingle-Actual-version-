using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    private PlayerController controller;
    private PlayerState playerState;

    private Transform landPlace;
    private bool isLanded;
    public bool IsLanded => isLanded;

    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("Player manager starting...".SetColor(Color.Yellow));



        Status = ManagerStatus.Started;
        Debug.Log("Player manager started.".SetColor(Color.Green));
    }

    public void Init(PlayerController controller, PlayerState state)
    {
        this.controller = controller;
        this.playerState = state;
    }


    public void ChangeGun(GunState gun)
    {
        this.playerState.ChangeGun(gun);
    }
    public void ChangeGun(GunState gun,IInventory inventory)
    {
        this.playerState.ChangeGun(gun, inventory);
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


    public void Land(Transform transform)
    {
        this.landPlace = transform;
        controller.gameObject.transform.position = new Vector3(transform.position.x + 150, transform.position.y + 150, 0);
        controller.UpdateCameraPosition();
        isLanded = true;
    }

    public void Rise()
    {
        controller.gameObject.transform.position = new Vector2(this.landPlace.position.x,this.landPlace.position.y);
        controller.UpdateCameraPosition();
        isLanded = false;
    }
}
