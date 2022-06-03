using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class MobView : MonoBehaviour
{
    private MobState mobState;
    private MobController mobController;

    private static GameObject infoWindow = null;
    private static bool isClicked = false;

    public MobView Init(MobController mobController,MobState state)
    {
        this.mobController = mobController;
        this.mobState = state;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = state.ShipState.Data.Icon;
        this.GetComponent<BoxCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;

        return this;
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

    private void OnMouseEnter()
    {
        if(isClicked == false)
        {
            CreateInfoWindow();
        }
    }

    private void OnMouseDown()
    {
        if(isClicked == true)
        {
            CreateInfoWindow();
        }
        else
        {
            isClicked = true;
        }
    }

    private void OnMouseExit()
    {
        if (isClicked == false)
        {
            CloseInfoWindow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AmmoController>())
        {
            Debug.Log("Враг повреждается");
            Object.Destroy(other.gameObject);
            this.mobController.ChangeMobHealth(-1 * other.GetComponent<AmmoController>().State.Data.BaseDamage);
        }

    }

    private void CreateInfoWindow()
    {
        if(infoWindow != null)
        {
            Object.Destroy(infoWindow.gameObject);
            infoWindow = null;
        }

        if (infoWindow == null)
        {
            infoWindow = new GameObject("InfoWindow", typeof(MobInfoWindow));
            infoWindow.GetComponent<MobInfoWindow>().Init(this, this.mobState);
        }
    }


}
