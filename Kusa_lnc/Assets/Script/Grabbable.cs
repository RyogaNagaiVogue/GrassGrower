using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]

public class Grabbable : MonoBehaviour
{

    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);
    private Interactable interactable;
    //public GameObject LeftController, RightController;

    void Awake()
    {
        interactable = this.GetComponent<Interactable>();
    }

    private void HandHoverUpdate(Hand hand)//物体間の衝突毎に呼び出す関数
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        //何も掴んでいないときに何かを掴んだときの処理
        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            //ここgameObjectの位置調整とかできるだろうか
            //Transform handTrans = hand.transform;
             // Vector3 pos = handTrans.position;
        //pos.x += 0.02f;    //x座標移動
        //pos.y += 0.02f;    //y座標移動
        //pos.z += 0.02f;    //z座標移動
            // gameObject.transform.position = pos;

            //if(gameObject.transform.parent == null)gameObject.transform.parent = hand.transform;
            //コントローラを親にします 
            hand.HoverLock(interactable);
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);//アタッチする
        }

        //物体を離したときの処理
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);//アタッチを外す
             gameObject.transform.parent = null;//☆

            hand.HoverUnlock(interactable);
            //別スクリプトのthrowableと併せて、物体を投げるのを実装

        }
    }
}
