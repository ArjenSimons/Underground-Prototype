using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{
    Regular = 0,
    Iron,
    Fuel,
    Crystal
}

public class GroundScript : MonoBehaviour
{

    public class Ground
    {
        public GroundType type;

        public Ground(GroundType groundType)
        {
            type = groundType;
        }
    }

    public GroundType type;
    public int iron;
    public int fuel;
    public int crystal;
    



    // Start is called before the first frame update
    void Start()
    {
       
        //Determine available resources and used textures per groundType
        switch (type)
        {
            case GroundType.Regular:
                break;
            case GroundType.Iron:
                iron = 100;
                break;
            case GroundType.Fuel:
                fuel = 100;
                break;
            case GroundType.Crystal:
                crystal = 50;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
