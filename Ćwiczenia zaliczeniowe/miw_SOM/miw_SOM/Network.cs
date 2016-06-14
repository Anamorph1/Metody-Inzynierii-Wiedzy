using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miw_SOM
{
    class Network
    {
        Node[,] nodes;
        int size;

        public Node this[int x, int y]
        {
            get
            {
                return nodes[x, y];
            }
        }
        
        public int Size
        {
            get
            {
                return size;
            }
        }

        public Network(int size)
        {
            nodes = new Node[size,size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    nodes[i, j] = new Node();
                }
            }
            this.size = size;
        }

        public Network(int size, List<Iris> li)
        {
            Random rand = new Random();
            nodes = new Node[size, size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    int index = rand.Next(0, li.Count);
                    nodes[i, j] = new Node(li[index].Parameters);
                    li.Remove(li[index]);
                }
            }
            this.size = size;
        }

        public Point GetCoordinatesOfMinimumDistance()
        {
            Point point = new Point();
            double minimumDistance = nodes[0, 0].CurrentDistance;
            
            for(int i=0;i<size;++i)
            {
                for(int j=0;j<size;++j)
                {
                    if (nodes[i, j].CurrentDistance < minimumDistance)
                    {
                        minimumDistance = nodes[i, j].CurrentDistance;
                        point.SetPoint(i, j);
                    }            
                }
            }
            return point;
        }

        public void SetDistance(Iris iris)
        {
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    nodes[i, j].SetDistance(iris);
                }
            }
        }

        public void InsertIris(Iris iris)
        {
            SetDistance(iris);
            Point point = GetCoordinatesOfMinimumDistance();
            /*while(nodes[point.X,point.Y].Objects.Count == 6)
            {
                nodes[point.X, point.Y].CurrentDistance = 100;
                point = GetCoordinatesOfMinimumDistance();
            }*/
            nodes[point.X, point.Y].AddElement(iris);
        }
    }
}
