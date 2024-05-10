using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SkillInfoSO))]
public class SkillInfoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SkillInfoSO skillInfo = (SkillInfoSO)target;
        // skillInfo.skillType = (SkillType)
        //     EditorGUILayout.EnumPopup("Skill Type", skillInfo.skillType);
        skillInfo.rangeType = (RangeType)
            EditorGUILayout.EnumPopup("Range Type", skillInfo.rangeType);
        skillInfo.skillDamage = EditorGUILayout.FloatField("Skill Damage", skillInfo.skillDamage);
        skillInfo.skillCoolDown = EditorGUILayout.FloatField(
            "Skill CoolDown",
            skillInfo.skillCoolDown
        );
        skillInfo.skillMana = EditorGUILayout.IntField("Skill Mana", skillInfo.skillMana);

        EditorGUILayout.Space();

        switch (skillInfo.rangeType)
        {
            case RangeType.Melee:
                skillInfo.meleeAttackRange = EditorGUILayout.FloatField(
                    "Melee Attack Range",
                    skillInfo.meleeAttackRange
                );
                break;
            case RangeType.Ranged:
                skillInfo.skillPrefab = (GameObject)
                    EditorGUILayout.ObjectField(
                        "Skill Prefab",
                        skillInfo.skillPrefab,
                        typeof(GameObject),
                        true
                    );
                skillInfo.skillMoveSpeed = EditorGUILayout.FloatField(
                    "Skill Move Speed",
                    skillInfo.skillMoveSpeed
                );
                break;
        }

        // Cập nhật thông tin khi thay đổi
        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }
}
#endif

public enum RangeType
{
    Melee,
    Ranged,
};

[CreateAssetMenu(fileName = "SkillSO", menuName = "ScriptableObjects/SkillSO")]
public class SkillInfoSO : ScriptableObject
{
    //COMMON INFO
    public RangeType rangeType;
    public float skillDamage;
    public float skillCoolDown;
    public int skillMana;

    //MELEE ATTACK INFO
    public float meleeAttackRange;

    //RANGE ATTACK INFO
    public GameObject skillPrefab;
    public float skillMoveSpeed;

    public bool CanDoSkill(int currentMana)
    {
        return currentMana - skillMana >= 0;
    }

    public void DoSkill() { }
}
