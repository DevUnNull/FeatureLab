using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatEnemy : MonoBehaviour
{
    public float MaxhealthEnemy = 10f;
    private float curentHealthEnemy;

    // Start is called before the first frame update
    void Start()
    {
        curentHealthEnemy = MaxhealthEnemy;
        Debug.Log("mau hien tai :" + curentHealthEnemy);
    }

    public void TakeDamage(float damage) 
    {
        curentHealthEnemy -= damage;
        Debug.Log("mau hien tai :" + curentHealthEnemy);
        if (curentHealthEnemy <= 0) 
        {
            die();
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }

}
