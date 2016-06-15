using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFadeInOut : MonoBehaviour
{
    public Image FadeImg;
    public float fadeSpeed = 1.5f;
    public bool sceneStarting = true;
    public int NextLevel;
    private string[] levels = { "1_Movement", "2_PickUp", "3_Drop", "4_PickUpFromGround" };

    void Start()
    {
        if (NextLevel == null)
        {
            NextLevel = 0;
        }
    }

    void Awake()
    {
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        // If the scene is starting...
        if (sceneStarting)
        {
            // ... call the StartScene function.
            StartScene();
        }

        if (Input.GetKeyDown("space"))
        {
            print("loading next level");
            EndScene();
        }
    }


    void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (FadeImg.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the RawImage.
            FadeImg.color = Color.clear;
            FadeImg.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }


    public IEnumerator EndSceneRoutine()
    {
        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;
        do
        {
            // Start fading towards black.
            FadeToBlack();

            // If the screen is almost black...
            if (FadeImg.color.a >= 0.99f)
            {
                // ... reload the level
                LoadNextLevel();
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void EndScene()
    {
        sceneStarting = false;
        StartCoroutine("EndSceneRoutine");
    }

    void LoadNextLevel()
    {
        if (!NextLevel.Equals(null))
        {
            Application.LoadLevel(levels[NextLevel]);
        }
        else
        {
            print("No next level found.");
        }
    }
}