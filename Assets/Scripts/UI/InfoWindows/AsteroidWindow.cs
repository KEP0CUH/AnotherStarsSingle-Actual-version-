using UnityEngine;
using UnityEngine.UI;

public class AsteroidWindow : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text health;
    [SerializeField] private Text size;
    [SerializeField] private Scrollbar slider;
    [SerializeField] private Button buttonAttack;
    [SerializeField] private Button buttonApproach;
    [SerializeField] private Button buttonClose;

    private AsteroidController controller;

    public void Init(AsteroidController controller)
    {
        this.controller = controller;
        this.controller.OnDamagedAsteroid += OnAsteroidDamaged;


        this.icon.sprite = controller.State.Data.Icon;
        this.health.text = controller.State.Health.ToString();
        this.size.text = $"<color=grey>Размер</color>      " + controller.State.MaxHealth.ToString();
        this.slider.value = (float)(controller.State.Health / controller.State.MaxHealth);

        this.buttonClose.onClick.AddListener(CloseWindow);
        this.buttonApproach.onClick.AddListener(LaunchPlayer);
        this.buttonAttack.onClick.AddListener(LaunchAttack);
    }

    private void OnAsteroidDamaged()
    {
        this.health.text = controller.State.Health.ToString();
        this.slider.value = (float)(controller.State.Health / controller.State.MaxHealth);
    }

    private void CloseWindow()
    {
        controller.CloseInfoWindow();
    }

    private void LaunchPlayer()
    {
        Managers.Player.Controller.MoveToApproach(controller.transform.position);
    }

    private void LaunchAttack()
    {
        Managers.Player.Controller.Shoot(this.controller.transform);
    }

    private void OnDestroy()
    {
        this.controller.OnDamagedAsteroid -= OnAsteroidDamaged;
    }
}
