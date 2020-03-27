using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlashingTextScript : MonoBehaviour
{
    TMPro.TextMeshProUGUI flashingText;
    public float flashTimer = .5f;

    void Start()
    {
        flashingText = GetComponent<TMPro.TextMeshProUGUI>();

        StartCoroutine(BlinkText());
    }

    //function to blink the text
    public IEnumerator BlinkText()
    {
        //blink it forever. You can set a terminating condition depending upon your requirement
        while (true)
        {
            flashingText.text = "";
            yield return new WaitForSeconds(flashTimer);
            flashingText.text = "PRESS ANY BUTTON";
            yield return new WaitForSeconds(flashTimer);
        }
    }
}