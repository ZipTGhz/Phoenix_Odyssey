using UnityEngine;

public class PlayerStatus : EntityStatus
{
    //SYSTEM
    public GameObject gameOverCanvas;

    //STATUS SLIDER & STATUS INFO
    public StatusBarSlider manaBarSlider;

    [SerializeField]
    int maxMana = 10;
    int currentMana;
    public int CurrentMana
    {
        get => currentMana;
    }

    protected override void Start()
    {
        base.Start();

        currentMana = maxMana;
        manaBarSlider.SetMaxValue(maxMana);
    }

    protected override void Hurt()
    {
        AnimatorStateInfo stateInfo = GetAnimator().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Player_Hurt_1"))
            return;
        base.Hurt();
    }

    public void UseMana(int mana)
    {
        currentMana -= mana;
        manaBarSlider.SetNewValue(currentMana);
    }

    protected override void Die()
    {
        // base.Die();

        //Disable player
        //Destroy player
        Destroy(gameObject);
        gameOverCanvas.SetActive(true);
    }
}
