using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : ItemState
{

    private int maxAmmo;
    public int MaxAmmo => maxAmmo;

    public override ItemState Init(ItemKind kind, int count)
    {
        base.Init(kind, count);
        return this;
    }

    public override ItemState Init(ItemState state)
    {
        base.Init(state);
        return this;
    }

    public void Shoot(Transform parent,GunState gun)
    {
        if(((GunData)data).AmmoKind != AmmoKind.EmptyAmmo)
        {
            Debug.Log("Стреляю");
            GameObject bullet = new GameObject("Bullet");

            var sourcePosition = new Vector3(parent.position.x, parent.position.y, 0);
            bullet.transform.position = sourcePosition;
            var sourceAngle = new Vector3(0, 0, parent.localEulerAngles.z);
            bullet.transform.localEulerAngles = sourceAngle;


            bullet.AddComponent<AmmoController>().Init(gun);

            Destroy(bullet, 4);
        }
    }

}
