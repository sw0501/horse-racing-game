using UnityEngine;

public class AnimalVisual : MonoBehaviour
{
    [System.Serializable]
    public struct AnimalController
    {
        public AnimalType type;
        public RuntimeAnimatorController controller;
    }

    public AnimalController[] controllers;

    public void Apply(AnimalType type)
    {
        Animator animator = GetComponent<Animator>();
        if (animator == null)
        {
            return;
        }

        foreach (AnimalController entry in controllers)
        {
            if (entry.type == type && entry.controller != null)
            {
                animator.runtimeAnimatorController = entry.controller;
                return;
            }
        }
    }
}
