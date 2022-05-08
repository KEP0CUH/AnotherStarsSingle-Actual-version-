using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobState : MonoBehaviour
{
    private static int ID = 1;

    private int id;
    private MobData data;


    public int Id => id;

    public void Init(MobKind kind)
    {

        id = GetId();
    }

    private static int GetId()
    {
        ID++;
        return ID;
    }
}
