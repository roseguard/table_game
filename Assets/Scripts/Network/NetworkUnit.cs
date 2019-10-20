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
    //  <uint>          <int>       <uint-enum>    <not specified>
    // messid > 0 - server inited mess
    // messid < 0 - client inited mess
    // messid - client decrement, server increment

    public delegate void MessRespondDelegate(UInt32 messid, UInt32 messcommand, string[] paramsArray);

    public enum Requests
    {
        InitNewCharacter,
        Count
    }

    private Socket m_socket = null;
    private const int m_bufferSize = 255;
    private byte[] m_buffer = new byte[m_bufferSize];
    private SocketAsyncEventArgs m_receiveEvent = new SocketAsyncEventArgs();
    private Dictionary<UInt32, MessRespondDelegate> m_callbacks = new Dictionary<uint, MessRespondDelegate>();

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
            string sizestr = new string(tempStr.ToCharArray(), 0, 4);
            string messidstr = new string(tempStr.ToCharArray(), 4, 4);
            string messcommandstr = new string(tempStr.ToCharArray(), 8, 4);
            string paramsStr = new string(tempStr.ToCharArray(), 12, tempStr.Length - 12);

            UInt32 size = CastCharToInt(sizestr.ToCharArray());
            UInt32 messid = CastCharToInt(messidstr.ToCharArray());
            UInt32 messcommand = CastCharToInt(messcommandstr.ToCharArray());
            string[] paramsArray = paramsStr.Split('&');
            if(m_callbacks.ContainsKey(messid))
            {
                m_callbacks[messid]?.Invoke(messid, messcommand, paramsArray);
            }
            else
            {
                ProcceedRequest(messid, messcommand, paramsArray);
            }
        }
        

        m_socket.ReceiveAsync(m_receiveEvent);
    }

    private void ProcceedRequest(UInt32 messid, UInt32 messcommand, string[] paramsArray)
    {
        if(messcommand >= (int)Requests.Count || messcommand < 0)
        {
            SendMessage("Invalid");
        }
        Requests request = (Requests)messcommand;
        switch(request)
        {
            case (Requests.InitNewCharacter): SendMessage("Goohd"); break;
        }
    }

    private UInt32 CastCharToInt(char[] bytes)
    {
        return ((UInt32)bytes[0]) | ((UInt32)bytes[1] << 8) | ((UInt32)bytes[2] << 16) | ((UInt32)bytes[3] << 24);
    }

    private byte[] CastIntToChars(UInt32 value)
    {
        return BitConverter.GetBytes(value);
    }

    public void SendMessage(string message)
    {
        byte[] size = CastIntToChars((UInt32)message.Length);
        byte[] messageOut = new byte[4 + message.Length];

        byte[] messageUTF8 = Encoding.UTF8.GetBytes(message);

        for (int i = 0; i < 4; i++)
        {
            messageOut[i] = size[i];
        }
        for(int i = 4, j = 0; j < message.Length; i++, j++)
        {
            messageOut[i] = messageUTF8[j];
        }

        SocketAsyncEventArgs e = new SocketAsyncEventArgs();
        e.SetBuffer(messageOut, 0, message.Length + 4);

        m_socket.SendAsync(e);
    }
}
