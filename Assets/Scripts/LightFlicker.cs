using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    private Light2D torchLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        torchLight = gameObject.GetComponent<Light2D>();
        torchLight.lightType = Light2D.LightType.Point;
        torchLight.pointLightOuterRadius = 10f;
    }

    void Update()
    {
        torchLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f));
    }
}

