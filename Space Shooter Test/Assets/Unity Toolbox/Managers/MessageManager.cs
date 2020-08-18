using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Runtime.InteropServices.ComTypes;

[CreateAssetMenu(fileName = "Message", menuName = "Toolbox/Managers/Message Manager")]
public class MessageManager : ManagerBase, IExecute
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    public void Subscribe(ServiceShareData id, Action next, object sender = null, string tag = "")
    {
        var sub = MessageBroker.Default.Receive<MessageBase>().Where(
            ctx =>
            (ctx.id == id) &&
            (ctx.sender == null || ctx.sender == sender) &&
            (ctx.tag == "" || ctx.tag == tag)
        ).Subscribe(_ => next.Invoke()).AddTo(Toolbox.GetManager<MessageManager>().subscriptions);
    }

    public void Send(ServiceShareData id, object sender = null, string tag = "")
    {
        MessageBroker.Default.Publish(MessageBase.Create(sender, id, tag));
    }

    private void ClearDisposables()
    {
        subscriptions.Dispose();
    }

	public void OnExecute()
	{
        Subscribe(ServiceShareData.SCENE_CHANGE, ClearDisposables);
    }
}
