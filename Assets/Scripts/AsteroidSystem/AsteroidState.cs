using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidState : MonoBehaviour
{
    private static int ID = 1;
    [SerializeField] private AsteroidData data;

    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    [SerializeField] protected int id;

    [SerializeField] private float moveSpeed = 2.0f / Constants.TICKS_PER_SEC;

    public AsteroidData Data => data;
    public float MaxHealth => maxHealth;
    public float Health => health;

    public int Id => id;

    public float MoveSpeed => moveSpeed;

    float radiusMax = 2;
    float radiusMin = 0.1f;

    private void Update()
    {
/*        if (Input.GetKey(KeyCode.P))
        {
            var angle = Random.Range(0, 360);
            var radius = Random.Range(radiusMin, radiusMax);

            var gameObj = new GameObject("New", typeof(ItemState), typeof(SpriteRenderer));
            gameObj.GetComponent<ItemState>().Init(ItemKind.GoldOre, 1);
            gameObj.GetComponent<SpriteRenderer>().sprite = gameObj.GetComponent<ItemState>().Data.Icon;
            gameObj.transform.position = new Vector3(transform.position.x + radius * Mathf.Sin(angle),
                                                     transform.position.y + radius * Mathf.Cos(angle),
                                                     0);
        }
*/           
        // Спавн предметов игроком по нажатию буквы Р. Здесь важен алгоритм спавна внутри окружности.
    }

    public void Init(AsteroidData data)
    {
        this.data = data;
        this.moveSpeed = 2.0f / Constants.TICKS_PER_SEC;

        maxHealth = Random.Range(400, 800);
        health = maxHealth;
        this.id = GetId();
    }

    public void ChangeHealth(float value)
    {
        if (health + value <= 0)
        {
            health = 0;
            Destroy(this.gameObject);
        }
        health += value;
    }

    [ContextMenu("OnDestroy")]
    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            CreateDrop();


/*            GameObject dropTest = new GameObject("Item: " + ItemKind.weaponMultiblaster.ToString());
            dropTest.transform.position = this.gameObject.transform.position;

            var dataTest = Managers.Resources.DownloadData(ItemKind.weaponMultiblaster);

            dropTest.AddComponent<GunViewGame>().Init(dataTest.ItemKind, 1);



            GameObject dropTest2 = new GameObject("Item: " + ItemKind.deviceTourbine.ToString());
            dropTest2.transform.position = this.gameObject.transform.position;

            var dataTest2 = Managers.Resources.DownloadData(ItemKind.deviceTourbine);

            dropTest2.AddComponent<DeviceViewGame>().Init(dataTest2.ItemKind, 1);*/
        }
    }

    private void CreateDrop()
    {
        var data = Managers.Resources.DownloadData(this.data.DropKind);

        var angle = Random.Range(0, 360);
        var radius = Random.Range(radiusMin, radiusMax);

        var dropView = new GameObject("Drop" + this.data.DropKind.ToString(), typeof(SpriteRenderer));
        dropView.AddComponent<ItemViewGame>().Init(data.ItemKind, 4);
        dropView.transform.position = new Vector3(transform.position.x + radius * Mathf.Sin(angle),
                                                 transform.position.y + radius * Mathf.Cos(angle),
                                                 0);
    }

    private static int GetId()
    {
        ID++;
        return ID;
    }
}
