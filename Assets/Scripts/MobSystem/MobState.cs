using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ShipState))]
public class MobState : MonoBehaviour
{
    private static int ID = 1;

    private int id;
    private MobData data;
    private ShipState ship;


    public int Id => id;
    public MobData Data => data;
    public ShipState Ship => ship;

    public void Init(MobKind kind)
    {
        this.data = Managers.Resources.DownloadData(kind);
        if(this.data != null)
        {
            Debug.Log($"{this.data.Ship}".SetColor(Color.Magenta));
            this.ship = this.gameObject.GetComponent<ShipState>().Init(data.Ship);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ship.Data.Icon;
            id = GetId();
        }

    }

    private static int GetId()
    {
        ID++;
        return ID;
    }
}
