using UnityEngine;
using UnityEngine.U2D;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public ThrowingDagger thrw;
    
    [SerializeField] private float timeDuration;
    
    private Vector3 direction;
    private float timer;
    private byte zero;

    private void Awake()
    {
        timer = timeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            timer -= Time.deltaTime;
        
            if (timer < zero)
            {
                Return();
            }
        
            transform.position += direction * thrw.speed * Time.deltaTime;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

            foreach (Collider2D element in colliders)
            {
                if (element.TryGetComponent(typeof(Enemy), out Component enemy))
                {
                    Enemy a = (Enemy)enemy;
                
                    a.TakeDamage(thrw.damage);
                    Return();
                    break;
                }
            }
        }
    }

    public void SetDirection(float dirX, float dirY, int count, float spread, int index)
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = Quaternion.Euler(0,0,0);
        
        direction = new Vector3(dirX, dirY);

        Vector3 position = transform.position;

        if (dirX < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            position.y -= (spread * count) / 2;
            position.y += index * spread;
        }
        else if (dirX > 0)
        {
            position.y -= (spread * count) / 2;
            position.y += index * spread;
        }

        if (dirY < 0)
        {
            transform.Rotate(new Vector3(0,0,-90));
            position.x -= (spread * count) / 2;
            position.x += index * spread;
        }
        else if (dirY > 0)
        {
            transform.Rotate(new Vector3(0,0,90));
            position.x -= (spread * count) / 2;
            position.x += index * spread;
        }
        
        transform.position = position;
    }

    void Return()
    {
        thrw.Return(this);
        timer = timeDuration;
    }
}
