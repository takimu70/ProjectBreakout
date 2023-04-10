using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyCodeBlock", menuName = "MyCodeBlock")]
public class MyCodeBlock : ScriptableObject
{
    [TextArea(15,20)]
    public string codeBlock;

    public GameEvent[] gameEvents;

}
