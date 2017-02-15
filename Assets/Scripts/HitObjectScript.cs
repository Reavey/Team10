using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectScript : MonoBehaviour {

    //This script has to be attached to the prefab that a ranged character will spawn. 

    public int minDamage;
    public int maxDamage;
    int damage;
    MeleeCharacter meleeCharScript;
    RangedCharacter rangedCharScript;
    float spawnTime;
    public int aliveTime;

    // Use this for initialization
    void Start ()
    {
        spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (spawnTime + aliveTime < Time.time)
        {
            Destroy(this.gameObject);
        }
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<MeleeCharacter>())
        {
            damage = Random.Range(minDamage, maxDamage);
            meleeCharScript = collision.gameObject.GetComponent<MeleeCharacter>();
            meleeCharScript.health -= damage;
            Destroy(this.gameObject);
        }

        if(collision.gameObject.GetComponent<RangedCharacter>())
        {
            damage = Random.Range(minDamage, maxDamage);
            rangedCharScript = collision.gameObject.GetComponent<RangedCharacter>();
            rangedCharScript.health -= damage;
            Destroy(this.gameObject);
        }
    }
}
