using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downgrade : MonoBehaviour
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
        Change();
    }
    void Change()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.Play("Downgrade_to_SmallMario");
            StartCoroutine(DestoryTimer());
        }
    }

    IEnumerator DestoryTimer()
    {
        float animationTime = animator.runtimeAnimatorController.animationClips[3].length;
        yield return new WaitForSeconds(animationTime);
        Transform spawnTransform = gameObject.GetComponent<Transform>();
        Instantiate(prefab, new Vector3(spawnTransform.position.x, spawnTransform.position.y + 1, spawnTransform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}