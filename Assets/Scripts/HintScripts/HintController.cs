using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI hintText;
    [SerializeField] public GameObject hintPanelObject;
    private float hintTimer = 3f;
    private void Start()
    {
        hintPanelObject.SetActive(false);
    }
    public void ShowHint(string hints)
    {
        StartCoroutine(DisplayHintForSeconds(hints));
    }
    private IEnumerator DisplayHintForSeconds(string hint)
    {
        hintText.text = hint;
        hintPanelObject.SetActive(true);
        yield return new WaitForSeconds(hintTimer);
        hintPanelObject.SetActive(false);
    }
}
