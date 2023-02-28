using System.Collections;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject bombPrefab;

    void Start()
    {
        StartCoroutine("SpawnBombs");
    }

    IEnumerator SpawnBombs()
    {
        if(GameState.Instance.State == GameStateEnum.PLAY)
        {
            yield return new WaitForSeconds(Random.Range(0f, 1f));

            Instantiate(bombPrefab, new Vector3(Random.Range(GameState.MIN_X, GameState.MAX_X), this.transform.position.y, -1), Quaternion.identity);
            StartCoroutine("SpawnBombs");
        }
    }
}
