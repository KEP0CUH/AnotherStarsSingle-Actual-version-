using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColliderable
{
    public void OnCollide(Collider collider);
}
