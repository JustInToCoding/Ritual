using UnityEngine;
using System.Collections;

public class LightIntensity : MonoBehaviour
{

    public float startIntensity;
    public float endIntensity;
    private float currIntensity;
    public float speed;
    private bool increase, decrease;
    public static bool changeColour;
    public Color color;
    public Vector4 RGBNew;
    private bool reset;

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
        if (light.intensity <= startIntensity)
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
            StartCoroutine(timer());
            decrease = true;
            changeColour = false;
            reset = false;
        }

        else if (changeColour == false)
        {
            if (light.intensity <= startIntensity && reset)
            {
                someObject.color = color;
                reset = false;
            }
        }
    }

    public IEnumerator timer()
    {
        yield return new WaitForSeconds(1.5f);
        //reset = true;
    }
}
