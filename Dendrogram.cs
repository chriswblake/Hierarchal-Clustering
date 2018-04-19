using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HierarchalClustering
{
    public partial class Dendrogram : UserControl
    {
        //Fields
        int verticalSpacing = 50;
        int horizontalSpacing = 50;
        private List<OrganizedImage> oImages = new List<OrganizedImage>();
        private NamedImagesCollection nImages;
        private DataPointCollection points;
        private AnnotationCollection annotations;
        
        //Constructor
        public Dendrogram()
        {
            InitializeComponent();
            points = chartHierarchy.Series[0].Points;
            annotations = chartHierarchy.Annotations;
            nImages = chartHierarchy.Images;

            //Remove axis lines
            ChartArea chArea = chartHierarchy.ChartAreas[0];
            chArea.AxisX.Enabled = AxisEnabled.False;
            chArea.AxisY.Enabled = AxisEnabled.False;
        }

        //Methods
        public void addImage(string name, Image image, List<string> children)
        {
            //Check if name is already present. If alread present, do not add it again.
            if (oImages.FindIndex(p => p.Name == name) > -1)
                return;

            //Determine vertical and horizontal position
            int level = 1 * verticalSpacing;
            int position = 1 *horizontalSpacing;
            if (children != null && children.Count > 0)
            {
                //Find average position of children
                int levelSum = 0;
                int positionSum = 0;           
                foreach(string child in children)
                {
                    OrganizedImage oImage = oImages.Find(p => p.Name == child);
                    levelSum += oImage.level;
                    positionSum += oImage.position;
                    
                }

                //Computer position of this parent above the children
                level = levelSum / children.Count + 1 *verticalSpacing;
                position = positionSum / children.Count;
            }
            else
            {
                //Level is 1
                level = 1 *verticalSpacing;

                //Position right of previous item
                position = oImages.FindAll(p => p.level == level).Count * verticalSpacing;
            }

            //Create image
            OrganizedImage img = new OrganizedImage(name, image, level, position);
            nImages.Add(img);
            oImages.Add(img);

            //Create point - parent
            DataPoint dpLabelLabel = new DataPoint(img.position, img.level) { Name = name, Label=name };
            DataPoint dpParent = new DataPoint(img.position, img.level) { Name = name };
            points.Add(dpParent);
            points.Add(dpLabelLabel);
            points.Add(new DataPoint() { IsEmpty = true });

            //Create image annotation of parent      
            ImageAnnotation imgAn = new ImageAnnotation()
            {
                Name = name,
                Image = name,
                AnchorDataPoint = dpParent
            };
            annotations.Add(imgAn);

            //Create points - children
            foreach (string child in children)
            {
                OrganizedImage childOImage = oImages.Find(p => p.Name == child);
                DataPoint dpChild = new DataPoint(childOImage.position, childOImage.level) { Name = childOImage.Name, };

                //Draw Line connecting them
                points.Add(dpParent);
                points.Add(dpChild);
                points.Add(new DataPoint() { IsEmpty = true }); //prevents lines from connecting

            }            
        }
        public void clear()
        {
            oImages.Clear();
            nImages.Clear();
            points.Clear();
            annotations.Clear();
        }

        private class OrganizedImage : NamedImage
        {
            //Fields
            public int level;
            public int position;
            public List<OrganizedImage> connectedTo = new List<OrganizedImage>();

            //Constructor
            public OrganizedImage(string name, Image image, int level, int position):base(name, image)
            {
                this.level = level;
                this.position = position;
            }
        }
    }
}
