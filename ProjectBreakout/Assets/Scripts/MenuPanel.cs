using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{

    private Image image;
    private Coroutine currentAppearRoutine;
    [SerializeField] float maxAlpha = 0.5f;
    [SerializeField] float secondForAppear = 1;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        StartAppear();
    }

    private void StartAppear()
    {
        if (currentAppearRoutine == null)
        {
            currentAppearRoutine = StartCoroutine(Appear());
        }
    }

    private IEnumerator Appear()
    {
        
        

        float duration = secondForAppear;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            Color colorThisFrame = image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t / duration);
            image.color = colorThisFrame;
            yield return null;
        }

    }


}
