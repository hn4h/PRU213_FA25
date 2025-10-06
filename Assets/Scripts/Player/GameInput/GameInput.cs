using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get;private set;}
    
    public enum Binding{
        Player1_Jump,
        Player1_MoveDown,
        Player1_MoveLeft,
        Player1_MoveRight,
        Player2_Jump,
        Player2_MoveDown,
        Player2_MoveLeft,
        Player2_MoveRight
    }
    private PlayerInputSystem playerInputSystem;
    private const string PLAYER_PREFS_BINDING = "InputBindings";
    private const string DEFAULT_PLAYER_PREFS_BINDING = "DefaultInputBindings";
    
    private void Awake() {
        Instance = this;
        playerInputSystem = new PlayerInputSystem();
        if (!PlayerPrefs.HasKey(DEFAULT_PLAYER_PREFS_BINDING))
        {
            PlayerPrefs.SetString(DEFAULT_PLAYER_PREFS_BINDING,playerInputSystem.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDING))
        {
            playerInputSystem.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDING));
        }
        playerInputSystem.Player1.Enable();
        playerInputSystem.Player2.Enable();

        
    }

    public string GetBindingText(Binding binding)
    {
        return binding switch
        {
            Binding.Player1_Jump => playerInputSystem.Player1.Jump.bindings[0].ToDisplayString(),
            Binding.Player1_MoveLeft => playerInputSystem.Player1.Move.bindings[1].ToDisplayString(),
            Binding.Player1_MoveRight => playerInputSystem.Player1.Move.bindings[3].ToDisplayString(),
            Binding.Player1_MoveDown => playerInputSystem.Player1.Move.bindings[2].ToDisplayString(),
            Binding.Player2_Jump => playerInputSystem.Player2.Jump.bindings[0].ToDisplayString(),
            Binding.Player2_MoveLeft => playerInputSystem.Player2.Move.bindings[1].ToDisplayString(),
            Binding.Player2_MoveRight => playerInputSystem.Player2.Move.bindings[3].ToDisplayString(),
            Binding.Player2_MoveDown => playerInputSystem.Player2.Move.bindings[2].ToDisplayString(),
            _ => null,
        };
    }

    public void RebindBinding(Binding binding, Action action)
    {
        playerInputSystem.Player1.Disable();
        playerInputSystem.Player2.Disable();
         
        InputAction inputAction;
        int bindingIndex;
        switch(binding)
        {
            default:
            case Binding.Player1_Jump:
                inputAction = playerInputSystem.Player1.Jump;
                bindingIndex = 0;
                break;
            case Binding.Player1_MoveDown:
                inputAction = playerInputSystem.Player1.Move;
                bindingIndex = 2;
                break;
            case Binding.Player1_MoveLeft:
                inputAction = playerInputSystem.Player1.Move;
                bindingIndex = 1;
                break;
            case Binding.Player1_MoveRight:
                inputAction = playerInputSystem.Player1.Move;
                bindingIndex = 3;
                break;
            case Binding.Player2_Jump:
                inputAction = playerInputSystem.Player2.Jump;
                bindingIndex = 0;
                break;
            case Binding.Player2_MoveDown:
                inputAction = playerInputSystem.Player2.Move;
                bindingIndex = 2;
                break;
            case Binding.Player2_MoveLeft:
                inputAction = playerInputSystem.Player2.Move;
                bindingIndex = 1;
                break;
            case Binding.Player2_MoveRight:
                inputAction = playerInputSystem.Player2.Move;
                bindingIndex = 3;
                break;
            
        }
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>{
            callback.Dispose();
            playerInputSystem.Player1.Enable();
            playerInputSystem.Player2.Enable();
            action();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDING,playerInputSystem.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }

    public Vector2 GetDirectionVector(String tagplayer)
    {
        Vector2 inputVector;
        if (tagplayer == "Player1")
        {
            inputVector = playerInputSystem.Player1.Move.ReadValue<Vector2>();

        }
        else
        {
            inputVector = playerInputSystem.Player2.Move.ReadValue<Vector2>();
        }

        inputVector = inputVector.normalized;
        return inputVector;
    }

    public PlayerInputSystem GetPlayerInputSystem()
    {
        return playerInputSystem;
    }

    public String getInputKey()
    {
        return PLAYER_PREFS_BINDING;
    }
    public String getDefaultInputKey()
    {
        return DEFAULT_PLAYER_PREFS_BINDING;
    }

    public void ResetKeyMap()
    {
        string defaultBinding = PlayerPrefs.GetString(DEFAULT_PLAYER_PREFS_BINDING);
    
        PlayerPrefs.SetString(PLAYER_PREFS_BINDING, defaultBinding);
        
        playerInputSystem.LoadBindingOverridesFromJson(defaultBinding);
        
        PlayerPrefs.Save();

        Debug.Log("reset keymap");
    }
    



}
