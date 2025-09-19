using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectManager : MonoBehaviour {
    //[SerializeField] private LevelDatabase levelDatabase;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform contentParent;

    private void Start() {
        GenerateLevelButtons();
    }

    private void GenerateLevelButtons() {
        for (int i = 0; i < 20; i++) {
            int levelIndex = i; // important: capture loop variable
            //LevelData data = levelDatabase.levels[i];

            GameObject buttonObj = Instantiate(levelButtonPrefab, contentParent);
            LevelButtonUI buttonUI = buttonObj.GetComponent<LevelButtonUI>();
            buttonUI.init(levelIndex);
        }
    }
}