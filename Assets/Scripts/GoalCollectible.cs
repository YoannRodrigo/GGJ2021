using UnityEngine;

public class GoalCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerManager>().Win();
            gameObject.SetActive(false);
        }
    }
}
