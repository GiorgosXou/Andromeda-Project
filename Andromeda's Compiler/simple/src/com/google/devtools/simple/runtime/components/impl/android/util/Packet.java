package com.google.devtools.simple.runtime.components.impl.android.util;

import java.util.concurrent.atomic.AtomicInteger;

public class Packet {

private static final AtomicInteger mAtomicInteger=new AtomicInteger();
private int id=mAtomicInteger.getAndIncrement();
private byte[] data;

public int getId() {
return id;
}

public void pack(String txt)
{
data=txt.getBytes();
}

public byte[] getPacket()
{
return data;
}
}