using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    
    public GameObject manualCanvas;
    public GameObject[] pages;
    public int pageNum;
    
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exit();
        }
             
    }

    public void manual()
    {
        if(!this.GetComponent<Manager>().DeliveryUI.activeInHierarchy && !this.GetComponent<Manager>().homeUI.activeInHierarchy && !this.GetComponent<Manager>().pauseMenuUI.activeInHierarchy)
        {
            pageNum = 0;
            Cursor.SetCursor(this.GetComponent<Manager>().mouse, Vector3.zero, CursorMode.Auto);
            manualCanvas.SetActive(true);
            pages[pageNum].SetActive(true);
        }
        
    }
    public void nextPage()
    {
        if(pageNum < pages.Length-1)
        {
            pageNum++;
            pages[pageNum].SetActive(true);
            pages[pageNum - 1].SetActive(false);
        }       
    }
    public void previousPage()
    {
        if(pageNum > 0) 
        {
            pageNum--;
            pages[pageNum].SetActive(true);
            pages[pageNum + 1].SetActive(false);
        }
    }
    public void exit()
    {
        manualCanvas.SetActive(false);
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
    }
}
