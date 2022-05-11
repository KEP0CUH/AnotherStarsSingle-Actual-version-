using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetState : MonoBehaviour
{
    private static int ID = 1;
    [SerializeField] private int id;
    [SerializeField] private PlanetData data;
    private PlanetController planetController;

    public int Id => id;
    public PlanetData Data => data;

    public PlanetState Init(PlanetController controller,Planet kind)
    {
        this.id = GetId();
        this.planetController = controller;
        this.data = Managers.Resources.DownloadData(kind);

        return this;
    }

    private int GetId()
    {
        ID++;
        return ID;
    }
}
