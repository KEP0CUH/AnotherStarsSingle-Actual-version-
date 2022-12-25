///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ConstantUI : MonoBehaviour
{
    [SerializeField] 
    private             GameObject              avatar;
    [SerializeField] 
    private             Text                    health;
    [SerializeField] 
    private             Scrollbar               sliderHealth;
    [SerializeField] 
    private             Text                    energy;
    [SerializeField] 
    private             Scrollbar               sliderEnergy;
    [SerializeField] 
    private             Button                  buttonSkills;
    [SerializeField] 
    private             Button                  buttonInventory;
    [SerializeField] 
    private             Button                  buttonQuests;
    [SerializeField]
    private             Button                  buttonDroids;
    [SerializeField] 
    private             Text                    kredits;

    private             PlayerController        playerController;

    public              ConstantUI              Init(PlayerController controller)
    {
        this.playerController = controller;
        Managers.Canvas.AddModule(this.gameObject);

        buttonInventory.onClick.AddListener(playerController.OpenInventory);

        UpdateInfo();

        return this;
    }

    public              void                    UpdateInfo()
    {
        health.text = playerController.ShipController.State.Data.Structure.ToString();
        energy.text = playerController.ShipController.State.Data.Energy.ToString();
    }
}
