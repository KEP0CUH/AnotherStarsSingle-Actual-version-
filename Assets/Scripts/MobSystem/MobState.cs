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


    public int Id => id;
    public MobData Data => data;
    public ShipState ShipState => shipState;

    public MobState Init(MobKind kind)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.shipState = this.gameObject.GetComponent<ShipState>().Init(data.Ship);
        id = GetId();
        return this;
    }

    private static int GetId()
    {
        ID++;
        return ID;
    }
}
