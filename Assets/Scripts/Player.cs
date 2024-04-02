using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.IO;

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
    public GameController gameController;

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

    [Header("State")]
    public bool canMove = true;
    public bool inUi = false;

    [Header("HUD/UI")]
    public Canvas playerHUD;
    public Canvas playerHotbar;
    public TMP_Text interactTooltip;
    private Image[] hotbarImages;
    public TextMeshProUGUI xPosUi;
    public TextMeshProUGUI yPosUi;
    public ReportMenu report;

    [Header("Inventory")]
    public InventoryData inventory = new InventoryData();
    public int hotbarSize = 4;
    public int currentHotbarIndex = 0;
    private GameObject heldItemObject;
    public GameObject playerHand;

    [Header("Keybinds")]
    public KeyCode keyForward       = KeyCode.W;
    public KeyCode keyBackwards     = KeyCode.S;
    public KeyCode keyLeft          = KeyCode.A;
    public KeyCode keyRight         = KeyCode.D;
    public KeyCode keySprint        = KeyCode.LeftShift;
    public KeyCode keyCrouch        = KeyCode.LeftControl;
    public KeyCode keyDrop          = KeyCode.G;
    public KeyCode keyInventory     = KeyCode.I;
    public KeyCode keyJournal       = KeyCode.J;
    public KeyCode keySave          = KeyCode.K;
    public KeyCode keyLoad          = KeyCode.L;
    public KeyCode keyUse           = KeyCode.E;
    public int buttonShoot          = (int)MouseButton.Left;
    public int buttonAim            = (int)MouseButton.Right;

    void Start()
    {
        // Find the gamecontroller
        gameController = FindObjectOfType<GameController>();

        // Get the character controller if not defined
        characterController = GetComponent<CharacterController>();

        // Hide the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get hotbar images
        hotbarImages = playerHotbar.GetComponentsInChildren<Image>();
        if (hotbarImages.Length > 0) hotbarSize = hotbarImages.Length;

        // Add OnSceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check for a spawnpoint
        GameObject spawnpoint = GameObject.FindWithTag("Spawnpoint");
        if (spawnpoint) transform.position = spawnpoint.transform.position;
    }

    public void Update()
    {
        HandleMovement();
        HandleCheckInput();
        HandleCheckMouseInput();
        HandleRaycast();
        HandleUpdateHUD();
        
        CheckIfFellThrough();
    }

    private void HandleCheckInput()
    {
        // Drop Held Item
        if (Input.GetKeyDown(keyDrop))
        {
            // Get current item
            ItemData currentItem = inventory.GetSlot(currentHotbarIndex).item;

            // Create item on ground
            GameObject itemObj = Instantiate(currentItem.prefab);
            itemObj.transform.position = transform.position;

            // Remove item from inventory
            inventory.RemoveItemAt(currentHotbarIndex);
        }

        // Open Journal
        if (Input.GetKeyDown(keyJournal))
        {
            report.gameObject.SetActive(!report.gameObject.active);
        }

        // Testing saving and loading
        string path = Application.dataPath + "/SaveFiles/" + GameController.contract.name;
        if (Input.GetKeyDown(keySave))
        {
            gameController.Save();
        }
        if (Input.GetKeyDown(keyLoad))
        {
            gameController.Load(path);
            
        }
    }

    private void CheckIfFellThrough()
    {
        if (transform.position.y < 0)
        {
            // Find the spawnpoint
            PlayerSpawnpoint spawnpoint = FindObjectOfType<PlayerSpawnpoint>();
            if (spawnpoint) transform.position = spawnpoint.transform.position + new Vector3(0, 2f, 0);
            else transform.position = new Vector3(transform.position.x, 300, transform.position.z);
        }
    }

    private void HandleUpdateHUD()
    {
        //========== Hotbar
        int previousHotbarIndex = currentHotbarIndex;

        // Check for scrolling forward and backwards
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) currentHotbarIndex -= 1;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) currentHotbarIndex += 1;

        // Check bounds of hotbar
        if (currentHotbarIndex >= hotbarSize) currentHotbarIndex = hotbarSize - 1;
        else if (currentHotbarIndex <= 0) currentHotbarIndex = 0;

        // Update the object at the player
        HandleHeldItem(previousHotbarIndex, currentHotbarIndex);

        // Reset colors of the current hotbar slots
        for (int i = 0; i < hotbarSize; i++)
        {
            hotbarImages[i].color = Color.white;

            try
            {
                hotbarImages[i].sprite = inventory.GetSlot(i).item.sprite;
            }
            catch 
            {
                hotbarImages[i].sprite = null;
            }
        }
        // Set the selected slot's color
        playerHotbar.GetComponentsInChildren<Image>()[currentHotbarIndex].color = Color.red;

        //========== Position
        xPosUi.text = transform.position.x.ToString();
        yPosUi.text = transform.position.z.ToString();
    }

    private void HandleHeldItem(int previousIndex, int currentIndex)
    {
        // Update the object at the player
        try
        {
            if (previousIndex != currentIndex)
            {
                // Get the currently held item
                ItemData heldItem = inventory.GetSlot(currentIndex).item;

                // Destroy the current game object
                Destroy(heldItemObject);

                // Create the new game object
                heldItemObject = Instantiate(heldItem.prefab);
                heldItemObject.transform.parent = playerHand.transform;

                // Update the rotation and positioning of the object
                heldItemObject.transform.SetPositionAndRotation(playerHand.transform.position, playerCamera.transform.rotation);
            }
        }
        catch (Exception e)
        {
            if (heldItemObject && previousIndex != currentIndex)
            {
                // Remove current object from the hand
                Destroy(heldItemObject);
            }

            // TODO: Error handling
            print(e);
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
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, reach) && !inUi) {

            // Change tooltip text
            switch (hit.transform.gameObject.tag)
            {
                case ("Item"):
                    interactTooltip.text = "Pick up";
                    if (Input.GetKeyDown(keyUse)) HandleItemPickup(hit.transform.parent.GetComponentInChildren<Item>());
                    break;
                case ("Interactable"):
                    interactTooltip.text = "Interact";
                    if (Input.GetKeyDown(keyUse)) HandleInteract(hit.transform.parent.GetComponentInChildren<IInteractable>());
                    break;
                case ("MissionPhoto"):
                    interactTooltip.text = "Select Mission";
                    if (Input.GetKeyDown(keyUse)) HandleMissionSelect(hit.transform.parent.gameObject.GetComponentInChildren<PinboardPhoto>());
                    break;
                case ("Computer"):
                    interactTooltip.text = "Open Command Line";
                    if (Input.GetKeyDown(keyUse))
                    {
                        hit.transform.parent.GetComponent<IInteractable>()?.Interact();
                    }
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
            // Attempt to add it to the inventory/hotbar
            try
            {
                inventory.AddItem(item.item, 1);
                Destroy(item.gameObject);
            }
            catch (OverflowException ex)
            {
                // Inventory is full
            }

        } else
        {
            print("Item is null.");
        }
    }

    private void HandleInteract(IInteractable interactable)
    {
        if (interactable != null) interactable.Interact();
    }

    private void HandleMissionSelect(PinboardPhoto photo)
    {
        // Set the current mission
        GameController.mission = photo.mission;

        // Remove the mission from the data
        GameController.contract.missions.Remove(photo.mission);

        // Remove the mission from the board
        Destroy(photo.gameObject);

        // Load the mission scene
        SceneManager.LoadScene(photo.mission.scene);
    }

    private void HandleItemInteraction(ItemData item)
    {
        // Functionality for specific items
        switch (item.itemName)
        {
            case ("Flashlight"):
                heldItemObject.GetComponent<Item>().OnFlashlightToggle();
                break;

            case ("Camera"):
                heldItemObject.GetComponent<Item>().OnCameraCapture();
                break;

            case ("Compass"):
                xPosUi.transform.parent.gameObject.SetActive(!xPosUi.transform.parent.gameObject.active);
                yPosUi.transform.parent.gameObject.SetActive(!yPosUi.transform.parent.gameObject.active);
                break;
        }
    }

    private void HandleCheckMouseInput()
    {
        if (Input.GetMouseButtonDown(((int)buttonShoot)))
        {
            try
            {
                if (inventory.GetSlot(currentHotbarIndex) != null)
                {
                    HandleItemInteraction(inventory.GetSlot(currentHotbarIndex).item);
                    print("Used item");
                }
            } catch (ArgumentOutOfRangeException ex) {
                //print("No item in that slot");
            }
        }
    }
}