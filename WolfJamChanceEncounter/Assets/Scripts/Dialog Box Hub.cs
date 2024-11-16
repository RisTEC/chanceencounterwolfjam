using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxHub : MonoBehaviour
{
    public Button attackButton;
    public Button healButton;
    public Button guardButton;

    public Image fire;
    public Image heart;
    public Image star;


    public TMP_Text dialogText;

    //[SerializeField] private List<Button> perks;

    public Transform spot1;

    private void Start()
    {
        fire.enabled = false;
        star.enabled = false;
        heart.enabled = false;
    }

    public void UIPerk(string word)
    {
        attackButton.enabled = false;
        healButton.enabled = false;
        guardButton.enabled = false;

        if(word == "fire"){
            fire.enabled = true;
        }
        else if (word == "heart")
        {
            heart.enabled = true;
        }
        else if (word == "star")
        {
            star.enabled = true;
        }
    }

    public void PerkOver()
    {
        if (fire.enabled)
        {
            fire.enabled = false;
        }
        else if (heart.enabled)
        {
            heart.enabled = false;
        }
        else if (star.enabled)
        {
            star.enabled = false;
        }
    }

}
