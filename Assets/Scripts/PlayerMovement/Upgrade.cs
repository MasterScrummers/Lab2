using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private Animator animator;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "powerUp")
        {
            animator.Play("Upgrade_to_BigMario");
            StartCoroutine(DestoryTimer());
        }

    }

    IEnumerator DestoryTimer()
    {
        float animationTime = animator.runtimeAnimatorController.animationClips[3].length;
        yield return new WaitForSeconds(animationTime);
        Transform spawnTransform = gameObject.GetComponent<Transform>();
        Instantiate(prefab, new Vector3(spawnTransform.position.x, spawnTransform.position.y + 0.5f, spawnTransform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
