using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LevelChoose(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    
}
