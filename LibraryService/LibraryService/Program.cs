using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web;
using LibraryService.Service;

namespace LibraryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Service.LibraryService), new Uri("http://localhost:8886/libraryservice"));

            try
            {
                host.Open();
                Console.ReadLine();
                host.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                host.Abort();
            }
        }
    }
}