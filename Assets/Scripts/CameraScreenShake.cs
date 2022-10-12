using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenShake : MonoBehaviour
{
    private float GlobalIntensity = 1f;
    private bool Check = false;
    private Transform startTransform;
    private void Awake()
    {
        startTransform = gameObject.transform;
    }
    private void FixedUpdate()
    {
        if (Check== true)
        {

            gameObject.transform.localPosition = Random.insideUnitSphere * GlobalIntensity;
        }
    }
    public void ScreenShake(float Intensity, float Duration)
    {

        GlobalIntensity = Intensity;        
        StartCoroutine(ShakeTime(Duration));
    }
    IEnumerator ShakeTime(float duration)
    {
        Check = true;
        yield return new WaitForSeconds(duration);
        Check = false;
        gameObject.transform.position = startTransform.position;
    }
}
