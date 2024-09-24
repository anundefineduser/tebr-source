using Unity.Collections;
using UnityEngine;

using NaughtyAttributes;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("References")]
    [NaughtyAttributes.Label("Character Controller")] public CharacterController cc; // Name is different in Editor to make it look nicer. all hail naughtyattributes

    [Header("Player Variables")]
    public float walkSpeed;
    public float runSpeed;
    [NaughtyAttributes.ReadOnly] public float currentSpeed; // Variable is used for switching between speed states.
    [NaughtyAttributes.ReadOnly] public bool isRunning;
    public float gravityScale; // How much gravity gets applied per frame, on a scale of 0 - 1; (Can be set higher or lower, but why?!?)

    [Header("HUD Variables")]
    public Slider staminometer;

    [Space]
    public float stamina;
    public float maxStamina;
    [NaughtyAttributes.Label("Stamina Decrease Rate")] public float staminaDecRate; // Rate at which stamina is decreased.
    [NaughtyAttributes.Label("Stamina Increase Rate")] public float staminaIncRate; // Rate at which stamina is increase.

    [Header("Player Statistics")]
    [NaughtyAttributes.ReadOnly] public Vector3 velocity; // Velocity is only in the x and z axis, read comment for `gravity`.
    [NaughtyAttributes.ReadOnly] public Vector3 gravity; // Gravity velocity is seperate from normal velocity due to stuff such as moving while playing Jump Rope.

    Vector2 input;

    private void Awake()
    {
        // Checks if there's no Character Controller set, if not it'll attempt to grab one from the script GameObject.
        if (!cc)
            cc  = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; //TODO: Replace this with new Unity Input System code.

        Stamina(); // Calls stamina function to handle running/stamina.
        Movement(); // Calls movement function to handle Player movement;
        
    }

    public void Stamina()
    {
        isRunning = stamina > 0f && Input.GetKey(KeyCode.LeftShift); //TODO: Replace GetKey with new Unity Input System code. 

        if (isRunning && input != Vector2.zero)
        {
            currentSpeed = runSpeed;
            stamina -= staminaDecRate * Time.deltaTime; // Gets rid of stamina every frame, deltaTime is accounted for.
        }
        else
        {
            currentSpeed = walkSpeed;

            float rate = stamina >= maxStamina ? 0f : staminaIncRate; // Read second part of line below.
            stamina += rate * Time.deltaTime; // Adds stamina every frame similar to stamina descrease, only addition is clamped to make sure you can't go over by waiting.
        }

        staminometer.value = stamina / maxStamina;
    }

    public void Movement()
    {
        if (cc.isGrounded)
            gravity = Physics.gravity * gravityScale;
        else
            gravity = gravity + Physics.gravity * gravityScale;
        
        Vector3 transformVelocity = transform.right * input.x + transform.forward * input.y; // Translates input to Player direction.
        velocity = new Vector3(transformVelocity.x * currentSpeed, 0f, transformVelocity.z * currentSpeed); // Velocity is comprised of the transform velocity multiplied by current speed, as well as the Player's gravity.
        velocity += gravity; // Adds gravity to velocity.

        cc.Move(velocity * Time.deltaTime); // Actually moves the Player by the current velocity, deltaTime is used for inconsistent/choppy framerates.
    }
}
