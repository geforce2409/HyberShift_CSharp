using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using HyberShift_CSharp.Utilities;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.ViewModel
{
    public class DrawingTestVM : BaseViewModel
    {
        private bool isNewLine;
        private double lastX, lastY;
        private ObservableCollection<Line> lines;
        private readonly Socket socket;

        public DrawingTestVM()
        {
            socket = SocketAPI.GetInstance().GetSocket();

            lines = new ObservableCollection<Line>();
            SelectedColor = Color.FromRgb(0, 0, 0);
            Thickness = 5;

            MouseDownCommand = new DelegateCommand<object>(OnMouseDown);
            MouseMoveCommand = new DelegateCommand<object>(OnMouseMove);
            MouseUpCommand = new DelegateCommand<object>(OnMouseUp);

            HandleSocket();
        }

        //getter and setter
        public DelegateCommand<object> MouseDownCommand { get; set; }
        public DelegateCommand<object> MouseMoveCommand { get; set; }
        public DelegateCommand<object> MouseUpCommand { get; set; }

        public int Thickness { get; set; }

        public Color SelectedColor { get; set; }

        public SolidColorBrush BrushColor => new SolidColorBrush(SelectedColor);


        public ObservableCollection<Line> ListLine
        {
            get => lines;
            set
            {
                lines = value;
                NotifyChanged("ListLine");
            }
        }


        private void OnMouseDown(object obj)
        {
        }

        private void OnMouseMove(object obj)
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
                return;


            var canvas = obj as Canvas;
            var currentPoint = Mouse.GetPosition(canvas);

            var line = new Line();

            var data = new JObject();


            if (lines.Count == 0 || isNewLine)
            {
                line.X2 = Mouse.GetPosition(canvas).X;
                line.Y2 = Mouse.GetPosition(canvas).Y;
                isNewLine = false;
                data.Add("x2", line.X2);
                data.Add("y2", line.Y2);
                lastX = line.X2;
                lastY = line.Y2;
            }
            else
            {
                line.X2 = lastX;
                line.Y2 = lastY;
                data.Add("x2", line.X2);
                data.Add("y2", line.Y2);
            }

            line.X1 = Mouse.GetPosition(canvas).X;
            line.Y1 = Mouse.GetPosition(canvas).Y;
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 5;
            data.Add("x1", line.X1);
            data.Add("y1", line.Y1);

            socket.Emit("test_drawing_line", data);

            Debug.LogOutput("Emit new drawing to server");

            Application.Current.Dispatcher.Invoke(delegate { lines.Add(line); });
        }

        private void OnMouseUp(object obj)
        {
            isNewLine = true;
        }

        private void HandleSocket()
        {
            socket.On("new_drawing", arg => { });

            socket.On("test_drawing_line", arg =>
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    Debug.LogOutput("On test drawing line to server");
                    var obj = (JObject) arg;
                    var newLine = new Line();
                    newLine.Stroke = Brushes.Black;
                    newLine.StrokeThickness = 5;
                    newLine.X1 = (double) obj.GetValue("x1");
                    newLine.Y1 = (double) obj.GetValue("y1");
                    newLine.X2 = (double) obj.GetValue("x2");
                    newLine.Y2 = (double) obj.GetValue("y2");

                    if (newLine.X1 == newLine.X2 && newLine.Y1 == newLine.Y2)
                    {
                        //return;
                    }

                    lines.Add(newLine);
                });
            });
        }
    }
}