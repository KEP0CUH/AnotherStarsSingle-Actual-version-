using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldState : MonoBehaviour
{
    [SerializeField] private AsteroidFieldData data;
    public AsteroidFieldData Data => data;

    public void Init(AsteroidFieldType type)
    {
        this.data = Managers.Resources.DownloadData(type);
    }
}
