using System.Collections.Generic;
using UnityEngine;

//MANAGE ALL SKILLS
public class SkillManager : CustomMonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    int _numOfSkills = 4;

    //SYSTEM
    public Animator PlayerAnimator;
    public PlayerStatus PlayerStatus;
    AnimatorStateInfo _stateInfo;

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

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
            LoadAllSkill();
        }
        else
        {
            Debug.LogError("More than one instance of Skill Manager!");
        }
    }

    void LoadAllSkill()
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
        for (int i = 0; i < _numOfSkills; ++i)
        {
            CheckSkill(i, currentTime);
        }
    }

    void CheckSkill(int index, float currentTime)
    {
        Skill curSkill = skillList[index];
        if (curSkill.isSkillPresses && currentTime >= curSkill.nextSkillTimes)
        {
            _stateInfo = PlayerAnimator.GetCurrentAnimatorStateInfo(0);
            if (curSkill.skillInfoSO.CanDoSkill(PlayerStatus.CurrentMana) && IsFreeAnimationState())
            {
                switch (curSkill.skillType)
                {
                    case SkillType.AutoAttack:
                        PlayerAnimator.SetTrigger("Attack");
                        break;
                    case SkillType.FirstSkill:
                        PlayerAnimator.SetTrigger("Skill");
                        break;
                    case SkillType.SecondSkill:
                        PlayerAnimator.SetTrigger("Skill");
                        break;
                    case SkillType.UltimateSkill:
                        PlayerAnimator.SetTrigger("Skill");
                        break;
                }
                curSkill.nextSkillTimes = currentTime + curSkill.skillInfoSO.skillCoolDown;
            }
        }
    }

    bool IsFreeAnimationState()
    {
        return _stateInfo.IsName("Player_Idle") || _stateInfo.IsName("Player_Run");
    }
}

public enum SkillType
{
    AutoAttack,
    FirstSkill,
    SecondSkill,
    UltimateSkill,
};
