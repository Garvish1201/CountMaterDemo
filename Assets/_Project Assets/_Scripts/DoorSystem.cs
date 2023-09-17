using TMPro;
using System;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public static Action<int> RecivingBonus;
    public static Action<int> RecivingLoss;
    public enum LeftSideFunction { Bonus, Loss }
    public enum RightSideFunction { Bonus, Loss }

    [Header(" Elemetns ")]
    [SerializeField] private MeshRenderer leftSideRenderer;
    [SerializeField] private MeshRenderer rightSideRenderer;
    [SerializeField] private Collider _collider;

    [Space]
    [SerializeField] private Material bonusMaterial;
    [SerializeField] private Material lossMaterial;

    [Header(" UI Elemetns ")]
    [SerializeField] private TMP_Text T_leftSide;
    [SerializeField] private TMP_Text T_rightSide;


    [Header(" Settings ")]
    [SerializeField] private bool swapValues = false;
    [SerializeField] private LeftSideFunction leftSideFunction;
    [SerializeField] private int leftSideValue;

    [Space]
    [SerializeField] private RightSideFunction rightSideFunction;
    [SerializeField] private int rightSideValue;

    #region Generating And Setting Door Values
    private void GenerteRandomValues()
    {
        if (swapValues)
        {
            int temp = leftSideValue;
            leftSideValue = rightSideValue; 
            rightSideValue = temp;
        }

        T_leftSide.text = $"+{leftSideValue}";
        T_rightSide.text = $"+{rightSideValue}";
    }

    private void GiveValuesToDoors()
    {
        // set LEFT side renderer material
        if (leftSideFunction == LeftSideFunction.Bonus)
            leftSideRenderer.material = bonusMaterial;
        else
            leftSideRenderer.material = lossMaterial;

        // set RIGHT side renderer material
        if (rightSideFunction == RightSideFunction.Bonus)
            rightSideRenderer.material = bonusMaterial;
        else
            rightSideRenderer.material = lossMaterial;
    } 
    #endregion

    private void Start()
    {
        GenerteRandomValues();
        GiveValuesToDoors();
    }

    #region When Player Trigger The Collider
    public void GetResults(float playerPosition)
    {
        // player on LEFT side
        if (playerPosition < 0)
        {
            leftSideRenderer.gameObject.SetActive(false);
            switch (leftSideFunction)
            {
                case LeftSideFunction.Bonus:
                    RecivingBonus?.Invoke(leftSideValue);
                    break;

                case LeftSideFunction.Loss:
                    RecivingLoss?.Invoke(leftSideValue);
                    break;
            }
        }
        
        // player on RIGHT side
        else
        {
            rightSideRenderer.gameObject.SetActive(false);
            switch (rightSideFunction)
            {
                case RightSideFunction.Bonus:
                    RecivingBonus?.Invoke(rightSideValue);
                    break;

                case RightSideFunction.Loss:
                    RecivingLoss?.Invoke(rightSideValue);
                    break;
            }
        }
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    } 
    #endregion
}
