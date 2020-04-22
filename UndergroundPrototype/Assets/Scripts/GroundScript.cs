using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{

    public class Ground
    {
        public BlockType type;
        public Ground(BlockType groundType)
        {
            type = groundType;
        }
    }

    public BlockType type;
    public int iron;
    public int fuel;
    public int crystal;

    [SerializeField]
    public bool freeSocket = true;


    // Start is called before the first frame update
    void Start()
    {
        //Determine available resources and used textures per groundType
        switch (type)
        {
            case BlockType.Regular:
                break;
            case BlockType.Iron:
                iron = 100;
                break;
            case BlockType.Fuel:
                fuel = 100;
                break;
            case BlockType.Crystal:
                crystal = 50;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void SetType(BlockType type)
    {
        this.type = type;
    }

    public BlockType GetType()
    {
        return this.type;
    }
}
