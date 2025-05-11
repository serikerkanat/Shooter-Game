using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public Text scoreText;
    public Text timerText;
    public Text messageText;        
    public Image resultImage;       

    [Header("Game Settings")]
    public float gameTime = 60f;

    private int score = 0;
    private float timer;
    private bool isGameActive = true;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = gameTime;
        UpdateUI();

        if (messageText != null) messageText.gameObject.SetActive(false);
        if (resultImage != null) resultImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isGameActive) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            isGameActive = false;
            EndGame();
        }

        UpdateUI();
    }

    public void AddScore()
    {
        if (!isGameActive) return;
        score++;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + Mathf.Ceil(timer);
    }

    void EndGame()
    {
        Debug.Log("Время вышло! Конец игры.");
        StartCoroutine(ShowResults());
    }

    IEnumerator ShowResults()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "Time's up";
        }

        yield return new WaitForSeconds(3f);

        if (resultImage != null) resultImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("menu"); 
    }
}
