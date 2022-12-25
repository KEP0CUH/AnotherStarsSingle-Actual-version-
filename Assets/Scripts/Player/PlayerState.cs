///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class PlayerState
{
    private             PlayerData                      data;
    private             ShipController                  shipController;
    private             PlayerController                playerController;

    private             float                           maxHealth;
    private             float                           health;

    public              float                           Health => health;
    public              PlayerData                      Data => data;
    public              ShipController                  ShipController => shipController;
    public              PlayerController                PlayerController => playerController;

    public              PlayerState(PlayerController    playerController, ShipController    shipController)
    {
        this.playerController = playerController;
        this.shipController = shipController;
    }

    public              void                            ChangeHealth(int         value)
    {
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }

        Debug.Log($"CurrentHealth: {health} / {maxHealth}");
    }

    public              void                            SetShip     (ShipKind    kind)
    {
        if(this.ShipController.State.Inventory != null)
        {
            Debug.Log("Удаление оборудования с корабля...");
            this.ShipController.State.Inventory.RemoveAllEquipmentFromShip();
            this.shipController.State.Inventory.OnInteractWithEquipment -= this.PlayerController.InventoryInside.ShowInventory;
            Debug.Log("Оборудование удалено...");
        }
        Debug.Log("Начата смена корабля...");
        this.shipController = this.shipController.Init(kind,playerController.Inventory);
        this.shipController.State.Inventory.OnInteractWithEquipment += this.PlayerController.InventoryInside.ShowInventory;
        Managers.Player.Controller.UpdateState();
    }
}
