using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Text timeLeftText;
    public Text MatchPairText;

    public CanvasGroup menuPanel;

    public enum GameState {
        menu,
        play,
        gameOver
    }

    public GameState currentGameState;

    public NecromancerSpawner nacromancerSpawner;
    public Launcher Launcher;

    //Delegate
    public delegate void NormalDelegate();
    public NormalDelegate OnGameStart;
    public NormalDelegate OnGameOver;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        currentGameState = GameState.menu;
    }

    // Update is called once per frame
    void Update() {
        if (currentGameState == GameState.menu && Input.GetKeyDown(KeyCode.J)) {
            GameStart();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log(currentGameState);
        }
    }

    void GameStart() {
        currentGameState = GameState.play;
        menuPanel.DOFade(0, 1);
        OnGameStart?.Invoke();
        StartCoroutine(TimeCountDown());
    }

    public void GameOver() {
        currentGameState = GameState.gameOver;
        OnGameOver?.Invoke();
        DOTween.To(() => Time.timeScale, (x) => Time.timeScale = x, 0, 3).SetUpdate(true);
        GetComponent<Camera>().DOOrthoSize(5.5f, 3).SetUpdate(true).OnComplete(() => {
            SceneManager.LoadScene(0);
            currentGameState = GameState.menu;
            Time.timeScale = 1;
        });
    }

    public IEnumerator TimeCountDown() {
        yield return new WaitForSeconds(1);
        var currentTime = int.Parse(timeLeftText.text);
        currentTime -= 1;
        if (currentTime < 0) {
            GameOver();
            yield break;
        }
        timeLeftText.text = currentTime.ToString();
        StartCoroutine(TimeCountDown());
    }

    public void AddMatchPair() {
        MatchPairText.text = (int.Parse(MatchPairText.text) + 1).ToString();
    }
}
