using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAsteroidState : MonoBehaviour
{
    [SerializeField] private BaseAsteroidData data;

    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    public BaseAsteroidData Data => data;
    public float MaxHealth => maxHealth;
    public float Health => health;

    float radiusMax = 3;
    float radiusMin = 0.1f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            var angle = Random.Range(0, 360);
            var radius = Random.Range(radiusMin, radiusMax);

            var gameObj = new GameObject("New", typeof(BaseItemState), typeof(SpriteRenderer));
            gameObj.GetComponent<BaseItemState>().Init(ItemKind.rudaGold, 1);
            gameObj.GetComponent<SpriteRenderer>().sprite = gameObj.GetComponent<BaseItemState>().Data.Icon;
            gameObj.transform.position = new Vector3(transform.position.x + radius * Mathf.Sin(angle),
                                                     transform.position.y + radius * Mathf.Cos(angle),
                                                     0);
        }

    }

    public void Init(BaseAsteroidData data)
    {
        this.data = data;

        maxHealth = Random.Range(400, 800);
        health = maxHealth;
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


            GameObject dropTest = new GameObject("Item: " + ItemKind.weaponMultiblaster.ToString());
            dropTest.transform.position = this.gameObject.transform.position;

            var dataTest = Managers.Resources.DownloadData(ItemKind.weaponMultiblaster);

            dropTest.AddComponent<ItemViewGame>().Init(dataTest.ItemKind, 4);




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


}
