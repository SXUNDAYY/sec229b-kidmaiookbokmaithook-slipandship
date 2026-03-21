using UnityEngine;

public class CheckpointColor : MonoBehaviour
{
    
    public Color defaultColor = Color.red;
    public Color activatedColor = Color.green;

    private Renderer rend;
    private bool isActivated = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = defaultColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            ActivateCheckpoint();
        }
    }

    void ActivateCheckpoint()
    {
        isActivated = true;
        rend.material.color = activatedColor;
    }
}
