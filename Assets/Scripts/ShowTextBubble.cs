using UnityEngine;
using System.Collections;

public class ShowTextBubble : MonoBehaviour
{
    #region class variables
    public UnityEngine.UI.Image TextBubble;
    public UnityEngine.UI.Text innerText;

    public float dissapearSpeed = 10f;
    public float appearingSpeed = 20f;

    public bool appear = true;

    #endregion

    #region unity functions


	// Update is called once per frame
	void Update () {

	    if (appear == true)
	    {
	        float alphaVal = TextBubble.color.a; //since both textbubble and innertext will appear and dissapear at the same time, we'll make them have the same alpha value
            alphaVal = Mathf.Lerp(alphaVal, 1f, appearingSpeed);
            Color textbubbleColor = TextBubble.color;
            Color innerTextColor = innerText.color;
	        textbubbleColor.a = alphaVal;
	        innerTextColor.a = alphaVal;
            TextBubble.color = textbubbleColor;
            innerText.color = innerTextColor;
    
	        if (alphaVal >0.999f)
	            appear = false;
         
	    }
	    else
	    {
            float alphaVal = TextBubble.color.a; //since both textbubble and innertext will appear and dissapear at the same time, we'll make them have the same alpha value
            alphaVal = Mathf.Lerp(alphaVal, 0,  dissapearSpeed);
            Color textbubbleColor = TextBubble.color;
            Color innerTextColor = innerText.color;
            textbubbleColor.a = alphaVal;
            innerTextColor.a = alphaVal;
            TextBubble.color = textbubbleColor;
            innerText.color = innerTextColor;
	        if (alphaVal < 0.1f)
	        {
                print("deactivate ");
	            gameObject.SetActive(false);
	        }

	    }
	}

    private void Start()
    {
       Color  textbubbleColor = TextBubble.color;
       Color  innerTextColor = innerText.color;

        textbubbleColor.a = 0f;
        innerTextColor.a = 0f;

        TextBubble.color = textbubbleColor;
        innerText.color = innerTextColor;

    }
    #endregion
    public void ApplyNewText(string text)
    {
        appear = true;
        innerText.text = text;
    }
}
