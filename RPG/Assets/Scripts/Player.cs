using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour , ICombatant
{
    public Button attack;

    public GameObject playerStatus;
    public GameObject player;

    private Slider[] sliders;

    private Slider playerHealth;
    private Slider playerAura;

    protected List<Button> command = new List<Button>();
    protected string type = "player";
    protected int health = 5000;
    protected int aura = 500;
    protected int str = 200;
    protected int spd = 10;

    private void Awake()
    {
        playerStatus.SetActive(true);

        sliders = playerStatus.GetComponentsInChildren<Slider>();

        playerHealth = sliders[0];
        playerAura = sliders[1];

        playerHealth.maxValue = health;
        playerHealth.value = playerHealth.maxValue;

        playerAura.maxValue = aura;
        playerAura.value = playerAura.maxValue;
    }

    void Update()
    {
        if (IsDead())
        {
            player.SetActive(false);
        }
    }
    private bool IsDead()
    {
        return playerHealth.value.Equals(0);
    }
    public List<Button> GetCommand()
    {
        command.Clear();
        command.Add(attack);
        return command;    
    }
    public GameObject GetObject()
    {
        return player; 
    }
    public Slider GetHealth()
    {
        return playerHealth;
    }
    public int GetStrength()
    {
        return str;
    }
    public int GetSpeed()
    {
        return spd;
    }
    public new string GetType()
    {
        return type;
    }
    public Vector3 GetPosition()
    {
        return player.transform.position;
    }
}
