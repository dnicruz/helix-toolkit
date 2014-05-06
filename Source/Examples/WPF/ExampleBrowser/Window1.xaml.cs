// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Window1.xaml.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ExampleBrowser
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Window1"/> class.
        /// </summary>
        public Window1()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Examples = new List<Example>();
            this.Examples.Add(new Example(typeof(AnaglyphDemo.MainWindow), null, "Showing a stereo view using the AnaglyphView3D control."));
            this.Examples.Add(new Example(typeof(AudioDemo.MainWindow), null, "Showing a spectrogram from NAudio using Transform3Ds."));
            this.Examples.Add(new Example(typeof(BackgroundUpdateDemo.MainWindow), null, "Updates the visual model in a background thread."));
            this.Examples.Add(new Example(typeof(BillboardDemo.MainWindow), null, "Showing billboards using the BillboardVisual3D."));
            this.Examples.Add(new Example(typeof(CameraControlDemo.MainWindow), null, "Shows the camera options in the HelixViewport3D."));
            this.Examples.Add(new Example(typeof(ChamferDemo.MainWindow), null, "Chamfers the corner of a cube by the MeshBuilder.ChamferCorner method."));
            this.Examples.Add(new Example(typeof(ClothDemo.MainWindow), null, "Simulates cloth physics."));
            this.Examples.Add(new Example(typeof(ColorAxisDemo.MainWindow), null, "Displays color axes (defined by gradient brushes) over the 3D view."));
            this.Examples.Add(new Example(typeof(ContourDemo.MainWindow), null, "Calculates the contours of an imported model by the MeshGeometryHelper."));
            this.Examples.Add(new Example(typeof(CuttingPlanesDemo.MainWindow), null, "Applies cutting planes to a model."));
            this.Examples.Add(new Example(typeof(DnaDemo.MainWindow), null, "Shows a double helix, the first exmaple of this library :)"));
            this.Examples.Add(new Example(typeof(EarthDemo.MainWindow), null, "Shows the earth with textures."));
            this.Examples.Add(new Example(typeof(EarthCuttingPlanesDemo.MainWindow), null, "Applies cutting planes to the Earth."));
            this.Examples.Add(new Example(typeof(ExportDemo.MainWindow), null, "Exports a model to Kerkythea or Octane (.obj)."));
            this.Examples.Add(new Example(typeof(FlightsDemo.MainWindow), null, "Shows great circles and calculates distances between airports."));
            this.Examples.Add(new Example(typeof(FractalDemo.MainWindow), null, "Performance test on self-similar geometries."));
            this.Examples.Add(new Example(typeof(HalfEdgeMeshDemo.MainWindow), null, "Example on a half-edge mesh geometry."));
            this.Examples.Add(new Example(typeof(LegoDemo.MainWindow), null, "Lego models."));
            this.Examples.Add(new Example(typeof(LightsDemo.MainWindow), null, "Light models."));
            this.Examples.Add(new Example(typeof(LorenzAttractorDemo.MainWindow), null, "Uses the MeshBuilder to create a tube, spheres and arrows."));
            this.Examples.Add(new Example(typeof(ManipulatorDemo.MainWindow), null, "Adding manipulators to the model."));
            this.Examples.Add(new Example(typeof(MaterialDemo.MainWindow), null, "Demonstrates material properties."));
            this.Examples.Add(new Example(typeof(MazeDemo.MainWindow), null, "Generates a simple maze. Using 'WalkAround' camera mode."));
            this.Examples.Add(new Example(typeof(MeshVisualsDemo.MainWindow), null, "Demonstrates the mesh based visual elements."));
            this.Examples.Add(new Example(typeof(OverlayDemo.MainWindow), null, "Overlays 2D text and geometry on the 3D model."));
            this.Examples.Add(new Example(typeof(PenroseTriangleDemo.MainWindow), null, "Shows a Penrose triangle in 3D."));
            this.Examples.Add(new Example(typeof(PointsAndLinesDemo.MainWindow), null, "Renders text and lines."));
            this.Examples.Add(new Example(typeof(PolyhedronDemo.MainWindow), null, "Polyhedra models."));
            this.Examples.Add(new Example(typeof(PyramidDemo.MainWindow), null, "Performance test showing a pyramid."));
            this.Examples.Add(new Example(typeof(RubikDemo.MainWindow), null, "Rubik's cube visualized."));
            this.Examples.Add(new Example(typeof(StereoDemo.MainWindow), null, "Stereo view"));
            this.Examples.Add(new Example(typeof(StreamlinesDemo.MainWindow), null, "Showing streamlines around a cylinder. The tubes are generated by the MeshBuilder.AddTube."));
            this.Examples.Add(new Example(typeof(SubdivisionDemo.MainWindow), null, "Surface subdivision by Loop's algorithm."));
            this.Examples.Add(new Example(typeof(SurfacePlotDemo.MainWindow), null, "Plotting a surface in 3D."));
            this.Examples.Add(new Example(typeof(SurfacePlotCuttingPlanesDemo.MainWindow), null, "Applies cutting planes to a surface that utilises texture coordinates."));
            this.Examples.Add(new Example(typeof(TerrainDemo.MainWindow), null, "Rendering a terrain loaded from a .bt file."));
            this.Examples.Add(new Example(typeof(TextDemo.MainWindow), null, "TextVisual3D and TextBillboardVisual3D."));
            this.Examples.Add(new Example(typeof(TransparencyDemo.MainWindow), null, "Uses 'depth sorting' to show a transparent model. The sorting frequency is reduced to show what is going on."));
            this.Examples.Add(new Example(typeof(TubeDemo.MainWindow), null, "Shows Borromean rings using the TubeVisual3D."));
            this.Examples.Add(new Example(typeof(UIElementDemo.MainWindow), null, "Test of UIElement3D in HelixViewport3D."));
            this.Examples.Add(new Example(typeof(UpDirectionDemo.MainWindow), null, "Test of y-up in HelixViewport3D."));
            this.Examples.Add(new Example(typeof(ViewportFeaturesDemo.MainWindow), null, "Demonstrates features of the HelixViewport3D."));
            this.Examples.Add(new Example(typeof(ViewMatrixDemo.MainWindow), null, "Visualization of view and projection matrices (under construction)."));
            this.Examples.Add(new Example(typeof(VoxelDemo.MainWindow), null, "Edit a voxel scene by clicking the sides of the voxels."));
            this.Examples.Add(new Example(typeof(WiiDemo.MainWindow), null, "Change the transformation of a model by the Wii remote."));
            this.Examples.Add(new Example(typeof(WindDemo.MainWindow), null, "Head tracking by the Wii remote."));
            this.Examples.Add(new Example(typeof(DataTemplateDemo.MainWindow), null, "Creating Visual3Ds by a template."));
        }

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <value>The examples.</value>
        public IList<Example> Examples { get; private set; }

        /// <summary>
        /// Handles the MouseDoubleClick event of the ListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ListBoxMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var lb = (ListBox)sender;
            var example = lb.SelectedItem as Example;
            if (example != null)
            {
                var window = example.Create();
                window.Show();
            }
        }
    }
}