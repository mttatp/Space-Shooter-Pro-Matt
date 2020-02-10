using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Text _instructionsImg;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _levelText;
    [SerializeField]
    private Text _gameOverImg;
    [SerializeField]
    private Text _levelImg;
    [SerializeField]
    private Text _extraStageImg;
    [SerializeField]
    private Text _restartImg;
    [SerializeField]
    private Text _winImg;
    [SerializeField]
    private Text _winImg2;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;

    public bool winActivate;

    [SerializeField]
    private GameManager _gameManager;

    void Start()
    {
        winActivate = false;
        _scoreText.text = "Score: " + 0;
        _levelText.text = "Level: " + 0;
        _instructionsImg.gameObject.SetActive(true);
        _winImg.gameObject.SetActive(false);
        _winImg2.gameObject.SetActive(false);
        _extraStageImg.gameObject.SetActive(false);
        _gameOverImg.gameObject.SetActive(false);
        _restartImg.gameObject.SetActive(false);
        _levelImg.gameObject.SetActive(false);
        _gameManager.gameObject.GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }
    }

    public void UpdateScore(float playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLevelNum(int currentLevel)
    {
        _levelText.text = "Level: " + currentLevel.ToString();
    }

    IEnumerator FinalStageCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        _extraStageImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        _extraStageImg.gameObject.SetActive(false);
    }

    public void FinalStageUpdate()
    {
        StartCoroutine(FinalStageCoroutine());
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            _levelImg.gameObject.SetActive(false);
            _extraStageImg.gameObject.SetActive(false);
            GameOverSequence();
        }
    }

    IEnumerator UpdateLevelCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        _levelImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        _levelImg.gameObject.SetActive(false);
    }

    public void UpdateLevel(int currentLevel)
    {
        _levelText.text = "Level: " + currentLevel.ToString();
        _levelImg.text = "Level: " + currentLevel.ToString();
        StartCoroutine(UpdateLevelCoroutine());
    }

    void GameOverSequence()
    {
        if (winActivate == true)
        {
            _winImg.gameObject.SetActive(true);
            _winImg2.gameObject.SetActive(true);
        }
        else
        {
            _gameOverImg.gameObject.SetActive(true);
        }
        _restartImg.gameObject.SetActive(true);
        _gameManager.GameOver();
    }
}
