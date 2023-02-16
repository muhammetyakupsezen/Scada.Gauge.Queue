using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scada.Gauge.Que; 

namespace ScadaApplicationQue
{
    public partial class Form1 : Form
    {
        private List<Scada.Data.TDevice> devices;

        private TProcess Process;
        public Form1()
        {
            InitializeComponent();

            Process = new TProcess();
            Process.OnGenerateDevice += Process_OnGenerateDevice;
            Process.OnGetDeviceData += Process_OnGetDeviceData;

        }

        private void Process_OnGetDeviceData(List<Scada.Data.TData> DataList)
        {
            foreach (var item in DataList)
            {
                var Cntrl = panel2.Controls.Find("gauge_" + item.DeviceId.ToString(), true).FirstOrDefault();
                if (Cntrl != null)
                {
                    ((ScadaGauge)Cntrl).Heat = item.Heat;
                }

                //    MessageBox.Show(item.DeviceId.ToString() + " " + item.Heat.ToString());
            }

        }

        private void Process_OnGenerateDevice(List<Scada.Data.TDevice> devices)
        {
            this.devices = devices;
            GenerateScadeGauge();
            Process.StartGenerateData();
            Thread.Sleep(2000);
            Process.GetDeviceDatas();
        }


        private void GenerateScadeGauge()
        {
            foreach (var device in devices)
            {
                ScadaGauge gaugeScada = new ScadaGauge();
                gaugeScada.Dock = DockStyle.Left;
                gaugeScada.Name = "gauge_" + device.DeviceId.ToString();
                gaugeScada.AlarmActive = true;
                gaugeScada.Device = device;
                gaugeScada.MaxHeat = 70;
                gaugeScada.Parent = panel2;


            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            int Count = (int)TxtCount.Value;
            Process.CreateDevice(Count);

        }



    }
}
