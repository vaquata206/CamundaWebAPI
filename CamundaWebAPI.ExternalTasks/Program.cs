using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ExternalTasks
{
    public class Program
    {
        public static string GetCurrentDomain()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }
    }
}
