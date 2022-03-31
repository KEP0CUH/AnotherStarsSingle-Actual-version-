using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetState : MonoBehaviour
{
    [SerializeField] private PlanetData data;

    public PlanetData Data => data;

    public void Init(Planet kind)
    {
        this.data = Managers.Resources.DownloadData(kind);
    }
}
