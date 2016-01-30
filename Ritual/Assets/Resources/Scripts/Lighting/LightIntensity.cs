using UnityEngine;
using System.Collections;

public class LightIntensity : MonoBehaviour
{

    public float startIntensity;
    public float endIntensity;
    private float currIntensity;
    public float speed;
    private bool increase, decrease;

    private Light light;

    // Use this for initialization
    void Start()
    {
        light = GetComponent<Light>();
        startIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        currIntensity = light.intensity;
        if (light.intensity == startIntensity)
        {
            decrease = false;
            increase = true;
        }

        else if (light.intensity >= endIntensity)
        {
            decrease = true;
            increase = false;
        }

        if (increase)
            light.intensity += speed;

        else if (decrease)
            light.intensity -= speed;


    }
}
