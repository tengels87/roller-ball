using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//using DG.Tweening;

public class SceneManager : MonoBehaviour
{
    public static UnityAction<string> MapChangedEvent;

    public SpriteRenderer loadingScreen;

    private string lastLoadedScene = null;
    private Coroutine loadSceneCoroutine = null;
    private bool isLoading = false;

    void Awake() {

    }

    void Start()
    {
        
    }

    void Update()
    {
        // begin on starter map
        if (lastLoadedScene == null && loadSceneCoroutine == null && isLoading == false) {
            string starterMap = "level_01";
            activateMap(starterMap);
        }
    }

    public void activateMap(string mapName) {
        if (isLoading == false) {
            isLoading = true;

            if (loadSceneCoroutine == null) {
                /*
                    loadingScreen.DOFade(1f, 2f).onComplete = () => {
                        loadSceneCoroutine = StartCoroutine(switchSceneCoroutine(sceneName));
                    };
                */
            }
        }
    }

    private IEnumerator switchSceneCoroutine(string sceneName) {
        AsyncOperation asyncLoad;

        MapChangedEvent?.Invoke(sceneName);

        // unload last scene
        if (lastLoadedScene != null) {
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
        /*
        loadingScreen.DOFade(0, 2f);
        */

        isLoading = false;
        loadSceneCoroutine = null;
    }
}
