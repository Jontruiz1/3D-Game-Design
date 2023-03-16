using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{

    private TMP_Text title;

    public void Awake() {
        title = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad > 9.5f) {
            StartCoroutine(ShowTitle());
        }

       // Debug.Log(Time.timeSinceLevelLoad);
    }

    public IEnumerator ShowTitle() {

        title.color = new Color(title.color.r, title.color.g, title.color.b, 1);
        title.SetText("Fractured");
        yield return null;

    }
}
