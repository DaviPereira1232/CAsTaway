using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ControlsManager : MonoBehaviour
{
    [Header("Controls")]
    public TMP_InputField upInputField;
    public TMP_InputField downInputField;
    public TMP_InputField leftInputField;
    public TMP_InputField rightInputField;
    public TMP_InputField captureInputField;

    // Store the keybindings
    public static Dictionary<string, KeyCode> KeyBindings = new Dictionary<string, KeyCode>();

    private TMP_InputField activeField;
    private string activeActionName;
    private bool isWaitingForKey = false;

    // Default keys
    private KeyCode defaultUp = KeyCode.W;
    private KeyCode defaultDown = KeyCode.S;
    private KeyCode defaultLeft = KeyCode.A;
    private KeyCode defaultRight = KeyCode.D;
    private KeyCode defaultCapture = KeyCode.Space;

    void Start()
    {
        // Initialize with defaults
        KeyBindings["Up"] = defaultUp;
        KeyBindings["Down"] = defaultDown;
        KeyBindings["Left"] = defaultLeft;
        KeyBindings["Right"] = defaultRight;
        KeyBindings["Capture"] = defaultCapture;

        LoadSavedControls();

        // Setup input fields
        SetupField(upInputField, "Up", defaultUp);
        SetupField(downInputField, "Down", defaultDown);
        SetupField(leftInputField, "Left", defaultLeft);
        SetupField(rightInputField, "Right", defaultRight);
        SetupField(captureInputField, "Capture", defaultCapture);
    }

    void SetupField(TMP_InputField field, string actionName, KeyCode defaultKey)
    {
        if (field == null) return;

        field.text = GetKeyDisplayName(defaultKey);

        field.onSelect.AddListener((string value) =>
        {
            activeField = field;
            activeActionName = actionName;
            isWaitingForKey = true;
            // field.text = "?";
        });

        field.onDeselect.AddListener((string value) =>
        {
            if (isWaitingForKey)
            {
                field.text = GetKeyDisplayName(KeyBindings[actionName]);
                isWaitingForKey = false;
                activeField = null;
            }
        });
    }

    void Update()
    {
        if (!isWaitingForKey || activeField == null) return;

        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                // Skip None
                if (key == KeyCode.None)
                    continue;

                // RESTRICT ONLY MOUSE BUTTONS
                // This prevents conflicts with UI clicks
                if (IsMouseKey(key))
                    continue;

                // Update the binding
                KeyBindings[activeActionName] = key;

                // Update the input field text
                activeField.text = GetKeyDisplayName(key);

                // Stop listening
                isWaitingForKey = false;
                activeField = null;

                // Deselect the field
                EventSystem.current.SetSelectedGameObject(null);
                
                   // Save to PlayerPrefs
                PlayerPrefs.SetInt(activeActionName + "Key", (int)key);
                PlayerPrefs.Save();

                break;
            }
        }
    }

    bool IsMouseKey(KeyCode key)
    {
        // Check if it's a mouse button
        return key == KeyCode.Mouse0 ||      // Left click
               key == KeyCode.Mouse1 ||      // Right click
               key == KeyCode.Mouse2 ||      // Middle click
               key == KeyCode.Mouse3 ||      // Extra mouse button 1
               key == KeyCode.Mouse4 ||      // Extra mouse button 2
               key == KeyCode.Mouse5 ||      // Extra mouse button 3
               key == KeyCode.Mouse6;        // Extra mouse button 4
    }

    string GetKeyDisplayName(KeyCode key)
    {
        string name = key.ToString();

        // Format for better readability
        if (name.Contains("Arrow"))
            name = name.Replace("Arrow", "");

        return name;
    }

    // Public method to get a key binding
    public static KeyCode GetKey(string actionName)
    {
        if (KeyBindings.ContainsKey(actionName))
            return KeyBindings[actionName];
        return KeyCode.None;
    }

    // Reset to defaults
    public void ResetToDefaults()
    {
        KeyBindings["Up"] = defaultUp;
        KeyBindings["Down"] = defaultDown;
        KeyBindings["Left"] = defaultLeft;
        KeyBindings["Right"] = defaultRight;
        KeyBindings["Capture"] = defaultCapture;

        upInputField.text = GetKeyDisplayName(defaultUp);
        downInputField.text = GetKeyDisplayName(defaultDown);
        leftInputField.text = GetKeyDisplayName(defaultLeft);
        rightInputField.text = GetKeyDisplayName(defaultRight);
        captureInputField.text = GetKeyDisplayName(defaultCapture);

        foreach (var binding in KeyBindings)
        {
            PlayerPrefs.SetInt(binding.Key + "Key", (int)binding.Value);
        }
        PlayerPrefs.Save();
    }

    public void LoadSavedControls()
    {
        // Load saved keys from PlayerPrefs, falling back to defaults
        KeyBindings["Up"] = (KeyCode)PlayerPrefs.GetInt("UpKey", (int)defaultUp);
        KeyBindings["Down"] = (KeyCode)PlayerPrefs.GetInt("DownKey", (int)defaultDown);
        KeyBindings["Left"] = (KeyCode)PlayerPrefs.GetInt("LeftKey", (int)defaultLeft);
        KeyBindings["Right"] = (KeyCode)PlayerPrefs.GetInt("RightKey", (int)defaultRight);
        KeyBindings["Capture"] = (KeyCode)PlayerPrefs.GetInt("CaptureKey", (int)defaultCapture);

        // Update the input field text displays
        if (upInputField != null)
            upInputField.text = GetKeyDisplayName(KeyBindings["Up"]);
        if (downInputField != null)
            downInputField.text = GetKeyDisplayName(KeyBindings["Down"]);
        if (leftInputField != null)
            leftInputField.text = GetKeyDisplayName(KeyBindings["Left"]);
        if (rightInputField != null)
            rightInputField.text = GetKeyDisplayName(KeyBindings["Right"]);
        if (captureInputField != null)
            captureInputField.text = GetKeyDisplayName(KeyBindings["Capture"]);

        Debug.Log("Controls loaded from PlayerPrefs");
    }
}