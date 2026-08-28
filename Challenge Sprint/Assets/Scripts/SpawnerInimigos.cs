using UnityEngine;

public class SpawnerInimigos : MonoBehaviour
{
    [Header("Inimigo")]
    public GameObject inimigoPrefab;

    [Header("Spawn")]
    public float intervalo = 2f;
    public float distanciaDoSpawn = 5f;

    private float proximoSpawn;

    void Update()
    {
        if (Time.time >= proximoSpawn)
        {
            SpawnarInimigo();

            proximoSpawn = Time.time + intervalo;
        }
    }

    void SpawnarInimigo()
    {
        Vector3 posicaoSpawn = transform.position;

        // Pequena variação aleatória na posição
        posicaoSpawn.x += Random.Range(
            -distanciaDoSpawn,
            distanciaDoSpawn
        );

        posicaoSpawn.z += Random.Range(
            -distanciaDoSpawn,
            distanciaDoSpawn
        );

        Instantiate(
            inimigoPrefab,
            posicaoSpawn,
            Quaternion.identity
        );
    }
}