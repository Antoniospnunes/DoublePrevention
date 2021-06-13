using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    protected bool goingRight;

    protected int directionNumber;
    // Start is called before the first frame update
    void Start()
    {
        directionNumber = Random.Range(0, 2);
        if(directionNumber == 0)
        {
            goingRight = true;
        }
        else
        {
            goingRight = false;
        }
        StartCoroutine(TurnRoutine(2));
    }

    // Update is called once per frame
    void Update()
    {
        if (goingRight)
        {
            transform.Translate(5 * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(-5 * Time.deltaTime, 0, 0);
        }
    }

    private IEnumerator TurnRoutine(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            if (goingRight)
            {
                goingRight = false;
            }
            else
            {
                goingRight = true;
            }
        }
    }
}
