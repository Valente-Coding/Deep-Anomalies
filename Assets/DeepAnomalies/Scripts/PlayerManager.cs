using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerInput m_PlayerInputs;
    [SerializeField] private FishingManager m_PlayerFishingManager;
    [SerializeField] private FishSpawner m_PlayerFishSpawner;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI m_PlayerScoreText;
    [SerializeField] private Image m_PlayerHeadBand;
    [SerializeField] private Image m_PlayerBackground;
    [SerializeField] private Image m_PlayerControlsDisplay;

    private InputAction m_MoveAction;
    private int m_Score = 0;
    private int m_PlayerNumber;

    public Camera m_PlayerCamera;
    public FishSpawner PlayerFishSpawner { get => m_PlayerFishSpawner; set => m_PlayerFishSpawner = value; }
    public PlayerInput PlayerInputs { get => m_PlayerInputs; set => m_PlayerInputs = value; }
    public InputAction MoveAction { get => m_MoveAction; set => m_MoveAction = value; }
    public Image PlayerHeadBand { get => m_PlayerHeadBand; set => m_PlayerHeadBand = value; }
    public Image PlayerBackground { get => m_PlayerBackground; set => m_PlayerBackground = value; }
    public Image PlayerControlsDisplay { get => m_PlayerControlsDisplay; set => m_PlayerControlsDisplay = value; }
    public int PlayerNumber { get => m_PlayerNumber; set => m_PlayerNumber = value; }
    public int Score { get => m_Score; set => m_Score = value; }

    void Awake()
    {
        MoveAction = PlayerInputs.actions["Move"];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float l_MoveValue = MoveAction.ReadValue<float>();

        if (l_MoveValue != 0)
        {
            m_PlayerFishingManager.MoveHook(l_MoveValue);

            if (PlayerControlsDisplay.transform.parent.gameObject.activeInHierarchy)
            {
                GameManager.Instance.ReadyPlayerUp();
                PlayerControlsDisplay.transform.parent.gameObject.SetActive(false);
            }    
        }


    }


    public void AddScore(GameObject p_FishGO)
    {
        Score += p_FishGO.GetComponent<FishHandler>().FishData.Value;
        Score = Score < 0 ? 0 : Score;

        m_PlayerScoreText.text = Score.ToString();
    }
}
