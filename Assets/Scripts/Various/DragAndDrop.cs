using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    private bool canMove;
    private bool dragging;
    private RectTransform rect;
    
    private Vector3 differenceBetweenRectAndClick;
    private Vector3 target;

    private void Start()
    {
        canMove = false;
        dragging = false;
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {


        if(Input.GetMouseButtonDown(0))
        {
            Input.GetMouseButton(0);
            Debug.Log("Зажатие мыши на перетаскиваемом объекте.");
            Debug.Log(rect.transform.position);
            Debug.Log(Input.mousePosition);
            differenceBetweenRectAndClick = Input.mousePosition - rect.transform.position;
            canMove = true;
            dragging = true;
        }

        if(dragging)
        {
            //differenceBetweenRectAndClick = Input.mousePosition - rect.transform.position;
            
            Debug.Log("Difference: " + differenceBetweenRectAndClick);
            Debug.Log("MousePos: " + Input.mousePosition);
            Debug.Log("RectPos: " + rect.transform.position);
            target = Input.mousePosition - differenceBetweenRectAndClick;//+ new Vector3(rect.sizeDelta.x / 2,rect.sizeDelta.y / 2,0) ;
            rect.transform.position = target;
        }

        if(Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
            Debug.Log("Отжатие мыши на перетаскиваеом объекте.");
        }
    }
}
