using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Scada.Data;

namespace Scada.Gauge.Que
{
    public partial class ScadaGauge: UserControl
    {
        public ScadaGauge()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }



        int Count = 0;

        private int FHeat;

        public int Heat
        {
            get { return GetHeat(); }
            set { SetHeat(value); }
        }

        private void SetHeat(int value)
        {
            FHeat = value;
            trackBar1.Value = FHeat;
            label1.Text = FHeat.ToString();

            if (FHeat >= FMaxHeat)
            {
                if (FAlarmActive)
                {
                    Blink();
                }
            }


        }



        private int GetHeat()
        {
            return FHeat;
        }



        private TDevice FDevice;

        public TDevice Device
        {
            get { return GetDevice(); }
            set { SetDevice(value); }
        }

        private void SetDevice(TDevice value)
        {
            FDevice = value;
            LblName.Text = FDevice.DeviceName;

        }

        private TDevice GetDevice()
        {
            return FDevice;
        }






        //private string FDeviceName;

        //public string DeviceName
        //{
        //    get { return GetDeviceName(); }
        //    set { SetDeviceName(value); }
        //}

        //private void SetDeviceName(string value)
        //{
        //    FDeviceName = value;
        //    LblName.Text = FDeviceName;

        //}

        //private string GetDeviceName()
        //{
        //    return FDeviceName;
        //}




        private int FMaxHeat;

        public int MaxHeat
        {
            get { return GetFMaxHeat(); }
            set { SetFMaxHeat(value); }
        }

        private void SetFMaxHeat(int value)
        {
            FMaxHeat = value;

        }

        private int GetFMaxHeat()
        {
            return FMaxHeat;
        }



        private bool FAlarmActive;

        public bool AlarmActive
        {
            get { return GetAlarmActive(); }
            set { SetAlarmActive(value); }
        }

        private void SetAlarmActive(bool value)
        {
            FAlarmActive = value;
        }

        private bool GetAlarmActive()
        {
            return FAlarmActive;
        }




        

        //private void timer1_Tick(object sender, EventArgs e)
        //{

        //    if (Count > 25)
        //        timer1.Enabled = false;





        //    Count++;

        //}

        private void Blink()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    if (LblName.BackColor == Color.PowderBlue)
                        LblName.BackColor = Color.Red;

                    else
                        LblName.BackColor = Color.PowderBlue;
                    Thread.Sleep(20);
                }


            });


        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
