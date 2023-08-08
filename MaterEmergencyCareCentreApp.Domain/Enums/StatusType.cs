namespace MaterEmergencyCareCentreApp.Domain.Enums
{
    public enum StatusType
    {
        Free,
        InUse
    }

    public static class StatusTypeExtensions
    {
        public static string ToStatus(this StatusType me)
        {
            switch (me)
            {
                case StatusType.Free:
                    return "Free";
                case StatusType.InUse:
                    return "In Use";
                default:
                    return "Unknown";
            }
        }
    }
}