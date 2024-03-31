using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    public bool hundredCarrots;//
    public GameObject hundredCarrotsPic;
    public bool thousandCarrots;//
    public GameObject thousandCarrotsPic;
    public bool tenthousandCarrots;//
    public GameObject tenthousandCarrotsPic;

    public bool hundredOnions;
    public bool thousandOnions;
    public bool tenthousandOnions;

    public bool hundredVeg;
    public bool thousandVeg;
    public bool tenthousandVeg;

    public bool playForInGameYear;
    public bool playInGameYearFive;

    public bool completeTutorial; //
    public GameObject completeTutorialPic;
    public bool visitTown; 

    public bool firstDelivery; //
    public GameObject firstDeliveryPic;
    public bool makeTenDeliveries;//
    public GameObject tenDeliveryPic;
    public bool makeHundredDeliveries;//
    public GameObject hundredDeliveryPic;

    public bool maxLevelHoe;
    public bool maxLevelShovel;
    public bool maxLevelWaterLimit;
    public bool maxLevelWaterSpeed;
    public bool maxLevelStorage;
    public bool maxLevelDeliveries;


    //Secret
    public bool makeAllLandSoil;
    public bool plant500Seeds;
    public bool remove500Seeds;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hundredCarrots)
        {
            hundredCarrotsPic.SetActive(true);
        }
        if (thousandCarrots)
        {
            thousandCarrotsPic.SetActive(true);
        }
        if (tenthousandCarrots)
        {
            tenthousandCarrotsPic.SetActive(true);
        }

        if (completeTutorial)
        {
            completeTutorialPic.SetActive(true);
        }

        if (firstDelivery)
        {
            firstDeliveryPic.SetActive(true);
        }
        if (makeTenDeliveries)
        {
            tenDeliveryPic.SetActive(true);
        }
        if (makeHundredDeliveries)
        {
            hundredDeliveryPic.SetActive(true);
        }
        
    }
}
