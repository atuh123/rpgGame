using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePoints : MonoBehaviour
{
    public Text healthPoints;
    public Slider healthBar;

    public Text auraPoints;
    public Slider auraBar;

    // Start is called before the first frame update
    void Start()
    {
        healthPoints.text = healthBar.value.ToString();
        auraPoints.text = auraBar.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthPoints.text = healthBar.value.ToString();
        auraPoints.text = auraBar.value.ToString();
    }
}
