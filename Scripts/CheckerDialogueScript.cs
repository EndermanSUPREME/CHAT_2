// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;

// public class CheckerDialogueScript : MonoBehaviour
// {
//     // Assets/AI_DialogueObjects
//     public bool search = false;
//     DialogObject[] D_Obj;
//     List<DialogObject> d_List;
//     public DialogObject[] Check_Obj;

//     void OnDrawGizmos()
//     {
//         if (search)
//         {
//             D_Obj = null;
//             FindDialogueObjectsInDirectory();
//             search = false;
//         }
//     }

//     void FindDialogueObjectsInDirectory()
//     {
//         print("[*] Searching. . .");
//         string[] guids = AssetDatabase.FindAssets("t:DialogObject", new[] {"Assets/AI_DialogueObjects"});
        
//         int count = guids.Length;
//         D_Obj = new DialogObject[count];
//         for(int n = 0; n < count; n++)
//         {
//             var path = AssetDatabase.GUIDToAssetPath(guids[n]);
//             D_Obj[n] = AssetDatabase.LoadAssetAtPath<DialogObject>(path);
//         }

//         print("[+] Array Created!");

//         RunArrayCheck();
//     }

//     void RunArrayCheck()
//     {
//         print("[*] Running Object Checks. . .");

//         if (D_Obj != null)
//         {
//             if (D_Obj.Length > 0)
//             {
//                 d_List = new List<DialogObject>();
//                 Check_Obj = null;

//                 for (int i = 0; i < D_Obj.Length; i++)
//                 {
//                     IndexSpecs(D_Obj[i]);
//                 }

//                 Check_Obj = d_List.ToArray();
//             }
//         }

//         print("[+] List Of Flagged Branches Created!");
//     }

//     void IndexSpecs(DialogObject d)
//     {
//         // DialogScript -> string[] PlayerResponces { DialogObject NextObject  boolean isEndOfBranch }

//         if (d.playerResponces.Length > 0)
//         {
//             for (int i = 0; i < d.playerResponces.Length; i++)
//             {
//                 // if we have something written down for a player response but the next branch to jump to hasnt been plugged in AND isnt marked at the end of the branch to tag this
//                 if (d.playerResponces[i].playerResponse != null && ( d.playerResponces[i].nextDialogueObject == null && !d.playerResponces[i].isEndOfDialogueBranch ) )
//                 {
//                     d_List.Add(d);
//                     i = 10;
//                 }
//             }
//         }

//         string testStr = "Just the normal no intense swearing, no racist stuff, if you see suspicious users cause we've had trolls and other hostiles bother folks just report them and etc";
//         if (d.AI_Dialogue.Length >= testStr.Length - 15)
//         {
//             d_List.Add(d);
//         }
//     }

// }//EndScript