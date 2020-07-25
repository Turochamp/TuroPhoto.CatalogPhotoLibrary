using System;
using System.IO;

namespace TuroPhoto.PhotoLibraryCatalog.Common.Dto
{
    enum ImportErrorType
    {
        MissingDateTime,
        FileAlreadyExits,
        Exception,
        Read
    }

    class ImportError
    {
        public Exception Exception { get; }
        public ImportErrorType ErrorType { get; }
        public string PhotoFilePath { get; }

        public ImportError(string photoFilePath, ImportErrorType errorType)
        {
            PhotoFilePath = photoFilePath;
            ErrorType = errorType;
        }

        public ImportError(string photoFilePath, ImportErrorType errorType, Exception exception) :
            this(photoFilePath, errorType)
        {
            Exception = exception;
        }

        public string PhotoFileName => Path.GetFileName(PhotoFilePath);

        public override string ToString()
        {
            return $"{PhotoFileName} {ErrorType}";
        }
    }
}