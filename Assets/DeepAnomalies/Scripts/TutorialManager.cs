using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    
    public void SkipTutorial()
    {
        GameManager.Instance.DisplayPlayersControls();
    }
}
