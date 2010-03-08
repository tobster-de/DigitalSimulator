using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ToolBox;
using System.Diagnostics;

namespace DigitalSimulator
{
    partial class AboutBox : Form
    {
        public AboutBox(Icon icon)
        {
            InitializeComponent();
            this.Text = String.Format("Info über {0}", AssemblyTitle);
            this.label_ProductName.Text = AssemblyProduct;
            this.label_Version.Text = String.Format("Version {0}", AssemblyVersion);
            this.label_Copyright.Text = AssemblyCopyright;
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            this.label_Sysinfo.Text = String.Format("{0} \n{1:f} MiB RAM verfügbar", WindowsVersion.VersionString, ramCounter.NextValue());
            //this.labelCompanyName.Text = AssemblyCompany;
            this.textBox_Description.Text = AssemblyDescription;
            this.pictureBox_Icon.Image = icon.ToBitmap();
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void pictureBox_Logo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.nospace.de");
        }

        private void DrawSeperator(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.Width > e.ClipRectangle.Height)
            {
                e.Graphics.DrawLine(new Pen(Color.DarkGray),
                    new Point(e.ClipRectangle.X, e.ClipRectangle.Height / 2 + e.ClipRectangle.Y),
                    new Point(e.ClipRectangle.X + e.ClipRectangle.Width, e.ClipRectangle.Height / 2 + e.ClipRectangle.Y));
                e.Graphics.DrawLine(new Pen(Color.White),
                    new Point(e.ClipRectangle.X, e.ClipRectangle.Height / 2 + e.ClipRectangle.Y + 1),
                    new Point(e.ClipRectangle.X + e.ClipRectangle.Width, e.ClipRectangle.Height / 2 + e.ClipRectangle.Y + 1));
            }
            else
            {
                e.Graphics.DrawLine(new Pen(Color.DarkGray),
                    new Point(e.ClipRectangle.Width / 2 + e.ClipRectangle.X, e.ClipRectangle.Y),
                    new Point(e.ClipRectangle.Width / 2 + e.ClipRectangle.X, e.ClipRectangle.Height + e.ClipRectangle.Y));
                e.Graphics.DrawLine(new Pen(Color.White),
                    new Point(e.ClipRectangle.Width / 2 + e.ClipRectangle.X + 1, e.ClipRectangle.Y),
                    new Point(e.ClipRectangle.Width / 2 + e.ClipRectangle.X + 1, e.ClipRectangle.Height + e.ClipRectangle.Y));
            }
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:tobias@nospace.de");
        }

    }
}
