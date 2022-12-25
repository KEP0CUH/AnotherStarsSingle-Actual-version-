///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AttackOnDoubleClick))]
public class MobView : MonoBehaviour
{
    private                 MobState                    mobState;
    private                 MobController               mobController;

    private static          GameObject                  infoWindow          = null;

    private static          TargetLight                 targetLight         = null;

    public                  MobView                     Init(MobController mobController,MobState state)
    {
        this.mobController = mobController;
        this.mobState = state;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = state.ShipState.Data.Icon;
        this.GetComponent<SphereCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;

        return this;
    }

    public                  void                        CloseInfoWindow()
    {
        if (infoWindow != null)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
        }

        if (targetLight != null)
        {
            Destroy(targetLight.gameObject);
            targetLight = null;
        }
    }

    private                 void                        OnMouseDown()
    {
        CreateInfoWindow();
        CreateTargetLight();
    }
    private                 void                        CreateInfoWindow()
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

    private                 void                        CreateTargetLight()
    {
        if(targetLight != null)
        {
            Destroy(targetLight.gameObject);
            targetLight = null;
        }

        var radius = this.GetComponent<SphereCollider>().radius;
        targetLight = new GameObject("TargetLight").AddComponent<TargetLight>().Init(this.transform, radius);
    }

    private                 void                        OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            this.mobController.Spawner.gameObject.GetComponent<MobSpawner>().UnspawnMob(this.mobState.Id);

            CloseInfoWindow();
        }
    }
}
