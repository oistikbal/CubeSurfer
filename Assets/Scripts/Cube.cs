using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static List<GameObject> m_cubesList = new List<GameObject>();
    public GameObject m_cubePrefab;

    GameObject m_player;
    BoxCollider m_collider;

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
                Destroy(other.gameObject);
                AddCube();
                SetPlayer(hit);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("cube_obstacle"))
        {
            transform.parent = null;
            m_cubesList.Remove(transform.gameObject);
            Destroy(transform.gameObject, 2f);
        }
    }

    void SetPlayer(RaycastHit hit)
    {
        m_player.transform.position = new Vector3(m_player.transform.position.x, hit.point.y + m_cubesList.Count, m_player.transform.position.z);
        for(int i = 0; i < m_cubesList.Count; i++)
        {
            m_cubesList[i].transform.position = new Vector3(m_player.transform.position.x, hit.collider.transform.position.y + (i + 1), m_player.transform.position.z);
        }
    }
    public void AddCube()
    {
        GameObject newCube = Instantiate(m_cubePrefab, GameObject.Find("player").transform);
        if (!m_cubesList.Contains(newCube.transform.gameObject))
             m_cubesList.Add(newCube.transform.gameObject);
    }
}
