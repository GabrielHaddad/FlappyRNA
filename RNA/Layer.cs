using System;
using System.Collections.Generic;

namespace RNA
{
    public class Layer
    {
        List<Neuron> neuronsList; //Lista de Neurinios
        List<float> output; //Lista de Saida dos Neuronios

        public Layer(int nPreviousLayer, int nNeuroniosLayer)
        {
            //Instancia Listas
            neuronsList = new List<Neuron>();
            output = new List<float>();

            //Cria os Neuronios
            for (int i = 0; i < nNeuroniosLayer; i++)
            {
                neuronsList.Add(new Neuron(nPreviousLayer));
            }
        }

        //Chama o process() dos Neuronios e guarda no Output
        public void process(List<float> input)
        {
            output = new List<float>();
            foreach (Neuron n in neuronsList)
            {
                n.process(input);
                output.Add(n.getOutput());
            }
        }
        public List<float> getOutput()
        {
            return this.output;
        }
        public List<Neuron> getNeuronList()
        {
            return neuronsList;
        }

    }
}
