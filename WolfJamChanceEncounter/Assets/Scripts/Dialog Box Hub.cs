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
        /*
        fire.enabled = false;
        star.enabled = false;
        heart.enabled = false; */
        heart.gameObject.SetActive(false);
        fire.gameObject.SetActive(false);
        star.gameObject.SetActive(false);
    }

    public void UIPerk(string word)
    {
       
        attackButton.gameObject.SetActive(false);
        healButton.gameObject.SetActive(false);
        guardButton.gameObject.SetActive(false);

        if (word == "fire")
        {
            //fire.enabled = true;
            fire.gameObject.SetActive(true);
        }
        else if (word == "heart")
        {
           // heart.enabled = true;
            heart.gameObject.SetActive(true);

        }
        else if (word == "star")
        {
            //star.enabled = true;
            star.gameObject.SetActive(true);
        }
    }

    public void PerkOver()
    {
        attackButton.gameObject.SetActive(true);
        healButton.gameObject.SetActive(true);
        guardButton.gameObject.SetActive(true);
     
            fire.gameObject.SetActive(false);
         
            // heart.enabled = false;
            heart.gameObject.SetActive(false);
       
            //star.enabled = false;
            star.gameObject.SetActive(false);
      
    }
  

}
