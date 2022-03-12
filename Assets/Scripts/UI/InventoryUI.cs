using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject inventory = new GameObject("Inventory");
        Managers.Canvas.AddModuleToCanvas(inventory);
        inventory.AddComponent<Image>();
        inventory.GetComponent<RectTransform>().anchorMin = new Vector2(1,1);
        inventory.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        inventory.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
        inventory.GetComponent<RectTransform>().offsetMin = new Vector2(-50, -50);
        inventory.GetComponent<RectTransform>().offsetMax = new Vector2(40, 32);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
