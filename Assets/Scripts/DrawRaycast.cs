using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawRaycast : MonoBehaviour
{
    //pointing at objects
    private Camera camera;
    private RaycastHit hit;
    private Ray ray;

    //highlights
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    public GameObject manager;
    public GameObject editor;

    public Slider toolSlider;
    public float fillTime;

    private float timer;
    public float hoeTimer;
    public float shovelTimer;
    private bool hasPlayedSound = false;
    public bool action = false;


    void Start()
    {
        camera = Camera.main;
        var outline = gameObject.AddComponent<Outline>();
    }


    void Update()
    {
        if (!editor.GetComponent<PlayerEditor>().editMode)
        {

            if (Input.GetMouseButtonDown(0))
            {
                //sets Timers
                if (manager.GetComponent<Manager>().shovel)
                {
                    shovelTimer = manager.GetComponent<Manager>().shovelTimer;
                }
                else if (manager.GetComponent<Manager>().wateringCan)
                {
                    timer = manager.GetComponent<Manager>().waterTimer;
                }
                else if (manager.GetComponent<Manager>().hoe)
                {
                    hoeTimer = manager.GetComponent<Manager>().hoeTimer;
                }
            }



            //draw ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject obj = hit.collider.gameObject;

                if (Input.GetMouseButton(0))
                {

                    //checks if seed is grown
                    if (obj.CompareTag("Grown") && (manager.GetComponent<Manager>().carrots + manager.GetComponent<Manager>().onions < manager.GetComponent<Manager>().maxStorage))
                    {
                        manager.GetComponent<Manager>().addVeg(obj.gameObject.GetComponentInParent<Growing>().vegNum);
                        Destroy(obj.transform.parent.gameObject);
                    }
                    else if (obj.CompareTag("House"))
                    {
                        manager.GetComponent<Manager>().openHome();
                    }
                    //planting
                    else if (manager.GetComponent<Manager>().hand)
                    {
                        if (obj.GetComponent<SeedChecker>().hoed && !obj.GetComponent<SeedChecker>().seed && manager.GetComponent<Manager>().seedEquipped != null)
                        {
                            Instantiate(manager.GetComponent<Manager>().seedEquipped, obj.transform.position + new Vector3(Random.Range(-0.1f,0.1f),0, Random.Range(-0.1f, 0.1f)), Quaternion.Euler(0,Random.Range(0,360),0));
                            obj.GetComponent<SeedChecker>().seed = true;
                            manager.GetComponent<Manager>().totalPlantedSeeds++;
                        }
                    }
                    else
                    {
                        //shows timer for tools to be used
                        if (manager.GetComponent<Manager>().hoe && obj.CompareTag("Soil") && !obj.GetComponent<SeedChecker>().seed && !obj.GetComponent<SeedChecker>().hoed)
                        {
                            if (!hasPlayedSound)
                            {
                                manager.GetComponent<Manager>().toolsAudio.Play();
                                hasPlayedSound=true;
                            }
                            
                            toolSlider.gameObject.SetActive(true);
                            fillTime += 0.375f * Time.deltaTime;

                            toolSlider.GetComponent<Slider>().maxValue = manager.GetComponent<Manager>().hoeTimer;
                            toolSlider.value = hoeTimer;
                            hoeTimer -= Time.deltaTime;
                            if(hoeTimer <= 0)
                            {
                                manager.GetComponent<Manager>().toolsAudio.Pause();
                                toolSlider.gameObject.SetActive(false);
                                if (!action)
                                {
                                    obj.GetComponent<SeedChecker>().hoed = true;
                                    obj.GetComponent<MeshRenderer>().material = obj.GetComponent<SeedChecker>().hoedSoil;
                                    hasPlayedSound = false;
                                    action = true;
                                }
                                
                            }
                        }
                        else if (manager.GetComponent<Manager>().shovel && !obj.CompareTag("Soil") && !obj.CompareTag("Grass"))
                        {
                            if (!hasPlayedSound)
                            {
                                manager.GetComponent<Manager>().toolsAudio.Play();
                                hasPlayedSound = true;
                            }
                            if (obj.CompareTag("Seed"))
                            {
                                manager.GetComponent<Manager>().totalRemovedSeeds++;
                            }
                            toolSlider.gameObject.SetActive(true);
                            fillTime += 0.375f * Time.deltaTime;

                            toolSlider.GetComponent<Slider>().maxValue = manager.GetComponent<Manager>().shovelTimer;
                            toolSlider.value = shovelTimer;
                            shovelTimer -= Time.deltaTime;
                            if (shovelTimer <= 0)
                            {
                                toolSlider.gameObject.SetActive(false);
                                manager.GetComponent<Manager>().toolsAudio.Pause();
                                if (!action)
                                {
                                    Destroy(obj.transform.parent.gameObject);
                                    hasPlayedSound = false;
                                    action = true;
                                }
                               
                            }
                            
                        }
                        else if (obj.CompareTag("Wilted") && (manager.GetComponent<Manager>().shovel || manager.GetComponent<Manager>().hand))
                        {
                            if (!hasPlayedSound)
                            {
                                manager.GetComponent<Manager>().toolsAudio.Play();
                                hasPlayedSound = true;
                            }
                            toolSlider.gameObject.SetActive(true);
                            fillTime += 0.375f * Time.deltaTime;
                            toolSlider.value = Mathf.Lerp(0, 3, fillTime);
                            timer -= Time.deltaTime;
                            if (timer <= 0)
                            {
                                toolSlider.gameObject.SetActive(false);
                                Destroy(obj.transform.parent.gameObject);
                                hasPlayedSound = false;
                            }                           
                        }                        
                        else if (manager.GetComponent<Manager>().wateringCan && obj.gameObject.CompareTag("waterFiller"))
                        {
                            if (!hasPlayedSound)
                            {
                                manager.GetComponent<Manager>().toolsAudio.Play();
                                hasPlayedSound = true;
                            }
                            manager.GetComponent<Manager>().waterFillerSlider.SetActive(true);
                            manager.GetComponent<Manager>().waterFillerSlider.GetComponent<Slider>().value += 1 * Time.deltaTime;
                            toolSlider.GetComponent<Slider>().maxValue = manager.GetComponent<Manager>().waterCanMax;
                            toolSlider.value = Mathf.Lerp(0, manager.GetComponent<Manager>().waterCanMax, fillTime);
                            timer -= Time.deltaTime;
                            if (timer <= 0)
                            {
                                manager.GetComponent<Manager>().waterFillerSlider.SetActive(false);
                                manager.GetComponent<Manager>().toolsAudio.Pause();
                                manager.GetComponent<Manager>().waterAmount = manager.GetComponent<Manager>().waterCanMax;
                            }                               
                        }
                        else if (manager.GetComponent<Manager>().wateringCan && obj.GetComponent<SeedChecker>().seed && manager.GetComponent<Manager>().waterAmount > 0)
                        {
                            manager.GetComponent<Manager>().waterAmountLeft.gameObject.SetActive(true);
                            if (!hasPlayedSound)
                            {
                                manager.GetComponent<Manager>().toolsAudio.Play();
                                hasPlayedSound = true;
                            }

                            toolSlider.gameObject.SetActive(true);
                            fillTime += 0.375f * Time.deltaTime;

                            toolSlider.GetComponent<Slider>().maxValue = manager.GetComponent<Manager>().waterCanMax;
                            toolSlider.value = Mathf.Lerp(0, manager.GetComponent<Manager>().waterCanMax, fillTime);
                            timer -= Time.deltaTime;
                            if (timer <= 0)
                            {
                                toolSlider.gameObject.SetActive(false);
                                manager.GetComponent<Manager>().waterAmountLeft.gameObject.SetActive(false);
                                hasPlayedSound = false;
                                manager.GetComponent<Manager>().toolsAudio.Pause();

                                if (manager.GetComponent<Manager>().waterAmount > 0)
                                {

                                    if (!action) {
                                        manager.GetComponent<Manager>().waterAmount -= 1;
                                        
                                        if (obj.GetComponent<SeedChecker>().watered)
                                        {
                                            obj.GetComponent<SeedChecker>().waterTimer = 20f;
                                        }
                                        else
                                        {
                                            obj.GetComponent<SeedChecker>().watered = true;
                                            obj.GetComponent<MeshRenderer>().material = obj.GetComponent<SeedChecker>().wateredSoil;
                                            obj.GetComponent<SeedChecker>().waterTimer = 20f;
                                        }
                                        action = true;
                                    }
                                    
                                    
                                }
                            }
                            
                        }
                        
                        else { }
                    }
                }

                
                
            }

            //highlight
            if (highlight != null)
            {
                highlight.gameObject.GetComponent<Outline>().enabled = false;
                highlight = null;
            }
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit, Mathf.Infinity))
            {
                highlight = raycastHit.transform;
                if ((highlight.CompareTag("Seed") || highlight.CompareTag("waterFiller") || highlight.CompareTag("Grown") || highlight.CompareTag("Soil") || highlight.CompareTag("Wilted") || highlight.CompareTag("House") || highlight.CompareTag("Grass")) && highlight != selection)
                {
                    if (highlight.gameObject.GetComponent<Outline>() != null)
                    {
                        highlight.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        if (highlight.CompareTag("Soil"))
                        {
                            Outline outline = highlight.GetComponent<Outline>();
                            outline.enabled = true;
                            highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.blue;
                            highlight.gameObject.GetComponent<Outline>().OutlineWidth = 1f;
                        }
                        else
                        {
                            Outline outline = highlight.GetComponent<Outline>();
                            outline.enabled = true;
                            highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
                            highlight.gameObject.GetComponent<Outline>().OutlineWidth = 1f;
                        }

                    }
                }
                else
                {
                    highlight = null;
                }
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            toolSlider.gameObject.SetActive(false);
            fillTime = 0;
            hasPlayedSound = false;
            manager.GetComponent<Manager>().toolsAudio.Stop();
            manager.GetComponent<Manager>().waterAmountLeft.gameObject.SetActive(false);
            manager.GetComponent<Manager>().waterFillerSlider.SetActive(false);
            action = false;
        }
        

    }
}
