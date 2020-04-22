using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceManager : MonoBehaviour
{

    private int fuel, iron, crystal;
    public int Iron => iron;
    public int Crystal => crystal;
    public int Fuel => fuel;

    [SerializeField] TextMeshProUGUI ironText;
    [SerializeField] TextMeshProUGUI crystalText;
    [SerializeField] TextMeshProUGUI fuelText;

    // Start is called before the first frame update
    void Start()
    {
        iron = 100;
        fuel = 100;
        crystal = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //Text update is done every frame, text should only be set when a change has happened.
        ironText.SetText("{0}", iron);
        fuelText.SetText("{0}", fuel);
        crystalText.SetText("{0}", crystal);
    }

    public void ChangeIronAmount(int amount)
    {
        iron += amount;
    }

    public void ChangeFuelAmount(int amount)
    {
        fuel += amount;
    }

    public void ChangeCrystalAmount(int amount)
    {
        crystal += amount;
    }
}
