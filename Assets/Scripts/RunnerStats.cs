using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerStats : MonoBehaviour
{
    public float currentDistance => distance;

    float distance;
    int jewels;
    int obstacles;
    int life;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPV(int pv)
    {
        life = pv;
    }

    public void Progress(float delta)
    {
        distance += delta;
    }

    public void TakeJewel()
    {
        jewels++;
    }

    public void PassObstacle()
    {
        obstacles++;
    }

    public bool Contact()
    {
        life--;

        return life > 0;
    }

}
