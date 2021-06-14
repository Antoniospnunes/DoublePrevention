using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcohol : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(10 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

}
