using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI inGameTimeText; // Text hiển thị khi đang chơi (ví dụ ở top)

    private float elapsed;
    private bool running;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetTimer();
        StartTimer();
    }

    private void Update()
    {
        if (!running) return;
        elapsed += Time.deltaTime;
        if (inGameTimeText != null)
        {
            inGameTimeText.text = GetFormattedTime();
        }
    }

    public void StartTimer()
    {
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    public void ResetTimer()
    {
        elapsed = 0f;
        if (inGameTimeText != null)
        {
            inGameTimeText.text = GetFormattedTime();
        }
    }

    public float ElapsedSeconds => elapsed;

    public string GetFormattedTime()
    {
        int minutes = (int)(elapsed / 60f);
        int seconds = (int)(elapsed % 60f);
        return string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
