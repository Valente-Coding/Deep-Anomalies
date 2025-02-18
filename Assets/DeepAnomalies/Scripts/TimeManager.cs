using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int m_TimeInSeconds;
    [SerializeField] private TextMeshProUGUI m_TimerText;
    [SerializeField] private GameObject m_CountDown;
    [SerializeField] private UnityEvent m_OnFinish;
    
    private int m_CurrentTimeInSeconds = 0;

    public static TimeManager Instance { get; private set; }

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
        //StartCoroutine(StartTimer());
        m_TimerText.text = SecondsToMinutes(m_TimeInSeconds);
    }

    public void StartCounting()
    {
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        m_CountDown.SetActive(true);

        yield return new WaitForSeconds(4f);

        m_CountDown.SetActive(false);

        StartCoroutine(StartTimer());

        yield return null;
    }

    IEnumerator StartTimer()
    {
        m_CurrentTimeInSeconds = m_TimeInSeconds;

        for (int i = 0; i < m_TimeInSeconds; i++)
        {
            m_TimerText.text = SecondsToMinutes(m_CurrentTimeInSeconds);
            yield return new WaitForSeconds(1f);

            m_CurrentTimeInSeconds--;
        }

        m_TimerText.text = "00:00";
        m_OnFinish.Invoke();

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndGame");
        
        yield return null;
    }

    public void ResetTimer()
    {
        m_TimerText.text = SecondsToMinutes(m_TimeInSeconds);
    }

    private string SecondsToMinutes(int p_Seconds)
    {
        int l_Minutes = (int)math.floor(p_Seconds / 60);
        int l_Seconds = p_Seconds - (l_Minutes * 60);


        string l_MinutesString = l_Minutes < 10 ? "0" + l_Minutes.ToString() : l_Minutes.ToString();
        string l_SecondsString = l_Seconds < 10 ? "0" + l_Seconds.ToString() : l_Seconds.ToString();

        return l_MinutesString + ":" + l_SecondsString;
    }
}
