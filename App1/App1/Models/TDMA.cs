using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class TDMA
    {
        private int size;
        public double[] equivalentMatrix;
        public double[,] reducedMatrix;
        public TDMA(int size)
        {
            this.size = size;
            equivalentMatrix = new double[size];
            reducedMatrix = new double[size, 3];
        }

        public int Size { get { return size; } }
    }
}
