using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassGen : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    [SerializeField] private GameObject concrete8;
    [SerializeField] private GameObject concrete9;
    [SerializeField] private GameObject ship;
    private int x,y,z,objToGen,objQuantity,shipQuantity;
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateObjects();
    }


    private void GenerateObjects() {
    GameObject[] objects = {brick,concrete8,concrete9};
    while (shipQuantity<20){
            x=Random.Range(-500,500);
            y=Random.Range(-500,500);
            z=Random.Range(-500,500);
            Instantiate(ship,new Vector3(x, y, z), Random.rotation);
            shipQuantity++;
    }
    while(objQuantity<50){
        x=Random.Range(-750,750);
        y=Random.Range(-750,750);
        z=Random.Range(-750,750);
        Instantiate(objects[Random.Range(0,objects.Length-1)],new Vector3(x, y, z), Random.rotation);
        objQuantity++;
    }
    }


}
