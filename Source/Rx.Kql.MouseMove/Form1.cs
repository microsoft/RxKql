using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Reactive.Kql;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.Hosting;
using System.Windows.Forms;

namespace RxWinforms
{
    // This sample illustrates the original usage of Reactive Extensions for event-driven UI
    // Good book with more examples is "Programming Reactive Extensions and LINQ" http://www.apress.com/9781430237471
    public partial class Form1 : Form
    {
        private int leftButtonCounter = 0;
        private int rightButtonCounter = 0;
        private int noButtonCounter = 0;

        List<IDisposable> _subscriptions = new List<IDisposable>();

        public Form1()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly().GetName();
            this.Text = $@"{assembly.Name} [{assembly.Version}]";

            var leftButtonPen = new Pen(Color.LightGreen, 2);
            leftButtonTextBox.ForeColor = Color.LightGreen;

            var rightButtonPen = new Pen(Color.Gold, 2);
            rightButtonTextBox.ForeColor = Color.Gold;

            var noButtonPen = new Pen(Color.Gainsboro, 2);
            noButtonTextBox.ForeColor = Color.Gainsboro;

            var leftButtonEvents = Observable.FromEventPattern<MouseEventArgs>(panel1, "MouseMove")
                .ToDynamic(m => m.EventArgs)
                .KustoQuery("where Button == \"Left\" | project X, Y");

            // Register Left button events
            _subscriptions.Add(leftButtonEvents.Subscribe(p =>
            {
                panel1.CreateGraphics().FillRectangle(leftButtonPen.Brush, float.Parse(p["X"].ToString()), float.Parse(p["Y"].ToString()), 5, 5);

                SetEventCounterDisplay(leftButtonTextBox, leftButtonCounter++);
                SetEventCounterDisplay(noButtonTextBox, noButtonCounter++);
            }));

            // Register Right button events
            var rightButtonEvents = Observable.FromEventPattern<MouseEventArgs>(panel1, "MouseMove")
                .ToDynamic(m => m.EventArgs)
                .KustoQuery("where Button == \"Right\" | project X, Y");

            _subscriptions.Add(rightButtonEvents.Subscribe(p =>
            {
                panel1.CreateGraphics().FillRectangle(rightButtonPen.Brush, float.Parse(p["X"].ToString()), float.Parse(p["Y"].ToString()), 5, 5);
                ;
                SetEventCounterDisplay(rightButtonTextBox, rightButtonCounter++);
                SetEventCounterDisplay(noButtonTextBox, noButtonCounter++);
            }));

            // Register Right button events
            var noButtonEvents = Observable.FromEventPattern<MouseEventArgs>(panel1, "MouseMove")
                .ToDynamic(m => m.EventArgs)
                .KustoQuery("where Button != \"Right\" and Button != \"Left\" | project X, Y");

            _subscriptions.Add(noButtonEvents.Subscribe(p =>
            {
                if (toggleAllEventsToolStripMenuItem.Checked)
                    panel1.CreateGraphics().FillRectangle(noButtonPen.Brush, float.Parse(p["X"].ToString()), float.Parse(p["Y"].ToString()), 5, 5);

                SetEventCounterDisplay(noButtonTextBox, noButtonCounter++);
            }));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clearAllDataPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetCounters();
            panel1.Invalidate();
        }

        private void resetCountersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetCounters();
        }

        private void ResetCounters()
        {
            SetEventCounterDisplay(leftButtonTextBox, leftButtonCounter = 0);
            SetEventCounterDisplay(noButtonTextBox, rightButtonCounter = 0);
            SetEventCounterDisplay(rightButtonTextBox, noButtonCounter = 0);
        }

        private void SetEventCounterDisplay(TextBox textBox, int value)
        {
            textBox.Text = $@"Moves: {value}";
        }
    }
}
