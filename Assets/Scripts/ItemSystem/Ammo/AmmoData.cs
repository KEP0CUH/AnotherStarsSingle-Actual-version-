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
        var pathGuns = "Icons/Items/Guns/";
        var pathSounds = "Sounds/";
        switch (kind)
        {

            case AmmoKind.Multiblaster:
                title = "bulletMultiblaster";
                icon = Resources.Load<Sprite>(pathGuns + "Multiblaster");
                clip = Resources.Load<AudioClip>(pathSounds + "ShotKinetic2");
                break;
            case AmmoKind.Desintegrator:
                title = "bulletDesintegrator";
                icon = Resources.Load<Sprite>(pathGuns + "Desintegrator");
                clip = Resources.Load <AudioClip>(pathSounds + "ShotEnergetic2");  
                break;

        }

    }
}
