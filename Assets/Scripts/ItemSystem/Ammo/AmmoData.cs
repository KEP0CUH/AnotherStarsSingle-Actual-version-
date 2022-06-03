using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ammo/newAmmo", fileName = "newAmmo", order = 54)]
public class AmmoData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private Sprite icon;
    [SerializeField] private string iconPath = "Icons/Items/Ammo/";
    [SerializeField] private AudioClip clip;
    [SerializeField] private string soundPath = "Sounds/";
    [SerializeField] private AmmoKind kind;


    [SerializeField] [Range(15,50)] private int baseDamage = 25;

    [SerializeField] [Range(2f / Constants.TICKS_PER_SEC, 5f / Constants.TICKS_PER_SEC)]
        private float speed = 2f / Constants.TICKS_PER_SEC;

    public string Title => title;
    public Sprite Icon => icon;
    public AudioClip Sound => clip;
    public AmmoKind Kind => kind;
    public int BaseDamage => baseDamage;
    public float Speed => speed;

    private void OnValidate()
    {
        var pathAmmo = iconPath;
        var pathSounds = soundPath;

        icon = Resources.Load<Sprite>(pathAmmo + kind);
        switch (kind)
        {
            case AmmoKind.MultiblasterAmmo:
                title = "bulletMultiblaster";
                clip = Resources.Load<AudioClip>(pathSounds + SoundKind.ShotKinetic2);
                break;
            case AmmoKind.DesintegratorAmmo:
                title = "bulletDesintegrator";
                clip = Resources.Load <AudioClip>(pathSounds + SoundKind.ShotEnergetic2);  
                break;
        }

    }
}
