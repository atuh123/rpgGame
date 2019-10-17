using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float speed = 0.01f;
    public BattleCamera battleCamera;
    protected Animator animator = new Animator();
    protected Player currentPlayer;
    protected Enemy chosenTarget;
    protected List<Button> currentPlayerCmd = new List<Button>();
    protected bool doneRound;
    protected bool doneTurn;
    protected bool canChoose;
    protected bool isChosen;
    protected List<Player> players = new List<Player>();
    protected List<Enemy> enemies = new List<Enemy>();
    protected List<ICombatant> combatants = new List<ICombatant>();

    int input;

    void Awake()
    {
        GetCombatants();
        GetOrder();
        StartCoroutine(Action());
    }

    //Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if done round, gets all alive combatants, orders, and begins new round
        if (doneRound)
        {
            Debug.Log("Round done");
            doneRound = false;
            players.Clear();
            enemies.Clear();
            combatants.Clear();
            GetCombatants();
            GetOrder();
            StartCoroutine(Action());
        }

        if (canChoose)
        {
            input -= Mathf.FloorToInt(Input.GetAxis("Horizontal"));
            if (input > enemies.Count - 1) { input = 0; }
            else if (input < 0) { input = enemies.Count - 1; }
            battleCamera.transform.LookAt(enemies[input].transform.position) ;
            ToggleStats(enemies[input]);
            isChosen = Input.GetButton("Submit");
            Debug.Log("Deciding target..." + input + ", " + Input.GetAxis("Horizontal") + ", " + chosenTarget);
        }

        if (DoneBattle())
        {
            Debug.Log("Victory!");
        }
    }
    //contains all listeners for current player buttons
    void Commands()
    {
        foreach(Button b in currentPlayerCmd)
        {
            b.onClick.RemoveAllListeners();
        }
        currentPlayerCmd[0].onClick.AddListener(Attack);
    }
    //toggle command visibility
    void HideCommands()
    {
        foreach (Button b in currentPlayerCmd)
        {
            b.gameObject.SetActive(false);
        }
    }
    void ShowCommands()
    {
        foreach (Button b in currentPlayerCmd)
        {
            b.gameObject.SetActive(true);
        }
    }
    void LockCommands()
    {
        foreach (Button b in currentPlayerCmd)
        {
            b.interactable = false; 
        } 
    }
    void UnlockCommands()
    {
        foreach (Button b in currentPlayerCmd)
        {
            b.interactable = true;
        }
    }
    //gets players and enemies and adds them to global List
    void GetCombatants()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject g in temp)
        {
            players.Add(g.GetComponent<Player>());
            combatants.Add(g.GetComponent<Player>());
            Debug.Log("Got player " + g);
        }

        temp = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject g in temp)
        {
            enemies.Add(g.GetComponent<Enemy>());
            combatants.Add(g.GetComponent<Enemy>());
            Debug.Log("Got enemy " + g);
        }

        foreach(ICombatant c in combatants)
        {
            Debug.Log("Combatants: " + c);
        }

    }
    //adds players and enemies to order array and sorts based on speed
    void GetOrder()
    {

    }

    //recieves combatant in battle and delegates whether player or enemy character
    IEnumerator Action() 
    {
        int i = 0;
        foreach(ICombatant c in combatants)
        {
            if (c.GetType().Equals("player"))
            {
                Debug.Log("c type is " + c.GetType());
                PlayerTurn((Player)c);
                yield return new WaitUntil(() => doneTurn);
                UnlockCommands();
                HideCommands();
            }
            else if (c.GetType().Equals("enemy"))
            {
                EnemyTurn((Enemy)c);
            }
            doneTurn = false;

            Debug.Log("Turn: " + i);
            i++;
        }
        doneRound = true;
    }

    //runs player turn
    void PlayerTurn(Player p)
    {
        doneTurn = false;
        currentPlayer = p;
        currentPlayerCmd = currentPlayer.GetCommand();
        ShowCommands();
        Commands();
        Debug.Log("Player turn ran.");
    }
    //runs enemy turn
    void EnemyTurn(Enemy e)
    {
        Player p = players[0];
        p.GetHealth().value -= e.GetStrength();
        Debug.Log("Enemy turn ran.");
        doneTurn = true;
    }
    //runs attack on enemy
    void Attack()
    {
        StartCoroutine(ChooseTarget());
    }
    IEnumerator ChooseTarget()
    {
        Debug.Log("Chose target.");
        LockCommands();
        input = 0;
        canChoose = true;
        isChosen = false;
        yield return new WaitUntil(() => isChosen);
        chosenTarget = enemies[input];
        battleCamera.ResetPosition();
        HideEnemyStats();
        canChoose = false;
        isChosen = false;
        AttackTarget();
        animator.Act(currentPlayer, chosenTarget);
        doneTurn = true;
    }
    void AttackTarget()
    {
        chosenTarget.GetHealth().value -= currentPlayer.GetStrength();
        Debug.Log("Attack ran");
    }
    //checks if battle is done
    bool DoneBattle()
    {
        return enemies.Count.Equals(0) || players.Count.Equals(0); 
    }

    void ToggleStats(Enemy currentTarget)
    {
        foreach (Enemy e in enemies)
        {
            if (e.Equals(currentTarget))
            {
                e.DisplayStats();
            }
            else
            {
                e.HideStats();
            }
        }
    }

    void HideEnemyStats()
    {
        foreach (Enemy e in enemies)
        {
            e.HideStats();
        }
    }
}
