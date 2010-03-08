using System;

namespace DigitalSimulator
{
    /// <summary>
    /// Common interface for documents which can be saved and loaded
    /// </summary>
    interface ISaveableDocument
    {
        string FileName
        {
            get;
        }

        bool LoadDocument(string fileName);
        void SaveDocument();
        void SaveDocumentAs();
    }
}
