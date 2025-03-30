using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindows : MonoBehaviour
{
    public delegate void CloseHandler(UIWindows sender, WindowResult result);
    public event CloseHandler OnClose;

    public virtual System.Type Type { get { return this.GetType(); } }

    public enum WindowResult
    {
        None=1,
        Yes,
        No
    }

    public void Close(WindowResult result =WindowResult.None)
    {
        UIManager.Instance.Close(this.Type);
        if (this.OnClose != null)
            this.OnClose(this,result);
        this.OnClose = null;
    }

    public virtual void OnCloseClick()
    {
        this.Close();
        
    }

    public virtual void OnYesClick()
    {
        this.Close(WindowResult.Yes);
        Destroy(this.gameObject);
    }

    public virtual void OnNoClick()
    {
        this.Close(WindowResult.No);
    }

    private void OnMouseDown()
    {
        Debug.LogFormat(this.name + "Clicked");
    }
}
