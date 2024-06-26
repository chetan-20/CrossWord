using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject titleText;
    [SerializeField] private GameObject buttonsObject;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    public void DisableMenu()
    {
        titleText.SetActive(false);
        buttonsObject.SetActive(false);
        backGround.SetActive(false);
    }
    public void EnableMenu()
    {
        titleText.SetActive(true);
        buttonsObject.SetActive(true);
        backGround.SetActive(true);
    }
    private void Start()
    {
        playButton.onClick.AddListener(DisableMenu);
        exitButton.onClick.AddListener(Application.Quit);
    }
}
