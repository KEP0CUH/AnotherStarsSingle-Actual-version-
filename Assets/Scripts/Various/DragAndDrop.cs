//////////////////////////////////////////////////
///     Created:    04.12.22
///     Author:     KEPOLLlblLLlKA
///     Updated:    06.12.22
///     Tested:     Not
/////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private             bool                canMove;
    private             bool                dragging;
    private             RectTransform       rect;

    private             Vector3             differenceBetweenRectAndClick;
    private             Vector3             target;

    private             void                Start()
    {
        canMove = false;
        dragging = false;
        rect = GetComponent<RectTransform>();
    }

    public              void                OnPointerEnter(PointerEventData     eventData)
    {
        canMove = true;
    }

    public              void                OnPointerExit(PointerEventData      eventData)
    {
        canMove = false;
        dragging = false;   
    }

    private void Update()
    {
        if(canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Input.GetMouseButton(0);
                Debug.Log("Зажатие мыши на перетаскиваемом объекте.");
                Debug.Log(rect.transform.position);
                Debug.Log(Input.mousePosition);
                differenceBetweenRectAndClick = Input.mousePosition - rect.transform.position;
                canMove = true;
                dragging = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                canMove = false;
                dragging = false;
                Debug.Log("Отжатие мыши на перетаскиваеом объекте.");
            }
        }

        if (dragging)
        {
            //differenceBetweenRectAndClick = Input.mousePosition - rect.transform.position;

            Debug.Log("Screen: width=" + Screen.width + "; height= " + Screen.height);
            Debug.Log("Difference: " + differenceBetweenRectAndClick);
            Debug.Log("MousePos: " + Input.mousePosition);
            Debug.Log("RectPos: " + rect.transform.position);
            target = Input.mousePosition - differenceBetweenRectAndClick;//+ new Vector3(rect.sizeDelta.x / 2,rect.sizeDelta.y / 2,0) ;
            Debug.Log(("Target = " + target).SetColor(Color.Yellow));

            if (rect.pivot.x >= 0.5f)
            {
                if (rect.pivot.y >= 0.5f)
                {
                    if (target.x > Screen.width || target.x - rect.sizeDelta.x < 0)
                    {
                        if (target.y > Screen.height || target.y - rect.sizeDelta.y < 0)
                        {
                            return;
                        }
                        else
                        {
                            rect.transform.position = new Vector3(rect.transform.position.x, target.y);
                        }
                    }
                    else if(target.y > Screen.height || target.y - rect.sizeDelta.y < 0)
                    {
                        if (target.x > Screen.width || target.x - rect.sizeDelta.x < 0)
                        {
                            return;
                        }
                        else
                        {
                            rect.transform.position = new Vector3(target.x, rect.transform.position.y);
                        }
                    }
                    else
                    {
                        rect.transform.position = target;
                    }
                }
            }
        }
    }
}
