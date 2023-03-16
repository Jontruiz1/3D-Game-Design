using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpNotif : MonoBehaviour
{
    private TMP_Text text;
    private TextMeshProUGUI tmp;

    public void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        text = GetComponent<TMP_Text>();
        tmp.enabled = false;
    }

    // fade the text away
    public IEnumerator FadeTextToZero()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 5f));
            yield return null;
        }
        tmp.enabled = false;
    }


    // Displays the name of the item that you picked up
    public void pickUpDisplay(string name)
    {
        
        tmp.enabled = true;
        text.SetText(name + " acquired!");
        StartCoroutine(FadeTextToZero());    
    }

    public void objectiveUpdate(string name)
    {
        tmp.enabled = true;
        text.SetText(name);
        StartCoroutine(FadeTextToZero());
    }

    public void sideObjectiveUpdate(string name)
    {
        tmp.enabled = true;
        text.SetText(name);
        StartCoroutine(FadeTextToZero());

    }

}
