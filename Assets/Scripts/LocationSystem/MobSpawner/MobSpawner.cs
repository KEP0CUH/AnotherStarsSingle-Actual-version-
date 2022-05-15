using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private List<MobKind> mobs;
    private Dictionary<int,MobController> spawnedMobs;

    private bool isInitialized = false;

    private int currentNumMobs = 0;
    private int maxNumMobs = 4;


    public void Init(MobSpawnerKind kind)
    {
        mobs = new List<MobKind>();
        spawnedMobs = new Dictionary<int,MobController>();
        switch (kind)
        {
            case MobSpawnerKind.pirateSpawner1:
                maxNumMobs = 3;
                mobs.Add(MobKind.PirateIndus1);
                mobs.Add(MobKind.PirateIndus1);
                mobs.Add(MobKind.PirateIndus1);
                break;
            case MobSpawnerKind.pirateSpawner2:
                maxNumMobs = 4;
                mobs.Add(MobKind.PirateIndus1);
                mobs.Add(MobKind.PirateFrigate1);
                mobs.Add(MobKind.PirateIstrebitel1);
                mobs.Add(MobKind.PirateIstrebitel1);
                break;
        }

        this.currentNumMobs = 0;

        SpawnMobs();
        isInitialized = true;
    }

    private void FixedUpdate()
    {
        if (isInitialized && this.currentNumMobs < this.maxNumMobs)
        {
            SpawnMobs();
        }
    }

    public void SpawnMobs()
    {
        int i = 0;
        while(this.currentNumMobs < this.maxNumMobs)
        {
            this.currentNumMobs++;
            var mob = new GameObject("Mob", typeof(MobController));
            mob.transform.SetParent(transform);

            var controller = mob.GetComponent<MobController>();
            controller.Init(this.transform, mobs[i]);

            Debug.Log(controller.MobState.Id);
            spawnedMobs.Add(controller.MobState.Id, controller);
            i++;
        }
    }

    public void RemoveMob(int id)
    {
        if(spawnedMobs.ContainsKey(id))
        {
            RespawnMob(spawnedMobs[id].MobState.Data.MobKind);
            Object.Destroy(spawnedMobs[id].gameObject);
            spawnedMobs.Remove(id);
            currentNumMobs--;
        }
    }

    private void RespawnMob(MobKind kind)
    {
        this.currentNumMobs++;
        var mob = new GameObject("Mob", typeof(MobController));
        mob.transform.SetParent(transform);

        var controller = mob.GetComponent<MobController>();
        controller.Init(this.transform, kind);

        Debug.Log(controller.MobState.Id);
        spawnedMobs.Add(controller.MobState.Id, controller);
    }
}
