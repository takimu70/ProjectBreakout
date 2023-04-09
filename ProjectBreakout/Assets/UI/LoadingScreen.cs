using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{


    #region INSTANCE
    public static LoadingScreen instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }

    }
    #endregion

    [SerializeField] private GameObject blackCanvas;
    [SerializeField] private Image filler;
    [SerializeField] private Image logo;

    private void Start()
    {
        blackCanvas.SetActive(false);


    }



    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));

    }


    IEnumerator LoadSceneAsync(string sceneName)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        blackCanvas.SetActive(true);
        blackCanvas.GetComponent<Canvas>().sortingOrder = 5;


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            filler.fillAmount = progress;

            



            yield return null;

        }


    }

}