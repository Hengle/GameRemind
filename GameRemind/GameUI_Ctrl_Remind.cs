using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：GameUI_Ctrl_Remind  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/9/2 9:16:23
// ================================
namespace Assets.JackCheng.GameRemind
{
    public class GameUI_Ctrl_Remind : MonoBehaviour
    {
        public enum eMsgType 
        { 
        
             MESSAGE_1 = 0,
                  
             MESSAGE_2,
                  
             MESSAGE_3,
                  
             MESSAGE_4,
                  
             MESSAGE_5,
                  
             MESSAGE_6,
                  
             MESSAGE_7,
                  
             MESSAGE_8,
                  
             MESSAGE_9,
                  
             MESSAGE_10,
                  
             MESSAGE_11,
                  
             MESSAGE_12,
        }

        public RemindType type;
        public RemindCtrlType ctrlType;
        public eMsgType msgType;
        public GameObject remind;
        public bool first = true;

        void OnDestroy() 
        {
            GameUI_Mgr_Remind.messageMgr.delMessageLister(this, msgType.ToString(), CheckStateCallBack);
        }

        void Start()
        {
            if (!first)
            {
                return;
            }
            first = false;

            GameUI_Mgr_Remind.messageMgr.addMessageLister(this, msgType.ToString(), CheckStateCallBack);

            if (!GameUI_Mgr_Remind.remindFunc.ContainsKey(this))
            {
                if (GameUI_Mgr_Remind.GetRemindState(type, ctrlType))
                {
                    GameUI_Mgr_Remind.remindFunc.Add(this, true);
                }
                else
                {
                    GameUI_Mgr_Remind.remindFunc.Add(this, false);
                }
            }
        }

        void CheckStateCallBack(object sender,MsgArgs e)
        {
            RemindType _type = (RemindType)(int.Parse(e.paramList[0].ToString()));
            RemindCtrlType _ctrlType = (RemindCtrlType)(int.Parse(e.paramList[1].ToString()));

            if (ctrlType == RemindCtrlType.None)
            {
                if (_type == type)
                {
                    if (!GameUI_Mgr_Remind.remindFunc.ContainsKey(this))
                    {
                        if (GameUI_Mgr_Remind.GetRemindState(type, ctrlType))
                        {
                            GameUI_Mgr_Remind.remindFunc.Add(this, true);
                        }
                        else
                        {
                            GameUI_Mgr_Remind.remindFunc.Add(this, false);
                        }
                    }
                    return;
                }
            }
            if (_ctrlType == ctrlType)
            {
                if (!GameUI_Mgr_Remind.remindFunc.ContainsKey(this))
                {
                    if (GameUI_Mgr_Remind.GetRemindState(type, ctrlType))
                    {
                        GameUI_Mgr_Remind.remindFunc.Add(this, true);
                    }
                    else
                    {
                        GameUI_Mgr_Remind.remindFunc.Add(this, false);
                    }
                }
            }


        }
    }
}
