using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

public class UIM_Scrip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] CarController carController;
    [SerializeField] Transform carTransform;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameOverPanner;

    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI totalDistanceText;
    [SerializeField] TextMeshProUGUI maximumSpeedText;

    [SerializeField] GameObject speedIcon;
    [SerializeField] GameObject distanceIcon;
    [SerializeField] GameObject scoreIcon;

    private float speed = 0f;
    private float distance = 0f;
    private float score = 0f;
    private float maxSpeed = 0f;
    void Start()
    {
        speedIcon.SetActive(true);
        distanceIcon.SetActive(true);
        scoreIcon.SetActive(true);
        gameOverPanner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SpeedUI();
        DistanceUI();
        ScoreUI();
    }

    void SpeedUI()
    {
        speed = carController.CarSpeed();
        speedText.text = speed.ToString("0" + "Km/H");
        if(maxSpeed < speed)
        {
            maxSpeed = speed;
        }
    }
    void DistanceUI()
    {
        distance = (carTransform.position.z)/1000;
        distanceText.text = distance.ToString("0.00" + "Km");
    }
    void ScoreUI()
    {
        score += (carTransform.position.z * speed)/10000;
        scoreText.text = score.ToString("0"+" ♦");
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanner.SetActive(true);
        totalScoreText.text = score.ToString("0" + " ♦");
        totalDistanceText.text = distance.ToString("0.00" + "Km");
        maximumSpeedText.text = maxSpeed.ToString("0" + "Km/H");
        speedIcon.SetActive(false);
        distanceIcon.SetActive(false);
        scoreIcon.SetActive(false);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
