using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject bombPrefab;

    private const float MIN_X = -2.55f;
    private const float MAX_X = 2.55f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnBombs");
    }

    IEnumerator SpawnBombs()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));

        Instantiate(bombPrefab, new Vector3(Random.Range(MIN_X, MAX_X), this.transform.position.y, -1), Quaternion.identity);
        StartCoroutine("SpawnBombs");
    }
}
