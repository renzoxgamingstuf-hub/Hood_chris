using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{
    private Animator animator;
    public GameObject LeftHand;
    public GameObject RightHand;
    public int cooldown;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && cooldown <= 0)
        {
            animator.SetTrigger("CPunchL");
            LeftHand.SetActive(true);
            RightHand.SetActive(false);
            cooldown = 100;
        }
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame && cooldown <= 0)
        {
            animator.SetTrigger("CPunchR");
            RightHand.SetActive(true);
            LeftHand.SetActive(false);
            cooldown = 100;
        }
        if (cooldown > 0)
        {
            cooldown--;
        }
    }
}
