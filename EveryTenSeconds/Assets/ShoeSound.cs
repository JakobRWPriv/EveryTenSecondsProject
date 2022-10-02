using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeSound : MonoBehaviour
{
    public void FallSound() {
        AudioHandler.Instance.PlaySound(AudioHandler.Instance.ShoeFall, 2f, 1f);
    }
    public void StompSound() {
        AudioHandler.Instance.PlaySound(AudioHandler.Instance.ShoeStomp, 1f, 1f);
    }
}
