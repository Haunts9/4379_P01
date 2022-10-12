using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteScript : MonoBehaviour
{
    private bool Check = false;
    Vector3 targetVector = new Vector3(2f, 2f, 2f);
    Vector3 startVector = new Vector3(1f, 1f, 1f);
    [SerializeField] float speed = 1.5f;
    private void Awake()
    {
        gameObject.transform.localScale = targetVector;
    }
    public void Vignette()
    {
        StopCoroutine(VignetteGrow());
        StartCoroutine(VignetteGrow());
    }
    IEnumerator VignetteGrow()
    {
        gameObject.transform.localScale = startVector;
        float counter = 0;
        while (gameObject.transform.localScale.x < targetVector.x)
        {
            counter += Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(startVector, targetVector, counter / speed);
            yield return null;
        }
    }
}
