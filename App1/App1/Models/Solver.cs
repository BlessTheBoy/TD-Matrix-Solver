using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public static class Solver
    {
        public static double[] MatrixSolver(TDMA matrix)
        {
            matrix.reducedMatrix[0, 0] = 0.0;
            matrix.reducedMatrix[matrix.Size - 1, 2] = 0.0;

            matrix.reducedMatrix[0, 2] = matrix.reducedMatrix[0, 2] / matrix.reducedMatrix[0, 1];
            matrix.equivalentMatrix[0] = matrix.equivalentMatrix[0] / matrix.reducedMatrix[0, 1];
            matrix.reducedMatrix[0, 1] = 1.0;

            for (int y = 1; y < matrix.Size; y++)
            {
                matrix.reducedMatrix[y, 1] = matrix.reducedMatrix[y, 1] - (matrix.reducedMatrix[y, 0] * matrix.reducedMatrix[y - 1, 2]);
                matrix.equivalentMatrix[y] = matrix.equivalentMatrix[y] - (matrix.reducedMatrix[y, 0] * matrix.equivalentMatrix[y - 1]);
                matrix.equivalentMatrix[y] = matrix.equivalentMatrix[y] / matrix.reducedMatrix[y, 1];
                matrix.reducedMatrix[y, 2] = matrix.reducedMatrix[y, 2] / matrix.reducedMatrix[y, 1];
                matrix.reducedMatrix[y, 0] = 0.0;
                matrix.reducedMatrix[y, 1] = 1.0;
            }


            double[] result = new double[matrix.Size];
            result[matrix.Size - 1] = matrix.equivalentMatrix[matrix.Size - 1];
            for (int y = matrix.Size - 2; y >= 0; y--)
            {
                result[y] = matrix.equivalentMatrix[y] - (result[y + 1] * matrix.reducedMatrix[y, 2]);
            }
            return result;
        }
    }
}
