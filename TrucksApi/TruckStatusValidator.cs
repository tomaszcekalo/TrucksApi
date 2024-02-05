using System.Runtime.CompilerServices;
using TrucksApi.Data;

namespace TrucksApi
{
    public interface ITruckStatusValidator
    {
        public bool IsStatusChangeAllowed(TruckStatusEnum currentStatus,  TruckStatusEnum newStatus);
    }
    public class TruckStatusValidator : ITruckStatusValidator
    {
        public bool IsStatusChangeAllowed(TruckStatusEnum currentStatus, TruckStatusEnum newStatus)
        {
            //"Out Of Service" status can be set regardless of the current status of the Truck
            if(newStatus == TruckStatusEnum.OutOfService)
            {
                return true;
            }
            //each status can be set if the current status of the Truck is "Out of service"
            if(currentStatus == TruckStatusEnum.OutOfService)
            {
                return true;
            }
            //the remaining statuses can only be changed in the following order:
            //Loading->To Job->At Job->Returning

            //Loading->To Job
            if (currentStatus == TruckStatusEnum.Loading && newStatus == TruckStatusEnum.ToJob)
            {
                return true;
            }
            //To Job->At Job
            if (currentStatus == TruckStatusEnum.ToJob && newStatus == TruckStatusEnum.AtJob)
            {
                return true;
            }
            //At Job->Returning
            if (currentStatus == TruckStatusEnum.AtJob && newStatus == TruckStatusEnum.Returning)
            {
                return true;
            }
            //when Truck has "Returning" status it can start "Loading" again.
            if (currentStatus == TruckStatusEnum.Returning && newStatus == TruckStatusEnum.Loading)
            {
                return true;
            }
            return false;
        }
    }
}
