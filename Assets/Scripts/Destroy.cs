using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float timer;
    void Start()
    {
        timer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer-= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
