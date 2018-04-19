using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HierarchalClustering
{
    public partial class Form1 : Form
    {
        public SymbolHierarchalClusterer symClust = new SymbolHierarchalClusterer();

        public Form1()
        {
            //Enable updating form from other threads.
            CheckForIllegalCrossThreadCalls = false; 

            InitializeComponent();
        }

        //Controls
        private void btnAddImage_Click(object sender, EventArgs e)
        {
            //Get values from canvas
            int[,] img = canvasInput.GetDigitMatrix();

            //Check if something was drawn. If nothing drawn, then exit early.
            if (canvasInput.GetDigit().Sum() == 0)
                return;

            //Clear the canvas
            canvasInput.Clear();

            //Add to categorizer sytem
            var symImage = symClust.addItem(img);

            //Show count
            tbImageCount.Text = ""+symClust.NumberSymImages;

            //Display on form
            pboxOutput.Image = symImage.ImageBMP;
            //chartDendo.addImage(symImage.ID.ToString(), symImage.ImageBMP, null);
            tbResults.Text = symClust.DistanceResultsToString();

            //Add clusters to dendrogram
            chartDendo.clear();
            displayClusters(symClust.clusterHierarchy);


        }
        private void displayClusters(List<SymbolHierarchalClusterer.SymImage> clusters)
        {
            //Add clusters to dendrogram
            foreach (SymbolHierarchalClusterer.SymImage c in clusters)
            {
                //Display children first
                displayClusters(c.members);

                //Get IDs of children
                List<string> clusterChildren = new List<string>();
                foreach (var m in c.members)
                    clusterChildren.Add(m.ID.ToString());

                //display parent
                chartDendo.addImage(c.ID.ToString(), c.ImageBMP, clusterChildren);
            }
        }
        private void trbarPenSize_Scroll(object sender, EventArgs e)
        {
            canvasInput.PenSize = trbarPenSize.Value;
        }
        private void btnClearCanvas_Click(object sender, EventArgs e)
        {
            canvasInput.Clear();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            symClust = new SymbolHierarchalClusterer();
            tbImageCount.Text = "0";
            chartDendo.clear();
        }
    }

    public class SymbolHierarchalClusterer
    {
        //Constants
        private const int matrixSize = 32;

        //Fields
        int idCounter = 0;
        private List<SymImage> symImages = new List<SymImage>();
        private List<SymImageDistance> symDistances = new List<SymImageDistance>();
        public List<SymImage> clusterHierarchy = new List<SymImage>();

        //Methods - Analysis
        private int generateID()
        {
            return idCounter++;
        }
        public SymImage addItem(int[,] imageMatrix)
        {
            //Check if image matrix size is correct
            if (imageMatrix.GetLength(0) != matrixSize || imageMatrix.GetLength(0) != matrixSize)
                throw new ArgumentException("The image matrix must be " + matrixSize + "x" + matrixSize + ".");

            //Create a new SymImage
            SymImage symImage = new SymImage(imageMatrix, generateID());

            //Calculate distance to all other items in the list
            foreach (SymImage sImg in symImages)
                symDistances.Add(new SymImageDistance(symImage, sImg));
            
            //Add item to symImages list. It is added after distance calculations are done, so it is note compared to itself.
            symImages.Add(symImage);

            //Perform hierarchy analysis
            createHierarchy();

            //Return the new SymImage
            return symImage;
        }
        public void removeItem(SymImage symImage)
        {
            //Remove from symDistances list
            symDistances.RemoveAll(p => p.symImageA == symImage || p.symImageB == symImage);

            //Remove from symImages list
            symImages.Remove(symImage);
        }
        public void reset()
        {
            symImages.Clear();
            symDistances.Clear();
        }
        public void createHierarchy()
        {
            //Create copy of original data
            List<SymImage> clusters = symImages.ToList();
            List<SymImageDistance> clusterDistances = symDistances.ToList();

            //Merge all pairs and until list is empty
            int AutoStop = 10;
            while (clusterDistances.Count > 0)
            {
                AutoStop--;
                if (AutoStop == 0)
                    AutoStop = 0;

                //Sort the distance list
                clusterDistances = clusterDistances.OrderBy(p => p.Distance).ToList();

                //Get first distance entry, since it is the nearest pair.
                SymImageDistance distEntry = clusterDistances[0];

                //Remove all items referencing these two items.
                clusters.Remove(distEntry.symImageA);
                clusters.Remove(distEntry.symImageB);
                clusterDistances.RemoveAll(p => p.symImageA == distEntry.symImageA || p.symImageA == distEntry.symImageB);
                clusterDistances.RemoveAll(p => p.symImageB == distEntry.symImageA || p.symImageB == distEntry.symImageB);

                //Create a new cluster from the distEntry items
                SymImage newCluster = new SymImage(new List<SymImage>() {distEntry.symImageA, distEntry.symImageB }, generateID());


                //Calculate distance to all existing clusters
                foreach (SymImage cluster in clusters)
                    clusterDistances.Add(new SymImageDistance(newCluster, cluster));


                //Add new cluster to list
                clusters.Add(newCluster);             
            }

            //Save results
            clusterHierarchy = clusters.ToList();

        }

        //Methods - Display Help
        public string DistanceResultsToString()
        {
            string s = "";

            foreach(SymImage img in symImages)
            {
                //Add Header
                s += "Image: " + img.ID + Environment.NewLine;

                //Get distances
                List<SymImageDistance> distances = symDistances
                                                        .FindAll(p => p.symImageA == img || p.symImageB == img) //Get related items
                                                        .OrderBy(p=> p.Distance).ToList(); //Sort by smallest distance

                //Show each distance
                foreach(SymImageDistance d in distances)
                {
                    s += d.ToString() + Environment.NewLine;
                }

                s += Environment.NewLine;          
            }   

            return s;
        }

        //Properties
        public int NumberSymImages
        {
            get { return symImages.Count; }
        }

        //Classes
        public class SymImage
        {
            //Fields
            public List<SymImage> members = new List<SymImage>();
            public int[,] imageMatrix = new int[matrixSize, matrixSize];
            private int id;
            private int numBlackPixels;
            private Point centroid;
            private Bitmap bitmap;

            //Constructor
            public SymImage(List<SymImage> subClusters, int id)
            {
                //Save info
                this.id = id;
                this.imageMatrix = new int[matrixSize, matrixSize];
                this.members = subClusters.ToList();

                //Combine members and submembers to create a centroid
                foreach (SymImage m in getMembersAndChildren())
                {
                    for (int x = 0; x < matrixSize; x++)
                        for (int y = 0; y < matrixSize; y++)
                            this.imageMatrix[x, y] += m.imageMatrix[x, y];
                }
                
                //Count black pixels
                countBlackPixels();

                //Center image
                centerImage();

                //Calculate centroid
                calculateCentroid();

                //Create bitmap
                createBitmapGreyScale();
            }
            public SymImage(int[,] imageMatrix, int id)
            {
                //Save info
                this.imageMatrix = imageMatrix;
                this.id = id;

                //Count black pixels
                countBlackPixels();

                //Center image
                centerImage();

                //Calculate centroid
                calculateCentroid();

                //Create bitmap
                createBitmap();
            }

            //Properties
            public int[,] ImageMatrix
            {
                get { return imageMatrix; }
            }
            public int ID
            {
                get { return id; }
            }
            public Point Centroid
            {
                get { return centroid; }
            }
            public Bitmap ImageBMP
            {
                get { return bitmap; }
            }
            
            //Methods - Private
            private void countBlackPixels()
            {
                for (int aX = 0; aX < matrixSize; aX++)
                {
                    for (int aY = 0; aY < matrixSize; aY++)
                    {
                        if (imageMatrix[aX, aY] > 0)
                            numBlackPixels++;
                    }
                }
            }
            private void centerImage()
            {
                bool done = false;

                //Find white space on left
                int left = 0;
                done = false;
                for (int x = 0; x<matrixSize; x++)
                { 
                    for (int y=0; y<matrixSize;y++)
                    { 
                        if(imageMatrix[x,y] > 0)
                        {
                            left = x;
                            done = true;
                            break;
                        }
                    }
                    if (done) break;
                }

                //Find white space on right
                int right = 0;
                done = false;
                for (int x = matrixSize-1; x>=0 ; x--)
                {
                    for (int y = 0; y < matrixSize; y++)
                    {
                        if (imageMatrix[x, y] > 0)
                        {
                            right = matrixSize-1-x;
                            done = true;
                            break;
                        }
                    }
                    if (done) break;
                }

                //Find white space on top
                int top = 0;
                done = false;
                for (int y = 0; y < matrixSize; y++)
                {
                    for (int x = 0; x < matrixSize; x++)
                    {
                        if (imageMatrix[x, y] > 0)
                        {
                            top = y;
                            done = true;
                            break;
                        }
                    }
                    if (done) break;
                }

                //Find white space on bottom
                int bottom = 0;
                done = false;
                for (int y = matrixSize-1; y >=0; y--)
                {
                    for (int x = 0; x < matrixSize; x++)
                    {
                        if (imageMatrix[x, y] > 0)
                        {
                            bottom = matrixSize-1 - y;
                            done = true;
                            break;
                        }
                    }
                    if (done) break;
                }

                //Calculate dx
                int middleX = (right + left) / 2;
                int dx = middleX - left;

                //Calculate dy
                int middleY = (top + bottom) / 2;
                int dy = middleY - top;

                //Shift matrix
                shiftMatrix(dx, dy);
            }
            private void shiftMatrix(int dx, int dy)
            {
                //Create a new matrix
                int[,] newMatrix = new int[matrixSize, matrixSize];

                //Transfer points to new matrix from original
                for (int y = 0; y < matrixSize; y++)
                {
                    for (int x = 0; x < matrixSize; x++)
                    {
                        int xNew = (x + dx + matrixSize) % matrixSize;
                        int yNew = (y + dy + matrixSize) % matrixSize;
                        newMatrix[xNew, yNew] = imageMatrix[x, y];
                    }
                }

                //Overwrite matrix
                imageMatrix = newMatrix;

            }
            private void calculateCentroid()
            {
                int area = 0;
                int momentX = 0;
                int momentY = 0;
                for(int x=0; x<matrixSize; x++)
                {
                    for (int y = 0; y < matrixSize; y++)
                    {
                        momentX += imageMatrix[x, y] * x;
                        momentY += imageMatrix[x, y] * y;
                        area += imageMatrix[x, y];
                    }
                }

                //Calculate centroid
                if (area != 0)
                centroid = new Point(momentX / area, momentY / area);
                
            }
            private void createBitmap()
            {
                //Convert to bitmap
                Bitmap theImage = new Bitmap(matrixSize, matrixSize);
                for (int x = 0; x < matrixSize; x++)
                    for (int y = 0; y < matrixSize; y++)
                    {
                        //Set the pixel
                        if (ImageMatrix[x, y] >= 1)
                            theImage.SetPixel(x, y, Color.Black);
                    }

                //Save
                bitmap = theImage;
            }
            private void createBitmapGreyScale()
            {
                //Get maximum
                int maxValue = 0;
                for (int x = 0; x < matrixSize; x++)
                    for (int y = 0; y < matrixSize; y++)
                    {
                        if (imageMatrix[x, y] > maxValue)
                            maxValue = imageMatrix[x, y];
                    }

                //Convert to grayscale bitmap
                Bitmap theImage = new Bitmap(matrixSize, matrixSize);
                for (int x = 0; x < matrixSize; x++)
                    for (int y = 0; y < matrixSize; y++)
                    {
                        //Skip pixels that are zero
                        if (imageMatrix[x, y] == 0)
                            continue;

                        //Choose grayness level
                        double percentGrayness = 1 - (imageMatrix[x, y] / (double)maxValue);
                        int level = Convert.ToInt32(percentGrayness * 255);
                        Color pixelColor = Color.FromArgb(level, level, level);

                        //Set the pixel
                        theImage.SetPixel(x, y, pixelColor);
                    }

                bitmap = theImage;
            }
            private List<SymImage> getMembersAndChildren()
            {
                //Get list of member in this group
                List<SymImage> memList = members.ToList();

                //Add their children to the list
                foreach(SymImage parent in members)
                    memList.AddRange(parent.getMembersAndChildren());
                
                //Return the total list of parents and children
                return memList;
            }

            //Methods - Public
            public int distanceFrom(SymImage symImageB)
            {
                //Result variable
                int distance = 0;

                //Cycle through every X pixel of image A
                for (int aX = 0; aX < matrixSize; aX++)
                {
                    //Cycle through every Y pixel of image A
                    for (int aY = 0; aY < matrixSize; aY++)
                    {
                        //Check if pixel A is white. Skip these
                        if (this.ImageMatrix[aX, aY] == 0)
                            continue;

                        //Cycle through every X pixel of image B
                        for (int bX = 0; bX < matrixSize; bX++)
                        {
                            //Cycle through every Y pixel of image B
                            for (int bY = 0; bY < matrixSize; bY++)
                            {
                                //Check if pixel B is white. Skip these
                                if (symImageB.ImageMatrix[bX, bY] == 0)
                                    continue;

                                //Calculate difference between coordinates
                                distance += Convert.ToInt32(Math.Sqrt(Math.Pow(aX - bX, 2) + Math.Pow(aY - bY, 2)));
                            }
                        }
                    }
                }

                //Normalized the distance by dividing by number of black pixels
                distance = distance / ((this.numBlackPixels + symImageB.numBlackPixels) / 2);

                return distance;
            }

            //Debug
            public override string ToString()
            {
                return "ID: " + ID.ToString();
            }
        }
        private class SymImageDistance
        {
            //Fields   
            private SymImage a;
            private SymImage b;
            private int distance;

            //Constructors
            public SymImageDistance(SymImage symImage1, SymImage symImage2)
            {
                //Compare IDs and put lower ID in position A
                if (symImage1.ID < symImage2.ID)
                {
                    a = symImage1;
                    b = symImage2;
                }
                else
                {
                    b = symImage1;
                    a = symImage2;
                }

                //Calculate distance and save
                distance = a.distanceFrom(b);
            }

            //Properties
            public SymImage symImageA
            {
                get { return a; }
            }
            public SymImage symImageB
            {
                get { return b; }
            }
            public int Distance
            {
                get { return distance; }
            }

            //Debug
            public override string ToString()
            {
                return "IDs:" + symImageA.ID + "," + symImageB.ID + " Dist:" + Distance;
            }
        }
    }
}
