using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//using DG.Tweening;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    public SpriteRenderer loadingScreen;

    private string lastLoadedScene = null;
    private Coroutine loadSceneCoroutine = null;
    private int lastLoadedLevelIndex = 0;
    private bool isLoading = false;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void loadLevel(int index) {
        if (isLoading == false) {
            isLoading = true;

            if (loadSceneCoroutine == null) {
                loadSceneCoroutine = StartCoroutine(switchLevelCoroutine(index));
                /*
                    loadingScreen.DOFade(1f, 2f).onComplete = () => {
                        loadSceneCoroutine = StartCoroutine(switchSceneCoroutine(mapName));
                    };
                */
            }
        }
    }

    private IEnumerator switchLevelCoroutine(int levelIndex) {
        string sceneName = "level_0" + levelIndex;

        AsyncOperation asyncLoad;

        // unload last scene
        if (lastLoadedScene != null && sceneName != lastLoadedScene) {
            asyncLoad = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(lastLoadedScene);

            while (!asyncLoad.isDone) {
                yield return null;
            }
        }

        // load new scene
        asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        while (!asyncLoad.isDone) {
            yield return null;
        }

        lastLoadedScene = sceneName;
        lastLoadedLevelIndex = levelIndex;
        /*
        loadingScreen.DOFade(0, 2f);
        */

        isLoading = false;
        loadSceneCoroutine = null;
    }

    public int getCurrentLevelIndex() {
        return lastLoadedLevelIndex;
    }

    public void loadMenuScene() {
        StartCoroutine(loadMenuSceneCoroutine());
    }

    public IEnumerator loadMenuSceneCoroutine() {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("menu", LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    public void fromMenuToLevel(int levelIndex) {
        StartCoroutine(fromMenuToLevelCoroutine(levelIndex));
    }

    public IEnumerator fromMenuToLevelCoroutine(int levelIndex) {
        string sceneName = "level_0" + levelIndex;

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
        }

        lastLoadedScene = sceneName;
        lastLoadedLevelIndex = levelIndex;

        asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("game", LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
        }

        asyncLoad = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("menu");
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    public void fromLevelToMenu() {
        StartCoroutine(fromLevelToMenuCoroutine());
    }

    public IEnumerator fromLevelToMenuCoroutine() {
        AsyncOperation asyncLoad;

        if (lastLoadedScene != null) {
            asyncLoad = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(lastLoadedScene);
            while (!asyncLoad.isDone) {
                yield return null;
            }
        }

        asyncLoad = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("game");
        while (!asyncLoad.isDone) {
            yield return null;
        }

        asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("menu", LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
