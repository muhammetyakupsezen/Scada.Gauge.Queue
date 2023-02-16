using Scada.Data;
using Scada.Data.DataList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScadaApplicationQue
{
    public delegate void RxOnGenerateDevice(List<TDevice> devices);
    public delegate void RxOnGetDeviceData(List<TData> DataList);
    public class TProcess
    {
        
       
            Thread ThrSimulation;
            private List<TDevice> devices;
            private int DeviceCount;
            public event RxOnGenerateDevice OnGenerateDevice;
            public event RxOnGetDeviceData OnGetDeviceData;
            public void CreateDevice(int Count)
            {
                devices = new List<TDevice>();

                for (int i = 0; i < Count; i++)
                {
                    TDevice device = new TDevice();
                    device.DeviceId = new Random().Next(1, 20);
                    Thread.Sleep(20);
                    device.DeviceName = device.DeviceId.ToString() + ".Cihaz ";

                    devices.Add(device);

                }

                if (OnGenerateDevice != null)
                {
                    OnGenerateDevice(devices);
                }

            }


            public void StartGenerateData()
            {
                if (ThrSimulation == null)
                {
                    ThrSimulation = new Thread(new ThreadStart(DoStartGenerateData));
                }
                ThrSimulation.Start();
            }

            public void DoStartGenerateData()
            {
                while (true)
                {

                    foreach (var device in devices)
                    {

                        TDeviceProcess deviceProcess = new TDeviceProcess(device);
                        deviceProcess.Start();

                       
                    }



                    int SleepTime = new Random().Next(2, 9);
                    Thread.Sleep(SleepTime * 1000);


                }
            }


            public void GetDeviceDatas()
            {
                foreach (var device in devices)
                {

                    TDeviceDataProcess TDeviceDataProcess = new TDeviceDataProcess(device);
                    TDeviceDataProcess.Start();
                    TDeviceDataProcess.OnGetDeviceData += TDeviceDataProcess_OnGetDeviceData;
                }
            }

            private void TDeviceDataProcess_OnGetDeviceData(List<TData> DataList)
            {
                if (OnGetDeviceData != null)
                {
                    OnGetDeviceData(DataList);
                }
            }
        




        public class TDeviceProcess
        {
            private TDevice device;
            Thread MyThread;
            public TDeviceProcess(TDevice Device)
            {
                device = Device;
            }

            public void Start()
            {
                if (MyThread == null)
                {
                    MyThread = new Thread(new ThreadStart(DoStart));
                }
                MyThread.Start();
            }

            public void DoStart()
            {

                while (true)
                {
                    TData data = new TData();
                    data.DataDate = DateTime.Now;
                    data.Heat = new Random().Next(1, 100);

                    Thread.Sleep(20);
                    data.DeviceId = device.DeviceId;

                    Class1.AddHeatData(device.DeviceId, data);




                    int SleepTime = new Random().Next(2, 9);
                    Thread.Sleep(SleepTime * 100);
                }  
                       

            }

        }

        public class TDeviceDataProcess
        {
            public event RxOnGetDeviceData OnGetDeviceData;
            private TDevice device;
            Thread MyThread;
            public TDeviceDataProcess(TDevice Device)
            {
                device = Device;
            }

            public void Start()
            {
                if (MyThread == null)
                {
                    MyThread = new Thread(new ThreadStart(DoStart));
                }
                MyThread.Start();
            }

            public void DoStart()
            {

                while (true)
                {

                    
                    List<TData> dataList = Class1.GetHeatDatas(device.DeviceId);

                    if (dataList != null)
                    {
                        if (OnGetDeviceData != null)
                        {
                            OnGetDeviceData(dataList);
                        }
                    }


                    Thread.Sleep(500);

                   

                  

                }

               

            }

        }






    }
}
