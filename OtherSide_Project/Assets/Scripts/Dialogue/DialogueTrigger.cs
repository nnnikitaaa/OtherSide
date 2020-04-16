using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool repeat;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().AddAndShowTextNow(dialogue.showText);
            if (!repeat)
            {
                Destroy(gameObject);
            }

        }
    }
}
