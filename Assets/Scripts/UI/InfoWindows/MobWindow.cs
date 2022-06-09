using UnityEngine;
using UnityEngine.UI;

public class MobWindow : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text health;
    [SerializeField] private Text size;
    [SerializeField] private Scrollbar slider;
    [SerializeField] private Button buttonAttack;
    [SerializeField] private Button buttonApproach;
    [SerializeField] private Button buttonClose;

    private MobController controller;

    public void Init(MobController controller)
    {
        this.controller = controller;
        this.controller.OnMobDamaged += UpdateHealthInfo;

        this.icon.sprite = controller.State.ShipState.Data.Icon;
        this.health.text = controller.State.MaxHealth.ToString();
        this.size.text = controller.State.Health.ToString();
        this.slider.value = (float)(controller.State.Health / controller.State.MaxHealth);

        this.buttonClose.onClick.AddListener(CloseWindow);
    }

    private void UpdateHealthInfo()
    {
        this.health.text = controller.State.MaxHealth.ToString();
        this.slider.value = (float)(controller.State.Health / controller.State.MaxHealth);
    }

    private void CloseWindow()
    {
        this.controller.View.CloseInfoWindow();
    }

    private void OnDestroy()
    {
        this.controller.OnMobDamaged -= UpdateHealthInfo;
    }
}
