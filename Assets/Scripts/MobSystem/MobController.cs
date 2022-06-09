using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MobState))]
[RequireComponent(typeof(MobView))]
public class MobController : MonoBehaviour
{
    private float moveSpeed;
    private MobState mobState;
    private MobView mobView;
    private InventoryController inventoryController;

    private GameObject spawner;

    public event System.Action OnMobDamaged;

    public InventoryController InventoryController => inventoryController;
    public MobState State => mobState;
    public MobView View => mobView;
    public GameObject Spawner => spawner;

    public void Init(Transform spawner,MobKind kind)
    {
        this.inventoryController = new InventoryController(this.transform);
        this.mobState = this.gameObject.GetComponent<MobState>().Init(this,kind,this.inventoryController);
        this.mobView = this.gameObject.GetComponent<MobView>().Init(this,this.mobState);
        this.spawner = spawner.gameObject;


        this.moveSpeed = 5.0f / Constants.TICKS_PER_SEC;
        transform.localPosition = new Vector3(0, 0, -10);
        transform.position = new Vector3(transform.position.x,transform.position.y, -10);
    }

    public void OnMouseDown()
    {
        SelectMob();
    }

    public void CloseInfoWindow()
    {
        this.View.CloseInfoWindow();
    }

    public void SelectMob()
    {
        Debug.Log("Замечен щелчок по мобу");
    }

    public void ChangeMobHealth(int value)
    {
        mobState.ChangeHealth(value);
    }

    public void KillMob(int id)
    {
        this.Spawner.GetComponent<MobSpawner>().RemoveMob(id);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AmmoController>())
        {
            Debug.Log("Враг повреждается");
            Object.Destroy(other.gameObject);
            ChangeMobHealth(-1 * other.GetComponent<AmmoController>().State.Data.BaseDamage);
            OnMobDamaged?.Invoke();
        }

    }

    #region MOVEMENT
    // Координаты, в пределах которых осуществляется движение
    public float xMax = +15;
    public float xMin = -15;
    public float yMax = 15;
    public float yMin = -15;

    // Радиус и углы не используются, но в будущем возможно генерация позиции будет именно по окружности, а не по прямоугольнику.
    public int radius = 50;
    public float angle = 0f;

    // Различие между текущей позицией и целевой. Это нужно для движения от и до.
    Vector2 difference;
    Vector2 targetPos;

    // Прибыл ли враг в целевую точку?
    bool moveEnd = true;

    // Целевые координаты - генерируются рандомно.
    private float xTarget;
    private float yTarget;

    // Максимальное время полета. Если враг раньше прибудет в целевую точку, то он сразу отправится в другую.
    [SerializeField]
    private float delayTime = 10f;

    private void FixedUpdate()
    {
        if (moveEnd)
        {
            moveEnd = false;
            xTarget = UnityEngine.Random.Range(xMin, xMax);
            yTarget = UnityEngine.Random.Range(yMin, yMax);
            targetPos = new Vector2(xTarget, yTarget);

            difference = new Vector2(xTarget - transform.localPosition.x, yTarget - transform.localPosition.y);

            Vector3 targetDir = difference;
            float angle = Mathf.Atan2(difference.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

            Debug.DrawLine(transform.localPosition, targetPos, UnityEngine.Color.yellow, 10);

            StartCoroutine(Delay());
        }

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPos, moveSpeed * Time.fixedDeltaTime);
        transform.localPosition -= 10 * Vector3.forward;

        if (Mathf.Abs(transform.localPosition.x - difference.x) < 0.1 && Mathf.Abs(transform.localPosition.y - difference.y) < 0.1)
        {
            moveEnd = true;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        moveEnd = true;
    }
    #endregion
}
