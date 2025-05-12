using UnityEngine;

public class Geo : MonoBehaviour
{
    [SerializeField] public int amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            PlayerManager.instance.AddCurrency(amount);

            Destroy(transform.parent.gameObject);
        }
    }
}
