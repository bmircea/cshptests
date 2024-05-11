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

        public void ComputeFigureDecision() // line 1
        {
            // The figures considered are only regular polygons

            int noOfVertices = vertices.Count; // line 2

            switch (noOfVertices) // line 3
            {
                case 0: // line 4
                    figureName = "Non-existent figure. Zero vertices."; // line 5
                    break; // line 6
                case 1: // line 7
                    figureName = "Single point"; // line 8
                    break; // line 9
                case 2: // line 10
                    figureName = "Line"; // line 11
                    break; // line 12
                case 3: // line 13
                    figureName = "Triangle "; // line 14
                    Triangle triangle = new Triangle(vertices[0], vertices[1], vertices[2]); // line 15

                    // Determine the exact triangle type
                    if (triangle.IsEquilateral()) // line 16
                    {
                        figureName += "equilateral"; // line 17
                    }
                    else if (triangle.IsIsosceles()) // line 18
                    {
                        figureName += "isosceles"; // line 19
                    }
                    else // line 20
                    {
                        figureName += "scalene"; // line 21
                    }

                    if (triangle.IsRightAngled()) // line 22
                    {
                        figureName += " right-angled"; // line 23
                    }
                    else if (triangle.IsObtuse()) // line 24
                    {
                        figureName += " obtuse"; // line 25
                    }

                    break; // line 26
                case 4: // line 27
                    figureName = "Quadrilateral"; // line 28
                    Quadrilateral quadrilateral = new Quadrilateral(vertices[0], vertices[1], vertices[2], vertices[3]); // line 29

                    // Determine the exact quadrilateral type
                    if (quadrilateral.IsTrapezoid()) // line 30
                    {
                        figureName = "Trapezoid"; // line 31
                    }
                    else if (quadrilateral.IsParallelogram()) // line 32
                    {
                        figureName = "Parallelogram"; // line 33

                        if (quadrilateral.IsRectangle()) // line 34
                        {
                            figureName = "Rectangle"; // line 35

                            if (quadrilateral.IsSquare()) // line 36
                            {
                                figureName = "Square"; // line 37
                            }
                        }
                        else if (quadrilateral.IsRhombus()) // line 38
                        {
                            figureName = "Rhombus"; // line 39
                        }
                    }

                    break; // line 40
                default: //41
                    figureName = "Unknown figure with " + noOfVertices.ToString() + " vertices"; // line 42
                    break; // line 43
            }
        }

        public string GetFigureName() { return figureName; }
    }
}
