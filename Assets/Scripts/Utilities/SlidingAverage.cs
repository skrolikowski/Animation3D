using System.Collections.Generic;
using UnityEngine;

public class SlidingAverage
{
    Queue<float> values;

    readonly int size;

    public float average
    {
        get
        {
            if (values.Count == 0)
                return 0;

            float sum = 0f;
            foreach (float value in values)
                sum += value;

            return sum / values.Count;
        }
    }

    public SlidingAverage(int size)
    {
        values = new Queue<float>(size);
    }

    public void Add(float value)
    {
        values.Enqueue(value);

        if (values.Count > size)
        {
            values.Dequeue();
        }
    }

    public void AddAbs(float value)
    {
        Add(Mathf.Abs(value));
    }

    public void Clear()
    {
        values.Clear();
    }
}
