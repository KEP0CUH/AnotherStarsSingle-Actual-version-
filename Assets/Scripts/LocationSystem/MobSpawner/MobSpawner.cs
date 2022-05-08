using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private List<MobKind> mobs;

    private int currentNumMobs = 0;
    private int maxNumMobs = 4;


    public void Init()
    {
        this.currentNumMobs = 0;
        this.maxNumMobs = 4;

        SpawnMobs();
    }

    private void FixedUpdate()
    {
        if(this.currentNumMobs < this.maxNumMobs)
        {
            this.currentNumMobs += mobs.Count;
            SpawnMobs();
        }
    }

    private void SpawnMobs()
    {
        for(int i = 0; i < mobs.Count; i++)
        {
            var mob = new GameObject("Mob", typeof(MobController));
            mob.transform.SetParent(transform);
            mob.GetComponent<MobController>().Init(this.transform,mobs[i]);
        }
    }
}
