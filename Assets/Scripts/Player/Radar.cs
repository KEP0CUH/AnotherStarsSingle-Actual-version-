using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonMap;

    private PlayerController playerController;
    public Radar Init(PlayerController controller)
    {
        this.playerController = controller;
        this.buttonMap.onClick.AddListener(CanvasUI.GlobalMap.Enable);

        Managers.Canvas.AddModule(this.gameObject);

        return this;
    }

    public void Enable()
    {
        this.gameObject.SetActive(true);   
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
