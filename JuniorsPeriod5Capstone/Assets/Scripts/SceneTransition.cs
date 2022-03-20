using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public RectTransform fader;
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInCo());
    }

    public IEnumerator FadeInCo()
    {
        yield return new WaitForSeconds(0.05f);

        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.7f).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }

    public IEnumerator OpenSceneCo()
    {
        PlayerMovement.instance.canMove = false;
        PlayerMovement.instance.speed = 0f;
        PlayerMovement.instance.moveInput = 0f;

        yield return new WaitForSeconds(.5f);

        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.7f).setOnComplete(() =>
        {
            if (sceneToLoad != "")
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.Log("Scene to load is empty on");
                return;
            }
        });
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Cp", 0);
            StartCoroutine(OpenSceneCo());
        }
    }
}
