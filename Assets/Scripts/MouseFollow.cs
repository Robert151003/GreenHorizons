using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10  ;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position += new Vector3(0.5f, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
