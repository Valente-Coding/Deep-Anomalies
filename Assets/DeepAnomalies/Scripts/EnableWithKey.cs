using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnableWithKey : MonoBehaviour
{
    [SerializeField] private UnityEvent m_OnKeyPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            m_OnKeyPressed.Invoke();
    }

    public void PauseGame(bool m_State)
    {
        Time.timeScale = m_State == true ? 0f : 1f;
    }

    public void MainMenu()
    {
        GameManager.Instance.RemovePlayers();
        SceneManager.LoadScene("MainMenu");
    }
}
