using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private float moveSpeed = 2f / Constants.TICKS_PER_SEC;

    private float strength = 59;
    private float baseStrength = 50;
    public float Strength => strength;

    void Awake()
    {
        this.gameObject.AddComponent<SpriteRenderer>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Icons/Items/Weapons/Bullets/BlueBullet");
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        baseStrength = 50;
        strength = baseStrength * 1.2f;
    }

    
    void Update()
    {
        transform.Translate(transform.up * moveSpeed,Space.World);
    }


}
