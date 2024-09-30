using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public static Runner instance = null;
    public float speed => currentSpeed;

    [SerializeField] int pv;
    [SerializeField] float currentSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float difficultySteps;
    [SerializeField] int tileForward;
    [SerializeField] float tileSize;
    [SerializeField] RunnerStats stats;
    [SerializeField] Transform level;
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject tile;


    int difficulty;
    int nextObstacle;
    float lastObstacle;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        nextObstacle = 0;
        lastObstacle = 0;
        stats.SetPV(pv);
    }

    private void Start()
    {
        for (int i = -1; i <= tileForward; i++)
        {
            Instantiate(i < 0 ? obstacle : tile, level.position + Vector3.right * tileSize * (i+0.5f), Quaternion.identity, level);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float oldSpeedStep = currentSpeed * Time.fixedDeltaTime;
        float step = (tileSize * (tileSize/currentSpeed));
        currentSpeed = Mathf.Min(maxSpeed, currentSpeed+acceleration * Time.fixedDeltaTime);
        difficulty = Mathf.CeilToInt(currentSpeed / difficultySteps);

        lastObstacle += currentSpeed * Time.fixedDeltaTime;

        if (lastObstacle >= step)
        {
            if (level.childCount < 30)
            {
                nextObstacle--;

                if (nextObstacle < 0)
                {
                    Instantiate(obstacle, level.GetChild(level.childCount - 1).position + Vector3.right * (tileSize - (lastObstacle % step) - oldSpeedStep), Quaternion.identity, level);
                    nextObstacle = difficulty;
                }
                else
                {
                    Instantiate(tile, level.GetChild(level.childCount - 1).position + Vector3.right * (tileSize - (lastObstacle % step) - oldSpeedStep), Quaternion.identity, level);
                }
            }

            lastObstacle %= step;
        }
    }

    public bool Hit()
    {
        if (stats.Contact())
        {
            currentSpeed *= 0.75f;
            return true;
        }
        else
        {
            // TODO: Death screen
            currentSpeed = 0;
            enabled = false;
        }

        return false;
    }
}
