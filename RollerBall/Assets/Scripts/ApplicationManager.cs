using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour {
    public static ApplicationManager Instance { get; private set; }

    [SerializeField] private GameObject canvasMainMenu;
    [SerializeField] private GameObject canvasLevelSelect;

    private bool isQuitting = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This will keep the object alive through scene changes
        } else {
            Destroy(gameObject); // If another instance exists, destroy this one
        }
    }

    void Start() {

        // load menu scene
        SceneManager.Instance.loadMenuScene();
    }

    public void QuitGame() {
        if (isQuitting) {
            return;
        }

        isQuitting = true;

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}