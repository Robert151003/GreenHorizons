using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerEditor : MonoBehaviour
{
    public GameObject PlotPrefab;
    public GameObject GrassPrefab;
    public GameObject Selected;
    public GameObject grid;

    public GameObject playCanvas;
    public GameObject editCanvas;

    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    public GameObject manager;

    public bool editMode;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playCanvas.SetActive(!editMode);
        editCanvas.SetActive(editMode);
        if (editMode)
        {
            //draw ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject obj = hit.collider.gameObject;
                if (Input.GetMouseButton(0))
                {
                    if(hit.transform.CompareTag("Soil") && Selected != null && !obj.GetComponent<SeedChecker>().seed)
                    {
                        Instantiate(Selected, hit.transform.position, Quaternion.identity, grid.transform);
                        Destroy(hit.transform.parent.gameObject);
                    }
                    else if(hit.transform.CompareTag("Grass") && Selected != null)
                    {
                        Instantiate(Selected, hit.transform.position, Quaternion.identity, grid.transform);
                        Destroy(hit.transform.gameObject);
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
                if ((highlight.CompareTag("Seed") || highlight.CompareTag("Grown") || highlight.CompareTag("Soil") || highlight.CompareTag("Wilted") || highlight.CompareTag("House") || highlight.CompareTag("Grass")) && highlight != selection)
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

    }

    public void equipGrass()
    {
        Selected = GrassPrefab;
    }
    public void equipSoil()
    {
        Selected = PlotPrefab;
    }
    public void _editMode()
    {
        
        Cursor.SetCursor(manager.GetComponent<Manager>().mouse, Vector2.zero, CursorMode.Auto);
        manager.GetComponent<Manager>().unequip();
        editMode = !editMode;
    }
}
