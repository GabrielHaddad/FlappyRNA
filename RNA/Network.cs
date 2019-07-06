using System;
using System.Collections.Generic;

namespace RNA
{
    public class Network
    {
        List<Layer> layerList; //Lista de Layers
        List<float> output; //Lista de Saidas

        public Network(int nEntradas, int nSaidas, int nLayersOcultas, int nNeuroniosOculta)
        {
            //Instancia Listas
            layerList = new List<Layer>();
            output = new List<float>();

            for (int i = 0; i <= nLayersOcultas; i++)
            {
                if (i == 0)
                {
                    //Cria 1ª camada Oculta
                    layerList.Add(new Layer(nEntradas, nNeuroniosOculta));
                }
                else if (i == nLayersOcultas)
                {
                    //Cria camada de Saida
                    layerList.Add(new Layer(nNeuroniosOculta, nSaidas));
                }
                else
                {
                    //Cria as outras camadas Ocultas
                    layerList.Add(new Layer(nNeuroniosOculta, nNeuroniosOculta));
                }
            }
        }

        public void process(List<float> input)
        {
            List<float> entrada = input;
            foreach (Layer l in layerList)
            {
                l.process(entrada);
                entrada = l.getOutput();
            }

            output = entrada;
        }

        //Erro usado para atualizar a camada de Saida
        public void backPropagation(List<float> input, List<float> desejado, float taxaAprendizagem)
        {

            //Predict ou Output ou Process da rede
            this.process(input);
            List<float> actualOutput = this.getOutput();

            //Gradientes
            List<float> gradienteSaida = new List<float>();
            List<float> gradienteOculta = new List<float>();

            for (int i = (layerList.Count - 1); i >= 0; i--)
            {
                //Conta os indices dos neuronios
                int count = 0;

                if (i == (layerList.Count - 1))
                {
                    //BackPropagation na camada de Saida
                    foreach (Neuron n in layerList[i].getNeuronList())
                    {
                        //Output da Layer Anterior
                        List<float> previousLayerOutput = layerList[i - 1].getOutput();
                        //Faz a soma do Output da Layer anterior
                        float soma = n.fSoma(previousLayerOutput);
                        //Acha o gradiente de Saida
                        gradienteSaida.Add(-(desejado[count] - actualOutput[count]) * n.sigmoideDerivada(soma));
                        //Atualiza pesos Neuronio
                        n.atualizaWeight(gradienteSaida[count], previousLayerOutput, taxaAprendizagem);
                        //Atualiza o contador
                        count++;
                    }
                }
                else if (i == (layerList.Count - 2))
                {
                    //BackPropagation nas camadas Ocultas que são antes das de entrada
                    foreach (Neuron n in layerList[i].getNeuronList())
                    {
                        //Output da Layer Anterior
                        List<float> previousLayerOutput = new List<float>();
                        //Se não for a primeira camada, pega o anterior
                        if (i != 0)
                        {
                            previousLayerOutput = layerList[i - 1].getOutput();
                        }
                        else //Se for a primeira camada e não tem anterior, o anterior é o input
                        {
                            previousLayerOutput = input;
                        }
                        //Faz a soma do Output da Layer anterior
                        float soma = n.fSoma(previousLayerOutput);
                        //Inicializa o somador dos gradientes de Saida
                        float sumGradienteSaida = 0f;
                        //Soma os gradientes de Saida 
                        for (int j = 0; j < layerList[i + 1].getNeuronList().Count; j++)
                        {
                            sumGradienteSaida += gradienteSaida[j] * layerList[i + 1].getNeuronList()[j].getWeight()[count];
                        }
                        //Acha o gradiente Oculto
                        gradienteOculta.Add(n.sigmoideDerivada(soma) * sumGradienteSaida);
                        //Atualiza pesos Neuronio
                        n.atualizaWeight(gradienteOculta[count], previousLayerOutput, taxaAprendizagem);
                        //Atualiza o contador
                        count++;
                    }
                }
                else
                {
                    //BackPropagation nas Camadas Ocultas seguintes
                    foreach (Neuron n in layerList[i].getNeuronList())
                    {
                        //Output da Layer Anterior
                        List<float> previousLayerOutput = new List<float>();
                        //Se não for a primeira camada, pega o anterior
                        if (i != 0)
                        {
                            previousLayerOutput = layerList[i - 1].getOutput();
                        }
                        else //Se for a primeira camada e não tem anterior, o anterior é o input
                        {
                            previousLayerOutput = input;
                        }
                        //Faz a soma do Output da Layer anterior
                        float soma = n.fSoma(previousLayerOutput);
                        //Inicializa o somador dos gradientes da camada Oculta
                        float sumGradienteOculta = 0f;
                        //Soma os gradientes da camada oculta
                        for (int j = 0; j < layerList[i + 1].getNeuronList().Count; j++)
                        {
                            sumGradienteOculta += gradienteOculta[j] * layerList[i + 1].getNeuronList()[j].getWeight()[count];
                        }
                        //Acha o gradiente Oculto
                        gradienteOculta[count] = (n.sigmoideDerivada(soma) * sumGradienteOculta);
                        //Atualiza pesos Neuronio
                        n.atualizaWeight(gradienteOculta[count], previousLayerOutput, taxaAprendizagem);
                        //Atualiza o contador
                        count++;
                    }
                }
            }
        }

        public List<float> getOutput()
        {
            return this.output;
        }


    }
}
