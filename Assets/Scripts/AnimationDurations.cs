using UnityEngine;

public class AnimationDurations : MonoBehaviour
{
    public float SlashTime { get; private set; }
    public float WalkTime { get; private set; }

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("Error: Did not find animator!");
        }

        UpdateAnimClipTimes();
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Slash":
                    SlashTime = clip.length;
                    break;
                case "Walk":
                    WalkTime = clip.length;
                    break;
            }
        }
    }
}