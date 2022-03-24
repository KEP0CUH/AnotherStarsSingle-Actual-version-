using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour
{
    [SerializeField] private ShipData data;
    //[SerializeField] private 

    public ShipData Data => data;

    public ShipState Init(ShipData data)
    {
        this.data = data;
        return this;
    }
}
