using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] GameObject AsteroidPiecePrefab = null;
    [SerializeField] ParticleSystem collisionVfx = null;
    [SerializeField] int numberOfPieces = 4;
    [SerializeField] float health = 4;
    [SerializeField] float speed = 3.0f;
    [SerializeField] int pointValue = 50;

    private Vector3 direction;
    private GameManager gameManager;
    private AsteroidSpawner asteroidSpawner;

    public static event Action<Asteroid> OnAsteroidDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0, 0, UnityEngine.Random.Range(0f, 360.0f));
        transform.eulerAngles = direction;

        gameManager = FindObjectOfType<GameManager>();
        asteroidSpawner = GameObject.FindObjectOfType<AsteroidSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    public int GetPointValue() { return pointValue; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            GameObject.Destroy(collision.gameObject);
        }

        if (collisionVfx != null)
        {
            StartCoroutine(PlayCollisionVfx(collision.transform.position));
        }

        health -= 1;
        
        if (health <= 0)
        {
            if (AsteroidPiecePrefab != null)
            { 
                // Create pieces
                for(int i = 0; i < numberOfPieces; i++)
                {
                    Instantiate<GameObject>(AsteroidPiecePrefab, this.transform.position, Quaternion.identity, asteroidSpawner.GetComponent<Transform>());
                    asteroidSpawner.AddAsteroidCount();
                }
            }

            asteroidSpawner.DecreaseAsteroidCount();

            OnAsteroidDestroyed?.Invoke(this);

            GameObject.Destroy(this.gameObject);
        }
    }

    private IEnumerator PlayCollisionVfx(Vector3 position)
    {
        ParticleSystem vfx = Instantiate<ParticleSystem>(collisionVfx, position, Quaternion.identity, gameObject.GetComponentInParent<Transform>());
        vfx.Play();

        yield return new WaitForSeconds(0.75f);

        GameObject.Destroy(vfx.gameObject);
    }
}
