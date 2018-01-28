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

    public AudioSource BGM;

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

    public GameObject ButtonA;
    public GameObject ButtonB;
    public GameObject ButtonC;

    public SerialHandler serialHandler;

    public int gameStartFrame;
    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        currentGameState = GameState.menu;
        serialHandler = GetComponent<SerialHandler>();
        serialHandler.OnDataReceived += SerialHandler_OnDataReceived;
    }

    private void SerialHandler_OnDataReceived(string message) {
        var data = message.Split(new string[] { "\t" }, System.StringSplitOptions.None);
        if (data.Length < 4) { return; }
        if (currentGameState == GameState.menu && data[1] == 1.ToString()) {
            GameStart();
        }
    }

    // Update is called once per frame
    void Update() {
        if (currentGameState == GameState.menu && Input.GetKeyDown(KeyCode.J)) {
            GameStart();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }

    void GameStart() {
        menuPanel.DOFade(0, 1).OnComplete(() => {
            if (ButtonA)
                ButtonA.transform.DOScale(0, 0.5f).SetDelay(4).SetEase(Ease.InBack);
            if (ButtonB)
                ButtonB?.transform.DOScale(0, 0.5f).SetDelay(4).SetEase(Ease.InBack);
            if (ButtonC)
                ButtonC?.transform.DOScale(0, 0.5f).SetDelay(4).SetEase(Ease.InBack);

        });
        OnGameStart?.Invoke();
        BGM.volume = 1;
        BGM.Play();
        GetComponent<TempoClock>().enabled = true;
        currentGameState = GameState.play;
        gameStartFrame = Time.frameCount;
        StartCoroutine(TimeCountDown());
    }

    public void GameOver() {
        currentGameState = GameState.gameOver;
        OnGameOver?.Invoke();
        DOTween.To(() => BGM.volume, (x) => BGM.volume = x, 0, 3).SetUpdate(true);
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
