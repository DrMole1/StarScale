using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Transform player;

    private Vector3 currentRot;
    private Quaternion currentQuaternionRot;


    private void Update()
    {
        transform.position = objectToFollow.position;

        currentRot = new Vector3(35f, player.localRotation.eulerAngles.y + 180f, 0f);
        currentQuaternionRot.eulerAngles = currentRot;
        transform.localRotation = currentQuaternionRot;
    }
}
