using System;
using System.Collections.Generic;

namespace RNA
{
    class Program
    {
        static void Main(string[] args)
        {
            float taxaAprendizagem = 0.01f;
            long epochs = 100000;
            Network net = new Network(2, 1, 1, 20);
            
            
            for (int i = 0; i < epochs; i++)
            {
                net.backPropagation(new List<float>() { 0, 0 }, new List<float>() { 0 }, taxaAprendizagem);
                net.backPropagation(new List<float>() { 0, 1 }, new List<float>() { 1 }, taxaAprendizagem);
                net.backPropagation(new List<float>() { 1, 0 }, new List<float>() { 1 }, taxaAprendizagem);
                net.backPropagation(new List<float>() { 1, 1 }, new List<float>() { 0 }, taxaAprendizagem);
            }

            net.process(new List<float>(){0 , 0});
            Console.WriteLine("Input: 0 , 0  Desejado: 0  Resultado: " + net.getOutput()[0]);
            net.process(new List<float>(){0 , 1});
            Console.WriteLine("Input: 0 , 1  Desejado: 1  Resultado: " + net.getOutput()[0]);
            net.process(new List<float>(){1 , 0});
            Console.WriteLine("Input: 1 , 0  Desejado: 1  Resultado: " + net.getOutput()[0]);
            net.process(new List<float>(){1 , 1});
            Console.WriteLine("Input: 1 , 1  Desejado: 0  Resultado: " + net.getOutput()[0]);
            
        }
        /*
            double currentErro = 1f;
            double previousErro = 1f;
            double epsilon = 0.00000000000000000000001f;

            do{
                
                previousErro = currentErro;

                net.backPropagation(new List<float>() { 0, 0 }, new List<float>() { 0 }, taxaAprendizagem);
                net.process(new List<float>() { 0, 0 });
                currentErro += (float) Math.Pow((0 - net.getOutput()[0]),2);
                net.backPropagation(new List<float>() { 0, 1 }, new List<float>() { 1 }, taxaAprendizagem);
                net.process(new List<float>() { 0, 1 });
                currentErro += (float) Math.Pow((0 - net.getOutput()[0]),2);
                net.backPropagation(new List<float>() { 1, 0 }, new List<float>() { 1 }, taxaAprendizagem);
                net.process(new List<float>() { 1, 0 });
                currentErro += (float) Math.Pow((0 - net.getOutput()[0]),2);
                net.backPropagation(new List<float>() { 1, 1 }, new List<float>() { 0 }, taxaAprendizagem);
                net.process(new List<float>() { 1, 1 });
                currentErro += (float) Math.Pow((0 - net.getOutput()[0]),2);

                currentErro = currentErro/4;

                Console.WriteLine(currentErro - previousErro);

            }while((currentErro - previousErro) <= epsilon);
            */
        public static float Round(float x){
            if(x > 0.5f){
                return 1;
            }else{
                return 0;
            }
        }
        
        
    }
}
