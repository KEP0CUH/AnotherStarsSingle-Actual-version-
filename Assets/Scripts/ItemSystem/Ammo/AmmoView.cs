using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class AmmoView : MonoBehaviour
{
    private AmmoState ammoState;
    public AmmoView Init(AmmoState state,SoundKind sound)
    {
        this.ammoState = state;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = ammoState.Data.Icon;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        CreateAudioSound(sound);

        var audio = gameObject.GetComponent<AudioSource>();
        audio.clip = Managers.Resources.DownloadData(sound);
        audio.Play();

        return this;
    }

    private void CreateAudioSound(SoundKind sound)
    {
        var audioObj = new GameObject("SoundObject",typeof(AudioSource));
        var audioComponent = audioObj.GetComponent<AudioSource>();
        audioComponent.clip = Managers.Resources.DownloadData(sound);
        audioComponent.Play();

        Destroy(audioObj, 3);
    }

    private void Update()
    {
        transform.Translate(transform.up * ammoState.MoveSpeed, Space.World);
    }


}
