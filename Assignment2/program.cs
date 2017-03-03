using Assignment2;
using ChatLibrary;
using LogLibrary;
//using Logging;
using Microsoft.Practices.Unity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


static class Program
    {
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        //step 1
        //Application.Run(new Form1(new Client(new TextFileLogger())));

        //UNITY CONTAINER
        //step 2  
        UnityContainer container = new UnityContainer();
        container.RegisterType<ILoggingService, TextFileLogger>();
        //container.RegisterType<ILoggingService, NLog>();
        //container.RegisterType<ILoggingService, Logging.NLog>();//step 3
        Application.Run(container.Resolve<Form1>());

        //NINJECT
        //IKernel kernel = new StandardKernel();
        //kernel.Bind<ILoggingService>().To<TextFileLogger>();
        //kernel.Bind<ILoggingService>().To<NLog>();
        //Application.Run(kernel.Get<Form1>());

    }
}
