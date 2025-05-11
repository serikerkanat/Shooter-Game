using UnityEngine;

public class TargetBall : MonoBehaviour
{
    public GameObject explosionEffect;
    public AudioClip destroySound;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioSource.PlayClipAtPoint(destroySound, transform.position);

            if (explosionEffect != null)
                Instantiate(explosionEffect, transform.position, Quaternion.identity);

            GameManager.Instance.AddScore();

            Destroy(gameObject);
        }
    }
}
