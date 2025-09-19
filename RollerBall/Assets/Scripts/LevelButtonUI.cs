using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButtonUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject[] stars; // assign 3 star icons
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Button button;

    private int levelIndex;

    public void init(int index) {
        levelIndex = index;
        levelText.text = "Level " + (index + 1).ToString();

        int earnedStars = 2;
        for (int i = 0; i < stars.Length; i++)
            stars[i].SetActive(i < earnedStars);

        // Unlock rule: unlock level 1 always, others unlock if previous has at least 1 star
        bool unlocked = (index == 0);// || SaveSystem.GetStars(index - 1) > 0;

        button.interactable = unlocked;
        lockIcon.SetActive(!unlocked);

        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        SceneManager.Instance.fromMenuToLevel(levelIndex);
    }

    
}
