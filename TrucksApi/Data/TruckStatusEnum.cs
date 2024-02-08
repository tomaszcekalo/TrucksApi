namespace TrucksApi.Data
{
    public enum TruckStatusEnum
    {
        OutOfService,
        Loading,
        ToJob,
        AtJob,
        Returning
    }

    public static class TruckStatusEnumExtensions
    {
        public static string ToFriendlyString(this TruckStatusEnum tse)
        {
            switch (tse)
            {
                case TruckStatusEnum.OutOfService:
                    return "Out Of Service";

                case TruckStatusEnum.ToJob:
                    return "To Job";

                case TruckStatusEnum.AtJob:
                    return "At Job";

                default:
                    return tse.ToString();
            }
        }
    }
}