using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private float moveSpeed = 2f / Constants.TICKS_PER_SEC;

    void Awake()
    {
        this.gameObject.AddComponent<SpriteRenderer>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Icons/Items/Weapons/Bullets/BlueBullet");
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    
    void Update()
    {
        transform.Translate(transform.up * moveSpeed,Space.World);
    }


}
