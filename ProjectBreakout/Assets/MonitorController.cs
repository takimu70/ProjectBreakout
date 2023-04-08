using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonitorController : MonoBehaviour
{
    
    [SerializeField] private MyCodeBlock[] validCodeBlocks;

    [SerializeField] private MyCodeBlock initialCodeBlock;

    [Header("References")]
    [SerializeField] private TMP_InputField textContainer;
    [SerializeField] private TMP_Text childText;
    private float flashDuration = 0.5f;

    Coroutine currentFlashRoutine;
    private Color initialColor;

    private void Start()
    {
        initialColor = Color.white;
        textContainer.text = initialCodeBlock.codeBlock;
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            bool flag = false;

            foreach (MyCodeBlock validCode in validCodeBlocks)
            {
                if (validCode.codeBlock == textContainer.text) 
                {
                    flag = true;
                    initialCodeBlock = validCode;
                    Completed();
                    
                    break;

                }

            }

            if (flag == false)
            {
                StartFlash(Color.red);
                textContainer.text = initialCodeBlock.codeBlock;
            }

        }

    }

    private void Completed()
    {
        StartFlash(Color.green);
        textContainer.text = initialCodeBlock.codeBlock;

    }



    private void StartFlash(Color color)
    {
        

        if (currentFlashRoutine != null)
        {
            StopCoroutine(currentFlashRoutine);
        }
        StartCoroutine(Flash(color));
    }

    IEnumerator Flash(Color color)
    {
        float duration = flashDuration;
        
        
        while (duration > 0)
        {
            childText.color = Color.Lerp(initialColor, color, duration);
            duration -= Time.deltaTime;
            yield return null;
        }

    }



}
