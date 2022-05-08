using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MobState))]
public class MobController : MonoBehaviour
{
    private MobState mobState;
    private MobKind mobKind;

    private GameObject spawner;

    public void Init(Transform spawner,MobState mobState)
    {
        this.mobState = mobState;
        this.spawner = spawner.gameObject;
    }

}
