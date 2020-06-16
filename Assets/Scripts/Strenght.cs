using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strenght : PowerUp
{
    public float duration;
    public GameObject icon;
    protected override void Pickup()
    {
        base.Pickup();
        audioManager.Play("PowerUp");
        StartCoroutine(AddStrenght());
        Destroyer();
    }

    IEnumerator AddStrenght()
    {
        playerObj.AddDamage(value);
        icon.SetActive(true);
        yield return new WaitForSeconds(duration);
        playerObj.AddDamage(-value);
        icon.SetActive(false);
    }
}