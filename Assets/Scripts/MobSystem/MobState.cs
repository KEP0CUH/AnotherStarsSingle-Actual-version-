using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ShipState))]
public class MobState : MonoBehaviour
{
    private static int ID = 1;

    private int id;
    private MobData data;
    private ShipState shipState;

    private MobController mobController;

    [Range(0, 10000)] 
        private float health;
    [Range(0, 10000)]
        private float maxHealth;


    public int Id => id;
    public MobData Data => data;
    public ShipState ShipState => shipState;

    public float Health => health;
    public float MaxHealth => maxHealth;

    public MobState Init(MobController controller,MobKind kind,InventoryController inventory)
    {
        this.mobController = controller;
        this.data = Managers.Resources.DownloadData(kind);
        this.shipState = this.gameObject.GetComponent<ShipState>().Init(data.Ship,inventory);
        id = GetId();

        switch(kind)
        {
            case MobKind.PirateIndus1:
                maxHealth = 350;
                break;
            case MobKind.PirateFrigate1:
                maxHealth = 1200;
                break;
            case MobKind.PirateIstrebitel1:
                maxHealth = 700;
                break;
        }
        this.health = maxHealth;

        return this;
    }

    public void ChangeHealth(int value)
    {
        this.health += value;
        if(this.health < 0)
        {
            this.health = 0;
            mobController.KillMob(this.id);
        }
        else if(this.health >= maxHealth)
        {
            this.health = maxHealth;
        }
    }

    private static int GetId()
    {
        ID++;
        return ID;
    }

}
