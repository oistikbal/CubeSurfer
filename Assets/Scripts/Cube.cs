using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cube : MonoBehaviour
{
    public static List<GameObject> m_cubesList = new List<GameObject>();
    public GameObject m_cubePrefab;

    GameObject m_player;
    BoxCollider m_collider;

    public static event Action m_collisionAdd;
    public static event Action<RaycastHit> m_collisionAddHit;

    public static event Action m_collisionObstacle;
    public static event Action<GameObject> m_collisionObstacleGo;

    void Start()
    {
        if (!m_cubesList.Contains(transform.gameObject))
            m_cubesList.Add(transform.gameObject);
        
        m_player = GameObject.Find("player");
        m_collider = m_player.GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision other)
    {
        PlayerCollision(other);
    }

    void PlayerCollision(Collision other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("cube_add"))
        {
            RaycastHit hit;
            Physics.SyncTransforms();
            if (Physics.Raycast(m_collider.transform.position, Vector3.down * 20, out hit, 100 , 1 << LayerMask.NameToLayer("platform")))
            {
                m_collisionAdd?.Invoke();
                m_collisionAddHit.Invoke(hit);
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("cube_obstacle"))
        {
            m_collisionObstacle?.Invoke();
            m_collisionObstacleGo?.Invoke(gameObject);
        }
    }

}
