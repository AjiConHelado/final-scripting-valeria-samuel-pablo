using UnityEngine;
using UnityEngine.U2D;

public class LookAt : MonoBehaviour
{
    private EnemySpawner spawner;
    private Transform player;
    private SpriteShapeRenderer rend;

    private void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        player = FindObjectOfType<PlayerController>().transform;
        rend = GetComponent<SpriteShapeRenderer>();
        rend.enabled = false;
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 1.09f, player.transform.position.y + 0.14f, 0);
        
        if (spawner.thisIsBoss != null)
        {
            LookAtFunction(spawner.thisIsBoss.position);
            rend.enabled = true;
        }
    }

    void LookAtFunction(Vector2 targetPosition)
    {
        Vector2 forward = targetPosition - (Vector2)transform.position;

        float rad = Mathf.Atan2(forward.y, forward.x) - Mathf.PI / 2;

        Rotate(rad);
    }
    
    void Rotate(float radians)
    {
        transform.rotation = Quaternion.Euler(0, 0, radians * Mathf.Rad2Deg);
    }
}
