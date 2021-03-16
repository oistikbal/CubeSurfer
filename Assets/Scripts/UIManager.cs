using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum GameState { MAIN_MENU, PLAYING, PAUSED, FINISH }
    public static GameState m_gameState = GameState.PLAYING;

    [SerializeField]Text m_score; 

    void Start()
    {
        Cube.e_collisionAdd += OnCollisionAdd;
        Cube.e_collisionObstacle += OnCollisionObstacle;
    }

    void OnCollisionAdd() 
    {
        m_score.text = (Cube.m_cubesList.Count).ToString();
    }

    void OnCollisionObstacle()
    {
        m_score.text = (Cube.m_cubesList.Count).ToString();

        if (Cube.m_cubesList.Count == 0)
            m_gameState = GameState.PAUSED;
    }
}
