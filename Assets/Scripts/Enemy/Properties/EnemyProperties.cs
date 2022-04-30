using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Properties")]
public class EnemyProperties : ScriptableObject
{
    public float ChasingSpeed = 5f;
    public float SearchingSpeed = 1.2f;
    public float PatrollingSpeed = 1f;

    public float HearRange = 5f;
    public float SightRange = 13f;
    public float VisionAngle = 60f;
    public float NoticeAngle = 120f;

    public float NoticeTime = 2f;

    public float SearchingTime = 5f;

    public Transform Player;

    public enum EnemyType { standart,blind};
    public EnemyType enemyType;

}
