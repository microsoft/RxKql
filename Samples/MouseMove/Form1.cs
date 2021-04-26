using System;
using System.Drawing;
using System.Reactive.Kql;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace RxWinforms
{
    // This sample illustrates how Rx.KQL translates Kusto Queries to Reactive Extensions
    // For reference see the original sample: (http://github.com/Microsoft/Tx/blob/master/Samples/RxWinforms/Readme.md
    //
    // Here the LINQ verb KustoQuery(...) allows the user to filter and transform events in KQL
    // ... as oposed to learning lambda exppressions, Disposables, C# types, etc.
    public partial class Form1 : Form
    {
        IDisposable _subscription;
        private int eventCounter = 0;
        public Form1()
        {
            InitializeComponent();

            var pen = new Pen(Color.Gold, 2);

            var points = Observable.FromEventPattern<MouseEventArgs>(panel1, "MouseMove")
                .ToDynamic(m => m.EventArgs)
                .KustoQuery("where Button == \"Left\" | project X, Y");

            _subscription = points.Subscribe(p =>
            {
                int X = Convert.ToInt32(p["X"]);
                int Y = Convert.ToInt32(p["Y"]);
                panel1.CreateGraphics().FillRectangle(pen.Brush, X, Y, 5, 5);
                textBox1.Text = $"{eventCounter++}";
            });
        }
    }
}
