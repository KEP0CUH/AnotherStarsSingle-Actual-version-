using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class AmmoView : MonoBehaviour
{
    private AmmoController ammoController;
    public AmmoView Init(AmmoController controller)
    {
        this.ammoController = controller;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = ammoController.State.Data.Icon;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        CreateAudioSound(ammoController.State.Data.Sound);
        return this;
    }

    private void CreateAudioSound(AudioClip sound)
    {
        var audioObj = new GameObject("SoundObject",typeof(AudioSource));
        var audioComponent = audioObj.GetComponent<AudioSource>();
        audioComponent.clip = sound;
        audioComponent.Play();

        Destroy(audioObj, 3);
    }

    private void Update()
    {
        transform.Translate(transform.up * ammoController.State.MoveSpeed, Space.World);
    }
}
