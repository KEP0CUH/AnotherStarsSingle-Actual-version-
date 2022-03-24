using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : MonoBehaviour
{
    [SerializeField] private GunData data;
    private int maxAmmo;

    public GunData Data => data;
    public int MaxAmmo => maxAmmo;

    public GunState Init(GunData data,int maxAmmo)
    {
        this.data = data;
        this.maxAmmo = maxAmmo;
        return this;
    }
}
