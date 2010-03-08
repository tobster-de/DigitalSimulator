using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using DigitalClasses.Controls;
using System.IO;
using DigitalClasses.Serialization;

namespace DigitalSimulator
{
    public partial class SymbolEditorForm : DockContent, ISaveableDocument
    {
        #region Fields

        private String m_FileName;
        private bool m_Persistent;

        #endregion

        #region Properties

        /// <summary>
        /// The symbol editor control displayes by this form
        /// </summary>
        public SymbolEditor SymbolEditor
        {
            get
            {
                return symbolEditor;
            }
        }

        /// <summary>
        /// Designates the filename used to store the symbol of this editor form
        /// </summary>
        public string FileName
        {
            get
            {
                return (string)m_FileName.Clone();
            }
            private set
            {
                if (m_FileName == null || m_FileName.Equals(value) == false)
                {
                    m_FileName = value;
                    Text = Path.GetFileName(value);
                }
                if (m_Persistent == false)
                {
                    if (Text.EndsWith("*") == false)
                    {
                        Text += "*";
                    }
                }
                else
                {
                    if (Text.EndsWith("*"))
                    {
                        Text = Text.TrimEnd('*');
                    }
                }
            }
        }

        #endregion

        public SymbolEditorForm()
        {
            InitializeComponent();
            m_FileName = "";
            m_Persistent = true;
        }

        #region Saving Methods

        private void SaveAs(string fileName)
        {
            try
            {
                if (String.IsNullOrEmpty(fileName))
                {
                    FileSelector fileSelector = new FileSelector(FileSelectorFilters.Symbols);
                    fileSelector.FileName = fileName;
                    if (fileSelector.ExecuteSaveDialog())
                    {
                        fileName = fileSelector.FileName;
                    }
                }
                if (String.IsNullOrEmpty(fileName) == false)
                {
                    SymbolData data = SymbolConverter.Instance.ConvertFromSymbol(symbolEditor.Symbol);
                    SymbolSerializer.SerializeSymbol(fileName, data);
                    m_Persistent = true;
                    FileName = fileName;
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                if (e.InnerException != null)
                {
                    msg += "\n" + e.InnerException.Message;
                }
                MessageBox.Show(msg + "\n-----\n" + e.StackTrace, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void SaveDocumentAs()
        {
            SaveAs(null);
        }

        public void SaveDocument()
        {
            SaveAs(m_FileName);
        }

        public bool LoadDocument(string fileName)
        {
            try
            {
                if (String.IsNullOrEmpty(fileName) == false)
                {
                    SymbolData symbolData = SymbolSerializer.DeserializeSymbol(fileName);
                    symbolEditor.Symbol = SymbolConverter.Instance.ConvertToSymbol(symbolData);
                    FileName = fileName;
                    return true;
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                if (e.InnerException != null)
                {
                    msg += "\n" + e.InnerException.Message;
                }
                MessageBox.Show(msg + "\n-----\n" + e.StackTrace, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return false;
        }

        #endregion

        private void HandleSizeChanged(object sender, EventArgs e)
        {
            symbolEditor.Size = this.Size;
            this.AutoScrollMinSize = this.Size;
        }

        private void symbolEditor_SymbolChanged(object sender)
        {
            m_Persistent = false;
            if (Text.EndsWith("*") == false)
            {
                Text += "*";
            }
        }

        private void SymbolEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_Persistent == false)
            {
                string msg = String.Copy(Text);
                if (MessageBox.Show(String.Format("Das Symbol \"{0}\" wurde verändert.\nMöchten Sie die Änderungen speichern?", msg.TrimEnd('*')),
                    "Hinweis", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    SaveDocument();
                }
            }
        }

    }
}
