using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetworkUnit
{
    //  <4>             <4>         <4>            <N>
    //  <size>          <messid>    <messcommand>  <params>

    private Socket m_socket = null;
    private const int m_bufferSize = 255;
    private byte[] m_buffer = new byte[m_bufferSize];
    private SocketAsyncEventArgs m_receiveEvent = new SocketAsyncEventArgs();

    public NetworkUnit(Socket socket)
    {
        m_socket = socket;

        m_receiveEvent.SetBuffer(m_buffer, 0, m_bufferSize);

        m_socket.ReceiveAsync(m_receiveEvent);
        m_receiveEvent.Completed += M_receiveEvent_Completed;
    }

    private void M_receiveEvent_Completed(object sender, SocketAsyncEventArgs e)
    {
        string tempStr = System.Text.Encoding.UTF8.GetString(e.Buffer);
        if(tempStr.Length > 12)
        {
            Array.Clear(e.Buffer, 0, m_bufferSize);
            string paramsizestr = new string(tempStr.ToCharArray(), 0, 4);
            string messidstr = new string(tempStr.ToCharArray(), 4, 4);
            string messcommandstr = new string(tempStr.ToCharArray(), 8, 4);

            UInt32 paramsize = CastCharToInt(paramsizestr.ToCharArray());
            UInt32 messid = CastCharToInt(messidstr.ToCharArray());
            UInt32 messcommand = CastCharToInt(messcommandstr.ToCharArray());

            Debug.Log("AAAAAAAAA");
            Debug.Log(paramsize);
            Debug.Log(messid);
            Debug.Log(messcommand);
        }
        

        m_socket.ReceiveAsync(m_receiveEvent);
    }

    private UInt32 CastCharToInt(char[] bytes)
    {
        return ((UInt32)bytes[0]) | ((UInt32)bytes[1] << 8) | ((UInt32)bytes[2] << 16) | ((UInt32)bytes[3] << 24);
    }

    public void SendMessage(string message)
    {
        SocketAsyncEventArgs e = new SocketAsyncEventArgs();
        e.SetBuffer(Encoding.UTF8.GetBytes(message), 0, message.Length);
        m_socket.SendAsync(e);
    }
}
