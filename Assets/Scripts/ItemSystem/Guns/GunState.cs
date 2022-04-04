using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : BaseItemState
{
    [SerializeField] private GunKind gunKind;
    [SerializeField] private AmmoKind ammoKind;
    private int maxAmmo;

    public GunKind GunKind => gunKind;
    public AmmoKind AmmoKind => ammoKind;
    public int MaxAmmo => maxAmmo;

    public override void Init(GunKind kind, int count)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.count = count;
        this.isWeapon = true;
        this.gunKind = kind;

        switch(kind)
        {
            case GunKind.weaponMultiblaster:
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
