using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour, IGameManager, ICanvas
{
    private static GameObject canvas;
    public ManagerStatus Status { get; private set; }
    public UIModuleKind Kind { get; private set; }

    public static InventoryUI Inventory;
    public static RadarUI Radar;
    public static GlobalMapUI GlobalMap;
    
    private List<IUIModule> modules = new List<IUIModule>();


    public void Startup()
    {
        Kind = UIModuleKind.Canvas;
        Debug.Log("CanvasUI starting...".SetColor(Color.Yellow));
        canvas = SetupCanvas();
        SetupEventSystem();

        canvas.AddComponent<InventoryUI>();
        Inventory = canvas.GetComponent<InventoryUI>();
        modules.Add(Inventory);

        canvas.AddComponent<RadarUI>();
        Radar = canvas.GetComponent<RadarUI>();
        modules.Add(Radar);

        canvas.AddComponent<GlobalMapUI>();
        GlobalMap = canvas.GetComponent<GlobalMapUI>();
        modules.Add(GlobalMap);

        StartCoroutine(StartupModules());

        Status = ManagerStatus.Started;
        Debug.Log("CanvasUI started...".SetColor(Color.Green));
    }

    public GameObject AddModule(GameObject gameObject, string layer = "UI")
    {
        gameObject.GetComponent<RectTransform>().SetParent(canvas.transform,false);
        gameObject.layer = LayerMask.NameToLayer(layer);
        return gameObject;
    }

    private GameObject SetupCanvas()
    {
        GameObject canvas = new GameObject("Canvas");
        canvas.layer = LayerMask.NameToLayer("UI");
        canvas.transform.parent = null;
        canvas.AddComponent<RectTransform>();
        canvas.AddComponent<Canvas>();
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        return canvas;
    }
    private void SetupEventSystem()
    {
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    public void DisableAllModules()
    {
        foreach(IUIModule module in modules)
        {
            module.Disable();
        }
    }

    public void EnableModule(UIModuleKind kind)
    {
        foreach(IUIModule module in modules)
        {
            if(module.Kind == kind)
            {
                module.Enable();
            }
        }
    }

    private IEnumerator StartupModules()
    {
        foreach(IUIModule module in modules)
        {
            module.Startup(this);
        }

        yield return null;

        int numModules = modules.Count;
        int numDownloaded = 0;

        while(numDownloaded < numModules)
        {
            int lastReady = numDownloaded;
            numDownloaded = 0;

            foreach(IUIModule module in modules)
            {
                if(module.Status == ManagerStatus.Started)
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
        Debug.Log("All UI modules downloaded.");

    }
}
