using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerProducer
{
    class Program
    {
       
        static string[] buffer = new string[10];
        static int totalProducto = 0;
        static Semaphore Smutex, SLleno, SVacio;

        static void Main(string[] args)
        {
            Thread TProducer = new Thread(Producer);
            Thread TConsumer = new Thread(Consumer);
            Smutex= new Semaphore(1, 1);
            SLleno = new Semaphore(0, 10);
            SVacio = new Semaphore(10,10);
            TProducer.Start();
            TConsumer.Start();
           
        }

        static private void Producer()
        {
            while (true)
            {
               
                //Crear algun producto
                string producto = "HolaMundo";
                //Checar si el buffer esta vacio  
                    
                SVacio.WaitOne();
                Smutex.WaitOne();
                PonerProducto(producto);
                Smutex.Release();
                SLleno.Release();
                
            }
        }

        private static void PonerProducto(string producto)
        {
            buffer[totalProducto] = producto;
            totalProducto++;
            Console.WriteLine("Added");
        }
        static private void Consumer()
        {
            while (true)
            {
                SLleno.WaitOne();
                Smutex.WaitOne();
                QuitarProducto();
                Smutex.Release();
                SVacio.Release();

            }
        }

        private static void QuitarProducto()
        {
            buffer[totalProducto - 1] = null;
            totalProducto--;
            Console.WriteLine("Taken Away");
        }
    }
}
