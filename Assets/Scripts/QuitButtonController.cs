using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text QuitButton;
    public void OnPointerEnter(PointerEventData eventData)
    {
        QuitButton.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        QuitButton.color = Color.white;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}