using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour {

    public int NextLevel;
    private string[] levels = { "1_Movement", "2_PickUp", "3_Drop", "4_PickUpFromGround" };

    void Start()
    {
        if (NextLevel == null)
        {
            NextLevel = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            print("loading next level");
            LoadNextLevel();
        }
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
