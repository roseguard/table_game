using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetworkUnit
{
    private Socket m_socket = null;
    private SocketAsyncEventArgs m_receiveEvent = new SocketAsyncEventArgs();

    public NetworkUnit(Socket socket)
    {
        m_socket = socket;

        m_receiveEvent.SetBuffer(new byte[1024], 0, 1024);

        m_socket.ReceiveAsync(m_receiveEvent);
        m_receiveEvent.Completed += MessageReceived;
    }

    public void MessageReceived(object sender, SocketAsyncEventArgs e)
    {
        Debug.Log(e.Buffer.ToString());

        m_socket.ReceiveAsync(m_receiveEvent);
        m_receiveEvent.Completed += MessageReceived;
    }

    public void SendMessage(string message)
    {
        SocketAsyncEventArgs e = new SocketAsyncEventArgs();
        e.SetBuffer(Encoding.UTF8.GetBytes(message), 0, message.Length);
        m_socket.SendAsync(e);
    }
}
