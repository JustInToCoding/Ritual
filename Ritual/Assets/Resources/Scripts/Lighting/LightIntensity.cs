using UnityEngine;
using System.Collections;

public class LightIntensity : MonoBehaviour
{

    public float startIntensity;
    public float endIntensity;
    private float currIntensity;
    public float speed;
    private bool increase, decrease;
    public bool changeColour;
    public Color color;
    public Vector4 RGBNew;

    public SpriteRenderer someObject;
    private Light light;

    // Use this for initialization
    void Start()
    {
        someObject = someObject.GetComponent<SpriteRenderer>();
        color = someObject.color;
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


        if (changeColour)
        {
            someObject.color = new Color32((byte)RGBNew.x, (byte)RGBNew.y, (byte)RGBNew.z, (byte)RGBNew.w);
            light.intensity = 8;
            decrease = true;
            changeColour = false;
        }

        else if (changeColour == false)
        {
            if (light.intensity == 0)
                someObject.color = color;
        }
    }
}
