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
        private RoomModel currentRoom;

        //getter and setter
        public DelegateCommand<object> MouseDownCommand { get; set; }
        public DelegateCommand<object> MouseMoveCommand { get; set; }
        public DelegateCommand<object> MouseUpCommand { get; set; }
        public DelegateCommand<RoomModel> RoomChangeCommand { get; set; }
        public DelegateCommand<object> CanvasChangeCommand { get; set; }

        public int Thickness { get; set; }

        public Color SelectedColor { get; set; }

        public SolidColorBrush BrushColor
        {
            get { return new SolidColorBrush(SelectedColor); }
        }
        
        public ObservableCollection<EllipseModel> ListPoint
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
            SelectedColor = Color.FromRgb(0, 0, 0);
            Thickness = 5;
      
            MouseDownCommand = new DelegateCommand<object>(OnMouseDown);
            MouseMoveCommand = new DelegateCommand<object>(OnMouseMove);
            MouseUpCommand = new DelegateCommand<object>(OnMouseUp);
            RoomChangeCommand = new DelegateCommand<RoomModel>(OnRoomChange);

            HandleSocket();
        }

        private void OnRoomChange(RoomModel obj)
        {
            currentRoom = obj;
            listPointModel.List.Clear();
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
            Ellipse ellipse = new Ellipse();
            ellipse.Width = Thickness;
            ellipse.Height = Thickness;
            ellipse.Fill = BrushColor;
            ellipse.Stroke = BrushColor;
            ellipse.StrokeThickness = 1;
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ListPoint.Add(new EllipseModel(ellipse, currentPoint));
            });
            
            JObject data = new JObject();
            data.Add("point_x", currentPoint.X);
            data.Add("point_y", currentPoint.Y);
            data.Add("thickness", Thickness);
            data.Add("r", BrushColor.Color.R);
            data.Add("g", BrushColor.Color.G);
            data.Add("b", BrushColor.Color.B);
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
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    Debug.LogOutput("recieved test_drawing event");
                    JObject obj = (JObject)arg;
                    Point newPoint = new Point((double)obj.GetValue("point_x"), (double)obj.GetValue("point_y"));
                    double thickness = (double)obj.GetValue("thickness");
                    byte r = (byte)obj.GetValue("r");
                    byte g = (byte)obj.GetValue("g");
                    byte b = (byte)obj.GetValue("b");
                    SolidColorBrush color = new SolidColorBrush(Color.FromRgb(r, g, b));
                    Ellipse newEllipse = new Ellipse();
                    newEllipse.Width = thickness;
                    newEllipse.Height = thickness;
                    newEllipse.Fill = color;
                    newEllipse.Stroke = color;
                    newEllipse.StrokeThickness = 1;
                    ListPoint.Add(new EllipseModel(newEllipse, newPoint));
                });
            });
        }
    }
}
