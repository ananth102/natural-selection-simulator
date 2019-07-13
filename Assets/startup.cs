using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startup : MonoBehaviour
{
    GameObject startButton;
    GameObject blur;
    GameObject mutation_slider;
    GameObject time_slider;
    GameObject animal_slider;
    GameObject food_slider;
    GameObject title;
    
    GameObject time_text;
    Manager m;
    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.Find("start_button");
        blur = GameObject.Find("blur");
        title = GameObject.Find("game_title");
        time_text = GameObject.Find("time_text");
        //time_text.SetActive(false);

        m = GameObject.Find("Manager").GetComponent<Manager>();

        mutation_slider = GameObject.Find("mutation_rat");
        time_slider = GameObject.Find("time each genaration");
        animal_slider = GameObject.Find("Amount of creatures");
        food_slider  = GameObject.Find("amount of food");
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick(){
        startButton.SetActive(false);
        blur.SetActive(false);
        title.SetActive(false);

        mutation_slider.SetActive(false);
        time_slider.SetActive(false);
        animal_slider.SetActive(false);
        food_slider.SetActive(false);
        m.restart();
    }

}
