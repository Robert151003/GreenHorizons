using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Growing : MonoBehaviour
{
    [Header("Timers")]
    public float timer;
    public float wiltTimer;

    [Header("Growing")]
    public GameObject seed;
    public GameObject Grown;
    public GameObject Wilted;
    public GameObject manager;
    public GameObject growTimer;
    public GameObject growTimerBackground;
    public GameObject growTimerFill;
    public bool isGrown;
    public bool watered;

    public GameObject wiltNotify;

    public LayerMask layerMask;

    //have a number for each vegetable. have manager access this and from a list choose what veg/value it is
    public int vegNum;
    
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        growTimer.gameObject.transform.rotation = Quaternion.Euler(0, 180, 180);
        growTimer.GetComponent<Slider>().maxValue = timer;
        growTimer.GetComponent<Slider>().value = timer;
    }

    
    void Update()
    {
        //draw ray
        Ray ray = new Ray(transform.position + new Vector3(0,2,0), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 6, layerMask))
        {
            watered = hit.collider.gameObject.GetComponent<SeedChecker>().watered;
        }

        if (watered)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                growTimer.GetComponent<Slider>().value = timer;              
            }
            else
            {
                Grown.SetActive(true);
                seed.SetActive(false);
                growTimerBackground.GetComponent<Image>().color = Color.white;
                growTimerFill.GetComponent<Image>().color = new Color(1, 0.4117647f, 0.3803922f);
                growTimer.GetComponent<Slider>().maxValue = 60;
                growTimer.GetComponent<Slider>().value = wiltTimer;
                isGrown = true;
            }
        }

        

        if (isGrown)
        {
            timer = 0;
            if (wiltTimer > 0)
            {
                if(wiltTimer < 10)
                {
                    wiltNotify.SetActive(true);
                }
                wiltTimer -= Time.deltaTime;
                growTimer.GetComponent<Slider>().value = wiltTimer;
            }           
            else
            {
                Wilted.SetActive(true);
                Grown.SetActive(false);
                growTimer.SetActive(false);
                isGrown = false;
                wiltNotify.SetActive(false);
            }
        }

        
    }

    void addCarrot()
    {
        manager.GetComponent<Manager>().carrots++;
    }

}
