using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    public float time = 3f;

    private GameObject combatant;
    private GameObject target;
    private float offset = 1.0f;
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Act(ICombatant c, ICombatant t)
    {
        SetObjects(c, t);
        GetTargetPosition();

        combatant.transform.LookAt(targetPosition);

        //combatant.transform.Translate(Vector3.Lerp(combatant.transform.position, targetPosition, time));
    }

    private void SetObjects(ICombatant c, ICombatant t)
    {
        combatant = c.GetObject();
        target = t.GetObject();
    }

    private void GetTargetPosition()
    {
        if (combatant.GetType().Equals("player"))
        {
            targetPosition = new Vector3(target.transform.position.x, combatant.transform.position.y, target.transform.position.z - offset);
        }
        else
        {
            targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - offset);
        }
    }
}
