///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class GunState : ItemState, IUsable
{
    public override         ItemState       Init(ItemKind kind, int count)
    {
        base.Init(kind, count);
        return this;
    }

    public override         ItemState       Init(ItemState state)
    {
        base.Init(state);
        return this;
    }

    public override         void            Set()
    {
        Debug.Log("Попытка одеть пушку на корабль.");
        //SetIsTrue();
    }

    public override         void            Unset()
    {
        Debug.Log("Попытка снять пушку с корабля");
        //SetIsFalse();
    }

    public                  GunData         GetData()
    {
        return (GunData)this.Data;
    }

    public                  void            Shoot(Transform parent,Transform target,GunState gun)
    {
        if(GetData().AmmoKind != AmmoKind.EmptyAmmo)
        {
            Debug.Log("Стреляю");
            GameObject bullet = new GameObject("Bullet");

            var sourcePosition = new Vector3(parent.position.x, parent.position.y, 0);
            bullet.transform.position = sourcePosition;
            var sourceAngle = new Vector3(0, 0, parent.localEulerAngles.z);
            bullet.transform.localEulerAngles = sourceAngle;


            bullet.AddComponent<AmmoController>().Init(target,gun);

            Destroy(bullet, 4);
        }
    }
}
