using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
public class Managers : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }
    public static InventoryManager Inventory { get; private set; }

    private List<IGameManager> gameManagers = new List<IGameManager>();

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory= GetComponent<InventoryManager>();

        gameManagers = new List<IGameManager>();
        gameManagers.Add(Player);
        gameManagers.Add(Inventory);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach(IGameManager manager in gameManagers)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = gameManagers.Count;
        int numDownloaded = 0;

        while(numDownloaded < numModules)
        {
            int lastReady = numDownloaded;
            numDownloaded = 0;

            foreach(IGameManager manager in gameManagers)
            {
                if(manager.Status == ManagerStatus.Started)
                {
                    numDownloaded++;
                }
            }

            if(numDownloaded > lastReady)
            {
                Debug.Log($"Progress: {numDownloaded} / {numModules}");
            }
            yield return null;
        }

        Debug.Log("All managers started up.");
    }
}
