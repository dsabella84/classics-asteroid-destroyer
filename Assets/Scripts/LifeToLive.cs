using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeToLive : MonoBehaviour
{
    [SerializeField] float timeToLive = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObjectDie());
    }

    IEnumerator ObjectDie()
    {
        yield return new WaitForSeconds(timeToLive);

        GameObject.Destroy(gameObject);
    }
}
