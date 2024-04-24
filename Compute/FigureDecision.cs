using System;
using System.Collections.Generic;

namespace Compute
{
    public class FigureDecision
    {
        private List<Point> vertices;
        private string figureName = "Not decided yet.";

        public FigureDecision(List<Point> vertices)
        {
            this.vertices = vertices;
        }

        public void ComputeFigureDecision()
        {
            // The figures considered are only regular polygons

            int noOfVertices = vertices.Count;

            switch(noOfVertices)
            {
                case 0:
                    figureName = "Non-existent figure. Zero vertices.";
                    break;
                case 1:
                    figureName = "Single point";
                    break;
                case 2:
                    figureName = "Line";
                    break;
                case 3:
                    figureName = "Triangle ";
                    Triangle triangle = new Triangle(vertices[0], vertices[1], vertices[2]);

                    // Determine the exact triangle type
                    if (triangle.IsEquilateral())
                    {
                        figureName += "equilateral";
                    }
                    else if (triangle.IsIsosceles())
                    {
                        figureName += "isosceles";
                    }
                    else
                    {
                        figureName += "scalene";
                    }

                    if (triangle.IsRightAngled())
                    {
                        figureName += " right-angled";
                    }
                    else if (triangle.IsObtuse())
                    {
                        figureName += " obtuse";
                    }

                    break;
                case 4:
                    figureName = "Quadrilateral";
                    Quadrilateral quadrilateral = new Quadrilateral(vertices[0], vertices[1], vertices[2], vertices[3]);

                    // Determine the exact quadrilateral type
                    if (quadrilateral.IsTrapezoid())
                    {
                        figureName = "Trapezoid";
                    }
                    else if (quadrilateral.IsParallelogram())
                    {
                        figureName = "Parallelogram";
                        
                        if (quadrilateral.IsRectangle())
                        {
                            figureName = "Rectangle";

                            if (quadrilateral.IsSquare())
                            {
                                figureName = "Square";
                            }
                        }
                        else if (quadrilateral.IsRhombus())
                        {
                            figureName = "Rhombus";
                        }
                    }

                    break;
                default:
                    figureName = "Unknown figure with " + noOfVertices.ToString() + " vertices";
                    break;
            }
        }

        public string GetFigureName() { return figureName; }
    }
}
