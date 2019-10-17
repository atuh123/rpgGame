using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour, ICombatant
{
    public GameObject enemyStatus;
    public GameObject enemy;

    private Slider[] sliders;

    private Slider enemyHealth;
    private Slider enemyAura;

    protected string type = "enemy";
    protected int health = 1000;
    protected int aura = 100;
    protected int str = 100;
    protected int spd = 5;

    private void Awake()
    {
        enemyStatus.SetActive(false);

        sliders = enemyStatus.GetComponentsInChildren<Slider>();

        enemyHealth = sliders[0];
        enemyAura = sliders[1];

        enemyHealth.maxValue = health;
        enemyHealth.value = enemyHealth.maxValue;

        enemyAura.maxValue = aura;
        enemyAura.value = enemyAura.maxValue;
    }
    void Update()
    {
        if (IsDead())
        {
            enemy.SetActive(false);
        }
    }

    private bool IsDead()
    {
        return enemyHealth.value.Equals(0);
    }
    public GameObject GetObject()
    {
        return enemy;
    }
    public Slider GetHealth()
    {
        return enemyHealth;
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
        return enemy.transform.position;
    }

    public void DisplayStats()
    {
        enemyStatus.SetActive(true);
    }

    public void HideStats()
    {
        enemyStatus.SetActive(false);
    }
}
