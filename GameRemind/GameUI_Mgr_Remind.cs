using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Game.UI;
using Game.Model.Define;
using Game;

// ================================
//* 功能描述：GameUI_Mgr_Remind  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/9/2 9:14:20
// ================================
namespace Assets.JackCheng.GameRemind
{
    public enum RemindType
    {
        RemindType_1,
        RemindType_2,
        RemindType_3,
        RemindType_4,
        RemindType_5,
    }


    public enum RemindCtrlType
    {
        None,
        RemindCtrlType_1,
        RemindCtrlType_2,
        RemindCtrlType_3,
        RemindCtrlType_4,
        RemindCtrlType_5,
    }
    public class GameUI_Mgr_Remind :MonoBehaviour
    {
        public static GameUI_Mgr_Remind ins;

        private static Dictionary<RemindType, Dictionary<RemindCtrlType, bool>> remindDic = new Dictionary<RemindType, Dictionary<RemindCtrlType, bool>>();

        static Dictionary<RemindType, string> remindCallBackDic = new Dictionary<RemindType, string>();

        public static Dictionary<GameUI_Ctrl_Remind, bool> remindFunc = new Dictionary<GameUI_Ctrl_Remind, bool>();

        public static MessagerMgr messageMgr;

        void Awake() 
        {
            ins = this;

            messageMgr = new MessagerMgr();

            Init();
        }

        void Update() 
        {
            if (remindFunc.Keys.Count != null)
            {
                foreach (GameUI_Ctrl_Remind ctrl in remindFunc.Keys)
                {
                    if (ctrl != null)
                    {
                        ctrl.remind.SetActive(remindFunc[ctrl]);
                    }
                }
                remindFunc.Clear();
            }
        }


        public void Init() 
        {
            remindCallBackDic.Clear();
            remindCallBackDic.Add(RemindType.RemindType_1, MsgType.MESSAGE_1);
            remindCallBackDic.Add(RemindType.RemindType_2, MsgType.MESSAGE_2);
            remindCallBackDic.Add(RemindType.RemindType_3, MsgType.MESSAGE_3);
            remindCallBackDic.Add(RemindType.RemindType_4, MsgType.MESSAGE_4);
            remindCallBackDic.Add(RemindType.RemindType_5, MsgType.MESSAGE_5);
        }


        /// <summary>
        /// 设置提示的感叹号状态
        /// </summary>
        /// <param name="remindType"></param>
        /// <param name="remindCtrlType"></param>
        /// <param name="isShow"></param>
        public static void SetRemindState(RemindType remindType, RemindCtrlType remindCtrlType, bool isShow)
        {
            if (!remindDic.ContainsKey(remindType))
            {
                remindDic.Add(remindType, new Dictionary<RemindCtrlType, bool>());
            }
            if (isShow)
            {
                if (!remindDic[remindType].ContainsKey(remindCtrlType))
                {
                    remindDic[remindType].Add(remindCtrlType, true);
                }
            }
            else
            {
                if (remindDic[remindType].ContainsKey(remindCtrlType))
                {
                    remindDic[remindType].Remove(remindCtrlType);
                }
            }

            if (remindCallBackDic.Keys.Count == 0)
            {
                return;
            }

            //发送消息
            messageMgr.sendMessage(null, remindCallBackDic[remindType], (int)remindType, (int)remindCtrlType);

        }

        /// <summary>
        /// 获得提示感叹号的状态
        /// </summary>
        /// <param name="remindType"></param>
        /// <param name="remindCtrlType"></param>
        /// <returns></returns>
        public static bool GetRemindState(RemindType remindType, RemindCtrlType remindCtrlType)
        {
            if (!remindDic.ContainsKey(remindType))
            {
                remindDic.Add(remindType, new Dictionary<RemindCtrlType, bool>());
            }
            if (remindCtrlType == RemindCtrlType.None)
            {
                if (remindDic[remindType].Keys.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return remindDic[remindType].ContainsKey(remindCtrlType);
            }
        }


        void OnGUI() {
            if (GUILayout.Button("添加消息Eat")) 
            {
                GameUI_Mgr_Remind.SetRemindState(RemindType.RemindType_1, RemindCtrlType.RemindCtrlType_1, true);
            }

            if (GUILayout.Button("添加消息Eight"))
            {
                GameUI_Mgr_Remind.SetRemindState(RemindType.RemindType_1, RemindCtrlType.RemindCtrlType_2, true);
            }

            if (GUILayout.Button("删除消息Eat"))
            {
                GameUI_Mgr_Remind.SetRemindState(RemindType.RemindType_1, RemindCtrlType.RemindCtrlType_1, false);
            }

            if (GUILayout.Button("删除消息Eight"))
            {
                GameUI_Mgr_Remind.SetRemindState(RemindType.RemindType_1, RemindCtrlType.RemindCtrlType_2, false);
            }

            if (GUILayout.Button("添加消息Team"))
            {
                GameUI_Mgr_Remind.SetRemindState(RemindType.RemindType_2, RemindCtrlType.RemindCtrlType_3, true);
            }

            if (GUILayout.Button("删除消息Team"))
            {
                GameUI_Mgr_Remind.SetRemindState(RemindType.RemindType_2, RemindCtrlType.RemindCtrlType_3, false);
            }
        }
    }
}
