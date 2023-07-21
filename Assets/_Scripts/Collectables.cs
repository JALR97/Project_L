using System;
using UnityEngine;

public class Collectables : MonoBehaviour{
    private void Start() {
        GameManager.Instance.LoadCollectables();
    }
}
