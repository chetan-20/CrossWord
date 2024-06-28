
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseInputHandler : MonoBehaviour,IPointerClickHandler
{    
    [SerializeField]private Image imageComponent;
    [SerializeField]private TextMeshProUGUI textComponent;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameService.Instance.PuzzleController.HandleCellClick(this);
    }
    public void SetSelected(bool selected)
    {
        Color color = selected ? Color.red : Color.white;
        imageComponent.color = color;
    }
    public void SetCorrect()
    {
        imageComponent.color = Color.green;
    }
    public void ResetCell()
    {
        imageComponent.color = Color.white;
    }
    public string GetText()
    {
        return textComponent.text;
    }
}

