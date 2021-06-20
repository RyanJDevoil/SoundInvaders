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
        QuitButton.color = new Color(255f / 255f, 0f / 255f, 0f / 255f); //Or however you do your color
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        QuitButton.color = Color.white; //Or however you do your color 
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        print("CLICK");
        Application.Quit();
    }
}