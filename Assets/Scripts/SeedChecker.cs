using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeedChecker : MonoBehaviour
{
    public bool hitObj;
    public bool hoed = false;
    public bool watered;
    public bool seed;
    public bool empty;

    public float waterTimer = 20f;

    public Material hoedSoil;
    public Material wateredSoil;
    public Material soil;

    void Start()
    {
    }

    void Update()
    {
        //draw ray to check for plants
        Ray ray = new Ray(transform.position - new Vector3(0,2,0), Vector3.up);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 6f, 1 << 0) && !hit.collider.CompareTag("Soil"))
        {
            seed = true;
            hoed = false;
        }
        if (!Physics.Raycast(ray, out hit, 6f, ~(1 << 3)) && !hoed)
        {
            seed = false;
            this.GetComponent<MeshRenderer>().material = soil;
        }

        if (watered)
        {
            waterTimer -= Time.deltaTime;
            if (waterTimer <= 0)
            {
                watered = false;
                if (seed)
                {
                    this.GetComponent<MeshRenderer>().material = hoedSoil;
                }
                else
                {
                    waterTimer = 0;
                    this.GetComponent<MeshRenderer>().material = soil;
                }
                
            }
        }
    }
}
