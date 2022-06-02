using UnityEngine;

public class AsteroidState : MonoBehaviour
{
    private static int ID = 1;


    [SerializeField]
    private int id;
    [SerializeField]
    private AsteroidType type;
    private AsteroidData data;

    private float currentHealth;
    private float maxHealth;

    private float moveSpeed;

    [Header("Size of asteroid.")]
    private float radiusMax = 2;
    private float radiusMin = 0.1f;

    public AsteroidData Data => data;
    public float Health => currentHealth;
    public int Id => id;
    public float MoveSpeed => moveSpeed;

    public AsteroidState Init(AsteroidData data)
    {
        this.id = GetId();
        this.data = data;
        this.moveSpeed = Random.Range(data.MoveSpeedMin, data.MoveSpeedMax);
        this.maxHealth = Random.Range(data.MinHealth, data.MaxHealth);
        this.currentHealth = this.maxHealth;

        return this;
    }

    public AsteroidState Init(AsteroidType type)
    {
        this.id = GetId();
        this.type = type;
        this.data = Managers.Resources.DownloadData(type);
        this.moveSpeed = Random.Range(data.MoveSpeedMin, data.MoveSpeedMax);
        this.maxHealth = Random.Range(data.MinHealth, data.MaxHealth);
        this.currentHealth = this.maxHealth;

        return this;
    }

    public void ChangeHealth(float value)
    {
        if (currentHealth + value <= 0)
        {
            currentHealth = 0;
            Destroy(this.gameObject);
        }
        currentHealth += value;
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            MakeDrop();
        }
    }

    private void MakeDrop()
    {
        var data = Managers.Resources.DownloadData(this.data.DropKind);

        var angle = Random.Range(0, 360f);
        var radius = Random.Range(radiusMin, radiusMax);

        var dropView = new GameObject("Drop" + this.data.DropKind.ToString(), typeof(SpriteRenderer));
        dropView.AddComponent<ItemViewGame>().Init(data.ItemKind, 4);
        dropView.transform.position =
            new Vector3(transform.position.x + radius * Mathf.Sin(angle),
                        transform.position.y + radius * Mathf.Cos(angle),
                        0);
    }

    private static int GetId()
    {
        ID++;
        return ID;
    }
}
