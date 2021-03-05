using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotation : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private int phaseIndex = 0;
    private int levelIndex = 0;
    private float speed;
    private float duration;

    public GameObject stuckPrefab;
    public GameObject adsPrefab;
    public SpritesAndMats SAM;
    public ParticleSystem PSM;

    [SerializeField]
    private LevelsList levels;

    private void Awake()
    {

        StartCoroutine("PlayPattern");
        levelIndex = ControllerScript.levelIndex;

        StuckCursors();
        SpawnAds();
        SpriteAndMaterial();
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        SpeedValues();
        target.Rotate(0, 0, speed);
    }

    private IEnumerator PlayPattern()
    {

        while (true)
        {
            duration = levels.Levels[levelIndex].levelPattern[phaseIndex].duration;


            yield return new WaitForSecondsRealtime(duration);
            phaseIndex++;
            phaseIndex = phaseIndex < levels.Levels[levelIndex].levelPattern.Length ? phaseIndex : 0;
        }
    }

    void SpeedValues()
    {
        if (levels.Levels[levelIndex].levelPattern[phaseIndex].direction == Patterns.Direction.clockwise)
        {
            if (levels.Levels[levelIndex].levelPattern[phaseIndex].movetype == Patterns.Movetype.accelerate)
            {
                speed -= (levels.Levels[levelIndex].levelPattern[phaseIndex].speedModifier / 10000) * levels.Levels[levelIndex].maxSpeed;
            }
            else if (levels.Levels[levelIndex].levelPattern[phaseIndex].movetype == Patterns.Movetype.decelerate)
            {
                speed += (levels.Levels[levelIndex].levelPattern[phaseIndex].speedModifier / 10000) * levels.Levels[levelIndex].maxSpeed;
            }

            speed = Mathf.Clamp(speed, -levels.Levels[levelIndex].maxSpeed, 0);
        }
        else if (levels.Levels[levelIndex].levelPattern[phaseIndex].direction == Patterns.Direction.counterclockwise)
        {
            if (levels.Levels[levelIndex].levelPattern[phaseIndex].movetype == Patterns.Movetype.accelerate)
            {
                speed += (levels.Levels[levelIndex].levelPattern[phaseIndex].speedModifier / 10000) * levels.Levels[levelIndex].maxSpeed;
            }
            else if (levels.Levels[levelIndex].levelPattern[phaseIndex].movetype == Patterns.Movetype.decelerate)
            {
                speed -= (levels.Levels[levelIndex].levelPattern[phaseIndex].speedModifier / 10000) * levels.Levels[levelIndex].maxSpeed;
            }
            speed = Mathf.Clamp(speed, 0, levels.Levels[levelIndex].maxSpeed);
        }

    }

    private float[] cursorPos = new float[] { 0, 120, 240 };

    private void StuckCursors()
    {
        int stuckCount = levels.Levels[levelIndex].stuckCount;

        for (int i = 0; i < stuckCount; i++)
        {
            GameObject cursorToSpawn = Instantiate(stuckPrefab);
            cursorToSpawn.transform.SetParent(transform);
            PresetCursors(transform, cursorToSpawn.transform, cursorPos[i], 0.0f, 180f);
            cursorToSpawn.transform.localScale = new Vector2(0.5f, 0.5f);

        }
    }

    private float[] adsPos = new float[] { 45, 165, 285 };
    
    private void SpawnAds()
    {
        int adsCount = levels.Levels[levelIndex].adsCount;

        for (int i = 0; i < adsCount; i++)
        {
            if (levels.Levels[levelIndex].adChance >= Random.value)
            {
                GameObject adToSpawn = Instantiate(adsPrefab);
                adToSpawn.transform.SetParent(transform);
                PresetCursors(transform, adToSpawn.transform, adsPos[i], 0.19f, 0f);
                adToSpawn.transform.localScale = new Vector2(0.5f, 0.5f);
            }
        }
    }
    public void ReleaseCursors()
    {
        gameObject.SetActive(false);
        transform.DetachChildren();
    }

    public void PresetCursors(Transform target, Transform thingToSpawn, float angle, float spaceOffset, float rotation)
    {
        Vector2 offset = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * (target.GetComponent<CircleCollider2D>().radius + spaceOffset);
        thingToSpawn.localPosition = (Vector2)target.localPosition + offset;
        thingToSpawn.rotation = Quaternion.Euler(0, 0, -angle + rotation);
    }

    public void SpriteAndMaterial()
    {
        if (!levels.Levels[levelIndex].BossLevel)
        {
            int randomValue = Random.Range(0, SAM.levelSprites.Length);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = SAM.levelSprites[randomValue];
            PSM.textureSheetAnimation.SetSprite(0, SAM.levelParts[randomValue]);
        }
        else
        {
            int randomValue = Random.Range(0, SAM.bossSprites.Length);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = SAM.bossSprites[randomValue];
            PSM.textureSheetAnimation.SetSprite(0, SAM.bossParts[randomValue]);
        }
    }
}
