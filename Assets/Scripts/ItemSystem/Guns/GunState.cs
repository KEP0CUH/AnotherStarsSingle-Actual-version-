using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : BaseItemState
{
    [SerializeField] private AmmoKind ammoKind;
    private int maxAmmo;

    public AmmoKind AmmoKind => ammoKind;
    public int MaxAmmo => maxAmmo;

    public override void Init(ItemKind kind, int count)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.count = count;

        switch(kind)
        {
            case ItemKind.weaponMultiblaster:
                ammoKind = AmmoKind.Multiblaster;
                break;
            case ItemKind.weaponDesintegrator:
                ammoKind = AmmoKind.Desintegrator;
                break;
            case ItemKind.EmptyItem:
                ammoKind = AmmoKind.Multiblaster;
                break;
        }
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
