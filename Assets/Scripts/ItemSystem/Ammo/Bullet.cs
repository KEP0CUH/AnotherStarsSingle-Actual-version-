using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    private float moveSpeed = 2f / Constants.TICKS_PER_SEC;

    private float strength = 59;
    private float baseStrength = 50;
    public float Strength => strength;


    public void Init(GunState gun)
    {

        var audioObj = new GameObject("Sound of shoot.", typeof(AudioSource));
        var audio = audioObj.GetComponent<AudioSource>();

        this.gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resources.DownloadData(gun.AmmoKind).Icon;


        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        baseStrength = 50;
        strength = baseStrength * 1.2f;

        switch (gun.Data.ItemKind)
        {
            case ItemKind.weaponMultiblaster:
                audio.clip = Managers.Resources.DownloadData(SoundKind.ShotKinetic2);
                break;
            case ItemKind.weaponDesintegrator:
                audio.clip = Managers.Resources.DownloadData(SoundKind.ShotEnergetic2);
                break;
            case ItemKind.weaponEmpty:
                audio.clip = Managers.Resources.DownloadData(SoundKind.ShotKinetic2);
                break;
        }

        audio.Play();

        Destroy(audioObj, 2);
    }
    
    void Update()
    {
        transform.Translate(transform.up * moveSpeed,Space.World);
    }

    


}
