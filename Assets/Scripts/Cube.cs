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

    void Start()
    {
        if (!m_cubesList.Contains(transform.gameObject))
            m_cubesList.Add(transform.gameObject);
        if (m_collisionAdd == null)
            m_collisionAdd += AddCube;

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
            DeleteCube();
        }
    }

    public void AddCube()
    {
        GameObject newCube = Instantiate(m_cubePrefab, GameObject.Find("player").transform);
        if (!m_cubesList.Contains(newCube.transform.gameObject))
             m_cubesList.Add(newCube.transform.gameObject);
    }

    public void DeleteCube() 
    {
        transform.parent = null;
        m_cubesList.Remove(transform.gameObject);
        m_collisionAdd -= AddCube;
        Destroy(transform.gameObject, 2f);
    }
}
