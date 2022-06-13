using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class MobView : MonoBehaviour
{
    private MobState mobState;
    private MobController mobController;

    private static GameObject infoWindow = null;
    private static bool isClicked = false;
    private static TargetLight targetLight = null;

    public MobView Init(MobController mobController,MobState state)
    {
        this.mobController = mobController;
        this.mobState = state;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = state.ShipState.Data.Icon;
        this.GetComponent<SphereCollider>().isTrigger = true;
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

        if (targetLight != null)
        {
            Destroy(targetLight.gameObject);
            targetLight = null;
        }
    }

    private void OnMouseDown()
    {
        isClicked = true;
        CreateInfoWindow();
        CreateTargetLight();
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

    private void CreateTargetLight()
    {
        if (targetLight != null)
        {
            Object.Destroy(targetLight.gameObject);
            targetLight = null;
        }

        if (targetLight == null)
        {
            targetLight = new GameObject().AddComponent<TargetLight>().Init(this.gameObject.GetComponent<SphereCollider>().radius);
            targetLight.transform.SetParent(this.transform, false);
            targetLight.transform.localPosition = new Vector3(0, 0, 1);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            this.mobController.Spawner.gameObject.GetComponent<MobSpawner>().RemoveMob(this.mobState.Id);

            CloseInfoWindow();
        }
    }


}
