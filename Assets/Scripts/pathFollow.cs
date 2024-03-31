using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFollow : MonoBehaviour
{
    private bool point1;
    private bool point2;
    private bool point3;
    private bool point4;
    private bool point5;
    private bool point6;
    private float timer = 3f;

    public GameObject manager;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!point1)
        {
            transform.position = Vector3.MoveTowards(transform.position, manager.GetComponent<Manager>().point1.position, 0.4f);
            if(transform.position == manager.GetComponent<Manager>().point1.position)
            {
                point1 = true;
            }
        }
        else if (!point2)
        {
            transform.position = Vector3.MoveTowards(transform.position, manager.GetComponent<Manager>().point2.position, 0.3f);
            if (transform.position == manager.GetComponent<Manager>().point2.position)
            {
                point2 = true;
            }
        }
        else if(!point3)
        {
            transform.position = Vector3.MoveTowards(transform.position, manager.GetComponent<Manager>().point3.position, 0.2f);
            if(transform.rotation.y < 180)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,180,0), Time.deltaTime*1.8f);
            }
            
            if (transform.position == manager.GetComponent<Manager>().point3.position)
            {
                point3 = true;
            }
        }
        else if (!point4)
        {
            transform.position = Vector3.MoveTowards(transform.position, manager.GetComponent<Manager>().point4.position, 0.1f);
            if (transform.rotation.y < 180)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 2.5f);
            }

            if (transform.position == manager.GetComponent<Manager>().point4.position)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    manager.GetComponent<Manager>().pay(0);
                    point4 = true;
                }
                
            }
        }
        else if (!point5)
        {
            transform.position = Vector3.MoveTowards(transform.position, manager.GetComponent<Manager>().point5.position, 0.2f);
            if (transform.rotation.y < 180)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 2.5f);
            }

            if (transform.position == manager.GetComponent<Manager>().point5.position)
            {
                point5 = true;
            }
        }
        else if (!point6)
        {
            transform.position = Vector3.MoveTowards(transform.position, manager.GetComponent<Manager>().point6.position, 0.3f);
            if (transform.rotation.y < 180)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 2.6f);
            }

            if (transform.position == manager.GetComponent<Manager>().point6.position)
            {
                point6 = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, manager.GetComponent<Manager>().point7.position, 0.4f);
            if (transform.position == manager.GetComponent<Manager>().point7.position)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
