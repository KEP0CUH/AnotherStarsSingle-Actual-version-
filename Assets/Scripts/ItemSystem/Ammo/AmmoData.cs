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
        var pathAmmo = "Icons/Items/Ammo/";
        var pathSounds = "Sounds/";

        icon = Resources.Load<Sprite>(pathAmmo + kind);
        switch (kind)
        {
            case AmmoKind.MultiblasterAmmo:
                title = "bulletMultiblaster";
                clip = Resources.Load<AudioClip>(pathSounds + "ShotKinetic2");
                break;
            case AmmoKind.DesintegratorAmmo:
                title = "bulletDesintegrator";
                clip = Resources.Load <AudioClip>(pathSounds + "ShotEnergetic2");  
                break;
        }

    }
}
