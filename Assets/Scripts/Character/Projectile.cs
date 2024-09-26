using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Projectile stat
    [SerializeField] private float speed;
    private BoxCollider2D projBoxCollider;
    private Animator projAnimator;
    private bool hit;
    private float direction;
    private float lifeTime;

    void Awake() 
    {
        projAnimator = GetComponent<Animator>();
        projBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (hit) return;

        float projMovementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(new Vector3(projMovementSpeed, 0, 0)); 

        lifeTime += Time.deltaTime;
        if (lifeTime >= 10) gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        Debug.Log("Explode");
        hit = true;
        projBoxCollider.enabled = false;
        projAnimator.SetTrigger("explode");

        if (collider.CompareTag("Enemy") || collider.CompareTag("Object"))
        {
            if (collider.GetComponent<HP>() != null)
                collider.GetComponent<HP>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction)
    {
        lifeTime = 0;

        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        projBoxCollider.enabled = true;

        float localScaleX = transform.localScale.x;

        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX *= -1;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
