using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace DigitalSimulator
{
    /// <summary>
    /// Enumeration to select according file filters. (bit field)
    /// </summary>
    [Flags]
    public enum FileSelectorFilters
    {
        None = 0,
        AnyFile = 1,
        Circuits = 2,
        Symbols = 4,
        Macros = 8
    }

    public class FileSelector
    {
        #region Constants

        private const string c_AnyFile = @"Alle Dateien (*.*)|*.*";
        private const string c_CircuitFilter = @"Schaltungen (*.xcir)|*.xcir";
        private const string c_SymbolFilter = @"Schaltungssymbole (*.xsym)|*.xsym";
        private const string c_MacroFilter = @"Schaltungsmakros (*.xmac)|*.xmac";

        #endregion

        #region Fields

        private string m_FileName;
        private string m_FilterString;
        private int m_FilterIndex;
        private List<FileSelectorFilters> m_FilterList;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the selected file name;
        /// </summary>
        public string FileName
        {
            get
            {
                return m_FileName;
            }
            set
            {
                m_FileName = value;
            }
        }

        /// <summary>
        /// Returns the file filter that the user selected
        /// </summary>
        public FileSelectorFilters UsedFilter
        {
            get
            {
                if (m_FilterIndex == 0)
                {
                    return FileSelectorFilters.None;
                }
                return m_FilterList[m_FilterIndex-1];
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filters">one or multiple file filters</param>
        public FileSelector(FileSelectorFilters filters)
        {
            m_FilterString = String.Empty;
            m_FilterList = new List<FileSelectorFilters>();
            TryAddFilter(filters, FileSelectorFilters.Circuits, c_CircuitFilter);
            TryAddFilter(filters, FileSelectorFilters.Symbols, c_SymbolFilter);
            TryAddFilter(filters, FileSelectorFilters.Macros, c_MacroFilter);
            TryAddFilter(filters, FileSelectorFilters.AnyFile, c_AnyFile);
        }

        #endregion

        #region Private Implementation

        private void TryAddFilter(FileSelectorFilters filterFlags, FileSelectorFilters certainFilter, string filterPart)
        {
            if ((filterFlags & certainFilter) == certainFilter)
            {
                m_FilterList.Add(certainFilter);
                if (String.IsNullOrEmpty(m_FilterString) == false)
                {
                    m_FilterString += @"|";
                }
                m_FilterString += filterPart;
            }
        }

        private bool ExecuteDialog(FileDialog dialog)
        {
            if (String.IsNullOrEmpty(m_FileName))
            {
                dialog.InitialDirectory = Application.StartupPath;  //Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            else
            {
                dialog.InitialDirectory = Path.GetFullPath(m_FileName);
            }
            dialog.Filter = m_FilterString;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                m_FilterIndex = dialog.FilterIndex;
                m_FileName = dialog.FileName;
                return true;
            }
            return false;

        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Executes the dialog for saving a file
        /// </summary>
        /// <returns></returns>
        public bool ExecuteSaveDialog()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.CustomPlaces.Clear();
            saveDialog.CustomPlaces.Add(Application.StartupPath);
            return ExecuteDialog(saveDialog);
        }

        /// <summary>
        /// Executes the dialog for opening a file
        /// </summary>
        /// <returns></returns>
        public bool ExecuteOpenDialog()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.CustomPlaces.Clear();
            openDialog.CustomPlaces.Add(Application.StartupPath);
            openDialog.Multiselect = false;
            return ExecuteDialog(openDialog);
        }
        #endregion
    }
}
