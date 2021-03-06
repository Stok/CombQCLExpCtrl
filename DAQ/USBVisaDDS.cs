﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NationalInstruments.Visa;
using Ivi.Visa;


namespace DAQ
{
    public class USBVisaDDS
    {
        SerialSession serial;
        string physicalAddress;

        public USBVisaDDS(string physicalAddress)
        {
            this.physicalAddress = physicalAddress;
        }


        public void Connect()
        {
            serial = new SerialSession(physicalAddress);
            serial.BaudRate =115200;
            serial.DataBits = 8;
            serial.StopBits = SerialStopBitsMode.One;
            serial.ReadTermination = SerialTerminationMethod.HighestBit;
            serial.Parity = SerialParity.None;
        }

        public void Disconnect()
        {
            try
            {
                serial.Dispose();
            }
            catch(Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
        }


        public void SetFrequency(double frequency)
        {
            byte[] command = freqToCommand(frequency);
            write(command);
        }

        public void SetFrequency(double frequency, string unit)
        {
            switch (unit)
            {
                case "K":
                    frequency = frequency * 1000;
                    break;
                case "M":
                    frequency = frequency * 1000000;
                    break;

            }
            byte[] command = freqToCommand(frequency);
            write(command);
        }

        private void write(byte[] command)
        {
            serial.RawIO.Write(command);
        }

        byte[] freqToCommand(double frequency)
        {
            int freq = (int)Math.Round(frequency);
            return BitConverter.GetBytes(freq);
        }
    }
}
