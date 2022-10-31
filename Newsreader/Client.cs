﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Microsoft.VisualBasic;

namespace Newsreader;

public class Client
{
    private const string SERVER_NAME = "news.sunsite.dk";
    private const int SERVER_PORT = 119;
    private NetworkStream? _networkStream;
    private StreamReader? _streamReader;
    private TcpClient? _tcpClient;
    private StreamWriter? _streamWriter;

    private readonly List<string> _badResponses = new()
    {
        "500 What?",
        "501 user Name|pass Password",
        "502 Authentication failed (Please register at http://dotsrc.org/usenet/)",
    };

    public ObservableCollection<string> Foo(string command)
    {
        var list = new ObservableCollection<string>();
        SendCommand(command);
        // get every line
        while (true)
        {
            // get the line
            string? line = _streamReader?.ReadLine();
            if (line is ".") break;
            list.Add(line);
        }

        return list;
    }
    
    public void Login(string username, string password)
    {
        Connect();
        // Ignore the welcome message
        _streamReader?.ReadLine();
        SendCommand($"authinfo user {username}"); 
        SendCommand($"authinfo pass {password}");
    }

    public void SendCommand(string command)
    {
        _streamWriter?.WriteLine(command);
        Debug.WriteLine($"Sent: {command}");
        ValidateResponse();
    }
    
    private void Close()
    {
        _tcpClient?.Close();
        _networkStream?.Close();
        _streamReader?.Close();
        _streamWriter?.Close();
    }

    private void Connect()
    {
        _tcpClient = new(SERVER_NAME, SERVER_PORT);
        _networkStream = _tcpClient.GetStream();
        _streamReader = new(_networkStream, Encoding.UTF8);
        _streamWriter = new(_networkStream);
        _streamWriter.AutoFlush = true;
    }

    private void ValidateResponse()
    {
        string? response = _streamReader?.ReadLine();
        Debug.WriteLine($"Received: {response}");
        if (response is null || _badResponses.Contains(response))
        {
            Close();
            throw new Exception(response);
        }
    }
}