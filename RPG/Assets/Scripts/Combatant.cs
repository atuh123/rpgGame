using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ICombatant 
{
    GameObject GetObject();
    string GetType();
    Slider GetHealth();
    int GetStrength();
    int GetSpeed();
}
