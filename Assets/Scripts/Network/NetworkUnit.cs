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
