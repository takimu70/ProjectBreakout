using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.InputSystem;

public class MonitorController : MonoBehaviour
{

    [SerializeField] private MyCodeBlock[] validCodeBlocks;

    [SerializeField] private MyCodeBlock initialCodeBlock;

    [Header("References")]
    [SerializeField] private TMP_InputField playerInput;
    [SerializeField] private TMP_Text childText;
    [SerializeField] private float flashDuration = 0.5f;

    Coroutine currentFlashRoutine;
    private Color initialColor;

    private string backupCodeBlock;

    private void Start()
    {
        initialColor = Color.white;
        backupCodeBlock = initialCodeBlock.codeBlock;
        playerInput.text = backupCodeBlock;
    }
    public void OnUIActiveEnable()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = false;
    }
    public void OnUIDeactive()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = true;
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

            //Hi� biri matchlemezse gelir.
            StartFlash(Color.red);
            playerInput.text = backupCodeBlock;


        }

    }

    private bool MatchesInput(MyCodeBlock validCodeBlock)
    {
        if (validCodeBlock.codeBlock == playerInput.text)
        {
            //Buradan eventler triggerlanacak
            StartFlash(Color.green);
            foreach (GameEvent gameEvent in validCodeBlock.gameEvents)
            {
                gameEvent.Raise();
            }

            backupCodeBlock = validCodeBlock.codeBlock;

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
