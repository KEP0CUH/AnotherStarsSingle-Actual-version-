using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : ItemState
{
    [SerializeField] private AmmoKind ammoKind;
    private int maxAmmo;

    public AmmoKind AmmoKind => ammoKind;
    public int MaxAmmo => maxAmmo;

    public override ItemState Init(ItemKind kind, int count)
    {

        switch (kind)
        {
            case ItemKind.MultiblasterGun:
                ammoKind = AmmoKind.MultiblasterAmmo;
                break;
            case ItemKind.DesintegratorGun:
                ammoKind = AmmoKind.DesintegratorAmmo;
                break;
            case ItemKind.EmptyGun:
                ammoKind = AmmoKind.MultiblasterAmmo;
                break;
        }

        base.Init(kind, count);
        this.ammoKind = ((GunData)data).AmmoKind;
        return this;
    }

    public override ItemState Init(ItemState state)
    {
        switch (state.Data.ItemKind)
        {
            case ItemKind.MultiblasterGun:
                ammoKind = AmmoKind.MultiblasterAmmo;
                break;
            case ItemKind.DesintegratorGun:
                ammoKind = AmmoKind.DesintegratorAmmo;
                break;
            case ItemKind.EmptyGun:
                ammoKind = AmmoKind.EmptyAmmo;
                break;
        }

        base.Init(state);
        return this;
    }

    public void Shoot(Transform parent,GunState gun)
    {
        if(ammoKind != AmmoKind.EmptyAmmo)
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
