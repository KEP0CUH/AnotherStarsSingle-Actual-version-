using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    private List<MobKind> mobs;

    private void SpawnMobs()
    {
        for(int i = 0; i < mobs.Count; i++)
        {
            var mob = new GameObject("Mob", typeof(MobController));
            mob.GetComponent<MobController>().Init(this.transform,mobState)
        }
    }
}
