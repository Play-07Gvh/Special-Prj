using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_animator)
            _animator = GetComponent<Animator>();
        if (!_animator)
            Debug.LogError("Animator not found on " + name);
    }

    public void animatorBool(string name, bool set)
    {
        _animator.SetBool(name, set);
    }

    public void animatorTrigger(string name)
    {
        _animator.SetTrigger(name);
    }

    public void animatorFloat(string name, float value)
    {
        _animator.SetFloat(name, value);
    }
}
