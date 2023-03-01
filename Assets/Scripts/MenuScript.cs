using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenu, levelSelect;
    public Button[] lvlButtons;

    void Start(){
        MainMenu();
        CheckLevels();
        lvlButtons[0].interactable = true;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.R)){
            PlayerPrefs.DeleteAll();
            CheckLevels();
            lvlButtons[0].interactable = true;
        }
    }


    public void MainMenu(){
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }


    public void LevelSelect(){
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void LoadLevel(int lvlIndex){
        SceneManager.LoadScene(lvlIndex);
    }

    public void CheckLevels(){
        for (int i = 0; i < lvlButtons.Length; i++){
            if(PlayerPrefs.HasKey("Level" + (i+1).ToString() + "Unlocked")){
                lvlButtons[i].interactable = true;
            } else {
                lvlButtons[i].interactable = false;
            }
        }
    }

    public void QuitGame(){
        Application.Quit();
    }
}
