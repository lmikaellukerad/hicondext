using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Author: Arjan
/// This was only used during interaction design testing to switch between various scenes and scenarios.
/// </summary>
public class SceneFadeInOut : MonoBehaviour
{
    public Image FadeImg;
    public float FadeSpeed = 1.5f;
    public bool SceneStarting = true;
    public int NextLevel;
    private string[] levels = { "1_Movement", "2_PickUp", "3_Drop", "4_PickUpFromGround", "SuperMarket" };

    public void Start()
    {
        if (this.NextLevel == null)
        {
            this.NextLevel = 0;
        }
    }

    public void Awake()
    {
        this.FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }

    public void Update()
    {
        // If the scene is starting...
        if (this.SceneStarting)
        {
            // ... call the StartScene function.
            this.StartScene();
        }

        if (Input.GetKeyDown("space"))
        {
            this.EndScene();
        }
    }

    public void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        this.FadeImg.color = Color.Lerp(this.FadeImg.color, Color.clear, this.FadeSpeed * Time.deltaTime);
    }

    public void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        this.FadeImg.color = Color.Lerp(this.FadeImg.color, Color.black, this.FadeSpeed * Time.deltaTime);
    }

    public void StartScene()
    {
        // Fade the texture to clear.
        this.FadeToClear();

        // If the texture is almost clear...
        if (this.FadeImg.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the RawImage.
            this.FadeImg.color = Color.clear;
            this.FadeImg.enabled = false;

            // The scene is no longer starting.
            this.SceneStarting = false;
        }
    }

    public IEnumerator EndSceneRoutine()
    {
        // Make sure the RawImage is enabled.
        this.FadeImg.enabled = true;
        do
        {
            // Start fading towards black.
            this.FadeToBlack();

            // If the screen is almost black...
            if (this.FadeImg.color.a >= 0.99f)
            {
                // ... reload the level
                this.LoadNextLevel();
                yield break;
            }
            else
            {
                yield return null;
            }
        }
        while (true);
    }

    public void EndScene()
    {
        this.SceneStarting = false;
        this.StartCoroutine("EndSceneRoutine");
    }

    public void LoadNextLevel()
    {
        if (!this.NextLevel.Equals(null))
        {
            Application.LoadLevel(this.levels[this.NextLevel]);
        }
        else
        {
            // no next level found
        }
    }
}