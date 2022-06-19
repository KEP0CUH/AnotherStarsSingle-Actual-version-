using System.Collections;
using UnityEngine;
public class LandPlanetOnDoubleClick : MonoBehaviour
{
    private float clickCounter = 0;
    private float firstClickTime = 0;
    private float timeBetweenClicks = 0.5f;
    private bool coroutineIsRunning = false;
    private PlanetController controller;

    public void Init(PlanetController controller)
    {
        this.controller = controller;
    }

    private void OnMouseOver()
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

    private IEnumerator DoubleClickDetection()
    {
        coroutineIsRunning = true;
        while (firstClickTime + timeBetweenClicks >= Time.time)
        {
            if (clickCounter == 2)
            {
                Land();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        clickCounter = 0;
        firstClickTime = 0;
        coroutineIsRunning = false;
    }

    private void Land()
    {
        Managers.Player.Land(controller.transform);

        var planetInside = new GameObject("PlanetInside", typeof(RectTransform));
        planetInside.AddComponent<PlanetInside>().Init(controller);

        controller.CloseInfoWindow();
    }
}
