using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    int kiwisCollected = 0, totalKiwis;
    bool isPaused = false;
    Scene scn;

    public int totalTime = 60;
    public Text kiwiCounterText, timerText;
    public static GameManager gm;
    public GameObject pausePainel, endLevelPanel;

    // Start is called before the first frame update
    void Start()
    {
        gm = this;
        scn = SceneManager.GetActiveScene();
        CountKiwis();
        kiwiCounterText.text = kiwisCollected.ToString("00") + "/" + totalKiwis.ToString("00");
        timerText.text = totalTime.ToString("00");
        pausePainel.SetActive(false);
        endLevelPanel.SetActive(false);
        Time.timeScale = 1;
        InvokeRepeating("CountTime", 1.0f, 1.0f);
        
    }
    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseGame();
        }
    }

    public void PauseGame(){
        if(isPaused){
            isPaused = false;
            pausePainel.SetActive(false);
            Time.timeScale = 1;
        } else {
            isPaused = true;
            pausePainel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //Funcao para resetar a fase
    public void ReloadScene(){
        SceneManager.LoadScene(scn.buildIndex);
    }

    //Funcao para chamar quando O player coleta uma fruta
    public void AddFruit(){
        kiwisCollected = kiwisCollected + 1;
        kiwiCounterText.text = kiwisCollected.ToString("00") + "/" + totalKiwis.ToString("00");
        if(kiwisCollected >= totalKiwis){
            endLevelPanel.SetActive(true);
            PlayerPrefs.SetInt("Level" + (scn.buildIndex + 1).ToString() + "Unlocked", 1);
            Time.timeScale = 0;
            // LevelFinish();
        }
    }

    //Funcao para contar a quantidade de kiwis na fase
    void CountKiwis(){
        GameObject[] kiwis = GameObject.FindGameObjectsWithTag("Kiwi");
        foreach (GameObject k in kiwis)
        {
            totalKiwis++;
        }
    }

    public void LevelFinish(){

        if(scn.buildIndex == 6){
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(scn.buildIndex + 1);
        }
    }

    public void LoadLevel(int lvlIndex){
        SceneManager.LoadScene(lvlIndex);
    }

    void CountTime(){
        totalTime--;
        timerText.text = totalTime.ToString("00");

        if(totalTime <= 0){
            CancelInvoke();
            GameObject.Find("Player").GetComponent<PlayerScript>().StartCoroutine("PlayerDeath");
        }
    }
}
