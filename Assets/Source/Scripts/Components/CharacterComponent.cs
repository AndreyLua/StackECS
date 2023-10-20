using UnityEngine;

internal struct CharacterComponent
{
    public CharacterController CharacterController;

    public CharacterComponent(CharacterController characterController)
    {
        CharacterController = characterController;
    }
}
