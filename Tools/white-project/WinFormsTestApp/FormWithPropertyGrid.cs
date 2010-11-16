using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsTestApp
{
    public partial class FormWithPropertyGrid : Form
    {
        public FormWithPropertyGrid()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = new BasicTypes("str", false, 6, 1.2f);
        }
    }

    [DefaultProperty("NotWhole")]
    public class BasicTypes
    {
        private Size windowSize = new Size(100, 100);
        private Font windowFont = new Font("Arial", 8, FontStyle.Regular);
        private Color toolbarColor = SystemColors.Control;
        private string fileName = "test.txt";

        public BasicTypes(string buffer, bool boolean, int integer, float notWhole)
        {
            Buffer = buffer;
            NotWhole = notWhole;
            Integer = integer;
            Boolean = boolean;
        }

        [Description("Select a file..."), Category("Input"), Browsable(true),
        Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        [Description("String")]
        public string Buffer { get; set; }

        [Category("Number")]
        public int Integer { get; set; }

        [DefaultValue(false)]
        public bool Boolean { get; set; }

        [Category("Number"), ReadOnly(true)]
        public float NotWhole { get; set; }

        [Category("General")]
        public Size WindowSize
        {
            get { return windowSize; }
            set { windowSize = value; }
        }

        [Category("General")]
        public Font WindowFont
        {
            get { return windowFont; }
            set { windowFont = value; }
        }

        [Category("General")]
        public Color ToolbarColor
        {
            get { return toolbarColor; }
            set { toolbarColor = value; }
        }
    }

    public class ComplexNumber
    {
        public ComplexNumber(int real, int imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public ComplexNumber() {}

        public int Real { get; set; }
        public int Imaginary { get; set; }
    }
}