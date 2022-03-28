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
            GameObject drop = new GameObject("Item: " + this.data.DropKind.ToString());
            drop.transform.position = this.gameObject.transform.position;

            var data = Managers.Resources.DownloadData(this.data.DropKind);

            drop.AddComponent<ItemViewGame>().Init(data.ItemKind, 4);


            GameObject dropTest = new GameObject("Item: " + GunKind.weaponKinetic.ToString());
            dropTest.transform.position = this.gameObject.transform.position;

            var dataTest = Managers.Resources.DownloadData(GunKind.weaponKinetic);

            dropTest.AddComponent<ItemViewGame>().Init(GunKind.weaponKinetic, 4);
        }
    }


}
