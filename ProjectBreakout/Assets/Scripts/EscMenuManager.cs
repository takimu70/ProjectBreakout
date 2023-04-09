using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EscMenuManager : MonoBehaviour
{
    [SerializeField] private Image menuPanel;
    private bool menuOn;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuOn = !menuOn;
            menuPanel.gameObject.SetActive(menuOn);
        }
    }



}
