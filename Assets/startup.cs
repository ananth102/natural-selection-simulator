using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    
    Slider mutation_rate;
    Slider timeS;
    Slider food;
    Slider anim;


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
        
        mutation_rate = mutation_slider.GetComponentInChildren<Slider>();
        timeS = time_slider.GetComponentInChildren<Slider>();
        anim = animal_slider.GetComponentInChildren<Slider>();
        food = food_slider.GetComponentInChildren<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick(){
        startButton.SetActive(false);
        blur.SetActive(false);
        title.SetActive(false);

        m.mutationRate = (int)(mutation_rate.value*100);
        m.timeAlloted = 5.0f + timeS.value*20.0f;
        m.initialAnimNum = 1 + (int)(anim.value*30.0);
        m.initialfoodNum = 10 + (int)(food.value*100.0);

        mutation_slider.SetActive(false);
        time_slider.SetActive(false);
        animal_slider.SetActive(false);
        food_slider.SetActive(false);
        m.restart();
        m.intro = false;
    }

}
