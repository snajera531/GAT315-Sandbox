using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCreator : Action
{    
    public GameObject original;
    public FloatData forceMagnitude;
    public FloatData size;
    public ForceModeData forceMode;

    public override eActionType ActionType => eActionType.Force;

    bool action { get; set; } = false;
    bool oneTime { get; set; } = false;

    public override void StartAction()
    {
        action = true;
        oneTime = true;
    }

    public override void StopAction()
    {
        action = false;
    }

    void Update()
    {
        if(action && (oneTime || Input.GetKey(KeyCode.LeftControl)))
        {
            oneTime = false;
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameObject = Instantiate(original, position, Quaternion.identity);
            if(gameObject.TryGetComponent<PointEffector>(out PointEffector effector))
            {
                effector.forceMagnitude = forceMagnitude;
                effector.forceMode = forceMode.value;
                effector.shape.Size = size;

                World.Instance.Forces.Add(effector);
            }
        }
    }
}
