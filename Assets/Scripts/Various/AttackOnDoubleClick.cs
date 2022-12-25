///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using System.Collections;
using UnityEngine;

public class AttackOnDoubleClick : MonoBehaviour
{
    private             float               clickCounter            = 0;
    private             float               firstClickTime          = 0;
    private             float               timeBetweenClicks       = 0.5f;
    private             bool                coroutineIsRunning      = false;

    private             void                OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            clickCounter++;
        }

        if (clickCounter == 1 && coroutineIsRunning == false)
        {
            firstClickTime = Time.time;
            StartCoroutine(DoubleClickDetection());
        }
    }

    private             IEnumerator         DoubleClickDetection()
    {
        coroutineIsRunning = true;
        while (firstClickTime + timeBetweenClicks >= Time.time)
        {
            if (clickCounter == 2)
            {
                Managers.Player.Attack(this.transform);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        clickCounter = 0;
        firstClickTime = 0;
        coroutineIsRunning = false;
    }
}
