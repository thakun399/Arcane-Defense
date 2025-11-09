using UnityEngine;
using TMPro;
using System.Collections;

public class WaveSpawnManager : MonoBehaviour
{
    public Wave[] waveConfigurations;
    public WaveController waveController;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI countdownText;

    public bool enableWaveCycling;

    private int currentWave = 0;
    private float waveEndTime = 0f;
    private bool waitingForNextWave = false;
    private bool showingWaveLabel = false;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        StartCoroutine(ShowWaveLabelAndStart(currentWave));
    }

    void Update()
    {
        if (waitingForNextWave)
        {
            float timeLeft = waveEndTime - Time.time;
            countdownText.text = $"Next Wave In: {Mathf.CeilToInt(timeLeft)}";

            if (Time.time >= waveEndTime)
            {
                countdownText.text = "";
                waitingForNextWave = false;

                currentWave++;
                if (currentWave >= waveConfigurations.Length)
                {
                    if (enableWaveCycling)
                    {
                        currentWave = 0;
                    }
                    else
                    {
                        enabled = false;
                        return;
                    }
                }

                StartCoroutine(ShowWaveLabelAndStart(currentWave));
            }
        }

        if (waveController.AllEnemiesDead() && !waitingForNextWave && !showingWaveLabel)
        {
            if (currentWave == waveConfigurations.Length - 1)
            {
                gameManager.WinGame();
                enabled = false;
                return;
            }

            waveEndTime = Time.time + waveConfigurations[currentWave].waveInterval;
            waitingForNextWave = true;
        }
    }

    IEnumerator ShowWaveLabelAndStart(int waveIndex)
    {
        showingWaveLabel = true;

        if (waveIndex == waveConfigurations.Length - 1)
            waveText.text = "Boss!";
        else
            waveText.text = $"Wave {waveIndex + 1}";

        waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        waveText.gameObject.SetActive(false);

        waveController.StartWave(waveConfigurations[waveIndex]);
        showingWaveLabel = false;
    }
}
