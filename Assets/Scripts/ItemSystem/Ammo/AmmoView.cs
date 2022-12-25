///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class AmmoView : MonoBehaviour
{
    private             AmmoController          ammoController;
    private             Transform               target;
    public              AmmoView                Init(Transform target, AmmoController controller)
    {
        this.ammoController = controller;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = ammoController.State.Data.Icon;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.target = target;

        CreateAudioSound(ammoController.State.Data.Sound);
        return this;
    }

    private             void                    CreateAudioSound(AudioClip sound)
    {
        var audioObj = new GameObject("SoundObject", typeof(AudioSource));
        var audioComponent = audioObj.GetComponent<AudioSource>();
        audioComponent.clip = sound;
        audioComponent.Play();

        Destroy(audioObj, 3);
    }

    private             void                    FixedUpdate()
    {
        if (target != null) Move();
        else
        {
            Destroy(this.gameObject);
        }
    }

    private             void                    Move()
    {
        // ¬ычисл€ем вектор с направлением, но с длиной единица - нормализуем.
        Vector2 _difference = (new Vector2(target.position.x, target.position.y)
                              - new Vector2(transform.position.x, transform.position.y)).normalized;

        // ѕоворачиваем пулю. ¬ычисление угла через тангенс.
        float angle = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);


        // ≈сли текущие координаты x и y игрока сильно отличаютс€ от целевых,то движение продолжаетс€
        if (Math.Abs(target.position.x - transform.position.x) >= 0.2f || Math.Abs(target.position.y - transform.position.y) >= 0.2f)
        {
            // „тобы не было ошибки при делении на нуль,если клик осуществл€етс€ в текущие координаты. ¬озможно даже не нужна
            if (_difference.magnitude != 0)
            {
                transform.position += new Vector3(_difference.x * ammoController.State.MoveSpeed, _difference.y * ammoController.State.MoveSpeed, 0);
            }
        }
    }
}
