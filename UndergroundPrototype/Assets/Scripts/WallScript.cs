using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType
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
        public BlockType type;

        public Wall(BlockType wallType)
        {
            type = wallType;
        }
    }

    public BlockType type;
    public int health;
    private Renderer renderer;
    private Color colour;

    private Material fullHealth;
    private Material slightCrack;
    private Material mediumCrack;
    private Material hardCrack;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        colour = renderer.material.color;
        

        health = 5;
        
        //Define textures etc. per wallType
        switch (type)
        {
            case BlockType.Regular:
                fullHealth = Resources.Load("Materials/reg1") as Material;
                slightCrack = Resources.Load("Materials/reg2") as Material;
                mediumCrack = Resources.Load("Materials/reg3") as Material;
                hardCrack = Resources.Load("Materials/reg4") as Material;
                break;
            case BlockType.Iron:
                fullHealth = Resources.Load("Materials/iron1") as Material;
                slightCrack = Resources.Load("Materials/iron2") as Material;
                mediumCrack = Resources.Load("Materials/iron3") as Material;
                hardCrack = Resources.Load("Materials/iron4") as Material;
                break;
            case BlockType.Fuel:
                fullHealth = Resources.Load("Materials/fuel1") as Material;
                slightCrack = Resources.Load("Materials/fuel2") as Material;
                mediumCrack = Resources.Load("Materials/fuel3") as Material;
                hardCrack = Resources.Load("Materials/fuel4") as Material;
                break;
            case BlockType.Crystal:
                fullHealth = Resources.Load("Materials/crystal1") as Material;
                slightCrack = Resources.Load("Materials/crystal2") as Material;
                mediumCrack = Resources.Load("Materials/crystal3") as Material;
                hardCrack = Resources.Load("Materials/crystal4") as Material;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (health)
        {
            case 5:
                renderer.material = fullHealth;
                break;
            case 4:
                renderer.material = slightCrack;
                break;
            case 3:
                renderer.material = mediumCrack;
                break;
            case 2:
                renderer.material = hardCrack;
                break;
            case 1:
                renderer.material = hardCrack;
                break;
        }
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void changeSprite(string material)
    {

    }
}
