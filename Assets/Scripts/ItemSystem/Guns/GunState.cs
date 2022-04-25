using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : ItemState
{
    [SerializeField] private AmmoKind ammoKind;
    private int maxAmmo;

    public AmmoKind AmmoKind => ammoKind;
    public int MaxAmmo => maxAmmo;

    public override void Init(ItemKind kind, int count)
    {
        switch (kind)
        {
            case ItemKind.weaponMultiblaster:
                ammoKind = AmmoKind.Multiblaster;
                break;
            case ItemKind.weaponDesintegrator:
                ammoKind = AmmoKind.Desintegrator;
                break;
            case ItemKind.weaponEmpty:
                ammoKind = AmmoKind.Multiblaster;
                break;
        }

        base.Init(kind, count);
    }

    public override void Init(ItemState state)
    {
        switch (state.Data.ItemKind)
        {
            case ItemKind.weaponMultiblaster:
                ammoKind = AmmoKind.Multiblaster;
                break;
            case ItemKind.weaponDesintegrator:
                ammoKind = AmmoKind.Desintegrator;
                break;
            case ItemKind.weaponEmpty:
                ammoKind = AmmoKind.Multiblaster;
                break;
        }

        base.Init(state);
    }

    public void Shoot(Transform parent,GunState gun)
    {
        Debug.Log("Стреляю");
        GameObject bullet = new GameObject("Bullet");
        bullet.transform.position = new Vector3(parent.position.x, parent.position.y, 0);
        bullet.transform.localEulerAngles = new Vector3(0, 0, parent.localEulerAngles.z);
        bullet.AddComponent<Bullet>().Init(gun);
        

        Destroy(bullet, 10);
    }

}
