using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 m_direction;
    GameObject m_platform;

    public float m_speed = 5f;
    public float m_turnSpeed = 3f;

    void Start()
    {
        m_direction = new Vector3();
    }

    void Update()
    {
        PlayerInput();
    }

    void PlayerInput() 
    {
        if (Input.touchCount > 0)
        {
            if(Input.GetTouch(0).position.x < Screen.width / 2) // left
            {
                m_direction = Vector3.left* Time.fixedDeltaTime * m_turnSpeed;
            }
            else                                                //right
            {
                m_direction = Vector3.right* Time.fixedDeltaTime * m_turnSpeed;
            }
        }
        else 
        {
            m_direction = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        GetPlatform();
        Movement(m_direction);
    }

    void GetPlatform() 
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down * 20, out hit, 100, 1 << LayerMask.NameToLayer("platform"));
        m_platform = hit.transform.gameObject;
    }

    void Movement(Vector3 direction) 
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * m_speed + direction);
        foreach(GameObject cube in Cube.m_cubesList) 
        {
            cube.transform.position = new Vector3(transform.position.x, cube.transform.position.y, transform.position.z);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, m_platform.transform.rotation, Time.fixedDeltaTime * 3f);
    }
}
