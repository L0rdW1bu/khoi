using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;
    public int damage = 10;
    private string targetTag = "Enemy"; 

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetTargetTag(string tag)
    {
        targetTag = tag;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log($"{targetTag} trúng đạn, mất {damage} máu!");
            
        }

       
        Destroy(gameObject);
    }
}