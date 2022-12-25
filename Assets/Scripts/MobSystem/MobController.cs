///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MobState))]
[RequireComponent(typeof(MobView))]
[RequireComponent(typeof(MobMovement))]
public class MobController : MonoBehaviour
{
    private             MobState                    mobState;
    private             MobView                     mobView;
    private             MobMovement                 mobMovement;
    private             InventoryController         inventoryController;

    private             GameObject                  spawner;

    public              event System.Action         OnMobDamaged;

    public              InventoryController         InventoryController => inventoryController;
    public              MobState                    State => mobState;
    public              MobView                     View => mobView;
    public              GameObject                  Spawner => spawner;

    public              void                        Init(Transform spawner,MobKind kind)
    {
        this.inventoryController = new InventoryController(this.transform);
        this.mobState = this.gameObject.GetComponent<MobState>().Init(this,kind,this.inventoryController);
        this.mobView = this.gameObject.GetComponent<MobView>().Init(this,this.mobState);
        this.mobMovement = this.gameObject.GetComponent<MobMovement>().Init(this);
        this.spawner = spawner.gameObject;

        transform.localPosition = new Vector3(0, 0, -10);
        transform.position = new Vector3(transform.position.x,transform.position.y, -10);
    }

    public              void                        CloseInfoWindow()
    {
        this.View.CloseInfoWindow();
    }

    public              void                        ChangeMobHealth(int value)
    {
        mobState.ChangeHealth(value);
    }

    public              void                        KillMob(int id)
    {
        this.Spawner.GetComponent<MobSpawner>().UnspawnMob(id);
    }

    private             void                        OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AmmoController>())
        {
            Destroy(other.gameObject);
            var gunData = (GunData)other.GetComponent<AmmoController>().GunState.Data;
            var damage = gunData.CalculateDamage() - this.State.ShipState.Data.Armor;
            Debug.Log($"${damage}");
            ChangeMobHealth(-1 * damage);
            OnMobDamaged?.Invoke();


/*            var damageText = new GameObject("DamageText",typeof(RectTransform));
            Managers.Canvas.AddModule(damageText);
            damageText.AddComponent<DamageText>().Init(this.transform,"Damage");
            Object.Destroy(damageText, 5);*/
        }
    }
}
