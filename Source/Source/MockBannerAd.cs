using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockBannerAd : MonoBehaviour
{
    public void Stop()
    {
        Destroy(gameObject);
    }
}