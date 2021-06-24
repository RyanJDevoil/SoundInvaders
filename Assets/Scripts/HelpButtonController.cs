using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public Text HelpButton;
    public Image controls;
    public void OnPointerEnter(PointerEventData eventData)
    {
        HelpButton.color = new Color(50f / 255f, 255f / 255f, 50f / 255f);
        controls.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData)
        {
            HelpButton.color = Color.white;
        controls.enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
       
    }
}
