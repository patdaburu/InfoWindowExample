using Esri.ArcGISRuntime.Mapping.Popup;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InfoWindowExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The graphics overlay we'll add to the map.  (Graphics will go here.)
        /// </summary>
        GraphicsOverlay graphicsOverlay = new GraphicsOverlay();

        /// <summary>
        /// The circle symbol for the graphics we'll create.
        /// </summary>
        SimpleMarkerSymbol circleSymbol = new SimpleMarkerSymbol(
                SimpleMarkerSymbolStyle.Circle,
                Colors.Red, 10);

        public MainWindow()
        {
            InitializeComponent();

            //// Some things we've tried...
            //graphicsOverlay.IsPopupEnabled = true;
            //graphicsOverlay.PopupDefinition = new PopupDefinition();
            //graphicsOverlay.PopupDefinition.Title = "Here's the Title!";

            // Add the graphics overlay to the map view.
            this.MapView.GraphicsOverlays.Add(graphicsOverlay);
        }

        private async void MapView_GeoViewTapped(object sender, GeoViewInputEventArgs e)
        {
            /**
             * The code that follows resembles the ESRI sample at 
             * https://github.com/Esri/arcgis-runtime-samples-dotnet/blob/quartz-beta/src/Desktop/ArcGISRuntime.Desktop.Samples/Samples/GraphicsOverlay/IdentifyGraphics/IdentifyGraphics.xaml.cs
             */
            // Let's use the tools we have to tell that a graphic has been
            // identified.
            var tolerance = 10d;
            var maximumResults = 1;

            var identifyResults = await this.MapView.IdentifyGraphicsOverlayAsync(
                this.graphicsOverlay,
                e.Position,
                tolerance,
                maximumResults); 

            // Let's see if we got any results.
            if(identifyResults.Count > 0)
            {
                // What now?  Can we open a popup?
                MessageBox.Show("OK.  You tapped the graphic.  What now?");
                MessageBox.Show("Can we now show a popup on the map through the API?");
                MessageBox.Show("Or do we have to create our own?");
            }
        }

        private void MapView_GeoViewDoubleTapped(object sender, GeoViewInputEventArgs e)
        {
            // Create the graphic.
            var circleGraphic = new Graphic(e.Location, circleSymbol);
            // Add the graphic to the graphics overlay.
            graphicsOverlay.Graphics.Add(circleGraphic);

            //// Something we've tried... 
            //Popup p = new Popup(
            //    circleGraphic, 
            //    this.graphicsOverlay.PopupDefinition);
            //// ...but now we're unsure what to do with the object.  (And we're
            //// also unsure if this is even the correct approach.)
        }
    }
}
