using System.Collections.Generic;
using UnityEngine;

//MANAGE ALL SKILLS
public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    //SYSTEM
    Animator animator;

    int numOfSkills = 4;

    //STATUS INFO
    PlayerStatus playerStatus;
    AnimatorStateInfo stateInfo;

    //SKILL
    [SerializeField]
    SkillInfoSO _autoAttack;

    [SerializeField]
    SkillInfoSO _firstSkill;

    [SerializeField]
    SkillInfoSO _secondSkill;

    [SerializeField]
    SkillInfoSO _ultimateSkill;

    public class Skill
    {
        public SkillType skillType;
        public SkillInfoSO skillInfoSO;
        public bool isSkillPresses;
        public float nextSkillTimes;

        public Skill(
            SkillType skillType,
            SkillInfoSO skillInfoSO,
            bool isSkillPresses,
            float nextSkillTimes
        )
        {
            this.skillType = skillType;
            this.skillInfoSO = skillInfoSO;
            this.isSkillPresses = isSkillPresses;
            this.nextSkillTimes = nextSkillTimes;
        }
    }

    List<Skill> skillList = new List<Skill>();
    public List<Skill> SkillList
    {
        get => skillList;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SetupSystemComponent();
            SetupAllSkill();
        }
        else
        {
            Debug.LogError("More than one instance of Skill Manager!");
        }
    }

    void SetupSystemComponent()
    {
        playerStatus = GetComponent<PlayerStatus>();
        animator = GetComponentInChildren<Animator>();
    }

    void SetupAllSkill()
    {
        SetupSkill(SkillType.AutoAttack, _autoAttack);
        SetupSkill(SkillType.FirstSkill, _firstSkill);
        SetupSkill(SkillType.SecondSkill, _secondSkill);
        SetupSkill(SkillType.UltimateSkill, _ultimateSkill);
    }

    void SetupSkill(SkillType skillType, SkillInfoSO skill)
    {
        if (skill == null)
        {
            Debug.LogError(skill.name + "IS NULL! PLEASE FILL THIS SKILL");
            return;
        }
        Skill tmpSkill = new Skill(skillType, skill, false, 0f);
        skillList.Add(tmpSkill);
    }

    void Update()
    {
        float currentTime = Time.time;
        //if cooldown is done, you can use the skill
        for (int i = 0; i < numOfSkills; ++i)
        {
            CheckSkill(i, currentTime);
        }
    }

    void CheckSkill(int index, float currentTime)
    {
        Skill curSkill = skillList[index];
        if (curSkill.isSkillPresses && currentTime >= curSkill.nextSkillTimes)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (curSkill.skillInfoSO.CanDoSkill(playerStatus.CurrentMana) && IsFreeAnimationState())
            {
                switch (curSkill.skillType)
                {
                    case SkillType.AutoAttack:
                        animator.SetTrigger("Attack");
                        break;
                    case SkillType.FirstSkill:
                        animator.SetTrigger("Skill");
                        break;
                    case SkillType.SecondSkill:
                        animator.SetTrigger("Skill");
                        break;
                    case SkillType.UltimateSkill:
                        animator.SetTrigger("Skill");
                        break;
                }
                curSkill.nextSkillTimes = currentTime + curSkill.skillInfoSO.skillCoolDown;
            }
        }
    }

    bool IsFreeAnimationState()
    {
        return stateInfo.IsName("Player_Idle") || stateInfo.IsName("Player_Run");
    }
}

public enum SkillType
{
    AutoAttack,
    FirstSkill,
    SecondSkill,
    UltimateSkill,
};
