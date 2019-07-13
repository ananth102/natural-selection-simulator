using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public bool reached = false;
    float yRotation =0;
    Vector3[] points;
    bool atHome =false;
    Vector3 speed;
    public double magnitude;
    public float size;
    Att attributes;
    public Manager managerref;
    // Start is called before the first frame update
    /*
    Left Barrier   = (0,7.64,-10->10)
    Bottom Barrier = (0->20,7.64,-9.84)
    Right Barrier  = (19.91,7.64,-10->10)
    Top Barrier    = (0->20,7.64,9.96) 
    
     */
    void Start()
    {
       // Debug.Log("Init"+ Time.time);
        points = new Vector3[4];
        points[0] = new Vector3(0,7.64f,Random.Range(-10f,10f));
        points[1] = new Vector3(Random.Range(0f,20f),7.64f,-9.84f);
        points[2] = new Vector3(19.91f,7.64f,Random.Range(-10f,10f));
        points[3] = new Vector3(Random.Range(0f,20f),7.64f,9.96f);

        yRotation = Random.Range(-100,100);
        transform.Rotate(0,yRotation,0,Space.World);
        
        
        speed = Vector3.forward;
        if(attributes == null){
            magnitude = 2;
            size = 1.0f;
            attributes = new Att();
            attributes.setMagnitude(magnitude);
            attributes.setSize(1f);
        }
       
        
        managerref = GameObject.Find("Manager").GetComponent<Manager>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed*(float)magnitude*Time.deltaTime);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        string w = collision.transform.name;
        Debug.Log("colwid");
    }
    private void OnTriggerEnter(Collider other){
        string Name = other.name;
        if((Name.Substring(0,4)).Equals("food")){
            Destroy(other.gameObject);
            returnHome();
            atHome = true;
        }
        if(Name.Equals("animal(Clone)")){
            float otherSize = other.gameObject.GetComponent<Animal>().size;
          //  Debug.Log("Encounter" + otherSize + "  " + size);
            if(size > otherSize){
               // Debug.Log("Killed other");
                managerref.destroyAnimal(other.gameObject);
                returnHome();
            }
        }
        /* 
        if(Name.Substring(0,4).Equals("Trigg")){
            
        }
        */
        transform.Rotate(0,180,0,Space.World);
        if(atHome && (Name.Substring(0,4)).Equals("Trig")){
            speed = Vector3.zero;
            transform.Rotate(0,30,0,Space.World);
            reached = true;
        }
    }
    private void returnHome(){
        atHome = true;
    }

    private Vector3 getClosestPoint(){
        int min = 0;
        float m = 1000f;
        for (int i = 0; i < points.Length; i++)
        {
           m = Vector3.Distance(transform.position,points[i]) < m ? Vector3.Distance(transform.position,points[i]) : m;
           min = Vector3.Distance(transform.position,points[i]) == m ? i : min;
        }
        return points[min];
    }

    public Att getAttributes(){
        return attributes;
    }
    public void setAttributes(Att a){
       // Debug.Log("Size :"+a.size);
        attributes = a;
        magnitude = a.magnitude;
        size = a.size;
        float newSizeScale = size - transform.lossyScale.x;
        transform.localScale += new Vector3(newSizeScale, newSizeScale, newSizeScale);

    }
    

}
public class Att {

    
    public double magnitude;
    public float size;
    private int genarations;
    private int number_mutations;
    public int replacementRate = 2;

    public void setMagnitude(double sp){
        magnitude = sp;
    }
    public void setSize(float s){
        size = s;
    }
    public Att getAt(){
        return this;
    }
} 
    




