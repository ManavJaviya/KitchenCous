using System;
using UnityEngine;

public interface IHasProgress 
{
    public event System.EventHandler<onProgressChangeEventArgs> onProgressChange;
    public class onProgressChangeEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
