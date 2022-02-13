using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Diagnostics;

namespace UICommon
{
    public class UIMethods
    {
        /// <summary>
        /// A class/struct to encapsulate that data needed to add points to a chart
        /// </summary>
        public class graphData_t
        {
            public Series serPtr;
            public Double[] data;

            public graphData_t(Series tmpSer, Double[] tmpData)
            {
                serPtr = tmpSer;
                data = tmpData;
            }
        };

        /// <summary>
        /// Add a message to a ListBox with snap to last checkbox
        /// </summary>
        /// <param name="myListBox">The listbox to add the message to</param>
        /// <param name="snapCheckBox">The "Snap to last" checkbox</param>
        /// <param name="message">The message to be added to the listbox</param>
        public static void AddMessageToLB(ListBox myListBox, CheckBox snapCheckBox, string message)
        {
            if (myListBox.InvokeRequired)
            {
                myListBox.Invoke(new Action<ListBox, CheckBox, string>(AddMessageToLB), new object[] { myListBox, snapCheckBox, message });
            }
            else
            {
                myListBox.Items.Add(message);
                if ((snapCheckBox != null) && snapCheckBox.Checked)
                {
                    myListBox.SelectedIndex = myListBox.Items.Count - 1;
                }
            }
        }

        /// <summary>
        /// Add a TCI message to a message specific ListView
        /// </summary>
        /// <param name="myListView">The ListView to add the message to</param>
        /// <param name="snapCheckBox">The "Snap to last" checkbox</param>
        /// <param name="direction">"TX" or "RX"</param>
        /// <param name="timestamp">Timestamp of when the message was sent/recieved</param>
        /// <param name="message">The entire packet of data</param>
        public static void AddMessageToMsgLV(ListView myListView, CheckBox snapCheckBox, string direction, string timestamp, string message)
        {
            if (myListView.InvokeRequired)
            {
                myListView.Invoke(new MethodInvoker(delegate
                {
                    AddMessageToMsgLV(myListView, snapCheckBox, direction, timestamp, message);
                }));
            }
            else
            {
                ListViewItem lvi = new ListViewItem(direction);
                lvi.SubItems.Add(timestamp);
                lvi.SubItems.Add(message);

                AddMsgToListView(myListView, lvi, snapCheckBox.Checked);
            }
        }

        /// <summary>
        /// Add points to a chart
        /// </summary>
        /// <param name="myChart">The chart ot add to</param>
        /// <param name="myData">An array of graphData_t containing the data to add to the chart</param>
        /// <param name="autoScale">Whether or not to automatically scale the chart to the data</param>
        /// <param name="valsToSave">How many values should be displayed in the graph at once</param>
        public static void AddPointsToChart(Chart myChart, graphData_t[] myData, Boolean autoScale, int valsToSave)
        {
            if (myChart.InvokeRequired)
            {
                myChart.BeginInvoke((MethodInvoker)delegate () {
                    AddPointsToChart(myChart, myData, autoScale, valsToSave);
                });
            }
            else
            {
                Double minVal = 10000.0;
                Double maxVal = -10000.0;
                Double yScale = 0.0;

                foreach (graphData_t graphItem in myData)
                {
                    // add the points
                    foreach (Double dVal in graphItem.data)
                    {
                        graphItem.serPtr.Points.AddY(dVal);
                    }

                    // remove points if over the max count
                    if (graphItem.serPtr.Points.Count > valsToSave)
                    {
                        for (var overCnt = (graphItem.serPtr.Points.Count - valsToSave); overCnt > 0; overCnt--)
                        {
                            graphItem.serPtr.Points.RemoveAt(0);
                        }
                    }

                    if (graphItem.serPtr.Points.FindMinByValue().YValues[0] < minVal)
                    {
                        minVal = graphItem.serPtr.Points.FindMinByValue().YValues[0];
                    }

                    if (graphItem.serPtr.Points.FindMaxByValue().YValues[0] > maxVal)
                    {
                        maxVal = graphItem.serPtr.Points.FindMaxByValue().YValues[0];
                    }
                }

                // must be more than 1 point
                if ((myData[0].serPtr.Points.Count() > 1) && autoScale)
                {
                    // rescale
                    yScale = Math.Max(maxVal - minVal, 10.0);

                    // adjust the y axis
                    //Console.WriteLine("Axis min/max: {0} {1}", minVal.ToString(), maxVal.ToString());
                    myChart.ChartAreas[0].AxisY.Minimum = minVal - (yScale * 0.2);
                    myChart.ChartAreas[0].AxisY.Maximum = maxVal + (yScale * 0.2);
                }

                // Update the chart
                myChart.Show();
            }
        }

        /// <summary>
        /// Clear all of the series in a chart of their data
        /// </summary>
        /// <param name="chart">The chart to clear</param>
        public static void ClearChart(Chart chart)
        {
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }
        }

        /// <summary>
        /// A simple helper method to construct graphData_t classes/structs
        /// </summary>
        /// <param name="series">The series in the chart you wish to add data to</param>
        /// <param name="vals">The data values you wish to add</param>
        /// <returns></returns>
        public static graphData_t GraphDataBuilder(Series series, List<double> vals)
        {
            return new graphData_t(series, vals.ToArray());
        }

        /// <summary>
        /// Update a TextBox's text attribute with multithreading protection
        /// </summary>
        /// <param name="tmpBox">The TextBox to change</param>
        /// <param name="tmpStr">What you want the text box to say</param>
        public static void UpdateTxBox(TextBox tmpBox, string tmpStr)
        {
            if (tmpBox.InvokeRequired)
            {
                tmpBox.Invoke((MethodInvoker)delegate ()
                {
                    UpdateTxBox(tmpBox, tmpStr);
                });
            }
            else
            {
                tmpBox.Text = tmpStr;
            }
        }

        /// <summary>
        /// Update a ComboBox's Text attribute with multithreading protection
        /// </summary>
        /// <param name="cBox">The ComboBox you wish to change the text of</param>
        /// <param name="tmpStr">What you want the ComboBox to say</param>
        public static void UpdateComboBox(ComboBox cBox, string tmpStr)
        {
            if (cBox.InvokeRequired)
            {
                cBox.Invoke(new MethodInvoker(delegate
                {
                    UpdateComboBox(cBox, tmpStr);
                }));
            }
            else
            {
                cBox.Text = tmpStr;
            }
        }

        /// <summary>
        /// Update a Label's Text attribute with multithreading protection
        /// </summary>
        /// <param name="lab">The label you wish to update</param>
        /// <param name="tmpStr">What you want the label to say</param>
        public static void UpdateLabel(Label lab, string tmpStr)
        {
            if (lab.InvokeRequired)
            {
                lab.Invoke(new MethodInvoker(delegate
                {
                    UpdateLabel(lab, tmpStr);
                }));
            }
            else
            {
                lab.Text = tmpStr;
            }
        }

        /// <summary>
        /// A generic method that will update the Text attribute of
        ///     a TextBox, Label, or ComboBox using it's respective
        ///     multithreading protected method in UIMethods.
        /// </summary>
        /// <param name="ctrl">The control with .Text attribute you want to change</param>
        /// <param name="tmpStr">The string you want to change the .Text attribute to</param>
        public static void UpdateTextControl(Object ctrl, string tmpStr)
        {
            var type = ctrl.GetType();
            if (type == typeof(TextBox))
            {
                UpdateTxBox((TextBox)ctrl, tmpStr);
            }
            else if (type == typeof(Label))
            {
                UpdateLabel((Label)ctrl, tmpStr);
            }
            else if (type == typeof(ComboBox))
            {
                UpdateComboBox((ComboBox)ctrl, tmpStr);
            }
        }

        /// <summary>
        /// Update the Checked attribute of a checkbox with multithreading protection
        /// </summary>
        /// <param name="chk">The CheckBox you wish to update</param>
        /// <param name="chkd">Whether to set the CheckBox to checked or not</param>
        public static void UpdateCheckBox(CheckBox chk, bool chkd)
        {
            if (chk.InvokeRequired)
            {
                chk.Invoke(new MethodInvoker(delegate
                {
                    UpdateCheckBox(chk, chkd);
                }));
            }
            else
            {
                chk.Checked = chkd;
            }
        }

        /// <summary>
        /// Update the Value attribute of a NumericUpDown with multithreading protection
        /// </summary>
        /// <param name="num">The NumericUpDown you want to update</param>
        /// <param name="value">The value you want to change the NumericUpDown to</param>
        public static void UpdateNumericUpDown(NumericUpDown num, decimal value)
        {
            if (num.InvokeRequired)
            {
                num.Invoke(new MethodInvoker(delegate
                {
                    UpdateNumericUpDown(num, value);
                }));
            }
            else
            {
                num.Value = value;
            }
        }

        /// <summary>
        /// Generic method to add a message to a ListView with multithreading protection
        /// </summary>
        /// <param name="lv">The ListView to add a message to</param>
        /// <param name="lvi">The ListViewItem to add to the ListView</param>
        /// <param name="snap">Whether or not to automatically snap to the last item added</param>
        public static void AddMsgToListView(ListView lv, ListViewItem lvi, bool snap)
        {
            if (lv.InvokeRequired)
            {
                lv.Invoke(new MethodInvoker(delegate
                {
                    AddMsgToListView(lv, lvi, snap);
                }));
            }
            else
            {
                lv.Items.Add(lvi);

                if (snap)
                {
                    lv.Items[lv.Items.Count - 1].EnsureVisible();
                }
            }
        }

        /// <summary>
        /// Set the Enabled attribute of a control with multithreading protection
        /// </summary>
        /// <param name="ctrl">The Control object to enable/disable</param>
        /// <param name="enabled">Whether to enable or disable the control</param>
        public static void ControlEnableSet(Control ctrl, bool enabled)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(new MethodInvoker(delegate
                {
                    ControlEnableSet(ctrl, enabled);
                }));
            }
            else
            {
                ctrl.Enabled = enabled;
            }
        }

        /// <summary>
        /// Method to double-buffer a ListView to stop it from flashing when updating it.
        /// Only use this if this is a problem. This method can cause buffering of messages and lagging.
        /// </summary>
        /// <param name="lv">The problematic ListView to double-buffer</param>
        public static void DoubleBufferLv(ListView lv)
        {
            // thanks to WraithNath on https://stackoverflow.com/questions/442817/c-sharp-flickering-listview-on-update
            //  for this solution to the flicker ListView problem. All you need to do is...
            // double buffer
            lv.GetType()
              .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
              .SetValue(lv, true, null);
        }

        /// <summary>
        /// Method to double-buffer a ListBox to stop it from flashing when updating it.
        /// Only use this if this is a problem. This method can cause buffering of messages and lagging.
        /// Haven't seen this problem with a ListBox but wanted to include this method for completeness
        /// </summary>
        /// <param name="lv">The problematic ListBox to double-buffer</param>
        public static void DoubleBufferLb(ListBox lb)
        {
            // thanks to WraithNath on https://stackoverflow.com/questions/442817/c-sharp-flickering-listview-on-update
            //  for this solution to the flicker ListView problem. All you need to do is...
            // double buffer
            lb.GetType()
              .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
              .SetValue(lb, true, null);
        }
    }
}
