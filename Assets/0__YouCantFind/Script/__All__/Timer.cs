using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float totalTime = 60f; // 타이머의 총 시간 (초)
    public Text timerText; // 타이머를 표시할 UI 텍스트
    private bool isRunning = false; // 타이머가 실행 중인지 여부
    private float elapsedTime = 0f; // 경과한 시간

    public GameManager gameManager;

    private void Start()
    {
        // 시작 시 타이머 값을 표시
        Invoke("UpdateTimerText",3f);
        Invoke("StartTimer",3f);
    }

    private void Update()
    {
        // 타이머가 실행 중이라면 경과한 시간을 증가시킴
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;

            // 타이머가 종료되었을 경우
            if (elapsedTime >= totalTime)
            {
                StopTimer();
            }

            // 경과한 시간을 텍스트로 업데이트
            UpdateTimerText();
        }
    }

    public void StartTimer()
    {
        // 타이머 시작
        isRunning = true;
    }

    public void StopTimer()
    {
        // 타이머 정지
        isRunning = false;
        gameManager.timeDie = true;
    }

    private void UpdateTimerText()
    {
        // 경과한 시간을 텍스트로 업데이트
        float remainingTime = Mathf.Max(totalTime - elapsedTime, 0f);
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private string FormatTime(float time)
    {
        // 시간을 분:초 형식의 문자열로 변환하여 반환
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

