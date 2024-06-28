
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/*
 * Provides the mouse Input interaction of the Player
 * Changes the color of cells based on correct selection or incorrect
 */
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

