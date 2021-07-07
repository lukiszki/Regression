using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Network : MonoBehaviour
{
    [Header("Network Params")]
    [SerializeField]
    [Range(0.0001f,0.01f)]
    float Learning_rate = 0.01f;
    [SerializeField]
    float W;
    [SerializeField]
    float b;

    [Header("Output")]

    public float z1;

    public float Output;

    System.Random rnd = new System.Random();
    
    //[HideInInspector]
    public float[,] TrainData = new float[,]{{1,0.1f},{2,0.4f},{4,0.8f},{15,1f}};

    [SerializeField]
    bool rndTrain = true;

    [SerializeField]
    bool nonlin;
    void Start()
    {
        RandomParams();
        printCost(TrainData);
    }
    void RandomParams()
    {
        W = (float)rnd.NextDouble();
        b =1;
    }

    // Update is called once per frame
    void Update()
    {
        //Train(TrainData);
    }
    public void feedForward(float input)
    {
        if(nonlin)
        {
            z1 = input * W + b;
            Output = ReLU(z1);
        }
        else
        Output = input * W + b;
    }
    public void printCost(float[,] data)
    {
        float totalCost = 0;
        for(int i=0; i<TrainData.Length/2; i++)
        {
            feedForward(data[i,0]);
            totalCost += (float)Math.Pow((data[i,1] - Output),2);
        }
        print("Total Cost: "+totalCost.ToString());
    }
    float Cost(float expec, float pred)
    {
        return (float)Math.Pow((expec - pred),2);
    }
    public void Train(float[,] data)
    {
        if(!rndTrain)
        {
            for(int j = 0; j<20; j++)
            {
                for(int i=0; i<TrainData.Length/2; i++)
                {
                    if(!nonlin)
                    {
                        feedForward(data[i,0]);
                        float dcostReOut = -(2*(data[i,1]-Output));
                        float dOutReW = data[i,0];
                        float dOutReB = 1;
                        //print(dcostReOut);

                        W -= (dOutReW * dcostReOut) * Learning_rate;
                        b -= 1 * dcostReOut * Learning_rate;
                    }
                    else
                    {
                            feedForward(data[i,0]);
                        float dcostReOut = -(2*(data[i,1]-Output));
                        float dOutReZ1 = d_ReLU(z1);
                        float dOutReW = data[i,0];
                        float dOutReB = 1;
                        //print(dcostReOut);

                        W -= (dOutReW * dcostReOut * dOutReZ1) * Learning_rate;
                        b -= (1 * dcostReOut * dOutReZ1) * Learning_rate;
                    }
                }
            }
            printCost(TrainData);
        }
       /* else
        {
             if(!nonlin)
                    {
                int i = UnityEngine.Random.Range(0,(TrainData.Length/2));
                 feedForward(data[i,0]);
                 float dcostReOut = -(2*(data[i,1]-Output));
                 float dOutReW = data[i,0];
                 float dOutReB = 1;
                 //print(dcostReOut);

                  W -= (dOutReW * dcostReOut) * Learning_rate;
                  b -= 1 * dcostReOut * Learning_rate;
                    }
             else
                    {
                            feedForward(data[i,0]);
                        float dcostReOut = -(2*(data[i,1]-Output));
                        float dOutReZ1 = d_sigmoid(z1);
                        float dOutReW = data[i,0];
                        float dOutReB = 1;
                        //print(dcostReOut);

                        W -= (dOutReW * dcostReOut * dOutReZ1) * Learning_rate;
                        b -= (1 * dcostReOut * dOutReZ1) * Learning_rate;
                    }
            printCost(TrainData);
        }*/

    }
    float ReLU(float x)
    {
        return Mathf.Max(0.01f*x,x);
    }
    float d_ReLU(float x)
    {
        return x > 0 ? 1 :0.01f;
    }
}
