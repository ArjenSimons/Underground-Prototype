using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WallType
{
    Regular = 0,
    Iron,
    Fuel,
    Crystal
}

public class WallScript : MonoBehaviour
{
    public class Wall
    {
        public WallType type;

        public Wall(WallType wallType)
        {
            type = wallType;
        }
    }

    public WallType type;
    public int health;
    private Renderer renderer;
    private Color colour;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        colour = renderer.material.color;
        

        health = 5;
        
        //Define textures etc. per wallType
        switch (type)
        {
            case WallType.Regular:
                break;
            case WallType.Iron:
                break;
            case WallType.Fuel:
                break;
            case WallType.Crystal:
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (health)
        {
            case 5:
                colour = renderer.material.color;
                colour.a = 0.1f;
                renderer.material.color = colour;
                break;
            case 4:
                colour = renderer.material.color;
                colour.a += 0.1f;
                renderer.material.color = colour;
                break;
            case 3:
                colour = renderer.material.color;
                colour.a += 0.1f;
                renderer.material.color = colour;
                break;
            case 2:
                colour = renderer.material.color;
                colour.a += 0.1f;
                renderer.material.color = colour;
                break;
            case 1:
                colour = renderer.material.color;
                colour.a += 0.1f;
                renderer.material.color = colour;
                break;
        }
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
