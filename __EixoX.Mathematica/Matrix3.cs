using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class Matrix3 : IMatrix<double>
    {
        public double a1, a2, a3, b1, b2, b3, c1, c2, c3;

        public int Rows
        {
            get { return 3; }
        }

        public int Cols
        {
            get { return 3; }
        }

        public double this[int i, int j]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        switch (j)
                        {
                            case 0: return a1;
                            case 1: return a2;
                            case 2: return a3;
                            default:
                                throw new ArgumentOutOfRangeException("j");
                        }
                    case 1:
                        switch (j)
                        {
                            case 0: return b1;
                            case 1: return b2;
                            case 2: return b3;
                            default:
                                throw new ArgumentOutOfRangeException("j");
                        }
                    case 2:
                        switch (j)
                        {
                            case 0: return c1;
                            case 1: return c2;
                            case 2: return c3;
                            default:
                                throw new ArgumentOutOfRangeException("j");
                        }
                    default:
                        throw new ArgumentOutOfRangeException("i");
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        switch (j)
                        {
                            case 0: a1 = value; break;
                            case 1: a2 = value; break;
                            case 2: a3 = value; break;
                            default:
                                throw new ArgumentOutOfRangeException("j");
                        } break;
                    case 1:
                        switch (j)
                        {
                            case 0: b1 = value; break;
                            case 1: b2 = value; break;
                            case 2: b3 = value; break;
                            default:
                                throw new ArgumentOutOfRangeException("j");
                        } break;
                    case 2:
                        switch (j)
                        {
                            case 0: c1 = value; break;
                            case 1: c2 = value; break;
                            case 2: c3 = value; break;
                            default:
                                throw new ArgumentOutOfRangeException("j");
                        } break;
                    default:
                        throw new ArgumentOutOfRangeException("i");
                }
            }
        }
    }
}
