using System;
using Godot;
using System.Collections.Generic;
using System.Linq;

public enum ScriptType
{
    MainStroke,
    Sway,
    Surge,
    Roll,
    Pitch,
    Twist,
    TypeCount
}

public struct Action
{
    public float timestamp;
    public float pos;
}

public partial class Funscript
{
    public string Name { get; private set; }
    public List<Action> Actions = new List<Action>();

    public Funscript(Godot.Collections.Dictionary changeEvent)
    {
        Name = changeEvent["name"].AsString();
        UpdateFromEvent(changeEvent);
    }

    public void UpdateFromEvent(Godot.Collections.Dictionary changeEvent)
    {
        Actions.Clear();
        var script = changeEvent["funscript"].AsGodotDictionary();
        var actions = script["actions"].AsGodotArray();
        foreach(Godot.Collections.Dictionary action in actions)
        {
            Actions.Add(new Action() { 
                timestamp = action["at"].AsSingle() / 1000.0f,
                pos = action["pos"].AsSingle() / 100.0f
            });
        }
    }

    public float GetPositionAt(float time) 
    {
        var idx = Actions.BinarySearch(0, Actions.Count, new Action() 
        {
            timestamp = time,
            pos = 0
        }, Comparer<Action>.Create((a,b) => Comparer<float>.Default.Compare(a.timestamp, b.timestamp)));

        if(idx < 0)
        {
            idx = ~idx;
            if(idx - 1 >= 0 && idx < Actions.Count)
            {
                var current = Actions[idx-1];
                var next = Actions[idx];

                float t = (time - current.timestamp) / (next.timestamp - current.timestamp);
                return Mathf.Lerp(current.pos, next.pos, t);
            }
            else if(Actions.Count > 0)
            {
                return Actions.Last().pos;
            }
        }
        else 
        {
            return Actions[idx].pos;
        }
        
        return 0.5f;
    }
}