using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IUIModule
{
    private GameObject inventory;

    //          PosX и PosY отвечают за позицию ui объекта. Соответсвенно при привязке
    //          к правому верхнему углу требуется смещать ui элемент в отрицательном направлении.
    //          поскольку начальной точкой (0,0) выступает угол.
    [SerializeField]
    private int posX = -32;             
    [SerializeField]
    private int posY = -32;

    //          Ширина и высота UI элемента(в данном случае инвентаря).
    [SerializeField]
    private int width = 400;
    [SerializeField]
    private int height = 220;

    public ManagerStatus Status { get;private set; }
    public UIModuleKind Kind { get;private set; }

    public void Startup(ICanvas canvas)
    {
        Kind = UIModuleKind.Inventory;
        Status = ManagerStatus.Initializing;
        Debug.Log("InventoryUI initializing...");
        CreateInventory(canvas);
        OnValidate();
    }

    [ContextMenu("Enable")]
    public void Enable()
    {
        inventory.SetActive(true);
    }
    [ContextMenu("Disable")]
    public void Disable()
    {
        inventory.SetActive(false);
    }


    private void CreateInventory(ICanvas canvas)
    {
        inventory = new GameObject("Inventory");
        canvas.AddModule(inventory);
        inventory.AddComponent<Image>();
        inventory.GetComponent<Image>().color = new UnityEngine.Color(134,183,219,90) / 256f;
        inventory.GetComponent<Image>().Rebuild(CanvasUpdate.PreRender);
        inventory.GetComponent<Image>().raycastTarget = false;

        inventory.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        inventory.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        inventory.GetComponent<RectTransform>().pivot = new Vector2(1, 1);

        inventory.GetComponent<RectTransform>().offsetMin = new Vector2(-50, -50);
        inventory.GetComponent<RectTransform>().offsetMax = new Vector2(40, 32);
        Status = ManagerStatus.Started;
        Debug.Log("InventoryUI started.");
    }

    private void OnValidate()
    {
        if(inventory != null)
        {
            //inventory.GetComponent<RectTransform>().offsetMin = new Vector2(posX, posY);
            inventory.GetComponent<RectTransform>().offsetMin = new Vector2(-width, -height);
            inventory.GetComponent<RectTransform>().offsetMax = new Vector2(width, height);
        }
    }
}
