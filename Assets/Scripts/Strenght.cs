using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strenght : PowerUp
{
    public float duration;
    protected override void Pickup()
    {
        base.Pickup();
        StartCoroutine(AddStrenght());
    }

    IEnumerator AddStrenght()
    {
        playerObj.AddDamage(value);
        yield return new WaitForSeconds(duration);
        playerObj.AddDamage(-value);
    }
}
