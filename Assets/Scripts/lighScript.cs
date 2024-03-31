using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class lighScript : MonoBehaviour
{
    public Light light;
    public Color startColor;
    public Color endColor;
    public bool dayNight = true;
    public float duration = 90; // The duration of the color transition.

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;

        if (dayNight)
        {
            light.color = Color.Lerp(startColor, endColor, t);
        }
        else
        {
            light.color = Color.Lerp(endColor, startColor, t);
        }

        if (t >= 1.0f)
        {
            dayNight = !dayNight;
            startTime = Time.time;
            t = 0.0f;
        }

    }
}
