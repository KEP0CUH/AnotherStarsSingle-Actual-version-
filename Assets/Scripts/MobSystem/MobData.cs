using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newMob",menuName="Mob",order =51)]
public class MobData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private MobKind kind;

    public string Title => title;

    private void OnValidate()
    {
        switch(kind)
        {
            case MobKind.PirateIndus1:
                title = "PirateIndus1";
                break;
        }
    }
}
