using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(CanvasUI))]
[RequireComponent(typeof(ResourceLoader))]
public class Managers : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }

    public static CanvasUI Canvas { get; private set; }
    public static ResourceLoader Resources { get; private set; }

    private List<IGameManager> gameManagers = new List<IGameManager>();

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Canvas = GetComponent<CanvasUI>();
        Resources = GetComponent<ResourceLoader>();

        gameManagers = new List<IGameManager>();
        gameManagers.Add(Player);

        gameManagers.Add(Canvas);
        gameManagers.Add(Resources);

        StartCoroutine(StartupManagers());

        Application.targetFrameRate = 60;
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

        Debug.Log("All managers started up.".SetColor(Color.Green));
    }
}
