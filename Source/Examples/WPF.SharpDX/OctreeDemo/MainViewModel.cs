﻿using DemoCore;
using HelixToolkit.SharpDX.Shared.Utilities;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Media3D = System.Windows.Media.Media3D;

namespace OctreeDemo
{
    public class MainViewModel : BaseViewModel
    {
        private Vector3 light1Direction = new Vector3();
        public Vector3 Light1Direction
        {
            set
            {
                if (light1Direction != value)
                {
                    light1Direction = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return light1Direction;
            }
        }
        private FillMode fillMode = FillMode.Solid;
        public FillMode FillMode
        {
            set
            {
                fillMode = value;
                OnPropertyChanged();
            }
            get
            {
                return fillMode;
            }
        }

        private bool showWireframe = false;
        public bool ShowWireframe
        {
            set
            {
                showWireframe = value;
                OnPropertyChanged();
                if (showWireframe)
                {
                    FillMode = FillMode.Wireframe;
                }
                else
                {
                    FillMode = FillMode.Solid;
                }
            }
            get
            {
                return showWireframe;
            }
        }

        private bool visibility = true;
        public bool Visibility
        {
            set
            {
                visibility = value;
                OnPropertyChanged();
            }
            get
            {
                return visibility;
            }
        }
        public Color4 Light1Color { get; set; }

        //public MeshGeometry3D Other { get; private set; }
        public Color4 AmbientLightColor { get; set; }

        public Color LineColor { set; get; }

        public LineGeometry3D OctreeModel { set; get; }

        public Color GroupLineColor { set; get; }

        public Color HitLineColor { set; get; }

        private LineGeometry3D groupOctreeModel = null;
        public LineGeometry3D GroupOctreeModel
        {
            set
            {
                groupOctreeModel = value;
                OnPropertyChanged();
            }
            get
            {
                return groupOctreeModel;
            }
        }

        private LineGeometry3D landerGroupOctreeModel = null;
        public LineGeometry3D LanderGroupOctreeModel
        {
            set
            {
                landerGroupOctreeModel = value;
                OnPropertyChanged();
            }
            get
            {
                return landerGroupOctreeModel;
            }
        }

        private LineGeometry3D hitModel = null;
        public LineGeometry3D HitModel
        {
            set
            {
                hitModel = value;
                OnPropertyChanged();
            }
            get
            {
                return hitModel;
            }
        }
        private PhongMaterial material;
        public PhongMaterial Material
        {
            private set
            {
                SetValue<PhongMaterial>(ref material, value, nameof(Material));
            }
            get
            {
                return material;
            }
        }
        public MeshGeometry3D DefaultModel { private set; get; }
        public ObservableCollection<DataModel> Items { set; get; }
        public List<DataModel> LanderItems { private set; get; }

        private IOctree groupOctree = null;
        public IOctree GroupOctree
        {
            set
            {
                if (groupOctree == value)
                    return;
                groupOctree = value;
                OnPropertyChanged();
                if (value != null)
                {
                    GroupOctreeModel = value.CreateOctreeLineModel();
                    value.RecordHitPathBoundingBoxes = true;
                }
                else
                {
                    GroupOctreeModel = null;
                }
            }
            get
            {
                return groupOctree;
            }
        }

        private IOctree landerGroupOctree = null;
        public IOctree LanderGroupOctree
        {
            set
            {
                if (landerGroupOctree == value)
                    return;
                landerGroupOctree = value;
                OnPropertyChanged();
                if (value != null)
                {
                    LanderGroupOctreeModel = value.CreateOctreeLineModel();
                    value.RecordHitPathBoundingBoxes = true;
                }
                else
                {
                    LanderGroupOctreeModel = null;
                }
            }
            get
            {
                return landerGroupOctree;
            }
        }

        private Media3D.Vector3D camLookDir = new Media3D.Vector3D(-10, -10, -10);
        public Media3D.Vector3D CamLookDir
        {
            set
            {
                if (camLookDir != value)
                {
                    camLookDir = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return camLookDir;
            }
        }

        public bool HitThrough { set; get; }

        private readonly IList<DataModel> HighlightItems = new List<DataModel>();

        private bool useOctreeHitTest = true;
        public bool UseOctreeHitTest
        {
            set
            {
                SetValue<bool>(ref useOctreeHitTest, value, nameof(UseOctreeHitTest));
            }
            get
            {
                return useOctreeHitTest;
            }
        }

        private int sphereSize = 1;
        public int SphereSize
        {
            set
            {
                if (SetValue<int>(ref sphereSize, value, nameof(SphereSize)))
                {
                    HitModel = null;
                    if (HighlightItems.Count > 0)
                    {
                        foreach (SphereModel item in HighlightItems)
                        {
                            item.Radius = value;
                        }
                    }
                }
            }
            get
            {
                return sphereSize;
            }
        }

        public ICommand AddModelCommand { private set; get; }
        public ICommand RemoveModelCommand { private set; get; }
        public ICommand ClearModelCommand { private set; get; }
        public ICommand AutoTestCommand { private set; get; }

        public MainViewModel()
        {            // titles
            this.Title = "DynamicTexture Demo";
            this.SubTitle = "WPF & SharpDX";
            RenderTechniquesManager = new DefaultRenderTechniquesManager();
            RenderTechnique = RenderTechniquesManager.RenderTechniques[DefaultRenderTechniqueNames.Blinn];
            EffectsManager = new DefaultEffectsManager(RenderTechniquesManager);
            this.Camera = new HelixToolkit.Wpf.SharpDX.PerspectiveCamera
            {
                Position = new Media3D.Point3D(10, 10, 10),
                LookDirection = new Media3D.Vector3D(-10, -10, -10),
                UpDirection = new Media3D.Vector3D(0, 1, 0)
            };
            this.Light1Color = (Color4)Color.White;
            this.Light1Direction = new Vector3(-10, -10, -10);
            this.AmbientLightColor = new Color4(0.2f, 0.2f, 0.2f, 1.0f);
            SetupCameraBindings(this.Camera);
            this.PropertyChanged += MainViewModel_PropertyChanged;

            LineColor = Color.Blue;
            GroupLineColor = Color.Green;
            HitLineColor = Color.Red;
            Items = new ObservableCollection<DataModel>();

            var sw = Stopwatch.StartNew();
            CreateDefaultModels();
            sw.Stop();
            Console.WriteLine("Create Models total time =" + sw.ElapsedMilliseconds + " ms");

            AddModelCommand = new RelayCommand(AddModel);
            RemoveModelCommand = new RelayCommand(RemoveModel);
            ClearModelCommand = new RelayCommand(ClearModel);
            AutoTestCommand = new RelayCommand(AutoTestAddRemove);
        }

        private void CreateDefaultModels()
        {
            Material = PhongMaterials.White;
            var b2 = new MeshBuilder(true, true, true);
            b2.AddSphere(new Vector3(15f, 0f, 0f), 4, 64, 64);
            b2.AddSphere(new Vector3(25f, 0f, 0f), 2, 32, 32);
            b2.AddTube(new Vector3[] { new Vector3(10f, 5f, 0f), new Vector3(10f, 7f, 0f) }, 2, 12, false, true, true);
            DefaultModel = b2.ToMeshGeometry3D();

            DefaultModel.UpdateOctree();
            OctreeModel = DefaultModel.Octree.CreateOctreeLineModel();

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    Items.Add(new SphereModel(new Vector3(10f + i + (float)Math.Pow((float)j / 2, 2), 10f + (float)Math.Pow((float)i / 2, 2), 5f + (float)Math.Pow(j, ((float)i / 5))), 1));
                }
            }

            LanderItems = Load3ds("Car.3ds").Select(x => new DataModel() { Model = x.Geometry as MeshGeometry3D, Material = PhongMaterials.Copper }).ToList();
            foreach (var item in LanderItems)
            {
                var scale = new Vector3(0.007f);
                for (int i = 0; i < item.Model.Positions.Count; ++i)
                {
                    item.Model.Positions[i] = item.Model.Positions[i] * scale;
                }
                item.Model.UpdateOctree();
            }
        }

        public List<Object3D> Load3ds(string path)
        {
            var reader = new StudioReader();
            var list = reader.Read(path);
            return list;
        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(CamLookDir)))
            {
                Light1Direction = CamLookDir.ToVector3();
            }
        }

        public void SetupCameraBindings(Camera camera)
        {
            if (camera is ProjectionCamera)
            {
                SetBinding("CamLookDir", camera, ProjectionCamera.LookDirectionProperty, this);
            }
        }

        private static void SetBinding(string path, DependencyObject dobj, DependencyProperty property, object viewModel, BindingMode mode = BindingMode.TwoWay)
        {
            var binding = new Binding(path);
            binding.Source = viewModel;
            binding.Mode = mode;
            BindingOperations.SetBinding(dobj, property, binding);
        }

        public void OnMouseLeftButtonDownHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            foreach (var item in HighlightItems)
            {
                item.Highlight = false;
            }
            HighlightItems.Clear();
            Material = PhongMaterials.White;
            var viewport = sender as Viewport3DX;
            if (viewport == null) { return; }
            var point = e.GetPosition(viewport);
            var hitTests = viewport.FindHits(point);
            if (hitTests != null && hitTests.Count > 0)
            {
                if (HitThrough)
                {
                    foreach (var hit in hitTests)
                    {
                        if (hit.ModelHit.DataContext is DataModel)
                        {
                            var model = hit.ModelHit.DataContext as DataModel;
                            model.Highlight = true;
                            HighlightItems.Add(model);
                        }
                        else if (hit.ModelHit.DataContext == this)
                        {
                            Material = PhongMaterials.Yellow;
                        }
                    }
                }
                else
                {
                    var hit = hitTests[0];
                    if (hit.ModelHit.DataContext is DataModel)
                    {
                        var model = hit.ModelHit.DataContext as DataModel;
                        model.Highlight = true;
                        HighlightItems.Add(model);
                    }
                    else if (hit.ModelHit.DataContext == this)
                    {
                        Material = PhongMaterials.Yellow;
                    }
                }
                if (GroupOctree != null && GroupOctree.HitPathBoundingBoxes.Count > 0)
                {
                    HitModel = GroupOctree.HitPathBoundingBoxes.CreatePathLines();
                }
                if (LanderGroupOctree != null && LanderGroupOctree.HitPathBoundingBoxes.Count > 0)
                {
                    HitModel = LanderGroupOctree.HitPathBoundingBoxes.CreatePathLines();
                }
            }
            else
            {
                HitModel = null;
            }
        }

        private double theta = 0;
        private double newModelZ = -5;
        private void AddModel(object o)
        {
            var x = 10 * (float)Math.Sin(theta);
            var y = 10 * (float)Math.Cos(theta);
            theta += 0.3;
            newModelZ += 0.5;
            var z = (float)(newModelZ);
            Items.Add(new SphereModel(new Vector3(x, y + 20, z + 14), 1));
            HitModel = null;
        }

        private void RemoveModel(object o)
        {
            if (Items.Count > 0)
            {
                Items.RemoveAt(Items.Count - 1);
                newModelZ = newModelZ > -5 ? newModelZ - 0.5 : 0;
            }
            HitModel = null;
        }

        private void ClearModel(object o)
        {
            Items.Clear();
            HitModel = null;
            HighlightItems.Clear();
        }

        private DispatcherTimer timer;
        private int counter = 0;
        private bool autoTesting = false;
        public bool AutoTesting
        {
            set
            {
                if (SetValue<bool>(ref autoTesting, value, nameof(AutoTesting)))
                {
                    Enabled = !value;
                }
            }
            get
            {
                return autoTesting;
            }
        }

        private bool enabled = true;
        public bool Enabled
        {
            set
            {
                SetValue<bool>(ref enabled, value, nameof(Enabled));
            }
            get
            {
                return enabled;
            }
        }
        private Random rnd = new Random();
        private void AutoTestAddRemove(object o)
        {
            if (timer == null)
            {
                AutoTesting = true;
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(50);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            else
            {
                timer.Stop();
                timer = null;
                AutoTesting = false;
                counter = 0;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {            
            if (counter > 99)
            {
                counter = -100;
            }
            if (counter < 0)
            {
                RemoveModel(null);
            }
            else
            {
                AddModel(null);
            }
            if(counter % 2 == 0)
            {
                int k = rnd.Next(0, Items.Count - 1);
                int radius = rnd.Next(1, 5);
                (Items[k] as SphereModel).Radius = radius;
            }
            ++counter;
        }
    }
}