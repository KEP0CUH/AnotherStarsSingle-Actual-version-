using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerController controller;
    private Transform target;
    private List<GunState> guns = new List<GunState>();

    private float timer = 0;
    private float shootDelay = 0.5f;
    private int numOfFires = 5;
    private int firesMade = 0;

    public PlayerShoot Init(PlayerController controller)
    {
        this.controller = controller;

        return this;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void ResetTarget()
    {
        this.target = null;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            timer += Time.fixedDeltaTime;
            if (timer > shootDelay)
            {
                if (numOfFires > firesMade)
                {
                    Shoot();
                    firesMade++;
                    timer -= 12 * Time.fixedDeltaTime;
                }
                if (numOfFires <= firesMade)
                {
                    timer = 0;
                    firesMade = 0;
                }
            }
        }
    }

    private void Shoot()
    {
        this.guns = this.controller.State.ShipController.State.Inventory.GetGuns();
        foreach (var gun in this.guns)
        {
            Debug.Log(100 * ((Vector2)transform.position - (Vector2)target.position).magnitude);
            if(((GunData)(gun.Data)).AttackRange >= 100 * ((Vector2)transform.position - (Vector2)target.position).magnitude)
            {
                gun.Shoot(this.gameObject.transform, target, gun);
            }
        }

    }
}
