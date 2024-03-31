using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class managerData
{
    [Header("Stats")]
    public int money;
    public int level;
    public int xp;
    public int maxXp;
    public int harvestedCarrots;
    public int harvestedOnions;
    public int totalDeliveries;
    public int totalRemovedSeeds;

    [Header("Storage")]
    public int carrots;
    public int onions;
    public int maxStorage;
    public int maxStorageLevel;
    public int storageUpgradeCost;

    [Header("Truck")]
    public int truckLevel;
    public int truckCost;
    public int deliverySize;

    [Header("Shovel")]
    public float shovelTimer;
    public int shovelLevel;
    public int shovelCost;

    [Header("Hoe")]
    public float hoeTimer;
    public int hoeLevel;
    public int hoeCost;

    [Header("Watering Can")]
    public float waterTimer;
    public int waterCanMax;
    public int waterAmount;
    public int wateringSpeedLevel;
    public int wateringSpeedCost;
    public int wateringAmountLevel;
    public int wateringAmountCost;

    [Header("Tutorial")]
    public bool tutorial;

    [Header("Achivements")]
    public bool hundredCarrtots;
    public bool thousandCarrots;
    public bool tenthousandCarrots;

    public bool hundredOnions;
    public bool thousandOnions;
    public bool tenthousandOnions;

    public bool hundredVeg;
    public bool thousandVeg;
    public bool tenthousandVeg;

    public bool playForInGameYear;
    public bool playInGameYearFive;

    public bool completeTutorial;
    public bool visitTown;

    public bool firstDelivery;

    public bool makeTenDeliveries;
    public bool makeHundredDeliveries;

    public bool maxLevelHoe;
    public bool maxLevelShovel;
    public bool maxLevelWaterLimit;
    public bool maxLevelWaterSpeed;
    public bool maxLevelStorage;
    public bool maxLevelDeliveries;

    //secret
    public bool makeAllLandSoil;
    public bool plant500Seeds;
    public bool remove500Seeds;

    public managerData (Manager manager)
    {
        //stats
        money = manager.money;
        level = manager.level;
        xp = manager.xp;
        maxXp = manager.maxXp;
        totalDeliveries = manager.totalDeliveries;
        harvestedCarrots = manager.harvestedVeg[0];
        harvestedOnions = manager.harvestedVeg[1];
        totalRemovedSeeds = manager.totalRemovedSeeds;

        //storage
        carrots = manager.vegetables[0];
        onions = manager.vegetables[1];
        maxStorage = manager.maxStorage;
        maxStorageLevel = manager.maxStorageLevel;
        storageUpgradeCost = manager.storageUpgradeCost;

        //truck
        truckLevel = manager.truckLevel;        
        truckCost = manager.truckCost;
        deliverySize = manager.deliverySize;

        //shovel
        shovelCost = manager.shovelCost;
        shovelLevel = manager.shovelLevel;
        shovelTimer = manager.shovelTimer;

        //hoe
        hoeTimer = manager.hoeTimer;
        hoeLevel = manager.hoeLevel;
        hoeCost = manager.hoeCost;

        //watering can
        waterTimer = manager.waterTimer;
        waterCanMax = manager.waterCanMax;
        waterAmount = manager.waterAmount;
        wateringSpeedLevel = manager.wateringSpeedLevel;
        wateringSpeedCost = manager.wateringSpeedCost;
        wateringAmountLevel = manager.wateringAmountLevel;
        wateringAmountCost = manager.wateringAmountCost;

        //tutorial
        tutorial = manager.tutorial;

        //achievements
        hundredCarrtots = manager.GetComponent<Achievements>().hundredCarrots;
        thousandCarrots = manager.GetComponent<Achievements>().thousandCarrots;
        tenthousandCarrots = manager.GetComponent<Achievements>().tenthousandCarrots;

        hundredOnions = manager.GetComponent<Achievements>().hundredCarrots;
        thousandOnions = manager.GetComponent<Achievements>().thousandCarrots;
        tenthousandOnions = manager.GetComponent<Achievements>().tenthousandOnions;

        hundredVeg = manager.GetComponent<Achievements>().hundredVeg;
        thousandVeg = manager.GetComponent<Achievements>().thousandVeg;
        tenthousandVeg = manager.GetComponent<Achievements>().tenthousandVeg;

        playForInGameYear = manager.GetComponent<Achievements>().playForInGameYear;
        playInGameYearFive = manager.GetComponent<Achievements>().playInGameYearFive;

        completeTutorial = manager.GetComponent<Achievements>().completeTutorial;
        visitTown = manager.GetComponent<Achievements>().visitTown;

        firstDelivery = manager.GetComponent<Achievements>().firstDelivery;

        makeTenDeliveries = manager.GetComponent<Achievements>().makeTenDeliveries;
        makeHundredDeliveries = manager.GetComponent<Achievements>().makeHundredDeliveries;

        maxLevelHoe = manager.GetComponent<Achievements>().maxLevelHoe;
        maxLevelShovel = manager.GetComponent<Achievements>().maxLevelShovel;
        maxLevelWaterLimit = manager.GetComponent<Achievements>().maxLevelWaterLimit;
        maxLevelWaterSpeed = manager.GetComponent<Achievements>().maxLevelWaterSpeed;
        maxLevelStorage = manager.GetComponent<Achievements>().maxLevelStorage;
        maxLevelDeliveries = manager.GetComponent<Achievements>().maxLevelDeliveries;

        makeAllLandSoil = manager.GetComponent<Achievements>().makeAllLandSoil;
        plant500Seeds = manager.GetComponent<Achievements>().plant500Seeds;
        remove500Seeds = manager.GetComponent<Achievements>().remove500Seeds;


    }

}
