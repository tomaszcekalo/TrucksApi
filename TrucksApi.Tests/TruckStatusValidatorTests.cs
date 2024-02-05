using TrucksApi.Data;

namespace TrucksApi.Tests
{
    [TestClass]
    public class TruckStatusValidatorTests
    {
        TruckStatusValidator _testedSystem = new TruckStatusValidator();
        TruckStatusEnum[] _allPossibleStatuses =
        [
            TruckStatusEnum.Loading,
            TruckStatusEnum.Returning,
            TruckStatusEnum.ToJob,
            TruckStatusEnum.AtJob,
            TruckStatusEnum.OutOfService
        ];
        //"Out Of Service" status can be set regardless of the current status of the Truck
        [TestMethod]
        public void SetOutOfService_FromAllPossibleStatuses_Passes()
        {
            Assert.IsTrue(_allPossibleStatuses.All(x=>_testedSystem.IsStatusChangeAllowed(x, TruckStatusEnum.OutOfService)));
        }
        //each status can be set if the current status of the Truck is "Out of service"
        [TestMethod]
        public void SetAnyStatus_FromOutOfService_Passes()
        {
            Assert.IsTrue(_allPossibleStatuses.All(x => _testedSystem.IsStatusChangeAllowed(TruckStatusEnum.OutOfService, x)));
        }
        //Loading->To Job
        [TestMethod]
        public void SetToJob_FromLoading_Passes()
        {
            Assert.IsTrue(_testedSystem.IsStatusChangeAllowed(TruckStatusEnum.Loading, TruckStatusEnum.ToJob));
        }
        //To Job->At Job
        [TestMethod]
        public void SetAtJob_FromToJob_Passes()
        {
            Assert.IsTrue(_testedSystem.IsStatusChangeAllowed(TruckStatusEnum.ToJob, TruckStatusEnum.AtJob));
        }
        //At Job->Returning
        [TestMethod]
        public void SetReturning_FromAtJob_Passes()
        {
            Assert.IsTrue(_testedSystem.IsStatusChangeAllowed(TruckStatusEnum.AtJob, TruckStatusEnum.Returning));
        }
        //when Truck has "Returning" status it can start "Loading" again.
        [TestMethod]
        public void SetLoading_FromReturning_Passes()
        {
            Assert.IsTrue(_testedSystem.IsStatusChangeAllowed(TruckStatusEnum.Returning, TruckStatusEnum.Loading));
        }

        //Tests for reversed order
        [TestMethod]
        public void SetAtJob_FromReturning_Fails()
        {
            Assert.IsFalse(_testedSystem.IsStatusChangeAllowed(TruckStatusEnum.Returning, TruckStatusEnum.AtJob));
        }
        [TestMethod]
        public void SetToJob_FromAtJob_Fails()
        {
            Assert.IsFalse(_testedSystem.IsStatusChangeAllowed(TruckStatusEnum.AtJob, TruckStatusEnum.ToJob));
        }
        [TestMethod]
        public void SetLoading_FromToJob_Fails()
        {
            Assert.IsFalse(_testedSystem.IsStatusChangeAllowed(TruckStatusEnum.ToJob, TruckStatusEnum.Loading));
        }
        [TestMethod]
        public void SetReturning_FromLoading_Fails()
        {
            Assert.IsFalse(_testedSystem.IsStatusChangeAllowed(TruckStatusEnum.Loading, TruckStatusEnum.Returning));
        }
    }
}