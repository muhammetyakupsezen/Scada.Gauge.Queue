using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scada.Data.DataList
{
    public static class Class1
    {
      //  public static List<TData> DataList = new List<TData>();
        public static Queue<Dictionary<int, TData>> DataList = new Queue<Dictionary<int, TData>>();
        public static ConcurrentDictionary<int, List<TData>> Datalarim = new ConcurrentDictionary<int, List<TData>>();


        //public static void AddData(TData Data)
        //{

        //    DataList.Add(Data);

        //}


        public static void AddHeatData(int DeviceId, TData Data)
        {
            Dictionary<int, TData> TmpData = new Dictionary<int, TData>();
            TmpData.Add(DeviceId, Data);
            DataList.Enqueue(TmpData);
        }

        public static List<TData> GetHeatDatas(int DeviceId)
        {
            List<TData> TmpData = null;


            if (DataList.Count > 0)
            {
                var g = DataList.Dequeue();
                if (g != null)
                {
                    TmpData = (from d in g.Values where d.DeviceId == DeviceId select d).ToList();

                }

            }


            return TmpData;
        }


        //public static void AddDatam(int DeviceId, TData Data)
        //{

        //    List<TData> datas = new List<TData>() { Data };
        //    Datalarim.TryAdd(DeviceId, datas);

        //}

        //public static void GetData(int DeviceId)
        //{

        //    List<TData> datas = new List<TData>();
        //    Datalarim.TryGetValue(DeviceId, out datas);

        //}




    }

}

