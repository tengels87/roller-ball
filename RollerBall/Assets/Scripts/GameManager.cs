using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasLevelCompleted;

    private bool isLevelFinished = false;

    public int CurrentLevelIndex {
        get { return SceneManager.Instance.getCurrentLevelIndex(); }
    }

    void Start()
    {
        setLevelFinished(false);
    }

    void Update()
    {
        
    }

    public void setLevelFinished(bool val) {
        isLevelFinished = val;
        canvasLevelCompleted.SetActive(val);
    }

    public void loadNextLevel() {
        setLevelFinished(false);
        SceneManager.Instance.loadLevel(CurrentLevelIndex + 1);
    }

    public void exitLevel() {
        setLevelFinished(false);
        SceneManager.Instance.fromLevelToMenu();
    }
}
