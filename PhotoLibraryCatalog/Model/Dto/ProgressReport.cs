namespace TuroPhoto.PhotoLibraryCatalog.Model.Dto
{
    class ProgressReport
    {
        public string OperationDescription { get; }
        public int? ProgressCount { get; }
        public int? TotalCount { get; }

        public ProgressReport(string operationDescription, int? progressCount = null, int? totalCount = null)
        {
            OperationDescription = operationDescription;
            ProgressCount = progressCount;
            TotalCount = totalCount;
        }
    }
}