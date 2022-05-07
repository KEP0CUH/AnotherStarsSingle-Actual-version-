using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    private PlayerController controller;
    private PlayerState playerState;

    private Transform landPlace;
    private bool isLanded;                      // Сидит ли на планете?
    public bool IsLanded => isLanded;
    public PlayerController Controller => controller;

    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("Player manager starting...".SetColor(Color.Yellow));



        Status = ManagerStatus.Started;
        Debug.Log("Player manager started.".SetColor(Color.Green));
    }

    public void Init(PlayerController controller, PlayerState state)
    {
        this.controller = controller;
        this.playerState = state;
    }

    public void Land(Transform transform)
    {
        this.landPlace = transform;
        controller.gameObject.transform.position = new Vector3(transform.position.x + 150, transform.position.y + 150, 0);
        controller.UpdateCameraPosition();
        isLanded = true;
        CanvasUI.Radar.Disable();
    }

    public void LandOnAsteroidField(Transform transform)
    {
        this.landPlace = transform;
        controller.gameObject.transform.position = new Vector3(transform.position.x + 150, transform.position.y + 150, 0);
        controller.UpdateCameraPosition();
        //isLanded = true;
    }

    public void Rise()
    {
        controller.gameObject.transform.position = new Vector2(this.landPlace.position.x,this.landPlace.position.y);
        controller.UpdateCameraPosition();
        isLanded = false;
        CanvasUI.Radar.Enable();
    }
}
