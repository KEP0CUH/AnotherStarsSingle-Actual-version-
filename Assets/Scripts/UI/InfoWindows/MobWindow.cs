///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

public class MobWindow : MonoBehaviour
{
    [SerializeField] 
    private             Image                   icon;
    [SerializeField] 
    private             Text                    health;
    [SerializeField] 
    private             Scrollbar               slider;
    [SerializeField] 
    private             Button                  buttonAttack;
    [SerializeField] 
    private             Button                  buttonApproach;
    [SerializeField] 
    private             Button                  buttonClose;
    [SerializeField] 
    private             Text                    title;
    [SerializeField] 
    private             Text                    race;
    [SerializeField] 
    private             Text                    shipClass;
    [SerializeField] 
    private             Text                    size;
    [SerializeField] 
    private             Text                    speed;

    private             MobController           controller;

    public              void                    Init(MobController      controller)
    {
        this.controller = controller;
        this.controller.OnMobDamaged += UpdateHealthInfo;

        var data = controller.State.ShipState.Data;
        this.icon.sprite = data.Icon;
        this.speed.text = $"<color=grey>Скорость</color>   {data.Speed}";


        this.health.text = controller.State.MaxHealth.ToString();
        this.size.text = $"<color=grey>Размер</color>   {controller.State.Health}";
        this.slider.value = (float)(controller.State.Health / controller.State.MaxHealth);

        this.buttonClose.onClick.AddListener(CloseWindow);
        this.buttonApproach.onClick.AddListener(LaunchPlayer);
        this.buttonAttack.onClick.AddListener(LaunchAttack);
    }

    private             void                    UpdateHealthInfo()
    {
        this.health.text = controller.State.MaxHealth.ToString();
        this.slider.value = (float)(controller.State.Health / controller.State.MaxHealth);
    }

    private             void                    CloseWindow()
    {
        this.controller.View.CloseInfoWindow();
    }

    private             void                    LaunchPlayer()
    {
        Managers.Player.Controller.MoveToApproach(controller.transform.position);
    }

    private             void                    LaunchAttack()
    {
        Managers.Player.Controller.Shoot(this.controller.transform);
    }

    private             void                    OnDestroy()
    {
        this.controller.OnMobDamaged -= UpdateHealthInfo;
    }
}
