using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private List<Image> m_PodiumBandanas;

    void Start()
    {
        GameManager.Instance.Players.Sort((x, y) => y.Score.CompareTo(x.Score));

        for (int i = 0; i < GameManager.Instance.Players.Count; i++)
        {
            m_PodiumBandanas[i].color = GameManager.Instance.Players[i].PlayerHeadBand.color;
            m_PodiumBandanas[i].transform.parent.gameObject.SetActive(true);
        }
        
    }

    public void RetryGame()
    {
        GameManager.Instance.PrepareLevel();
    }

    public void GoToMainMenu()
    {
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene("MainMenu");
    }
}
