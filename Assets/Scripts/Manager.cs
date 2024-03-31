using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine.Audio;

[System.Serializable]
public class Manager : MonoBehaviour
{
    [Header("Stats")]
    public int level;
    public int xp;
    public int maxXp;
    public Slider xpSlider;
    public int money;
    public TMP_Text moneyText;
    public TMP_Text levelText;
    public List<int> harvestedVeg = new List<int>();
    public int totalDeliveries;
    public int totalRemovedSeeds;
    public int totalPlantedSeeds;

    [Header("Storage")]
    public int carrots;
    private int carrotValue = 5;
    public int onions;
    private int onionValue = 8;
    public int maxStorage;
    public int maxStorageLevel;
    public int storageUpgradeCost;
    public TMP_Text maxStorageLevelText;
    public TMP_Text maxStorageUpgradeButtonText;
    public Button maxStorageUpgradeButton;


    [Header("Tool")]
    public GameObject toolImage;
    public Texture2D mouse;
    public bool hand = true;

    [Header("Shovel")]
    public bool shovel;
    public float shovelTimer;
    public int shovelLevel;
    public int shovelCost;
    public Texture2D gardenShovel;
    public Sprite _gardenShovel;
    public TMP_Text shovelLevelText;
    public TMP_Text shovelUpgradeButtonText;
    public Button shovelUpgradeButton;


    [Header("Hoe")]
    public bool hoe;
    public float hoeTimer;
    public int hoeLevel;
    public int hoeCost;
    public Texture2D gardenHoe;
    public Sprite _gardenHoe;
    public TMP_Text hoeLevelText;
    public TMP_Text hoeUpgradeButtonText;
    public Button hoeUpgradeButton;

    [Header("Wateringcan")]
    public bool wateringCan;
    public float waterTimer;
    public int waterCanMax;
    public int waterAmount;
    public int wateringSpeedLevel;
    public int wateringSpeedCost;
    public int wateringAmountLevel;
    public int wateringAmountCost;
    public TMP_Text wateringSpeedLevelText;
    public TMP_Text wateringSpeedUpgradeButtonText;
    public Button wateringSpeedUpgradeButton;
    public TMP_Text wateringAmountLevelText;
    public TMP_Text wateringAmountUpgradeButtonText;
    public Button wateringAmountUpgradeButton;
    public Texture2D gardenWateringCan;
    public Sprite _gardenWateringCan;
    public TMP_Text waterAmountLeft;
    public GameObject waterFillerSlider;
    public GameObject waterFillNotify;
    public GameObject waterEmptyNotify;

    [Header("Veg")]
    public GameObject carrot;
    public GameObject onion;
    public TMP_Text carrotAmount;
    public TMP_Text onionAmount;
    public TMP_Text TotalAmount;
    public GameObject seedEquipped;
    public GameObject onionSeedPacket;
    public List<int> vegetables = new List<int>();
    public List<int> deliveryVegetables = new List<int>();
    public List<int> vegetableValue = new List<int>();

    [Header("UI")]
    public GameObject pauseMenuUI;
    public GameObject homeUI;
    public GameObject achievementMenu;
    public GameObject settingsMenu;

    [Header("Audio")]
    public AudioSource toolsAudio;
    public AudioSource buttonAudio;
    public AudioClip buttonPress, digging, watering, hoeing, wateringCanFill;

    [Header("Truck")]
    public GameObject truck;
    public Transform spawnPoint;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public Transform point5;
    public Transform point6;
    public Transform point7;
    public bool delivering;
    public GameObject DeliveryUI;
    public TMP_Text carrotCount;
    public TMP_Text onionCount;
    public int truckLevel;
    public int truckCost;
    public int deliverySize;
    public TMP_Text truckUpgradeButtonText;
    public TMP_Text truckLevelText;
    public Button truckUpgradeButton;
    public TMP_Text deliveryAmount;
    

    [Header("Market")]
    public GameObject marketCanvas;
    public GameObject shopCanvas;
    public GameObject toolsUpgradeCanvas;
    public GameObject seedShopCanvas;

    [Header("Seeds")]
    public GameObject seedCanvas;

    [Header("Saving")]
    public float saveTimer = 2f;

    [Header("Tutorial")]
    public bool tutorial;
    public List<GameObject> tutorialScreens;
    public GameObject tutorialCanvas;
    public int screenNum;

    [Header("Settings")]
    public bool fullScreen;
    public TMP_Text windowMode;
    public bool usingEdgeScrolling;
    public GameObject camera;
    public AudioMixer masterVolume;
    public GameObject volumeSlider;

    public void Awake()
    {
        loadData();
    }

    private void Start()
    {
        hand = true;
        fullScreen = true;
        usingEdgeScrolling = false;
        Cursor.SetCursor(mouse, Vector3.zero, CursorMode.Auto);
        
        level1();
    }

    private void Update()
    {
        if (tutorial)
        {
            tutorialCanvas.SetActive(true);
        }

        saveTimer -= Time.deltaTime;
        if(saveTimer <= 0)
        {
            saveData();
            Debug.Log("saved");
            saveTimer = 5f;
        }


        //Market Strings\\
        shovelLevelText.text = (shovelLevel + 1).ToString();
        if(shovelLevel != 9)
        {
            shovelUpgradeButtonText.text = shovelCost.ToString();
        }
        else
        {
            shovelUpgradeButtonText.text = "Max Level";
        }
        
        
        truckLevelText.text = (truckLevel + 1).ToString();
        if(truckLevel != 9)
        {
            truckUpgradeButtonText.text = truckCost.ToString();
        }
        else
        {
            truckUpgradeButtonText.text = "Max Level";
        }
        

        hoeLevelText.text = (hoeLevel+1).ToString();
        if (hoeLevel != 9)
        {
            hoeUpgradeButtonText.text = hoeCost.ToString();
        }
        else
        {
            hoeUpgradeButtonText.text = "Max Level";
        }
        

        wateringAmountLevelText.text = (wateringAmountLevel + 1).ToString();
        if(wateringAmountLevel != 9)
        {
            wateringAmountUpgradeButtonText.text = wateringAmountCost.ToString();
        }
        else
        {
            wateringAmountUpgradeButtonText.text = "Max Level";
        }
        

        wateringSpeedLevelText.text = (wateringSpeedLevel+1).ToString();
        if(wateringSpeedLevel != 9)
        {
            wateringSpeedUpgradeButtonText.text = wateringSpeedCost.ToString();
        }
        else
        {
            wateringSpeedUpgradeButtonText.text = "Max Level";
        }

        maxStorageLevelText.text = (maxStorageLevel + 1).ToString();
        if(maxStorageLevel != 4)
        {
            maxStorageUpgradeButtonText.text = storageUpgradeCost.ToString();
        }
        else
        {
            maxStorageUpgradeButtonText.text = "Max Level";
        }
        
        //UI Elements\\
        xpValue();
        levelText.text = level.ToString();
        moneyText.text = money.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenuUI.activeInHierarchy)
            {
                closePauseMenu();
            }
            else
            {
                PauseMenu();
            }            
            closeHome();
            closeDelUI();
            closeMarketMenu();
        }
        
        if (xp >= maxXp)
        {
            int tempXp = xp - maxXp;
            if (level == 1)
            {
                level2();
            }
            else if(level == 2)
            {

            }
            
            xp = 0 + tempXp;
        }

        moneyText.text = money.ToString();

        //Updating Info\\
        carrots = vegetables[0];
        onions = vegetables[1];

        carrotAmount.text = carrots.ToString();
        onionAmount.text = onions.ToString();
        
        TotalAmount.text = (carrots+onions).ToString() + "/" + maxStorage.ToString();

        carrotCount.text = deliveryVegetables[0].ToString();
        onionCount.text = deliveryVegetables[1].ToString();

        waterAmountLeft.text = waterAmount.ToString();

        if(hoeCost <= money)
        {
            hoeUpgradeButton.GetComponent<CanvasGroup>().alpha = 0.8f;
        }
        else
        {
            hoeUpgradeButton.GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        if (shovelCost <= money)
        {
            shovelUpgradeButton.GetComponent<CanvasGroup>().alpha = 0.8f;
        }
        else
        {
            shovelUpgradeButton.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
        
        if (truckCost <= money)
        {
            truckUpgradeButton.GetComponent<CanvasGroup>().alpha = 0.8f;
        }
        else
        {
            truckUpgradeButton.GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        if (wateringSpeedCost <= money)
        {
            wateringSpeedUpgradeButton.GetComponent<CanvasGroup>().alpha = 0.8f;
        }
        else
        {
            wateringSpeedUpgradeButton.GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        if (wateringAmountCost <= money)
        {
            wateringAmountUpgradeButton.GetComponent<CanvasGroup>().alpha = 0.8f;
        }
        else
        {
            wateringAmountUpgradeButton.GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        int totalDelivering = 0;
        for (int i = 0; i < deliveryVegetables.Count; i++)
        {
            totalDelivering += deliveryVegetables[i];
        }
        deliveryAmount.text = (totalDelivering.ToString()) + "/" + (deliverySize.ToString());

        //water notification
        if(waterAmount < 3 && waterAmount > 0)
        {
            waterFillNotify.SetActive(true);
        }
        else if(waterAmount == 0)
        {
            waterFillNotify.SetActive(false);
            waterEmptyNotify.SetActive(true);
        }
        else
        {
            waterFillNotify.SetActive(false);
            waterEmptyNotify.SetActive(false);
        }

        //Achievements\\
        //carrots
        if (harvestedVeg[0] > 100 && !this.GetComponent<Achievements>().hundredCarrots)
        {
            this.GetComponent<Achievements>().hundredCarrots = true;
        }
        else if (harvestedVeg[0] > 1000 && !this.GetComponent<Achievements>().thousandCarrots)
        {
            this.GetComponent<Achievements>().thousandCarrots = true;
        }
        else if (harvestedVeg[0] > 10000 && !this.GetComponent<Achievements>().tenthousandCarrots)
        {
            this.GetComponent<Achievements>().tenthousandCarrots = true;
        }
        //onions
        if (harvestedVeg[1] > 100 && !this.GetComponent<Achievements>().hundredOnions)
        {
            this.GetComponent<Achievements>().hundredOnions = true;
        }
        else if (harvestedVeg[1] > 1000 && !this.GetComponent<Achievements>().thousandOnions)
        {
            this.GetComponent<Achievements>().thousandOnions = true;
        }
        else if (harvestedVeg[1] > 10000 && !this.GetComponent<Achievements>().tenthousandOnions)
        {
            this.GetComponent<Achievements>().tenthousandOnions = true;
        }
        //allVeg
        if ((harvestedVeg[1] + harvestedVeg[0]) > 100 && !this.GetComponent<Achievements>().hundredVeg)
        {
            this.GetComponent<Achievements>().hundredVeg = true;
        }
        else if ((harvestedVeg[1] + harvestedVeg[0]) > 1000 && !this.GetComponent<Achievements>().thousandVeg)
        {
            this.GetComponent<Achievements>().thousandVeg = true;
        }
        else if ((harvestedVeg[1] + harvestedVeg[0]) > 10000 && !this.GetComponent<Achievements>().tenthousandVeg)
        {
            this.GetComponent<Achievements>().tenthousandVeg = true;
        }
        //deliveries
        if(totalDeliveries > 10 && !this.GetComponent<Achievements>().makeTenDeliveries)
        {
            this.GetComponent<Achievements>().makeTenDeliveries = true;
        }
        else if (totalDeliveries > 100 && !this.GetComponent<Achievements>().makeHundredDeliveries)
        {
            this.GetComponent<Achievements>().makeHundredDeliveries = true;
        }
        //secrets
        if(totalPlantedSeeds >= 500 && !this.GetComponent<Achievements>().plant500Seeds)
        {
            this.GetComponent<Achievements>().plant500Seeds = true;
        }
        if(totalRemovedSeeds >= 500 && !this.GetComponent<Achievements>().remove500Seeds)
        {
            this.GetComponent<Achievements>().remove500Seeds = true;
        }

    }

    public void equipShovel()
    {
        noTools();
        shovel = true;
        toolsAudio.clip = digging;
        Vector2 cursorHotspot = new Vector2(gardenShovel.width / 2, gardenShovel.height / 2);
        Cursor.SetCursor(gardenShovel, cursorHotspot, CursorMode.Auto);
        toolImage.GetComponent<Image>().sprite = _gardenShovel;
    }
    public void equipWateringCan()
    {
        noTools();
        wateringCan = true;
        toolsAudio.clip = watering;
        Vector2 cursorHotspot = new Vector2(gardenWateringCan.width / 2, gardenWateringCan.height / 2);
        Cursor.SetCursor(gardenWateringCan, cursorHotspot, CursorMode.Auto);
        toolImage.GetComponent<Image>().sprite = _gardenWateringCan;
    }
    public void equipHoe()
    {
        noTools();
        hoe = true;
        toolsAudio.clip = hoeing;
        Vector2 cursorHotspot = new Vector2(gardenHoe.width / 2, gardenHoe.height / 2);
        Cursor.SetCursor(gardenHoe, cursorHotspot, CursorMode.Auto);
        toolImage.GetComponent<Image>().sprite = _gardenHoe;
    }
    public void unequip()
    {
        noTools();
        hand = true;
        toolsAudio.clip = null;
        Cursor.SetCursor(mouse, Vector3.zero, CursorMode.Auto);
    }
    public void carrotEquip()
    {
        seedEquipped = carrot;
        unequip();
    }
    public void onionEquip()
    {
        seedEquipped = onion;
        unequip();
    }
    public void openHome()
    {
        if(!DeliveryUI.activeInHierarchy && !pauseMenuUI.activeInHierarchy && !this.GetComponent<Manual>().manualCanvas.activeInHierarchy && !marketCanvas.activeInHierarchy)
        {
            homeUI.SetActive(true);
            Cursor.SetCursor(mouse, Vector3.zero, CursorMode.Auto);
        }
        
    }
    public void closeHome()
    {
        homeUI.SetActive(false);
    }
    public void addVeg(int vegNum)
    {
        vegetables[vegNum]++;
        harvestedVeg[vegNum]++;
    }
    public void noTools()
    {
        hand = false;
        shovel = false;
        wateringCan = false;
        hoe = false;
    }
    public void delivery()
    {
        if ((deliveryVegetables[0] > 0 || deliveryVegetables[1] > 0) && !delivering)
        {
            delivering = true;
            this.GetComponent<Achievements>().firstDelivery = true;
            Instantiate(truck, spawnPoint.transform.position, Quaternion.Euler(0, 90, 0));
            totalDeliveries += 1;
        }
        
    }
    public void addValue(int vegNum)
    {
        int totalDelivery = 0;
        for (int i = 0; i < deliveryVegetables.Count; i++)
        {
            
            totalDelivery += deliveryVegetables[i];
        }
        if(totalDelivery < 20)
        {
            if (vegetables[vegNum] != deliveryVegetables[vegNum])
            {
                deliveryVegetables[vegNum]++;
            }
                
        }
        
    }
    public void subtractValue(int vegNum)
    {
        if (deliveryVegetables[vegNum] != 0)
        {
            deliveryVegetables[vegNum]--;
        }
        
    }
    public void delUI()
    {
        if(!homeUI.activeInHierarchy && !pauseMenuUI.activeInHierarchy && !this.GetComponent<Manual>().manualCanvas.activeInHierarchy && !marketCanvas.activeInHierarchy)
        {
            DeliveryUI.SetActive(true);
            Cursor.SetCursor(mouse, Vector3.zero, CursorMode.Auto);
        }       
    }
    public void closeDelUI()
    {
        DeliveryUI.SetActive(false);
    }
    public void pay(int vegNum)
    {
        for (int i = 0; i < vegetables.Count; i++)
        {
            money += deliveryVegetables[i] * vegetableValue[i];
            xp += (deliveryVegetables[i] * vegetableValue[i]) / 2;
            vegetables[i] -= deliveryVegetables[i];
            deliveryVegetables[i] = 0;
        }
        delivering = false;
    }
    public void xpValue()
    {
        xpSlider.value = xp;
        xpSlider.maxValue = maxXp;
    }

    //Pause Menu
    public void PauseMenu()
    {
        if(!homeUI.activeInHierarchy && !DeliveryUI.activeInHierarchy && !this.GetComponent<Manual>().manualCanvas.activeInHierarchy && !marketCanvas.activeInHierarchy)
        {
            pauseMenuUI.SetActive(true);
            Cursor.SetCursor(mouse, Vector3.zero, CursorMode.Auto);
            Time.timeScale = 0;
        }
        
    }
    public void closePauseMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    //Achievement Menu
    public void AchievementMenu()
    {
        achievementMenu.SetActive(true);
    }
    public void closeAchievementMenu()
    {
        achievementMenu.SetActive(false);
    }

    //Settings Menu
    public void openSettings()
    {
        settingsMenu.SetActive(true);
    }
    public void closeSettings()
    {
        settingsMenu.SetActive(false);
    }

    //market UI
    public void marketMenu()
    {
        if(!homeUI.activeInHierarchy && !DeliveryUI.activeInHierarchy && !this.GetComponent<Manual>().manualCanvas.activeInHierarchy && !pauseMenuUI.activeInHierarchy)
        {
            marketCanvas.SetActive(true);
            this.GetComponent<Achievements>().visitTown = true;
        }
        
    }
    public void closeMarketMenu()
    {
        toolsUpgradeCanvas.SetActive(false);
        shopCanvas.SetActive(true);
        marketCanvas.SetActive(false);
    }
    public void openToolMenu()
    {
        shopCanvas.SetActive(false);
        toolsUpgradeCanvas.SetActive(true);
    }

    //seed UI
    public void openSeedUI()
    {
        seedCanvas.SetActive(true);
    }
    public void closeSeedUI()
    {
        seedCanvas.SetActive(false);
    }

    //exitGame
    public void exitGame()
    {
        SceneManager.LoadScene("Menu");
    }

    //Audio
    public void buttonSound()
    {
        buttonAudio.clip = buttonPress;
        buttonAudio.Play(); 
    }

    //Level Manager
    public void level1()
    {
        level = 1;
        maxXp = 50;
    }
    public void level2()
    {
        level = 2;
        maxXp = 100;
        onionSeedPacket.SetActive(true);
    }

    //Tool Level
    public void upgradeShovel()
    {
        if(shovelLevel < 9 && shovelCost <= money)
        {
            money -= shovelCost;
            shovelLevel += 1;
            shovelCost = (shovelLevel + 1) * 8;
            if(shovelLevel == 9)
            {
                shovelTimer = 0.1f;
                this.GetComponent<Achievements>().maxLevelShovel = true;
            }
            else
            {
                shovelTimer = 3f;
                shovelTimer -= (shovelLevel + 1) * 0.3f;
            }
        }
    }
    public void upgradeHoe()
    {
        if (hoeLevel < 9 && hoeCost <= money)
        {
            money -= hoeCost;
            hoeLevel += 1;
            hoeCost = (hoeLevel + 1) * 12;
            if(hoeLevel == 9)
            {
                hoeTimer = 0.1f;
                this.GetComponent<Achievements>().maxLevelHoe = true;
            }
            else
            {
                hoeTimer = 3f;
                hoeTimer -= (hoeLevel + 1) * 0.3f;
            }
            
        }
    }
    public void upgradeTruck()
    {
        if (truckLevel < 9 && truckCost <= money)
        {
            money -= truckCost;
            deliverySize *= 2;
            truckLevel += 1;
            truckCost = (truckLevel+1) * 25;
            if(truckLevel == 9)
            {
                this.GetComponent<Achievements>().maxLevelDeliveries = true;
            }
        }
    }
    public void upgradeWaterSpeed()
    {
        if (wateringSpeedLevel < 9 && wateringSpeedCost <= money)
        {
            money -= wateringSpeedCost;
            wateringSpeedLevel += 1;
            wateringSpeedCost = (wateringSpeedLevel + 1) * 10;
            if(wateringSpeedLevel == 9)
            {
                waterTimer = 0.1f;
                this.GetComponent<Achievements>().maxLevelWaterSpeed = true;
            }
            else
            {
                waterTimer = 3f;
                waterTimer -= (wateringSpeedLevel + 1) * 0.3f;
            }           
        }
    }
    public void upgradeWaterAmount()
    {
        if (wateringAmountLevel < 9 && wateringAmountCost <= money)
        {
            money -= wateringAmountCost;
            wateringAmountLevel += 1;
            wateringAmountCost = (wateringAmountLevel + 1) * 15;
            waterCanMax += 2;
            if(wateringAmountLevel == 9)
            {
                this.GetComponent<Achievements>().maxLevelWaterLimit = true;
            }
        }
    }
    public void upgradeMaxStorage()
    {
        if (maxStorageLevel < 4 && storageUpgradeCost <= money)
        {
            money -= storageUpgradeCost;
            maxStorageLevel += 1;
            storageUpgradeCost = (maxStorageLevel + 1) * 30;
            maxStorage = (maxStorageLevel + 1) * 40;
            if(maxStorageLevel == 4)
            {
                this.GetComponent<Achievements>().maxLevelStorage = true;
            }
        }
    }

    //settings
    public void screenMode()
    {
        fullScreen = !fullScreen;
        if (fullScreen)
        {
            windowMode.text = "fullscreen";
        }
        else
        {
            windowMode.text = "windowded";
        }
        Screen.fullScreen = fullScreen;
    }
    public void enableEdgeScrolling()
    {
        usingEdgeScrolling = !usingEdgeScrolling;
        camera.GetComponent<Movement>().useEdgeScrolling = usingEdgeScrolling;
    }
    public void audioControl(float sliderValue)
    {
        masterVolume.SetFloat("MainVolume", MathF.Log10(sliderValue)*20);
    }

    //save system
    public void saveData()
    {
        SaveSystem.saveManager(this);
    }
    public void loadData()
    {
        try
        {
            managerData data = SaveSystem.loadManager();

            if (data != null)
            {
                //stats
                money = data.money;
                level = data.level;
                xp = data.xp;
                maxXp = data.maxXp;
                totalDeliveries = data.totalDeliveries;
                harvestedVeg[0] = data.harvestedCarrots;
                harvestedVeg[1] = data.harvestedOnions;
                totalRemovedSeeds = data.totalRemovedSeeds;

                //storage
                vegetables[0] = data.carrots;
                vegetables[1] = data.onions;
                maxStorage = data.maxStorage;
                maxStorageLevel = data.maxStorageLevel;
                storageUpgradeCost = data.storageUpgradeCost;

                //truck
                truckLevel = data.truckLevel;
                truckCost = data.truckCost;
                deliverySize = data.deliverySize;

                //shovel
                shovelCost = data.shovelCost;
                shovelLevel = data.shovelLevel;
                shovelTimer = data.shovelTimer;

                //hoe
                hoeTimer = data.hoeTimer;
                hoeLevel = data.hoeLevel;
                hoeCost = data.hoeCost;

                //watering can
                waterTimer = data.waterTimer;
                waterCanMax = data.waterCanMax;
                waterAmount = data.waterAmount;
                wateringSpeedLevel = data.wateringSpeedLevel;
                wateringSpeedCost = data.wateringSpeedCost;
                wateringAmountLevel = data.wateringAmountLevel;
                wateringAmountCost = data.wateringAmountCost;

                //tutorial
                tutorial = data.tutorial;

                //achievements
                this.GetComponent<Achievements>().hundredCarrots = data.hundredCarrtots;
                this.GetComponent<Achievements>().thousandCarrots = data.thousandCarrots;
                this.GetComponent<Achievements>().tenthousandCarrots = data.tenthousandCarrots;

                this.GetComponent<Achievements>().hundredOnions = data.hundredCarrtots;
                this.GetComponent<Achievements>().thousandOnions = data.thousandCarrots;
                this.GetComponent<Achievements>().tenthousandOnions = data.tenthousandOnions;

                this.GetComponent<Achievements>().hundredVeg = data.hundredVeg;
                this.GetComponent<Achievements>().thousandVeg = data.thousandVeg;
                this.GetComponent<Achievements>().tenthousandVeg = data.tenthousandVeg;

                this.GetComponent<Achievements>().playForInGameYear = data.playForInGameYear;
                this.GetComponent<Achievements>().playInGameYearFive = data.playInGameYearFive;

                this.GetComponent<Achievements>().completeTutorial = data.completeTutorial;
                this.GetComponent<Achievements>().visitTown = data.visitTown;

                this.GetComponent<Achievements>().firstDelivery = data.firstDelivery;

                this.GetComponent<Achievements>().makeTenDeliveries = data.makeTenDeliveries;
                this.GetComponent<Achievements>().makeHundredDeliveries = data.makeHundredDeliveries;

                this.GetComponent<Achievements>().maxLevelHoe = data.maxLevelHoe;
                this.GetComponent<Achievements>().maxLevelShovel = data.maxLevelShovel;
                this.GetComponent<Achievements>().maxLevelWaterLimit = data.maxLevelWaterLimit;
                this.GetComponent<Achievements>().maxLevelWaterSpeed = data.maxLevelWaterSpeed;
                this.GetComponent<Achievements>().maxLevelStorage = data.maxLevelStorage;
                this.GetComponent<Achievements>().maxLevelDeliveries = data.maxLevelDeliveries;

                this.GetComponent<Achievements>().makeAllLandSoil = data.makeAllLandSoil;
                this.GetComponent<Achievements>().plant500Seeds = data.plant500Seeds;
                this.GetComponent<Achievements>().plant500Seeds = data.remove500Seeds;
            }

            else
            {
                Debug.LogWarning("Failed to load data");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading data: " + e.Message);
        }
    }


    //tutorial
    public void tutorialScreen()
    {
        if(screenNum < 9)
        {
            screenNum++;
            tutorialScreens[screenNum].SetActive(true);
            tutorialScreens[screenNum - 1].SetActive(false);
        }
        else
        {
            tutorialScreens[screenNum].SetActive(false);
            tutorialCanvas.SetActive(false);
            tutorial = false;
            this.GetComponent<Achievements>().completeTutorial = true;
        }
        
    }
    public void skipTutorial()
    {
        for (int i = 0; i < tutorialScreens.Count; i++)
        {
            tutorialScreens[i].SetActive(false);
        }
        tutorialCanvas.SetActive(false);
        this.GetComponent<Achievements>().completeTutorial = true;
        tutorial = false;
    }

}