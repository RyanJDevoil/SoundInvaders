using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text StartButton;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartButton.color = new Color(50f/255f,255f/255f,50f/255f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        StartButton.color = Color.white;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}