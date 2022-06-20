using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private List<GunState> guns = new List<GunState>();

    private PlayerState playerState;
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private InventoryController inventoryController;
    private InventoryInside inventoryInside;
    private ShipController shipController;
    private ConstantUI constantUI;
    private Radar radar;

    private float timer = 0;
    private float shootDelay = 0.5f;
    private int numOfFires = 5;
    private int firesMade = 0;

    private Camera mainCamera;
    private Camera radarCamera;
    private Camera globalMapCamera;

    public PlayerState State => playerState;
    public InventoryController Inventory => inventoryController;
    public InventoryInside InventoryInside => inventoryInside;
    public ShipController ShipController => shipController;
    public ConstantUI ConstantUI => constantUI;

    public Camera MainCamera => mainCamera;
    

    public PlayerController Init()
    {
        this.inventoryController = new InventoryController(this.transform);
        this.shipController = this.gameObject.AddComponent<ShipController>().Init(ShipKind.GreenLinkor,this.inventoryController);
        this.playerState = new PlayerState(this,shipController);

        this.inventoryInside = Instantiate(Managers.Resources.DownloadData(ObjectType.InventoryInside))
                                        .GetComponent<InventoryInside>().Init(this);
        
        Managers.Canvas.AddModule(this.inventoryInside.gameObject);
        Managers.Player.Init(this);

        this.constantUI = Instantiate(Managers.Resources.DownloadData(ObjectType.ConstantUI)).GetComponent<ConstantUI>().Init(this);
        this.playerMovement = gameObject.AddComponent<PlayerMovement>().Init(this);
        this.playerShoot = gameObject.AddComponent<PlayerShoot>().Init(this);


        SetupCamera();
        SetupRadar();
        SetupGlobalMap();
        UpdateState();

        return this;
    }

    public void ChangeShip(ShipKind kind)
    {
        this.State.SetShip(kind);
    }

    public void UpdateState()
    {
        if (this.playerState != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = playerState.ShipController.State.Data.Icon;
            ShowInventory();
        }
    }
    public void UpdateCameraPosition()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        radarCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -40);
        globalMapCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -40);
    }

    public void OpenInventory()
    {
        this.inventoryInside.Reswitch();
    }

    public void ShowInventory()
    {
        this.inventoryInside.ShowInventory();
    }

    public void MoveToApproach(Vector2 target)
    {
        this.playerMovement.Move(target);
    }

    /// <summary>
    /// Создаёт главную камеру игрока и настраивает её на работу.
    /// </summary>
    private void SetupCamera()
    {
        GameObject camera = new GameObject("MainCamera", typeof(Camera), typeof(AudioListener));

        mainCamera = camera.GetComponent<Camera>();
        mainCamera.backgroundColor = UnityEngine.Color.gray;
        mainCamera.orthographic = true;
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        mainCamera = camera.GetComponent<Camera>();
    }

    private void SetupRadar()
    {
        radar = Instantiate(Managers.Resources.DownloadData(ObjectType.Radar)).GetComponent<Radar>().Init(this);


        GameObject radarCam = new GameObject("RadarCamera");
        radarCam.AddComponent<Camera>();


        var cam = radarCam.GetComponent<Camera>();
        cam.orthographicSize = 20;
        cam.backgroundColor = UnityEngine.Color.grey;
        cam.orthographic = true;
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        cam.targetTexture = Resources.Load<RenderTexture>("Textures/Radar");
        radarCamera = cam;
    }

    private void SetupGlobalMap()
    {
        GameObject globalMapCameraObj = new GameObject("GlobalMap");
        globalMapCameraObj.AddComponent<Camera>();

        var cam = globalMapCameraObj.GetComponent<Camera>();
        cam.cullingMask = 6;
        cam.orthographicSize = 20;
        cam.backgroundColor = UnityEngine.Color.grey;
        cam.orthographic = true;
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        cam.targetTexture = Resources.Load<RenderTexture>("Textures/GlobalMap");
        globalMapCamera = cam;
    }



    public void Shoot(Transform target)
    {
        this.playerShoot.SetTarget(target);
    }

    private void Start()
    {
        this.Init();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryInside.Reswitch();
        }
    }

    /// <summary>
    /// Обработка ввода.
    /// </summary>
    private void FixedUpdate()
    {

        if (Managers.Player.IsLanded == false)
        {

            if (Input.GetKey(KeyCode.M))
            {
                radar.Enable();
            }

            if(Input.GetKey(KeyCode.L))
            {
                CanvasUI.GlobalMap.Enable();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Managers.Canvas.DisableAllModules();
            radar.Disable();
        }
    }


   
}
