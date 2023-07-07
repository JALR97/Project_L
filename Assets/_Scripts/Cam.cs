using Cinemachine;
using UnityEngine;

public class Cam : MonoBehaviour
{
    //**    ---Components---    **//
    [SerializeField] private CinemachineFreeLook cinemachineFL;
    
    
    //**    ---Variables---    **//
    [SerializeField] private float maxXSpeedCam;
    [SerializeField] private float maxYSpeedCam;
    
    
    //**    ---Properties---    **//
    
    
    //**    ---Functions---    **//
    private void Awake() {
        GameManager.OnGameStateChange += StateChange;
    }
    
    private void OnDestroy() {
        GameManager.OnGameStateChange -= StateChange;
    }

    private void StateChange(GameManager.GameState newState) {
        if (newState == GameManager.GameState.Exploring) {
            cinemachineFL.m_XAxis.m_MaxSpeed = maxXSpeedCam;
            cinemachineFL.m_YAxis.m_MaxSpeed = maxYSpeedCam;
        }
        else {
            cinemachineFL.m_XAxis.m_MaxSpeed = 0;
            cinemachineFL.m_YAxis.m_MaxSpeed = 0;
        }
    }
}
