using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public bool inLife = true;
    public bool canWalk = true;

    
    [SerializeField] private Image imageCooldown;
    [SerializeField] private TMP_Text textCooldown;
    [SerializeField] private float coolDownTime = 5f;
    private float coolDownTimer = 0f;
    private bool isCoolDown = false;

    [SerializeField] private Image imageCooldownSkill2;
    [SerializeField] private TMP_Text textCooldownSkill2;
    [SerializeField] private float coolDownTimeSkill2 = 5f;
    private float skill2CoolDownTimer = 0f;
    private bool skill2isCoolDown = false;

    [SerializeField] private GameObject swordTrails;
    [SerializeField] private float trailTime =7f;
    private float trailTimer =0f;
    public bool swordTrail = false;
    

    public GameObject pickupUI;
    
    [SerializeField] private float MaxHealth;
    public float currentHealth;
    [SerializeField] Slider healthBar;
    [SerializeField] private TMP_Text healthText;

    PlayerInput playerInput;
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private bool isMovementPressed;
    private CharacterController characterController;
    private Animator animator;
    
    //private int combatLayer;
    public bool inCombat = false;
    private float timeCombat;
    public float comboTime = 2f;
    public float unequipTime = 4f;
    
    private float movementSpeed;
    public float combatMovSpeed = 3f;
    public float normalMovSpeed = 5f;
    
    
    private int isWalkingHash;

    private float rotationFactorPerFrame = 15f;

    [SerializeField] private GameObject handWeapon;
    [SerializeField] private GameObject backWeapon;
    
    [SerializeField] private GameObject damageBox;
    [SerializeField] private GameObject spinCollider;
    [SerializeField] private GameObject skill2Col;

    private GameObject item;
    private GameObject door;
    public bool key;


    private int randomX;
    void Awake()
    {
        currentHealth = MaxHealth;
        
        characterController = GetComponent<CharacterController>();
        movementSpeed = normalMovSpeed;
        playerInput = new PlayerInput();
        animator = GetComponent<Animator>();
        
        isWalkingHash = Animator.StringToHash("isWalking");
        
        playerInput.PlayerController.Movement.started += onMovementInput;
        playerInput.PlayerController.Movement.canceled += onMovementInput;
        playerInput.PlayerController.Movement.performed += onMovementInput;

        //combatLayer = animator.GetLayerIndex("Combat");
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }
    
    void Update()
    {
        
        handleGravity();
        handleRotation();
        handleAnimation();

        if (canWalk)
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        

        timeCombat += Time.deltaTime;
        
        if (timeCombat >= comboTime)
        {
            animator.SetBool("isCombo",false);
        }

        if (timeCombat >= unequipTime)
        {
            if (currentMovement.x == 0 && currentMovement.z == 0)
            {
                animator.SetBool("inCombat",false);
                inCombat = false;
                movementSpeed = normalMovSpeed;
            }
            
        }

        if (inCombat)
        {
            movementSpeed = combatMovSpeed;
        }

        healthBar.value = currentHealth;
        healthText.text = currentHealth.ToString();


        if (trailTimer > 0)
        {
            trailTimer -= Time.deltaTime;
        }else if (trailTimer <= 0)
        {
            swordTrails.SetActive(false);
            swordTrail = false;

        }
        
        if (isCoolDown)
        {
            ApplyCooldown();
        }

        if (skill2isCoolDown)
        {
            ApplyCooldownSkill2(); 
        }
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;
        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation= Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame*Time.deltaTime);
        }
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        if (canWalk)
        {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x * movementSpeed;
            currentMovement.z = currentMovementInput.y * movementSpeed;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;    
        }
        
    }

    private void Stop()
    {
        isMovementPressed = false;
        currentMovement.x = 0;
        currentMovement.z = 0;

    }
    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity * Time.deltaTime;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity * Time.deltaTime;
        }
    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash,true);
        }else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash,false);
        }
        else if(!isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash,false);
        }

        
    }
    

    private void OnEnable()
    {
        playerInput.PlayerController.Enable();
    }

    private void OnDisable()
    {
        playerInput.PlayerController.Disable();

    }

    public void Attack()
    {
        Stop();
        inCombat = true;
        animator.SetBool("inCombat",true);
        animator.SetBool("isCombo",true);
        timeCombat = 0;
       // animator.SetLayerWeight(combatLayer,1);
        animator.SetTrigger("attack");
    }

    public void SpinSkill()
    {
        if (!isCoolDown)
        {
            Stop();
            inCombat = true;
            animator.SetBool("inCombat",true);
            animator.SetBool("isCombo",false);

            timeCombat = 0;
            animator.SetBool("Spin",true);
            StartCoroutine(wait());


            isCoolDown = true;
            textCooldown.gameObject.SetActive(true);
            coolDownTimer = coolDownTime;
        }
        else
        {
            Debug.Log("Bekleme Süresinde!");
        }
        
    }
    public void Skill2Play()
    {
        if (!skill2isCoolDown)
        {
            Stop();
            inCombat = true;
            animator.SetBool("inCombat",true);
            animator.SetBool("isCombo",false);

            timeCombat = 0;
            animator.SetBool("Skill2",true);
            swordTrails.SetActive(true);
            swordTrail = true;
            trailTimer = trailTime;
            
            
            StartCoroutine(wait());
        
            skill2isCoolDown = true;
            textCooldownSkill2.gameObject.SetActive(true);
            skill2CoolDownTimer = coolDownTimeSkill2;
        }


    }
    public IEnumerator spinAttack()
    {
        spinCollider.SetActive(true);
        yield return null; // 1 frame bekle
        spinCollider.SetActive(false);
    }
    public IEnumerator Skill2()
    {
        skill2Col.SetActive(true);
        yield return null; // 1 frame bekle
        skill2Col.SetActive(false);
    }

    private void ApplyCooldown()
    {
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer < 0.0f)
        {
            isCoolDown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(coolDownTimer).ToString();
            imageCooldown.fillAmount = coolDownTimer / coolDownTime;
        }
    }
    private void ApplyCooldownSkill2()
    {
        skill2CoolDownTimer -= Time.deltaTime;
        if (skill2CoolDownTimer < 0.0f)
        {
            skill2isCoolDown = false;
            textCooldownSkill2.gameObject.SetActive(false);
            imageCooldownSkill2.fillAmount = 0.0f;
        }
        else
        {
            textCooldownSkill2.text = Mathf.RoundToInt(skill2CoolDownTimer).ToString();
            imageCooldownSkill2.fillAmount = skill2CoolDownTimer / coolDownTimeSkill2;
        }
    }
    public void Equip()
    {
        backWeapon.SetActive(false);
        handWeapon.SetActive(true);
    }
    public void UnEquip()
    {
        backWeapon.SetActive(true);
        handWeapon.SetActive(false);
    }
    
    public IEnumerator animAttack()
    {
        damageBox.SetActive(true);
        yield return null; // 1 frame bekle
        damageBox.SetActive(false);
    }
    
    
    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            randomX = Random.Range(0, 5);
            currentHealth -= damage;
            GetComponent<PlayerSounds>().PlayDamageSound(randomX);
            DamagePopUpGenerator.current.CreatePopUp(transform.position,damage.ToString(),Color.yellow);
            gameObject.GetComponent<EffectController>().PlayHitEffect();
            animator.SetTrigger("GetHit");
            
        }
        else if (currentHealth <=2 && inLife)
        {
            inLife = false;
            animator.SetTrigger("Death");
            StartCoroutine(scene());
        }
    }

    public void CanWalk(int a)
    {
        if (a==0)
        {
            canWalk = false;
        }
        else
        {
            canWalk = true;
        }
    }

    public IEnumerator wait()
    {
        yield return  new WaitForSeconds(1);
        animator.SetBool("Spin",false);
        animator.SetBool("Skill2",false);

    }
    public IEnumerator scene()
    {
        yield return  new WaitForSeconds(1);
        SceneManager.LoadScene(0);

    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Door")
        {
            door = col.gameObject;
        }
    }


    public void Open()
    {
        if (door)
        {
            door.GetComponent<EnvironmentInterection>().OpenDoor(key);
        }
    }
    
}
