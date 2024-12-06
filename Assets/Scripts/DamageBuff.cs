using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stim")]

public class DamageBuff : PowerUpEffect
{

    public float decreaseAmount;
    public float increaseDamage;
    

    public override void Apply(GameObject target)
    {

        target.GetComponent<CyberPsychosisHandler>().psychosisLevel -= decreaseAmount;
        target.GetComponent<PlayerMovement>().damage += increaseDamage;

    }


}
