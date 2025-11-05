using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIndex : MonoBehaviour
{
    private int[] uniqueIndices;
    private int currentIndex = 0;

    void Awake()
    {
        int arraySize = 7; // Ganti dengan ukuran array yang diinginkan
        InitializeUniqueIndices(arraySize);
    }

    void InitializeUniqueIndices(int size)
    {
        uniqueIndices = new int[size];

        for (int i = 0; i < size; i++)
        {
            uniqueIndices[i] = i;
        }

        // Acak indeks menggunakan Fisher-Yates algorithm
        System.Random random = new System.Random();

        for (int i = size - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);

            // Tukar nilai
            int temp = uniqueIndices[i];
            uniqueIndices[i] = uniqueIndices[j];
            uniqueIndices[j] = temp;
        }
    }

    public int GetNextUniqueIndex()
    {
        if (currentIndex < uniqueIndices.Length)
        {
            return uniqueIndices[currentIndex++];
        }
        else
        {
            
            currentIndex = 0;
            return GetNextUniqueIndex();
        }
    }
}
