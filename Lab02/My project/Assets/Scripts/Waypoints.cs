using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waypoints
{
    [SerializeField] public Vector3 pos;
    public void SetPos(Vector3 pos)
    {
        this.pos = pos;
    }

    public Vector3 getPos()
    {
        return pos;
    }
    public Waypoints()
    {
        pos = Vector3.zero;
    }
}
