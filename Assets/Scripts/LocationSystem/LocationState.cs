using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocationState : MonoBehaviour
{
    [SerializeField] private LocationData data;
    [SerializeField] private List<string> planets;


    public LocationData Data => data;

    public void Init(Location location)
    {
        this.data = Managers.Resources.DownloadData(location);
    }
}
