using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public enum PlayerStates
{
    Idle,
    Walk,
    Run,
    InMenu
}

[RequireComponent(typeof(CharacterController))]
public class Player : StateMachine<PlayerStates>
{
    [Header("Player Stats")]
    public int maxHealth;
    private int health;

    public float maxStamina;
    private float stamina;
    private float staminaDrain;

    [Header("Movement")]
    public Camera playerCamera;
    CharacterController characterController;

    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float sensitivity = 2f;
    public float lookXLimit = 45f;
    RaycastHit hit;
    public float reach = 10f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    [Header("HUD/UI")]
    public Canvas playerHUD;
    public Canvas playerHotbar;
    public TMP_Text interactTooltip;
    private Image[] hotbarImages;

    [Header("Inventory")]
    public InventoryObject inventory;
    public HotbarObject hotbar;
    public int currentHotbarIndex;
    private GameObject heldItemObject;
    public GameObject playerHand;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check for a spawnpoint
        GameObject spawnpoint = GameObject.FindWithTag("Spawnpoint");
        if (spawnpoint)
        {
            transform.position = spawnpoint.transform.position;
        }
    }

    void Start()
    {
        // Get the character controller if not defined
        characterController = GetComponent<CharacterController>();

        // Hide the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get hotbar images
        hotbarImages = playerHotbar.GetComponentsInChildren<Image>();
    }

    public void Update()
    {
        // Hide the mouse
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        HandleMovement();
        HandleRaycast();
        HandleUpdateHotbar();
        CheckForMouseClick();
    }

    private void HandleUpdateHotbar()
    {
        int previousHotbarIndex = currentHotbarIndex;

        // Check for scrolling forward and backwards
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) currentHotbarIndex -= 1;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) currentHotbarIndex += 1;

        // Check bounds of hotbar
        if (currentHotbarIndex >= hotbar.maxSize) currentHotbarIndex = hotbar.maxSize - 1;
        else if (currentHotbarIndex < 0) currentHotbarIndex = 0;

        // Update the object at the player
        HandleHeldItem(previousHotbarIndex, currentHotbarIndex);

        // Reset colors of the current hotbar slots
        for (int i = 0; i < hotbar.maxSize; i++)
        {
            hotbarImages[i].color = Color.white;

            try
            {
                hotbarImages[i].sprite = hotbar.Container[i].item.image;
            }
            catch { }
        }
        // Set the selected slot's color
        playerHotbar.GetComponentsInChildren<Image>()[currentHotbarIndex].color = Color.red;
    }

    private void HandleHeldItem(int previousIndex, int currentIndex)
    {
        // Update the object at the player
        try
        {
            // Check if the user has scrolled/changed items
            if (previousIndex != currentIndex)
            {
                // Get the currently held item
                ItemObject heldItem = hotbar.Container[currentIndex].item;

                // Destroy the current game object
                Destroy(heldItemObject);

                // Create the new game object
                heldItemObject = Instantiate(heldItem.prefab);
                heldItemObject.transform.parent = playerHand.transform;

            }
            // Update the rotation and positioning of the object
            heldItemObject.transform.position = playerHand.transform.position;
            heldItemObject.transform.rotation = playerCamera.transform.rotation;

        }
        catch (Exception e)
        {
            if (heldItemObject)
            {
                // Remove current object from the hand
                Destroy(heldItemObject);
            }

            // TODO: Error handling
            //print(e);
        }
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press left shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float currentSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);

        // Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        }
    }

    private void HandleRaycast()
    {
        // Draw the raycast line
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * reach, Color.red);

        // Reset the tooltip text
        interactTooltip.text = "";

        // Check if the raycast hit anything
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, reach)) {

            // Change tooltip text
            switch (hit.transform.gameObject.tag)
            {
                case ("Item"):
                    interactTooltip.text = "Pick up";
                    if (Input.GetKeyDown(KeyCode.E)) HandleItemPickup(hit.transform.parent.GetComponentInChildren<Item>());
                    break;
                case ("Interactable"):
                    interactTooltip.text = "Interact";
                    if (Input.GetKeyDown(KeyCode.E)) HandleInteract(hit.transform.parent.GetComponentInChildren<Interactable>());
                    break;
                case ("MissionPhoto"):
                    interactTooltip.text = "Select Mission";
                    if (Input.GetKeyDown(KeyCode.E)) HandleMissionSelect(hit.transform.parent.gameObject.GetComponentInChildren<TMP_Text>().text);
                    break;
                default:
                    interactTooltip.text = "";
                    break;
            }
        }
    }

    private void HandleItemPickup(Item item)
    {
        if (item)
        {
            // Attempt to add it to the hotbar
            try
            {
                hotbar.AddItem(item.item, 1);
                Destroy(item.gameObject);
            }
            catch (OverflowException e)
            {
                // Attempt to add it to the inventory
                try
                {
                    inventory.AddItem(item.item, 1);
                    Destroy(item.gameObject);
                }
                catch (OverflowException ex)
                {
                    // Inventory is full
                }
            }

        } else
        {
            print("Item is null.");
        }
    }

    private void HandleInteract(Interactable interactable)
    {
        if (interactable)
        {
            interactable.OnInteract();
        }
    }

    private void HandleMissionSelect(string missionName)
    {
        SceneManager.LoadScene(missionName);
    }

    private void HandleItemInteraction(ItemObject item)
    {
        // Functionality for specific items
        switch (item.itemName)
        {
            case ("Flashlight"):
                heldItemObject.GetComponent<Item>().OnFlashlightToggle();
                break;
        }
    }

    private void CheckForMouseClick()
    {
        if (Input.GetMouseButtonDown(((int)MouseButton.Left)))
        {
            
            if (hotbar.Container[currentHotbarIndex] != null)
            {
                print("Used item");
                HandleItemInteraction(hotbar.Container[currentHotbarIndex].item);
            }
        }
    }

    public void OnApplicationQuit()
    {
        hotbar.Container.Clear();
        inventory.Container.Clear();
    }
}