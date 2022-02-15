using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Properties")]
public class EnemyMainFeatures : ScriptableObject
{
    public float ChasingSpeed = 3f;
    public float SearchingSpeed = 1.2f;
    public float PatrollingSpeed = 1f;

    public float HearRange = 4f;
    public float SightRange = 8f;
    public float VisionAngle = 120f;
    public float NoticeAngle = 180f;

    public float NoticeTime = 3f;

    public float MemoryTime = 5f;
    public float CacheTime = 3f;

    public Transform Player;


}
