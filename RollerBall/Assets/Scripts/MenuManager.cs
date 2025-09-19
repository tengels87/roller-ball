using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject canvasMainMenu;
    [SerializeField] private GameObject canvasLevelSelect;

    private void Awake() {
        setMainMenuVisibility(true);
        setLevelselectMenuVisibility(false);
    }

    public void showMainMenu() {
        setLevelselectMenuVisibility(false);
        setMainMenuVisibility(true);
    }

    public void showLevelSelectMenu() {
        setMainMenuVisibility(false);
        setLevelselectMenuVisibility(true);
    }

    private void setMainMenuVisibility(bool val) {
        if (canvasMainMenu != null) {
            canvasMainMenu.SetActive(val);
        }
    }

    private void setLevelselectMenuVisibility(bool val) {
        if (canvasLevelSelect != null) {
            canvasLevelSelect.SetActive(val);
        }
    }

    public void loadFirstLevel() {
        SceneManager.Instance.fromMenuToLevel(0);
    }
}
