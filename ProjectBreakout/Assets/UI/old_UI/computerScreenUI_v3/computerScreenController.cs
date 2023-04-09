using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class computerScreenController : MonoBehaviour
{
    
    [SerializeField] private MyCodeBlock[] validCodeBlocks;

    [SerializeField] private MyCodeBlock initialCodeBlock;

    [Header("References")]
    [SerializeField] private TMP_InputField playerInput;
    [SerializeField] private TMP_Text childText;
    [SerializeField] private float flashDuration = 0.5f;

    Coroutine currentFlashRoutine;
    private Color initialColor;

    private void Start()
    {
        initialColor = Color.white;
        playerInput.text = initialCodeBlock.codeBlock;
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            foreach (MyCodeBlock validCode in validCodeBlocks)
            {
                //PlayerInput validlerden biriyle matchliyor mu diye bakar.
                if (MatchesInput(validCode)) return;

            }

            //Hic biri matchlemezse gelir.
            StartFlash(Color.red);
            playerInput.text = initialCodeBlock.codeBlock;
            

        }

    }

    private bool MatchesInput(MyCodeBlock validCodeBlock)
    {
        if (validCodeBlock.codeBlock == playerInput.text)
        {
            //Buradan event triggerlanacak

            StartFlash(Color.green);
            playerInput.interactable = false;
            return true;
        }
        else return false;
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
