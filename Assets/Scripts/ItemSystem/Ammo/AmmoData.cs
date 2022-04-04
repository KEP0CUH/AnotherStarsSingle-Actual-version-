using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ammo", fileName = "newAmmo", order = 54)]
public class AmmoData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private Sprite icon;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AmmoKind kind;

    public string Title => title;
    public Sprite Icon => icon;
    public AudioClip Clip => clip;
    public AmmoKind Kind => kind;

    private void OnValidate()
    {
        switch (kind)
        {
            case AmmoKind.Multiblaster:
                title = "bulletMultiblaster";
                icon = Resources.Load<Sprite>("Icons/Items/Guns/Multiblaster");
                clip = Resources.Load<AudioClip>("Sounds/ShotKinetic2");
                break;
        }

    }
}
