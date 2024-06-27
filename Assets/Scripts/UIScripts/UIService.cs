using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject levelSelectorObject;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;      
    private void Start()
    {
        playButton.onClick.AddListener(SelectLevel);
        exitButton.onClick.AddListener(Application.Quit);
        AssignLevelstoButtons();
    }
    private void EnableMenu()=> menuObject.SetActive(true);    
    private void EnableLevelSelector()=> levelSelectorObject.SetActive(true);          
    private void DisableLevelSelector()=> levelSelectorObject.SetActive(false);private void SelectLevel()
    {
        menuObject.SetActive(false);
        EnableLevelSelector();

    }
    private void AssignLevelstoButtons()
    {
        Button[] buttons = levelSelectorObject.GetComponentsInChildren<Button>();
        for(int i=0; i<buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(()=>OnLevelSelect(index));
        }
    }
    public void OnLevelSelect(int level)
    {
        DisableLevelSelector();        
        GameService.Instance.GridGenerator.gameData = GameService.Instance.LevelService.puzzleData[level];
        GameService.Instance.LevelService.EnableGird();
    }
}
