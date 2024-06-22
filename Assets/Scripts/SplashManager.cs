using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;

    [SerializeField]
    private Slider splashLoading;


    // Start is called before the first frame update
    void Start()
    {
      //  StartCoroutine(LoadAsynchronously(1));
    }

    // Update is called once per frame
    void Update()
    {
        if (splashLoading.value==1)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    IEnumerator LoadAsynchronously(int sceneIndex) {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            splashLoading.value = progress;

            yield return null;

        }

    }
}
