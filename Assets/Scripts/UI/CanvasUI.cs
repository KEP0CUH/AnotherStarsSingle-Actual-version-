using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour, IGameManager
{
    private GameObject canvas;

    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("CanvasUI started.");
        canvas = SetupCanvas();
        SetupEventSystem();


        Status = ManagerStatus.Started;
    }

    public GameObject AddModuleToCanvas(GameObject gameObject, string layer = "UI")
    {
        gameObject.transform.parent = canvas.transform;
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

}
