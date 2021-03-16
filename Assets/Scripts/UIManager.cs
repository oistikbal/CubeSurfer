using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    void Start()
    {
        Cube.m_collisionAdd += OnCollisionAdd;
        Cube.m_collisionObstacle += OnCollisionObstacle;
    }

    void OnCollisionAdd() 
    {
    }

    void OnCollisionObstacle()
    {

        if(Cube.m_cubesList.Count == 0)
            Time.timeScale = 0f;
    }
}
