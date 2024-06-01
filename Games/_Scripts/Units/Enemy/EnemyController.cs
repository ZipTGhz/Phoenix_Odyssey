using UnityEngine;

//MANAGER ALL SCRIPTS OF ENEMY
[RequireComponent(typeof(EnemyStatus))]
[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(LifeTimeDisabler))]
public class EnemyController : CustomMonoBehaviour
{
    [Header("SYSTEMS")]
    [SerializeField]
    protected GameObject _statusObjectUI;
    public GameObject StatusObjectUI
    {
        get => _statusObjectUI;
    }

    [Header("SCRIPTS")]
    [SerializeField]
    protected EnemyAI _enemyAI;

    [SerializeField]
    protected EnemyStatus _enemyStatus;

    [SerializeField]
    protected LifeTimeDisabler _lifeTimeDisabler;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _statusObjectUI = GetComponentInChildren<Canvas>().gameObject;
        _enemyAI = GetComponent<EnemyAI>();
        _enemyStatus = GetComponent<EnemyStatus>();
        _lifeTimeDisabler = GetComponent<LifeTimeDisabler>();
    }

    public void SetActive(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        _statusObjectUI.SetActive(true);
        _enemyAI.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        _enemyStatus.enabled = true;
        _enemyStatus.SetDefaultInfo();
        transform.position = position;
        transform.rotation = rotation;
    }

    public void SetDeactivate()
    {
        //Disable collider
        GetComponent<Collider2D>().enabled = false;
        //Disable status UI
        _statusObjectUI.SetActive(false);
        //Disable AI
        _enemyAI.DisableUpdatePath();
        _enemyAI.enabled = false;
        //Enable disabler
        _lifeTimeDisabler.DelayedDisable();
    }

    // void Disable()
    // {
    // 	enemyController.StatusObjectUI.SetActive(false);
    // 	GetComponent<Collider2D>().enabled = false;
    // 	this.enabled = false;
    // 	GetComponentInParent<LifeTimeDisabler>().DelayedDisable();
    // }
}
