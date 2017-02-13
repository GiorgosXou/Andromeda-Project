/*
	2014 04 14 Lu Chengwei VB4A with reference to http://blog.csdn.net/zz7zz7zz/article/details/9341635
 */

package com.google.devtools.simple.runtime.components.impl.android;

import com.google.devtools.simple.runtime.android.ApplicationImpl;
import com.google.devtools.simple.runtime.components.ComponentContainer;
import com.google.devtools.simple.runtime.components.VB4ASocket;

import com.google.devtools.simple.runtime.components.impl.ComponentImpl;

import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Vibrator;
import java.util.Arrays;  

import android.app.Activity;
import android.app.PendingIntent;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Toast; 

import com.google.devtools.simple.runtime.events.EventDispatcher;

import java.io.BufferedReader;  
import java.io.BufferedWriter;  
import java.io.InputStreamReader;  
import java.io.OutputStreamWriter;  
import java.io.PrintWriter;  
import java.net.Socket;  
import java.io.IOException;  
import java.io.InputStream;
import java.io.OutputStream;
import java.io.ByteArrayOutputStream;

//import com.google.devtools.simple.runtime.components.impl.android.VB4ASocHelper;
import com.google.devtools.simple.runtime.components.impl.android.util.ISocketResponse;
import com.google.devtools.simple.runtime.components.impl.android.util.Packet;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.URLDecoder;

import java.io.IOException;  
import java.io.InputStream;  
import java.io.OutputStream;  
import java.net.InetSocketAddress;  
import java.net.Socket;  
import java.net.SocketException;  
import java.util.Iterator;  
import java.util.concurrent.LinkedBlockingQueue;  
  
import android.content.Context;  
import android.util.Log;  

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;

import android.app.Service;
import android.content.Intent;
import android.os.Binder;
import android.os.Handler;
import android.os.IBinder;
import android.os.Message;

import com.google.devtools.simple.runtime.components.impl.android.util.ISocketResponse;
import com.google.devtools.simple.runtime.components.impl.android.util.Packet;
import com.google.devtools.simple.runtime.components.impl.android.util.NetworkUtil;

public final class VB4ASocketImpl extends ComponentImpl implements VB4ASocket {
  private String sendContent,recContent;
    private final int STATE_OPEN=1;
    private final int STATE_CLOSE=1<<1;
    private final int STATE_CONNECT_START=1<<2; 
    private final int STATE_CONNECT_SUCCESS=1<<3;
    private final int STATE_CONNECT_FAILED=1<<4;
    private final int STATE_CONNECT_WAIT=1<<5;
      
    private String IP="192.168.1.100";  
    private int PORT=60000;  
      
    private int state=STATE_CONNECT_START;  
      
    private Socket socket=null;  
    private OutputStream outStream=null;  
    private InputStream inStream=null;  
      
    private Thread conn=null;  
    private Thread send=null;  
    private Thread rec=null;  
      
    private Context context;  
    private LinkedBlockingQueue<Packet> requestQueen=new LinkedBlockingQueue<Packet>();  
    private final Object lock=new Object();  
	
  public VB4ASocketImpl(ComponentContainer container) {
    super(container);
	this.context=ApplicationImpl.getContext();  
  }


  @Override
  public void SocketResponse(String rec){
	EventDispatcher.dispatchEvent(this, "SocketResponse",rec);
  }
  
  @Override
  public void OpenSoc(String ip, int port) {
	open(ip,port);
  }
  
  @Override
  public void CloseSoc() {
	close();
  }
  
  @Override
  public void Reconnect() {
	reconn();
  }
  
  @Override
  public void Send(String txt) {
	Packet packet=new Packet();
	packet.pack(txt);
	send(packet);
  }
  
  	private static NetworkInfo getNetworkInfo(Context context) {

	ConnectivityManager cm = (ConnectivityManager)context.getSystemService(Context.CONNECTIVITY_SERVICE);
	return cm.getActiveNetworkInfo();
	}

    public int send(Packet in)  
    {  
        requestQueen.add(in);  
        synchronized (lock)   
        {  
            lock.notifyAll();  
        }  
        return in.getId();  
    }  
      
    public void cancel(int reqId)  
    {  
         Iterator<Packet> mIterator=requestQueen.iterator();  
         while (mIterator.hasNext())   
         {  
             Packet packet=mIterator.next();  
             if(packet.getId()==reqId)  
             {  
                 mIterator.remove();  
             }  
        }  
    }  
      
      
    public boolean isNeedConn()  
    {  
        return !((state==STATE_CONNECT_SUCCESS)&&(null!=send&&send.isAlive())&&(null!=rec&&rec.isAlive()));  
    }  
      
    public void open()  
    {  
        reconn();  
    }  
      
    public void open(String host,int port)  
    {  
        this.IP=host;  
        this.PORT=port;  
        reconn();  
    }  
      
    private long lastConnTime=0;  
    public synchronized void reconn()  
    {  
        if(System.currentTimeMillis()-lastConnTime<2000)  
        {  
            return;  
        }  
        lastConnTime=System.currentTimeMillis();  
          
        close();  
        state=STATE_OPEN;  
        conn=new Thread(new Conn());  
        conn.start();  
    }  
      
    public synchronized void close()  
    {  
        if(state!=STATE_CLOSE)  
        {  
            try {  
                if(null!=socket)  
                {  
                    socket.close();  
                }  
            } catch (Exception e) {  
                e.printStackTrace();  
            }finally{  
                socket=null;  
            }  
              
            try {  
                if(null!=outStream)  
                {  
                    outStream.close();  
                }  
            } catch (Exception e) {  
                e.printStackTrace();  
            }finally{  
                outStream=null;  
            }  
              
            try {  
                if(null!=inStream)  
                {  
                    inStream.close();  
                }  
            } catch (Exception e) {  
                e.printStackTrace();  
            }finally{  
                inStream=null;  
            }  
              
            try {  
                if(null!=conn&&conn.isAlive())  
                {  
                    conn.interrupt();  
                }  
            } catch (Exception e) {  
                e.printStackTrace();  
            }finally{  
                conn=null;  
            }  
              
            try {  
                if(null!=send&&send.isAlive())  
                {  
                    send.interrupt();  
                }  
            } catch (Exception e) {  
                e.printStackTrace();  
            }finally{  
                send=null;  
            }  
              
            try {  
                if(null!=rec&&rec.isAlive())  
                {  
                    rec.interrupt();  
                }  
            } catch (Exception e) {  
                e.printStackTrace();  
            }finally{  
                rec=null;  
            }  
              
            state=STATE_CLOSE;  
        }  
        requestQueen.clear();  
    }  
      
    private class Conn implements Runnable  
    {  
        public void run() {  
            while(state!=STATE_CLOSE)  
            {  
                try {  
                    state=STATE_CONNECT_START;  
                    socket=new Socket();  
                    socket.connect(new InetSocketAddress(IP, PORT), 15*1000);  
                    state=STATE_CONNECT_SUCCESS;  
                } catch (Exception e) {  
                    e.printStackTrace();  
                    state=STATE_CONNECT_FAILED;  
                }  
                  
                if(state==STATE_CONNECT_SUCCESS)  
                {  
                    try {  
                        outStream=socket.getOutputStream();  
                        inStream=socket.getInputStream();  
                    } catch (IOException e) {  
                        e.printStackTrace();  
                    }  
                      
                    send=new Thread(new Send());  
                    rec=new Thread(new Rec());  
                    send.start();  
                    rec.start();  
                    break;  
                }  
                else  
                {  
                    state=STATE_CONNECT_WAIT;  
                    if(NetworkUtil.isNetworkAvailable(context))  
                    {  
                        try {  
                                Thread.sleep(15*1000);  
                        } catch (InterruptedException e) {  
                            e.printStackTrace();  
                            break;  
                        }  
                    }  
                    else  
                    {  
                        break;  
                    }  
                }  
            }  
        }  
    }  
      
    private class Send implements Runnable  
    {  
        public void run() {  
            try {  
                    while(state!=STATE_CLOSE&&state==STATE_CONNECT_SUCCESS&&null!=outStream)  
                    {  
                                Packet item;  
                                while(null!=(item=requestQueen.poll()))  
                                {  
                                    outStream.write(item.getPacket());  
                                    outStream.flush();  
                                    item=null;  
                                }  
                                  
                                synchronized (lock)  
                                {  
                                    lock.wait();  
                                }  
                    }  
            }catch(SocketException e1)   
            {  
                e1.printStackTrace();
                reconn();  
            }   
            catch (Exception e) {  
                e.printStackTrace();  
            }  
        }  
    }  
      
    private class Rec implements Runnable  
    {  
        public void run() {  
              
            try {  
                    while(state!=STATE_CLOSE&&state==STATE_CONNECT_SUCCESS&&null!=inStream) 
                    {  
                            byte[] bodyBytes=new byte[5];  
                            int offset=0;  
                            int length=5;  
                            int read=0;  
                              
                            while((read=inStream.read(bodyBytes, offset, length))>0)  
                            {  
                                if(length-read==0)  
                                {   
									Toast.makeText(ApplicationImpl.getContext(), new String(bodyBytes),Toast.LENGTH_SHORT).show();
                                    SocketResponse(new String(bodyBytes));  
                                    offset=0;  
                                    length=5;  
                                    read=0;  
                                    continue;  
                                }  
                                offset+=read;  
                                length=5-offset;  
                            }  
                              
                            reconn();
                            break;  
                    }  
            }  
            catch(SocketException e1)   
            {  
				e1.printStackTrace();
            }   
            catch (Exception e2) {  
                e2.printStackTrace();  
            }  
              
        }  
    }  	
  
}
