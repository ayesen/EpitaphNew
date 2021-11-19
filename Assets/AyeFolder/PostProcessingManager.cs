using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public Volume PpVolume;
    ColorAdjustments CA;
    Vignette Vig;

    private void Awake()
    {
        PpVolume.profile.TryGet<ColorAdjustments>(out CA);
        PpVolume.profile.TryGet<Vignette>(out Vig);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            StartCoroutine(DeadFilter());
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ChangeFilter();
        }
    }

    public void Reset()
    {
        CA.saturation.value = 0;
        CA.colorFilter.value = Color.white;
        Vig.intensity.value = 0;
    }

    public void ChangeFilter()
    {
        CA.saturation.value -= 5;
        Vig.intensity.value += 0.05f;
    }
    
    public IEnumerator DeadFilter()
    {
        float time = 0;
        float timecolor = 0;
        float timevig = 0;
        float originalSat = CA.saturation.value;
        float originalVig = Vig.intensity.value;

        while(CA.colorFilter.value != Color.black)
        {
            time += Time.fixedDeltaTime/10;
            timecolor += Time.fixedDeltaTime/20;
            timevig += Time.fixedDeltaTime/20;
            CA.saturation.value = Mathf.Lerp(originalSat, -100, time);
            CA.colorFilter.value = Color.Lerp(Color.white, Color.black, timecolor);
            Vig.intensity.value = Mathf.Lerp(originalVig, 1, timevig);
            yield return null;
        }
        
    }

}
