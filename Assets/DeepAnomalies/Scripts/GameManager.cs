using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerPreset {
    public string Name;
    public List<Sprite> BgSprites;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private float m_PlayerScreensGap = 100f;
    [SerializeField] private int m_NumberOfPlayers = 2;
    [SerializeField] private List<Color> m_PlayerColors;
    [SerializeField] private List<Sprite> m_PlayerControlDisplays;
    [SerializeField] private List<PlayerPreset> m_PlayerPresets;
    [SerializeField] private List<RoundSO> m_Rounds;

    private List<PlayerManager> m_Players = new List<PlayerManager>();
    private int m_PlayerReady = 0;

    public static GameManager Instance { get; private set; }
    public int NumberOfPlayers { get => m_NumberOfPlayers; set => m_NumberOfPlayers = value; }
    public List<PlayerManager> Players { get => m_Players; set => m_Players = value; }

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

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {

    }

    public void PrepareLevel()
    {
        SceneManager.LoadScene("GameplayLevel");

        LoadPlayers();

        if (TimeManager.Instance)
            TimeManager.Instance.ResetTimer();
    }

    private void LoadPlayers()
    {
        Players = new List<PlayerManager>();

        for (int i = 0; i < NumberOfPlayers; i++)
        {
            GameObject l_PlayerGO = Instantiate(m_PlayerPrefab, transform);
            l_PlayerGO.transform.Translate(new Vector3(i * m_PlayerScreensGap, 0f, 0f));

            Players.Add(l_PlayerGO.GetComponent<PlayerManager>());
            Players[i].m_PlayerCamera.rect = new Rect(1f/NumberOfPlayers * i, 0.0f, 1f/NumberOfPlayers, 1.0f);
            Players[i].PlayerHeadBand.color = m_PlayerColors[i];
            Players[i].PlayerBackground.sprite = m_PlayerPresets[m_NumberOfPlayers-1].BgSprites[i];
            Players[i].PlayerControlsDisplay.sprite = m_PlayerControlDisplays[i];
            Players[i].PlayerNumber = i;

            PlayerInput l_PlayerInput = Players[i].PlayerInputs;
            string l_PlayerInputMap = "Player" + (i+1).ToString();
            l_PlayerInput.defaultActionMap = l_PlayerInputMap;
            l_PlayerInput.SwitchCurrentActionMap(l_PlayerInputMap);
            l_PlayerInput.currentActionMap.Enable();
            Players[i].MoveAction = l_PlayerInput.actions["Move"];
        }
    }

    private void StartGame()
    {
        TimeManager.Instance.StartCounting();

        StartCoroutine(StartRound(m_Rounds[0]));
    }

    IEnumerator StartRound(RoundSO p_RoundData)
    {
        foreach (Wave l_Wave in p_RoundData.Waves)
        {
            yield return new WaitForSeconds(l_Wave.DelayInSeconds);

            foreach (FishSO l_FishSO in l_Wave.FishesInOrder)
            {
                SpawnFishForAllPlayers(l_FishSO);

                yield return new WaitForSeconds(l_Wave.IntervalInSeconds);
            }
        }


        yield return null;
    }

    private void SpawnFishForAllPlayers(FishSO p_FishSO)
    {
        int l_Edge = UnityEngine.Random.Range(0, 2);
        Players.ForEach(l_Player => {
            l_Player.PlayerFishSpawner.SpawnFish(p_FishSO, l_Edge);
        });
    }

    public void DisplayPlayersControls()
    {
        Players.ForEach(l_Player => {
            l_Player.PlayerControlsDisplay.transform.parent.gameObject.SetActive(true);
        });
    }

    public void ReadyPlayerUp()
    {
        m_PlayerReady++;

        if (m_PlayerReady >= m_NumberOfPlayers)
            StartGame();
    }

    public void RemovePlayers()
    {
        for (int i = 0; i < m_Players.Count; i++)
        {
            Destroy(m_Players[i].gameObject);
        }

        m_Players = new List<PlayerManager>();
    }
}
