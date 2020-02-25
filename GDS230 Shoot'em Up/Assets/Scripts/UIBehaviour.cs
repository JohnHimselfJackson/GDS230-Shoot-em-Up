using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    Text myText;
    public GameObject myParticle;

    void Start()
    {
        myText = GetComponentInChildren<Text>();
    }

    public void Embiggen(Transform myObject)
    {
        Debug.Log("Mouse Entered!");
        myObject.transform.localScale += new Vector3(0.1F, 0, 0);
        TextWhite();
        Particle();
    }

    public void Smally(Transform myObject)
    {
        Debug.Log("Mouse Exit!");
        myObject.transform.localScale -= new Vector3(0.1F, 0, 0);
        TextBlack();
        NoParticle();
    }

    void TextWhite()
    {
        myText.color = Color.white;
    }

    void TextBlack()
    {
        myText.color = Color.black;
    }

    void Particle()
    {
        myParticle.SetActive(true);
    }

    void NoParticle()
    {
        myParticle.SetActive(false);
    }
}
