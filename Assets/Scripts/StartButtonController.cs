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
        StartButton.color = new Color(50f/255f,255f/255f,50f/255f); //Or however you do your color
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        StartButton.color = Color.white; //Or however you do your color 
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        print("CLICK");
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}