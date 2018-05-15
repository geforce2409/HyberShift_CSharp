using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.Interface;
using HyberShift_CSharp.Model.List;
using HyberShift_CSharp.Utilities;
using HyberShift_CSharp.View;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace HyberShift_CSharp.ViewModel
{
    public class BoardViewModel: BaseViewModel
    {
        private Socket socket;
        private ListPointModel listPointModel;
        private DrawingModel drawingModel;

        //getter and setter
        public DelegateCommand<object> MouseDownCommand { get; set; }
        public DelegateCommand<object> MouseMoveCommand { get; set; }
        public DelegateCommand<object> MouseUpCommand { get; set; }

        public int Thickness { get; set; }

        public Color SelectedColor { get; set; }

        public SolidColorBrush BrushColor
        {
            get { return new SolidColorBrush(SelectedColor); }
        }

        public Double CurrentX
        {
            get { return listPointModel.List[listPointModel.List.Count - 1].X; }
        }

        public Double CurrentY
        {
            get { return listPointModel.List[listPointModel.List.Count - 1].Y; }
        }

        public Double LastX
        {
            get { return listPointModel.List[listPointModel.List.Count - 2].X; }
        }

        public Double LastY
        {
            get { return listPointModel.List[listPointModel.List.Count - 2].Y; }
        }

        public ObservableCollection<Point> ListPoint
        {
            get { return listPointModel.List; }
            set
            {
                listPointModel.List = value;
                NotifyChanged("ListPoint");
            }
        }

        public BoardViewModel(): base()
        {
            socket = SocketAPI.GetInstance().GetSocket();

            listPointModel = new ListPointModel();
            drawingModel = new DrawingModel();
            SelectedColor = Color.FromRgb(0, 0, 0);
            Thickness = 5;
      
            MouseDownCommand = new DelegateCommand<object>(OnMouseDown);
            MouseMoveCommand = new DelegateCommand<object>(OnMouseMove);
            MouseUpCommand = new DelegateCommand<object>(OnMouseUp);

            HandleSocket();
        }

        private void OnMouseDown(object obj)
        {
          
        }

        private void OnMouseMove(object obj)
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
                return;

            Canvas canvas = obj as Canvas;
            Point currentPoint = Mouse.GetPosition(canvas);
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ListPoint.Add(currentPoint);
            });
            
            JObject data = new JObject();
            data.Add("point_x", currentPoint.X);
            data.Add("point_y", currentPoint.Y);
            socket.Emit("test_drawing", data);
        }

        private void OnMouseUp(object obj)
        {
       
        }

        private void HandleSocket()
        {
            socket.On("new_drawing", (arg) =>
            {
               
            });

            socket.On("test_drawing", (arg) =>
            {
                JObject obj = (JObject)arg;
                Point newPoint = new Point((double)obj.GetValue("point_x"), (double)obj.GetValue("point_y"));
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    ListPoint.Add(newPoint);
                });
            });
        }
    }
}
