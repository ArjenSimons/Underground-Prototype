using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceManager : MonoBehaviour
{

    private int fuel, iron, crystal;
    public int Iron { get; set; }
    public int Crystal { get; set; }
    public int Fuel { get; set; }

    [SerializeField] TextMeshProUGUI ironText;
    [SerializeField] TextMeshProUGUI crystalText;
    [SerializeField] TextMeshProUGUI fuelText;

    // Start is called before the first frame update
    void Start()
    {
        iron = 0;
        fuel = 0;
        crystal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Text update is done every frame, text should only be set when a change has happened.
        ironText.SetText("{0}", iron);
        fuelText.SetText("{0}", fuel);
        crystalText.SetText("{0}", crystal);
    }
}
