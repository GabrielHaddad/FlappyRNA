using System;
using System.Collections.Generic;

namespace RNA
{
    public class Neuron
    {
        List<float> weight; //Pesos W
        float output; //Saidas Y
        float maxRand;
        float minRand;

        //Recebe as entradas e atribui elas ao Neuronio
        public Neuron(int nPreviousLayer)
        {
            //Inicializa Random
            Random random = new Random();
            this.maxRand = 1;
            this.minRand = 0;

            //Inicializa Listas
            this.weight = new List<float>();
            this.output = 0f;

            //Inicializa com pesos aleatorios entre 0 e 1
            for (int i = 0; i < nPreviousLayer; i++)
            {
                weight.Add((float)random.NextDouble() * (this.maxRand - this.minRand) + this.minRand);
            }
        }
        //Realiza todo processo interno do neuronio
        public void process(List<float> input)
        {
            //Calcula o Somatorio
            float soma = fSoma(input);
            //Calcula a saida
            float saida = sigmoide(soma);
            //Atribui a saida
            this.output = saida;
        }
        public float fSoma(List<float> input)
        {
            float soma = 0f;

            //Calcula o somatorio
            for (int i = 0; i < weight.Count; i++)
            {
                soma += weight[i] * input[i];
                //Printa pesos dos neuronios
                //Console.WriteLine("Peso: " + i + " Valor: " + weight[i]);
            }
            return soma;
        }
        //Atualiza, diante dos paramentros, os seus pesos
        public void atualizaWeight(float gradienteSaida, List<float> input, float taxaAprendizagem)
        {
            for (int i = 0; i < weight.Count; i++)
            {
                weight[i] += -taxaAprendizagem * gradienteSaida * input[i];
            }
        }
        public List<float> getWeight()
        {
            return this.weight;
        }
        //Retorna o Output do Neuronio
        public float getOutput()
        {
            return this.output;
        }
        //Função Sigmoide
        public float sigmoide(float soma)
        {
            return (float)(1 / (1 + Math.Pow(Math.E, soma)));
        }
        //Derivada da Função Sigmoide
        public float sigmoideDerivada(float soma)
        {
            return (float)(sigmoide(soma) * (1 - sigmoide(soma)));
        }
        public float reLU(float soma)
        {
            return (float)(Math.Max(0, soma));
        }
        public float reLUDerivada(float soma)
        {
            return (float)(soma >= 0 ? 1 : 0);
        }



    }
}