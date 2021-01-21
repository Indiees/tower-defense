using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;

    [Header("UI")]
    public GameObject newWavePanel;
    public GameObject selectTurretPanel;
    public GameObject menuPanel;
    public TextMeshProUGUI titleMenuText;
    public Button playButton;
    public Button resumeButton;
    public Button playAgainButton;
    public Button exitButton;

    private bool isPlay;
    private bool isPause;
    [HideInInspector]public bool isGameOver;

    private void Awake() {
        if(ins != null && ins != this)
            Destroy(this.gameObject);
        else   
            ins = this;

        playButton.onClick.AddListener(Play);
        resumeButton.onClick.AddListener(Resume);
        playAgainButton.onClick.AddListener(PlayAgain);
        exitButton.onClick.AddListener(Exit);
        titleMenuText.text = "PLAY";
        playButton.gameObject.SetActive(true);
        menuPanel.SetActive(true);

    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && isPlay){
            isPause = !isPause;
            if(isPause)
                Pause();
            else
                Resume();
        }
    }

    public IEnumerator NewWave(){
        newWavePanel.SetActive(true);
        yield return new WaitForSeconds(3);
        newWavePanel.SetActive(false);
    }

    private void Play(){
        ShipsManager.ins.SelectWave();
        playButton.gameObject.SetActive(false);
        menuPanel.SetActive(false);
        selectTurretPanel.SetActive(true);
        isPlay = true;
    }

    private void Pause(){
        Time.timeScale = 0;
        titleMenuText.text = "PAUSE";
        selectTurretPanel.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        menuPanel.SetActive(true);
    }

    public void GameOver(){
        isGameOver = true;
        titleMenuText.text = "GAME OVER";
        selectTurretPanel.SetActive(false);
        playAgainButton.gameObject.SetActive(true);
        menuPanel.SetActive(true);
    }

    private void Resume(){
        Time.timeScale = 1;
        menuPanel.SetActive(false);
        selectTurretPanel.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        isPause = false;
    }

    private void PlayAgain(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Exit(){
        Application.Quit();
    }
}
