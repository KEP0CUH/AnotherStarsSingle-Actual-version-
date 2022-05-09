using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class MobView : MonoBehaviour
{
    private MobState mobState;

    private static GameObject infoWindow = null;
    private static bool isClicked = false;

    public MobView Init(MobState state)
    {
        this.mobState = state;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = state.ShipState.Data.Icon;
        this.GetComponent<BoxCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;

        return this;
    }

    private void OnMouseEnter()
    {
        CreateInfoWindow();
    }

    private void OnMouseDown()
    {
        isClicked = true;
    }

    private void OnMouseExit()
    {
        if (isClicked == false)
        {
            CloseInfoWindow();
        }
    }

    private void CreateInfoWindow()
    {
        if (infoWindow == null)
        {
            infoWindow = new GameObject("InfoWindow", typeof(MobInfoWindow));
            infoWindow.GetComponent<MobInfoWindow>().Init(this, this.mobState);
        }
        else
        {
            CloseInfoWindow();
            CreateInfoWindow();
        }
    }

    public void CloseInfoWindow()
    {
        if (infoWindow != null)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
            isClicked = false;
        }
    }

}
