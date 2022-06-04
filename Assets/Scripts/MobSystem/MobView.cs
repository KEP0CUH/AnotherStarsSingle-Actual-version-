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


    private void OnMouseDown()
    {
        isClicked = true;
        CreateInfoWindow();
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
            infoWindow = Instantiate(Managers.Resources.DownloadData(ObjectType.MobWindow));
            Managers.Canvas.AddModule(infoWindow);
            infoWindow.GetComponent<MobWindow>().Init(this.mobController);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            this.mobController.Spawner.gameObject.GetComponent<MobSpawner>().RemoveMob(this.mobState.Id);

            if (infoWindow != null)
            {
                Destroy(infoWindow.gameObject);
                infoWindow = null;
            }
        }
    }


}
