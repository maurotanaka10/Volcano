using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float maxLifeWall;
    public float currentLifeWall;
    public float damagetaken;
    public int pointsToGave;
    public int pointsForDestroy;

    private void Start()
    {
        currentLifeWall = maxLifeWall;
    }

    public void HurtWall(int damageTaken)
    {
        currentLifeWall -= damagetaken;

        if(currentLifeWall <= 0)
        {
            gameObject.SetActive(false);
            GameManager.instance.IncreaseScore(pointsForDestroy);
        }
        GameManager.instance.IncreaseScore(pointsToGave);
    }

    public void RestoreWall()
    {
        maxLifeWall++;
        currentLifeWall = maxLifeWall;
    }
}
