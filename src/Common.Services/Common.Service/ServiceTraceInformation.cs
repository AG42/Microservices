using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Diagnostics;

namespace Common.Service
{
   public class ServiceTraceInformation
    {

        //public string GetServiceInformation()
        //{
        //   StackTrace stackTrace=new StackTrace();
        //   string methodname= stackTrace.GetFrame(0).GetMethod().Name;
        //    return methodname;
        //}
        public void GetServiceInformation(string filename, string eventname,string methodInputs)
        {
            //log.write(BLL method invoked, Method Name ={0},filename={1}  )
            TextWriterTraceListener listener = new TextWriterTraceListener(filename);
            //Date time
            listener.WriteLine(DateTime.UtcNow);
            //Event Name like service ,methodname 
            listener.WriteLine(eventname);

            //input details
            listener.WriteLine(methodInputs);

            listener.Flush();
        }

    }
}
