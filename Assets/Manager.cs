using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class Manager : MonoBehaviour
{
    public GameObject animalPrefab;
    GameObject butt; //.GetComponent<Renderer>().enabled = true;
        
    public GameObject foodPrefab;
    GameObject timRef;
    private TextMeshProUGUI timeText;
    private bool updated = false;
    public float timeAlloted;
    // Start is called before the first frame update
    List<Animal> survivedObjects;
    List<GameObject> foodList;
    List<GameObject> sob;
    public float mutationRate;
    int gen;
    bool pause = false;
    float generationTime;
    float endofLast;
    void Start()
    {
    /**Initialze lists and gameobjects */
    survivedObjects = new List<Animal>();
    generationTime = 0;
    endofLast = 0;
    foodList = new List<GameObject>();
    sob = new List<GameObject>();
    timRef = GameObject.Find("time_text");
    timeText = timRef.GetComponent<TextMeshProUGUI>();
    gen = 0;
    mutationRate = 60f; // 0->1
    butt = GameObject.Find("next");
    butt.SetActive(false);
    spawn();

    }

    // Update is called once per frame
    void Update()
    {
    

    timeText.SetText("Time: "+Time.time);
    generationTime = Time.time - endofLast;
    if(generationTime >= timeAlloted){
        Time.timeScale = 0.0f;
         if(!updated){
             updateList();
             //Debug.Log("Done");
         
         }
         pause = true;
         //Debug.Log(survivedObjects.Count);
         //Debug.Log(sob.Count);
        // Application.Quit();
       // Debug.Log(generationTime);
     }else{
         
     }
     if(pause)butt.SetActive(true);
    else butt.SetActive(false);

    }

    private void activateStatistics(){

    }
    private void updateList(){
        List<int> ins = new List<int>();
        Debug.Log("Survivor " +survivedObjects.Count);
        for(int i=0;i<survivedObjects.Count;i++){
             if(!survivedObjects[i].reached){
                 Destroy(sob[i]);
                // Debug.Log(i);
                 ins.Add(i);
                 //GameObject temp =  sob[i];
                 survivedObjects.RemoveAt(i);
                 sob.RemoveAt(i);
                 //survivedObjects.Remove(temp.GetComponent<Animal>());
                 //sob.Remove(temp);
                 i--;
             }
        }
        updated = true;
         
    }
    public void nextGenaration(){
        /*
        Save - done
        Destroy -done
        Respawn
         */
        /**Save survivors */
        List<Att> l = new List<Att>();
        for(int i=0;i<survivedObjects.Count;i++){
            l.Add(survivedObjects[i].getAttributes());
        }
        destroyOnScreenObjects();
        gen++;
        //r.enabled = true;
        //Debug.Log(butt);
       respawn(l);
        spawnfood();
        
        pause = false;
        endofLast = Time.time;
        Time.timeScale = 1f;
        Debug.Log("Creature count " + sob.Count);
        while(l.Count > 0){
            l.RemoveAt(0);
        }
        updated = false;
    }
    private void spawn(){
    
        /**Adds animals */
        for(int i =0;i<10;i++){
            
            Vector3 instance = new Vector3(Random.Range(0.5f,19.5f),2.5f,Random.Range(-9.5f,9f));
            sob.Add(Instantiate(animalPrefab,instance,Quaternion.identity));
            survivedObjects.Add(sob[i].GetComponent<Animal>());  
                  
        }
        //sob.Add(GameObject.Find("animal"));
        //survivedObjects.Add(GameObject.Find("animal").GetComponent<Animal>());
        Destroy(GameObject.Find("animal"));
        spawnfood();

    }

    private void respawn(List<Att> listA){
        //Debug.Log("saved ob" + listA.Count);
        Queue<Att> q = new Queue<Att>();
        for(int i =0;i<listA.Count;i++){
            int prob = Random.Range(0,100);
           // Debug.Log(i +"  "+prob);
            if(prob <= mutationRate){
               // Debug.Log("a "+i);
                listA[i].magnitude*=1.4;
            }
            if(prob <= mutationRate/2){
                listA[i].replacementRate+=0;
            }
            if(prob <= mutationRate/3){
                listA[i].size*=(float)1.3;
                //Debug.Log(listA[i].size);
            }
              Vector3 instance = new Vector3(Random.Range(0.5f,19.5f),2.5f,Random.Range(-9.5f,9.5f));
                sob.Add(Instantiate(animalPrefab,instance,Quaternion.identity));
                survivedObjects.Add(sob[i].GetComponent<Animal>());
                sob[i].GetComponent<Animal>().setAttributes(listA[i]);
                Debug.Log("size after init" + survivedObjects[i].size);
                if(listA[i].replacementRate > 1){
                   // for(int y=0;y<listA[i].replacementRate;y++)q.Enqueue(listA[i]);
                }
   
        }
        while(q.Count > 0){
            int i = sob.Count;
            Vector3 instance = new Vector3(Random.Range(0.5f,19.5f),2.5f,Random.Range(-9.5f,9.5f));
            sob.Add(Instantiate(animalPrefab,instance,Quaternion.identity));
            survivedObjects.Add(sob[i].GetComponent<Animal>());
            sob[i].GetComponent<Animal>().setAttributes(q.Dequeue());
        }
        //spawnfood();
    }
    private void spawnfood(){
        
        /**Adds food */
        for(int i =0;i<40;i++){
            Vector3 instance = new Vector3(Random.Range(0f,20f),1.85f,Random.Range(-10f,10f));
            foodList.Add(Instantiate(foodPrefab,instance,Quaternion.identity));        
        }
    }
    private void destroyOnScreenObjects(){
        while(survivedObjects.Count > 0){
            survivedObjects.RemoveAt(0);
            if(sob[0] != null)Destroy(sob[0]);
            sob.RemoveAt(0);
        }
        
        while(foodList.Count > 0){
            if(foodList[0] != null)Destroy(foodList[0]);
            foodList.RemoveAt(0);
        }
        
        Destroy(GameObject.Find("food"));
        
    }

    public void destroyAnimal(GameObject g){
        survivedObjects.Remove(g.GetComponent<Animal>());
        sob.Remove(g);
        Destroy(g);
    }
}
