using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditScreen_Controller : MonoBehaviour {

    public float screenFadeTime, screenWaitTime, screenShowTime;
    public float alphaChangeValue;
    Image fadeImage;
    public bool screenTransitioning = true;
    public float fadeControlLevel = 0.3f;//the amount of fade when you can start controlling again

    private void Awake()
    {
        fadeImage = this.GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(ScreenShow());
    }

    private void Update()
    {
        if (Input.GetAxis("Activate") > 0.1 || Input.GetKeyDown(KeyCode.Space))
        {
            GoToNextScreen();
        }
    }



    IEnumerator ScreenShow()
    {

        screenTransitioning = true;
        Debug.Log(fadeImage.color);
        for (float i = fadeImage.color.a; i > 0; i -= alphaChangeValue)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, i);

            if (i <= fadeControlLevel)
                screenTransitioning = false;
            yield return new WaitForSeconds(screenShowTime);
        }

        yield return new WaitForSeconds(screenWaitTime);
        FadeScreen();

    }

    IEnumerator ScreenFade()
    {

        screenTransitioning = true;
        for (float i = 0; i < 1; i += alphaChangeValue)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, i);
            yield return new WaitForSeconds(screenFadeTime);
        }
        screenTransitioning = false;
        GoToNextScreen();

    }

    public void FadeScreen()
    {
        StartCoroutine(ScreenFade());
    }

    void GoToNextScreen()
    {
        SceneManager.LoadScene("Level_1");
    }
}
